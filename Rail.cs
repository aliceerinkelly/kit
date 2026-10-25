using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Kit;

/// <summary>
/// The line rail is a small tree: category headers, and the designs that follow a
/// header belong to it. Kept out of the window so the rules can be tested without a UI.
/// </summary>
public static class Rail
{
    public const int MaxNameLength = 80;
    private const double ChildIndent = 14.0;

    public static DesignEntry? Selected(ObservableCollection<DesignEntry> lines) =>
        lines.FirstOrDefault(line => line.IsSelected);

    public static void Select(ObservableCollection<DesignEntry> lines, DesignEntry? entry)
    {
        foreach (var line in lines)
        {
            line.IsSelected = ReferenceEquals(line, entry);
        }
    }

    /// <summary>Index just past the last design belonging to the header at this index.</summary>
    public static int GroupEnd(ObservableCollection<DesignEntry> lines, int headerIndex)
    {
        var end = headerIndex + 1;
        while (end < lines.Count && !lines[end].IsHeader)
        {
            end++;
        }

        return end;
    }

    /// <summary>Recomputes the indent after any change: designs sit under their header.</summary>
    public static void Reindex(ObservableCollection<DesignEntry> lines)
    {
        var inside = false;

        foreach (var line in lines)
        {
            if (line.IsHeader)
            {
                inside = true;
                line.Indent = new Thickness(0.0);
            }
            else
            {
                line.Indent = new Thickness(inside ? ChildIndent : 0.0, 0.0, 0.0, 0.0);
            }
        }
    }

    /// <summary>
    /// Moves a design one place, or a header with its whole group. Returns the new index.
    /// </summary>
    public static int Move(ObservableCollection<DesignEntry> lines, DesignEntry entry, int delta)
    {
        var index = lines.IndexOf(entry);
        if (index < 0 || delta == 0)
        {
            return -1;
        }

        if (!entry.IsHeader)
        {
            var target = index + delta;
            if (target < 0 || target >= lines.Count)
            {
                return index;
            }

            lines.Move(index, target);
            Reindex(lines);
            return target;
        }

        var end = GroupEnd(lines, index);

        if (delta > 0)
        {
            if (end >= lines.Count)
            {
                return index;
            }

            // Swap past the next header's group.
            var nextEnd = GroupEnd(lines, end);
            var block = lines.Skip(index).Take(end - index).ToList();
            for (var i = 0; i < block.Count; i++)
            {
                lines.RemoveAt(index);
            }

            var insert = nextEnd - block.Count;
            for (var i = 0; i < block.Count; i++)
            {
                lines.Insert(insert + i, block[i]);
            }

            Reindex(lines);
            return insert;
        }

        // Up: insert in front of the previous header (or at the very top).
        var previous = 0;
        for (var i = index - 1; i >= 0; i--)
        {
            if (lines[i].IsHeader)
            {
                previous = i;
                break;
            }
        }

        if (previous == index)
        {
            return index;
        }

        var moving = lines.Skip(index).Take(end - index).ToList();
        for (var i = 0; i < moving.Count; i++)
        {
            lines.RemoveAt(index);
        }

        for (var i = 0; i < moving.Count; i++)
        {
            lines.Insert(previous + i, moving[i]);
        }

        Reindex(lines);
        return previous;
    }

    /// <summary>What Copy puts on the clipboard: one clean name, never a pasted document.</summary>
    public static string CopyName(DesignEntry entry) =>
        FirstLine(entry.Name).Trim();

    /// <summary>
    /// Paste inserts right after the selection, code and header flag included, under a
    /// unique name. A copied header becomes an empty category at the end of its group.
    /// </summary>
    public static DesignEntry Paste(
        ObservableCollection<DesignEntry> lines,
        string name,
        string? code = null,
        bool isHeader = false)
    {
        var baseName = FirstLine(name).Trim();
        if (baseName.Length == 0)
        {
            baseName = isHeader ? "New category" : "Untitled";
        }

        if (baseName.Length > MaxNameLength)
        {
            baseName = baseName[..MaxNameLength].TrimEnd();
        }

        var entry = new DesignEntry(Unique(lines, baseName), custom: true, code, isHeader);
        var selected = Selected(lines);

        int index;
        if (isHeader && selected is not null)
        {
            var at = lines.IndexOf(selected);
            index = selected.IsHeader ? GroupEnd(lines, at) : lines.IndexOf(selected) + 1;
        }
        else if (selected is null)
        {
            index = lines.Count;
        }
        else
        {
            index = lines.IndexOf(selected) + 1;
        }

        lines.Insert(index, entry);
        Select(lines, entry);
        Reindex(lines);
        return entry;
    }

    /// <summary>A new design: under the selected header, after the selection, or at the end.</summary>
    public static DesignEntry AddDesign(ObservableCollection<DesignEntry> lines, string name)
    {
        var entry = new DesignEntry(Unique(lines, name), custom: true);
        var selected = Selected(lines);

        var index = selected is null ? lines.Count : lines.IndexOf(selected) + 1;

        lines.Insert(index, entry);
        Select(lines, entry);
        Reindex(lines);
        return entry;
    }

    /// <summary>A new category: appended after the selected group so existing designs stay put.</summary>
    public static DesignEntry AddHeader(ObservableCollection<DesignEntry> lines, string name)
    {
        var entry = new DesignEntry(Unique(lines, name), custom: true, isHeader: true);
        var selected = Selected(lines);

        // A new category goes after the whole group, so no existing design is adopted.
        int index;
        if (selected is null)
        {
            index = lines.Count;
        }
        else if (selected.IsHeader)
        {
            index = GroupEnd(lines, lines.IndexOf(selected));
        }
        else
        {
            var owner = -1;
            for (var i = lines.IndexOf(selected); i >= 0; i--)
            {
                if (lines[i].IsHeader)
                {
                    owner = i;
                    break;
                }
            }

            index = owner >= 0 ? GroupEnd(lines, owner) : lines.Count;
        }

        lines.Insert(index, entry);
        Select(lines, entry);
        Reindex(lines);
        return entry;
    }

    /// <summary>Deletes a design, or a header with every design under it.</summary>
    public static DesignEntry? Delete(ObservableCollection<DesignEntry> lines, DesignEntry entry)
    {
        var index = lines.IndexOf(entry);
        if (index < 0)
        {
            return Selected(lines);
        }

        var end = entry.IsHeader ? GroupEnd(lines, index) : index + 1;

        for (var i = end - 1; i >= index; i--)
        {
            lines.RemoveAt(i);
        }

        var next = lines.Count == 0
            ? null
            : lines[Math.Min(index, lines.Count - 1)];

        Select(lines, next);
        Reindex(lines);
        return next;
    }

    /// <summary>How many designs a header would take with it.</summary>
    public static int ChildCount(ObservableCollection<DesignEntry> lines, DesignEntry header)
    {
        var index = lines.IndexOf(header);
        return index < 0 ? 0 : GroupEnd(lines, index) - index - 1;
    }

    /// <summary>
    /// Renames a row in place. Empty names are refused, long names are clipped, and a
    /// name already used by another row gets a number instead of silently colliding.
    /// </summary>
    public static string Rename(ObservableCollection<DesignEntry> lines, DesignEntry entry, string name)
    {
        var clean = FirstLine(name).Trim();

        if (clean.Length == 0)
        {
            return entry.Name;
        }

        if (clean.Length > MaxNameLength)
        {
            clean = clean[..MaxNameLength].TrimEnd();
        }

        if (clean == entry.Name)
        {
            return entry.Name;
        }

        var candidate = clean;
        var counter = 2;

        while (lines.Any(line => !ReferenceEquals(line, entry) && line.Name == candidate))
        {
            candidate = $"{clean} {counter}";
            counter++;
        }

        entry.Name = candidate;
        return candidate;
    }

    public static string Unique(ObservableCollection<DesignEntry> lines, string baseName)
    {
        if (!lines.Any(line => line.Name == baseName))
        {
            return baseName;
        }

        var counter = 2;
        var candidate = $"{baseName} copy";

        while (lines.Any(line => line.Name == candidate))
        {
            candidate = $"{baseName} copy {counter}";
            counter++;
        }

        return candidate;
    }

    /// <summary>A name is a label, not a document: keep the first line, drop the rest.</summary>
    public static string FirstLine(string text)
    {
        var index = text.IndexOfAny(['\r', '\n']);
        return index < 0 ? text : text[..index];
    }
}
