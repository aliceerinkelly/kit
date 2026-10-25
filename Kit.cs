// KIT: the whole program in one file, grouped by namespace so the XAML keeps working.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Kit.Controls;
using Kit.Theming;

namespace Kit.Theming
{
using System;
using System.Windows;
using System.Windows.Media;


public sealed record KitAccent(string Name, string Hex, string Ink);

public static class KitThemes
{
    public static readonly KitAccent[] Accents =
    [
        new("Emerald", "#00FF9C", "#00734A"),
        new("Rose", "#FF2E88", "#A8004F"),
        new("Sapphire", "#22D3FF", "#006A8C"),
        new("Amethyst", "#B14BFF", "#5E00A8"),
        new("Amber", "#FFE600", "#8A6200")
    ];
}

public sealed class KitPalette
{
    public string Background { get; init; } = "";
    public string Panel { get; init; } = "";
    public string Card { get; init; } = "";
    public string Row { get; init; } = "";
    public string Text { get; init; } = "";
    public string Muted { get; init; } = "";
    public string Outline { get; init; } = "";
    public string Border { get; init; } = "";
    public string Accent { get; init; } = "";
    public string AccentInk { get; init; } = "";
    public string AccentSoft { get; init; } = "";
    public string AccentDeep { get; init; } = "";
    public string Hover { get; init; } = "";
    public bool Dark { get; init; }
}

public static class KitPalettes
{
    /// <summary>
    /// Flat first: every surface is one solid colour, every edge is a 1px line.
    /// The gem still carries the theme; it just isn't glowing through glass.
    /// </summary>
    public static KitPalette For(KitAccent accent, bool dark)
    {
        var background = dark ? Mix("#0A0C10", accent.Hex, 0.02) : "#F4F5F7";

        return new KitPalette
        {
            Background = background,
            Panel = dark ? Mix("#11141B", accent.Hex, 0.03) : "#FFFFFF",
            Card = dark ? Mix("#151922", accent.Hex, 0.04) : "#FFFFFF",
            Row = dark ? Mix("#1A1F29", accent.Hex, 0.04) : "#F7F8FA",
            Text = dark ? Mix("#E9EDF4", accent.Hex, 0.03) : accent.Ink,
            Muted = dark ? Mix("#8A95A6", accent.Hex, 0.12) : accent.Ink,
            Outline = dark ? Mix("#2C3340", accent.Hex, 0.25) : Mix("#A7B0BD", accent.Hex, 0.08),
            Border = dark ? Mix("#343C4A", accent.Hex, 0.30) : Mix("#D6DCE5", accent.Hex, 0.06),
            Accent = accent.Hex,
            AccentInk = dark ? accent.Hex : accent.Ink,
            AccentSoft = dark ? Mix(accent.Hex, background, 0.82) : Mix(accent.Hex, "#FFFFFF", 0.90),
            AccentDeep = dark ? Mix(accent.Hex, "#000000", 0.35) : Mix(accent.Hex, accent.Ink, 0.55),
            Hover = dark ? Mix("#202633", accent.Hex, 0.15) : Mix("#EDF0F4", accent.Hex, 0.08),
            Dark = dark
        };
    }

    public static Color Parse(string hex)
    {
        var text = hex.TrimStart('#');
        static int Part(string s, int start) => Convert.ToInt32(s.Substring(start, 2), 16);

        if (text.Length == 8)
        {
            return Color.FromArgb((byte)Part(text, 0), (byte)Part(text, 2), (byte)Part(text, 4), (byte)Part(text, 6));
        }

        return Color.FromRgb((byte)Part(text, 0), (byte)Part(text, 2), (byte)Part(text, 4));
    }

    public static string Mix(string a, string b, double weight)
    {
        var ca = Parse(a);
        var cb = Parse(b);
        static byte Blend(byte x, byte y, double w) => (byte)Math.Round(x * (1 - w) + y * w);
        return $"#{Blend(ca.R, cb.R, weight):X2}{Blend(ca.G, cb.G, weight):X2}{Blend(ca.B, cb.B, weight):X2}";
    }
}

public static class ThemeManager
{
    public static void Apply(KitAccent accent, bool dark)
    {
        if (Application.Current is null)
        {
            return;
        }

        var p = KitPalettes.For(accent, dark);
        var resources = Application.Current.Resources;

        resources["Kit.BackgroundBrush"] = Solid(p.Background);
        resources["Kit.PanelBrush"] = Solid(p.Panel);
        resources["Kit.CardBrush"] = Solid(p.Card);
        resources["Kit.RowBrush"] = Solid(p.Row);
        resources["Kit.PrimaryBrush"] = Solid(p.Accent);
        resources["Kit.PillBrush"] = Solid(p.AccentSoft);
        resources["Kit.FieldBrush"] = Solid(p.Card);
        resources["Kit.TextBrush"] = Solid(p.Text);
        resources["Kit.MutedBrush"] = Solid(p.Muted);
        resources["Kit.OutlineBrush"] = Solid(p.Outline);
        resources["Kit.BorderBrush"] = Solid(p.Border);
        resources["Kit.AccentBrush"] = Solid(p.Accent);
        resources["Kit.AccentInkBrush"] = Solid(p.AccentInk);
        resources["Kit.AccentSoftBrush"] = Solid(p.AccentSoft);
        resources["Kit.AccentDeepBrush"] = Solid(p.AccentDeep);
        resources["Kit.HoverBrush"] = Solid(p.Hover);
        resources["Kit.OnAccentBrush"] = Solid("#0B1220");

        // Black/white button base. It inverts with the mode, and every brush can be
        // overridden to theme the six size styles without touching the template.
        var monoFill = dark ? "#F4F4F4" : "#0B0B0B";
        var monoInk = dark ? "#0B0B0B" : "#FFFFFF";
        var monoHoverFill = dark ? "#0B0B0B" : "#FFFFFF";
        var monoHoverInk = dark ? "#FFFFFF" : "#0B0B0B";
        resources["Kit.MonoFillBrush"] = Solid(monoFill);
        resources["Kit.MonoInkBrush"] = Solid(monoInk);
        resources["Kit.MonoEdgeBrush"] = Solid(monoFill);
        resources["Kit.MonoHoverFillBrush"] = Solid(monoHoverFill);
        resources["Kit.MonoHoverInkBrush"] = Solid(monoHoverInk);
        resources["Kit.MonoHoverEdgeBrush"] = Solid(monoHoverInk);
    }

    private static SolidColorBrush Solid(string hex)
    {
        var brush = new SolidColorBrush(KitPalettes.Parse(hex));
        brush.Freeze();
        return brush;
    }
}
}

namespace Kit
{
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;


public sealed class DesignEntry : INotifyPropertyChanged
{
    private bool _selected;
    private bool _editing;
    private string _name;
    private string? _code;
    private Thickness _indent;

    public DesignEntry(string name, bool custom, string? code = null, bool isHeader = false)
    {
        _name = name;
        Custom = custom;
        _code = code;
        IsHeader = isHeader;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (_name == value)
            {
                return;
            }

            _name = value;
            Raise(nameof(Name));
        }
    }

    public bool Custom { get; }

    /// <summary>True while the row is being renamed in place.</summary>
    public bool IsEditing
    {
        get => _editing;
        set
        {
            if (_editing == value)
            {
                return;
            }

            _editing = value;
            Raise(nameof(IsEditing));
        }
    }

    /// <summary>A category header owns the designs that follow it in the rail.</summary>
    public bool IsHeader { get; }

    /// <summary>The snippet this design owns. Selecting it puts the code in the editor.</summary>
    public string? Code
    {
        get => _code;
        set
        {
            if (_code == value)
            {
                return;
            }

            _code = value;
            Raise(nameof(Code));
        }
    }

    /// <summary>Left margin set by the rail: designs sit under their header.</summary>
    public Thickness Indent
    {
        get => _indent;
        set
        {
            if (_indent == value)
            {
                return;
            }

            _indent = value;
            Raise(nameof(Indent));
        }
    }

    /// <summary>The little mark in front of a header row.</summary>
    public string Glyph => IsHeader ? "▾" : string.Empty;

    public bool IsSelected
    {
        get => _selected;
        set
        {
            if (_selected == value)
            {
                return;
            }

            _selected = value;
            Raise(nameof(IsSelected));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Raise(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public sealed record DesignRecord(string Name, string? Code, bool IsHeader);

public static class DesignStore
{
    private static string FilePath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Kit",
            "designs.json");

    /// <summary>True once this machine has saved a rail of its own.</summary>
    public static bool Exists => File.Exists(FilePath);

    public static IReadOnlyList<DesignRecord> Load()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                return Array.Empty<DesignRecord>();
            }

            var json = File.ReadAllText(FilePath);

            // A hand-edited file can be one record, an array, or an array wrapped in an
            // array. Read all three rather than silently showing an empty rail.
            var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            if (root.ValueKind == JsonValueKind.Array
                && root.GetArrayLength() == 1
                && root[0].ValueKind == JsonValueKind.Array)
            {
                root = root[0];
            }

            if (root.ValueKind == JsonValueKind.Array)
            {
                return JsonSerializer.Deserialize<List<DesignRecord>>(root.GetRawText()) ?? [];
            }

            if (root.ValueKind == JsonValueKind.Object)
            {
                var single = JsonSerializer.Deserialize<DesignRecord>(root.GetRawText());
                return single is null ? [] : [single];
            }

            return Array.Empty<DesignRecord>();
        }
        catch
        {
            return Array.Empty<DesignRecord>();
        }
    }

    public static void Save(IEnumerable<DesignEntry> entries)
    {
        try
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var records = new List<DesignRecord>();
            foreach (var entry in entries)
            {
                records.Add(new DesignRecord(entry.Name, entry.Code, entry.IsHeader));
            }

            File.WriteAllText(FilePath, JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch
        {
            // The library still works in memory if the local file cannot be written.
        }
    }
}
}

namespace Kit
{
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


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
}

namespace Kit
{
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Kit.Controls;
using Kit.Theming;


public partial class MainWindow : Window
{
    private KitAccent _accent = KitThemes.Accents[0];
    private bool _dark = true;
    private string? _copiedName;
    private string? _copiedCode;
    private bool _copiedHeader;
    private DesignEntry? _shown;

    public ObservableCollection<DesignEntry> Designs { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        var stored = DesignStore.Load();

        // First run on a machine with no saved rail: open with the shipped library
        // rather than an empty canvas. Only when no file exists, so deleting every row
        // stays deleted instead of resurrecting on the next start.
        var seeded = false;
        if (stored.Count == 0 && !DesignStore.Exists)
        {
            stored = StarterCatalog.Read();
            seeded = stored.Count > 0;
        }

        foreach (var record in stored)
        {
            if (!string.IsNullOrWhiteSpace(record.Name))
            {
                Designs.Add(new DesignEntry(record.Name, custom: true, record.Code, record.IsHeader));
            }
        }

        if (seeded)
        {
            DesignStore.Save(Designs);
        }

        Rail.Reindex(Designs);

        DataContext = this;
        EmptyHint.Visibility = Designs.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        UpdateChrome();

        if (Designs.Count > 0)
        {
            Rail.Select(Designs, Designs[0]);
            ShowEntry(Designs[0]);
        }
        else
        {
            ShowEntry(null);
        }
    }

    private void Accent_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string name })
        {
            _accent = Array.Find(KitThemes.Accents, a => a.Name == name) ?? _accent;
            ThemeManager.Apply(_accent, _dark);
            UpdateChrome();
        }
    }

    private void Mode_Click(object sender, RoutedEventArgs e)
    {
        _dark = !_dark;
        ThemeManager.Apply(_accent, _dark);
        UpdateChrome();
    }

    private void UpdateChrome()
    {
        ThemeName.Text = $"{_accent.Name} · {(_dark ? "Dark" : "Light")}";
        ModeButton.Content = _dark ? "Light mode" : "Dark mode";
    }

    private void Design_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton { DataContext: DesignEntry entry })
        {
            ShowEntry(entry);
        }
    }

    private void AddLine_Click(object sender, RoutedEventArgs e)
    {
        // An empty box still adds something: a design you can name later beats a
        // button that looks broken.
        var name = NewLineBox.Text.Trim();
        if (name.Length == 0)
        {
            name = "New design";
        }

        var entry = Rail.AddDesign(Designs, name);
        NewLineBox.Clear();
        EmptyHint.Visibility = Visibility.Collapsed;
        ShowEntry(entry);
        DesignStore.Save(Designs);

        // Queue the focus: the click that got us here would otherwise keep it on the button.
        Dispatcher.BeginInvoke(new Action(() => CodeBox.Focus()), System.Windows.Threading.DispatcherPriority.Input);
    }

    private void AddHeader_Click(object sender, RoutedEventArgs e)
    {
        var name = NewLineBox.Text.Trim();
        if (name.Length == 0)
        {
            name = "New category";
        }

        var entry = Rail.AddHeader(Designs, name);
        NewLineBox.Clear();
        EmptyHint.Visibility = Visibility.Collapsed;
        ShowEntry(entry);
        DesignStore.Save(Designs);
    }

    private void ButtonSizes_Click(object sender, RoutedEventArgs e)
    {
        var window = new ButtonSizesWindow { Owner = this };
        window.Show();
    }

    /// <summary>
    /// The label is the unit: selecting it loads its own code on the right and shows
    /// whatever that code builds underneath. No label, no editor.
    /// </summary>
    private void ShowEntry(DesignEntry? entry)
    {
        // Switching labels keeps what you typed: the editor is a draft of the design
        // it was opened on, whether or not you pressed Run.
        StashEditor();
        _shown = entry;

        if (CanvasTitle is null)
        {
            return;
        }

        CanvasTitle.Text = entry?.Name ?? "No line selected";
        CanvasHint.Text = entry is null
            ? "Add a design on the left to start."
            : entry.IsHeader
                ? "Category. Select a design under it, or add one."
                : "Paste C#, press Run, and the control appears below.";

        CodeBox.IsEnabled = entry is not null && !entry.IsHeader;
        RunButton.IsEnabled = entry is not null && !entry.IsHeader;
        SampleButton.IsEnabled = entry is not null && !entry.IsHeader;
        RunErrors.Visibility = Visibility.Collapsed;
        RunStatus.Text = string.Empty;
        PreviewHost.Content = null;

        if (entry is null)
        {
            CodeBox.Text = string.Empty;
            RunStatus.Text = "No line selected";
            PreviewHint.Text = "Add a design on the left to start.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        if (entry.IsHeader)
        {
            var count = Rail.ChildCount(Designs, entry);
            CodeBox.Text = string.Empty;
            RunStatus.Text = count == 1 ? "1 design" : $"{count} designs";
            PreviewHint.Text = "A category holds designs. Select one to see its code.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        CodeBox.Text = entry.Code ?? string.Empty;
        CodeBox.ScrollToHome();

        if (string.IsNullOrWhiteSpace(entry.Code))
        {
            PreviewHint.Text = "This line has no code yet. Paste some and press Run.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        RunSnippet(save: false);
    }

    /// <summary>
    /// Writes the editor back onto the design it belongs to. Without this, selecting
    /// another label threw away code that had not been run yet.
    /// </summary>
    private void StashEditor()
    {
        if (_shown is not { IsHeader: false } leaving || CodeBox is null)
        {
            return;
        }

        var text = CodeBox.Text ?? string.Empty;
        if (string.Equals(leaving.Code ?? string.Empty, text, StringComparison.Ordinal))
        {
            return;
        }

        leaving.Code = text;
        DesignStore.Save(Designs);
    }

    // --- Rail: move, copy, paste, delete ---------------------------------

    private void Line_RightClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is RadioButton { DataContext: DesignEntry entry })
        {
            Rail.Select(Designs, entry);
            ShowEntry(entry);
        }
    }

    // --- Rename ----------------------------------------------------------

    private void Line_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is RadioButton { DataContext: DesignEntry entry } row)
        {
            BeginRename(row, entry);
            e.Handled = true;
        }
    }

    private void RenameLine_Click(object sender, RoutedEventArgs e)
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        var row = LinesList.ItemContainerGenerator.ContainerFromItem(entry) as ContentPresenter;
        var button = row is null ? null : FindChild<RadioButton>(row);

        if (button is not null)
        {
            BeginRename(button, entry);
        }
        else
        {
            entry.IsEditing = true;
        }
    }

    private void BeginRename(RadioButton row, DesignEntry entry)
    {
        // One row at a time: another row still editing gets committed first.
        foreach (var other in Designs.Where(line => line.IsEditing && !ReferenceEquals(line, entry)))
        {
            other.IsEditing = false;
        }

        Rail.Select(Designs, entry);
        ShowEntry(entry);
        entry.IsEditing = true;

        row.UpdateLayout();

        var box = FindChild<TextBox>(row);
        if (box is null)
        {
            return;
        }

        Dispatcher.BeginInvoke(new Action(() =>
        {
            box.Focus();
            box.SelectAll();
        }), DispatcherPriority.Input);
    }

    private void Rename_KeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not TextBox box)
        {
            return;
        }

        if (e.Key == Key.Enter)
        {
            CommitRename(box);
            e.Handled = true;
        }
        else if (e.Key == Key.Escape)
        {
            CancelRename(box);
            e.Handled = true;
        }
    }

    private void Rename_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (sender is TextBox box)
        {
            CommitRename(box);
        }
    }

    private void CommitRename(TextBox box)
    {
        if (box.DataContext is not DesignEntry entry || !entry.IsEditing)
        {
            return;
        }

        entry.IsEditing = false;

        var name = Rail.Rename(Designs, entry, box.Text);
        box.Text = name;

        if (entry.IsSelected)
        {
            CanvasTitle.Text = name;
            ShowEntry(entry);
        }

        DesignStore.Save(Designs);
    }

    private void CancelRename(TextBox box)
    {
        if (box.DataContext is DesignEntry entry)
        {
            box.Text = entry.Name;
            entry.IsEditing = false;
        }
    }

    private static T? FindChild<T>(DependencyObject root) where T : DependencyObject
    {
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
        {
            var child = VisualTreeHelper.GetChild(root, i);

            if (child is T match)
            {
                return match;
            }

            var inner = FindChild<T>(child);
            if (inner is not null)
            {
                return inner;
            }
        }

        return null;
    }

    private void MoveUp_Click(object sender, RoutedEventArgs e) => MoveLine(-1);

    private void MoveDown_Click(object sender, RoutedEventArgs e) => MoveLine(1);

    private void CopyLine_Click(object sender, RoutedEventArgs e) => CopyLine();

    private void PasteLine_Click(object sender, RoutedEventArgs e) => PasteLine();

    private void DeleteLine_Click(object sender, RoutedEventArgs e) => DeleteLine();

    private void LinesList_KeyDown(object sender, KeyEventArgs e)
    {
        var control = Keyboard.Modifiers == ModifierKeys.Control;

        if (e.Key == Key.F2)
        {
            RenameLine_Click(sender, e);
            e.Handled = true;
        }
        else if (e.Key == Key.Delete)
        {
            DeleteLine();
            e.Handled = true;
        }
        else if (control && e.Key == Key.Up)
        {
            MoveLine(-1);
            e.Handled = true;
        }
        else if (control && e.Key == Key.Down)
        {
            MoveLine(1);
            e.Handled = true;
        }
        else if (control && e.Key == Key.C)
        {
            CopyLine();
            e.Handled = true;
        }
        else if (control && e.Key == Key.V)
        {
            PasteLine();
            e.Handled = true;
        }
    }

    private void MoveLine(int delta)
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        if (Rail.Move(Designs, entry, delta) >= 0)
        {
            DesignStore.Save(Designs);
        }
    }

    private void CopyLine()
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        _copiedName = Rail.CopyName(entry);
        _copiedCode = entry.Code;
        _copiedHeader = entry.IsHeader;

        try
        {
            Clipboard.SetText(_copiedName);
        }
        catch
        {
            // The rail copy still works when the system clipboard is locked.
        }
    }

    private void PasteLine()
    {
        var name = _copiedName;

        if (string.IsNullOrWhiteSpace(name))
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    name = Clipboard.GetText();
                }
            }
            catch
            {
                name = null;
            }
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var entry = Rail.Paste(Designs, name, _copiedCode, _copiedHeader);
        _copiedCode = null;
        _copiedName = null;
        _copiedHeader = false;

        EmptyHint.Visibility = Visibility.Collapsed;
        ShowEntry(entry);
        DesignStore.Save(Designs);
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
        {
            OpenSnippet_Click(sender, e);
            e.Handled = true;
        }
    }

    private void DeleteLine()
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        if (entry.IsHeader)
        {
            var children = Rail.ChildCount(Designs, entry);
            var question = children == 0
                ? $"Delete the category \"{entry.Name}\"?"
                : $"Delete the category \"{entry.Name}\" and its {children} design{(children == 1 ? "" : "s")}?";

            if (MessageBox.Show(this, question, "KIT", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
        }

        var next = Rail.Delete(Designs, entry);
        EmptyHint.Visibility = Designs.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        ShowEntry(next);
        DesignStore.Save(Designs);
    }

    private void Run_Click(object sender, RoutedEventArgs e)
    {
        RunSnippet();
    }

    /// <summary>
    /// The no-paste path: open a snippet file, run it, and keep it on the selected label.
    /// </summary>
    private void OpenSnippet_Click(object sender, RoutedEventArgs e)
    {
        if (Rail.Selected(Designs) is null)
        {
            return;
        }

        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Title = "Open a snippet",
            Filter = "C# snippets (*.cs)|*.cs|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            CheckFileExists = true
        };

        if (dialog.ShowDialog(this) != true)
        {
            return;
        }

        try
        {
            var code = System.IO.File.ReadAllText(dialog.FileName);
            var target = Rail.Selected(Designs);

            // A category cannot hold code: the file becomes a design named after itself.
            if (target is null || target.IsHeader)
            {
                target = Rail.AddDesign(Designs, System.IO.Path.GetFileNameWithoutExtension(dialog.FileName));
                EmptyHint.Visibility = Visibility.Collapsed;
            }
            else if (!string.IsNullOrWhiteSpace(target.Code)
                     && !string.Equals(target.Code.Trim(), code.Trim(), StringComparison.Ordinal))
            {
                var replace = MessageBox.Show(
                    this,
                    $"Replace the code in \"{target.Name}\"?",
                    "KIT",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (replace != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            target.Code = code;
            Rail.Select(Designs, target);
            ShowEntry(target);
            DesignStore.Save(Designs);
            RunStatus.Text = $"Loaded {System.IO.Path.GetFileName(dialog.FileName)}";
        }
        catch (Exception ex)
        {
            RunStatus.Text = "Could not open file";
            RunErrors.Text = ex.Message;
            RunErrors.Visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Sample belongs to the design you are in: a flyout with the code for one button,
    /// nothing added to the rail and nothing overwritten until you press Insert.
    /// </summary>
    private void Sample_Click(object sender, RoutedEventArgs e)
    {
        if (sender is UIElement anchor)
        {
            SampleFlyout.PlacementTarget = anchor;
        }

        // The flyout is its own transparent window, so paint it from live resources
        // rather than relying on dynamic lookup inside the popup tree.
        if (TryFindResource("Kit.RowBrush") is System.Windows.Media.Brush card)
        {
            SampleCard.Background = card;
        }

        if (TryFindResource("Kit.OutlineBrush") is System.Windows.Media.Brush edge)
        {
            SampleCard.BorderBrush = edge;
        }

        SampleCode.Text = SnippetRunner.OnePill;
        SampleFlyout.IsOpen = true;
    }

    private void InsertSample_Click(object sender, RoutedEventArgs e)
    {
        SampleFlyout.IsOpen = false;

        if (Rail.Selected(Designs) is null || Rail.Selected(Designs)!.IsHeader)
        {
            RunStatus.Text = "Select a design first";
            return;
        }

        CodeBox.Text = SnippetRunner.OnePill;
        CodeBox.Focus();
        RunStatus.Text = "One-button sample in the editor — press Run";
    }

    private void CopySample_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Clipboard.SetText(SnippetRunner.OnePill);
            RunStatus.Text = "One-button sample copied";
        }
        catch
        {
            RunStatus.Text = "Clipboard is busy";
        }

        SampleFlyout.IsOpen = false;
    }

    private void CodeBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
        {
            RunSnippet();
            e.Handled = true;
        }
    }

    /// <summary>
    /// Compiles the editor and drops the result into the preview. A successful run also
    /// saves the code onto the selected label, so each line keeps its own snippet.
    /// Errors land under the Run button, next to the code that caused them.
    /// </summary>
    private void RunSnippet(bool save = true)
    {
        var entry = Rail.Selected(Designs);
        var result = SnippetRunner.Run(CodeBox.Text ?? string.Empty);
        PreviewHost.Content = null;
        RunErrors.Visibility = Visibility.Collapsed;

        if (result.Messages.Count > 0)
        {
            RunStatus.Text = "Did not run";
            RunErrors.Text = string.Join(Environment.NewLine, result.Messages);
            RunErrors.Visibility = Visibility.Visible;
            PreviewHint.Text = "Fix the code above and press Run.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        if (result.Preview is not { } element)
        {
            PreviewHint.Text = result.Text ?? "Build() returned nothing to show.";
            PreviewHint.Visibility = Visibility.Visible;
            RunStatus.Text = "Nothing to render";
            return;
        }

        PreviewHost.Content = element;
        PreviewHint.Visibility = Visibility.Collapsed;
        RunStatus.Text = save
            ? $"Rendered {element.GetType().Name} · saved to {entry?.Name}"
            : $"Rendered {element.GetType().Name} · auto from {entry?.Name}";

        if (save && entry is not null)
        {
            entry.Code = CodeBox.Text;
            DesignStore.Save(Designs);
        }
    }

    private void NewLineBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            AddLine_Click(sender, e);
            e.Handled = true;
        }
    }

}
}

namespace Kit
{
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kit.Controls;


/// <summary>
/// The gallery for the C# pill button: all six sizes, drawn from the same template.
/// Built in code and free of KIT styles, so it can be pasted into any WPF project.
/// </summary>
public sealed class ButtonSizesWindow : Window
{
    private static readonly PillSize[] Sizes =
    [
        PillSize.Tiny,
        PillSize.Small,
        PillSize.Medium,
        PillSize.Large,
        PillSize.XLarge,
        PillSize.Huge
    ];

    private readonly Brush _text;
    private readonly Brush _muted;
    private readonly Brush _panel;
    private readonly Brush _panelEdge;

    public ButtonSizesWindow()
    {
        _text = Brush("Kit.TextBrush", Brushes.Black);
        _muted = Brush("Kit.MutedBrush", Brushes.Gray);
        _panel = Brush("Kit.PanelBrush", Brushes.Transparent);
        _panelEdge = Brush("Kit.BorderBrush", Brushes.Gray);

        Title = "KIT — Button sizes";
        Width = 520;
        Height = 580;
        MinWidth = 440;
        MinHeight = 480;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        // Uses the KIT font when the host app has it, otherwise WPF's default.
        if (TryFindResource("Kit.UiFont") is FontFamily font)
        {
            FontFamily = font;
        }

        if (TryFindResource("Kit.BackgroundBrush") is Brush page)
        {
            Background = page;
        }
        else
        {
            Background = Brushes.White;
        }

        var root = new Grid { Margin = new Thickness(22.0) };
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var heading = new TextBlock
        {
            Text = "Button sizes",
            FontSize = 20.0,
            FontWeight = FontWeights.SemiBold,
            Foreground = _text
        };

        var subheading = new TextBlock
        {
            Text = "One pill template, six sizes, black and white.",
            FontSize = 12.0,
            Foreground = _text,
            Margin = new Thickness(0.0, 5.0, 0.0, 14.0)
        };

        var header = new StackPanel();
        header.Children.Add(heading);
        header.Children.Add(subheading);
        Grid.SetRow(header, 0);
        root.Children.Add(header);

        var panel = new Border
        {
            Background = _panel,
            BorderBrush = _panelEdge,
            BorderThickness = new Thickness(1.0),
            CornerRadius = new CornerRadius(10.0),
            Padding = new Thickness(12.0)
        };

        var rows = new StackPanel { Margin = new Thickness(8.0, 6.0, 8.0, 6.0) };

        foreach (var size in Sizes)
        {
            rows.Children.Add(BuildRow(size));
        }

        panel.Child = rows;
        Grid.SetRow(panel, 1);
        root.Children.Add(panel);

        var footer = new TextBlock
        {
            Text = "Hover inverts the pill. Each size is 22, 26, 32, 38, 46, 56 tall.",
            FontSize = 11.0,
            Foreground = _muted,
            Margin = new Thickness(2.0, 10.0, 2.0, 0.0)
        };
        Grid.SetRow(footer, 2);
        root.Children.Add(footer);

        Content = root;
    }

    private Brush Brush(string key, Brush fallback) =>
        TryFindResource(key) as Brush ?? fallback;

    private Grid BuildRow(PillSize size)
    {
        var button = new PillButton
        {
            Size = size,
            Content = size.ToString(),
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0.0, 0.0, 0.0, 10.0)
        };

        var label = new TextBlock
        {
            Text = $"{size} · {button.Height:0}",
            FontSize = 11.0,
            Foreground = _muted,
            VerticalAlignment = VerticalAlignment.Center
        };

        var row = new Grid();
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        Grid.SetColumn(button, 1);
        row.Children.Add(label);
        row.Children.Add(button);
        return row;
    }
}
}

namespace Kit.Controls
{
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


/// <summary>The six pill heights KIT ships.</summary>
public enum PillSize
{
    Tiny,
    Small,
    Medium,
    Large,
    XLarge,
    Huge
}

/// <summary>
/// A pill button written in C#: one template, six sizes, black and white by theme,
/// and every brush exposed as a dependency property so it can be rethemed without
/// touching the template. The radius is always half the height, so it stays a pill
/// at any size a consumer sets.
/// </summary>
public class PillButton : Button
{
    private readonly record struct Metrics(
        double Height,
        double MinWidth,
        double FontSize,
        double PadX,
        double PadY);

    private static Metrics MetricsFor(PillSize size) => size switch
    {
        PillSize.Tiny => new(22.0, 52.0, 10.0, 8.0, 2.0),
        PillSize.Small => new(26.0, 62.0, 10.5, 10.0, 3.0),
        PillSize.Medium => new(32.0, 80.0, 12.0, 14.0, 5.0),
        PillSize.Large => new(38.0, 100.0, 13.0, 18.0, 7.0),
        PillSize.XLarge => new(46.0, 120.0, 15.0, 22.0, 9.0),
        _ => new(56.0, 144.0, 17.0, 28.0, 12.0)
    };

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size),
        typeof(PillSize),
        typeof(PillButton),
        new PropertyMetadata(PillSize.Medium, OnSizeChanged));

    /// <summary>Half the height, kept in sync on every layout pass.</summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
        nameof(Radius),
        typeof(CornerRadius),
        typeof(PillButton),
        new PropertyMetadata(new CornerRadius(0.0), OnBrushChanged));

    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
        nameof(Fill),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty InkProperty = DependencyProperty.Register(
        nameof(Ink),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty EdgeProperty = DependencyProperty.Register(
        nameof(Edge),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty HoverFillProperty = DependencyProperty.Register(
        nameof(HoverFill),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty HoverInkProperty = DependencyProperty.Register(
        nameof(HoverInk),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty HoverEdgeProperty = DependencyProperty.Register(
        nameof(HoverEdge),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty FocusEdgeProperty = DependencyProperty.Register(
        nameof(FocusEdge),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.DodgerBlue, OnBrushChanged));

    private Border? _chrome;

    public PillButton()
    {
        Cursor = Cursors.Hand;
        SnapsToDevicePixels = true;
        UseLayoutRounding = true;

        // Theme first: these resolve against the live application resources, so a
        // light/dark switch repaints every pill without rebuilding it.
        SetResourceReference(FillProperty, "Kit.MonoFillBrush");
        SetResourceReference(InkProperty, "Kit.MonoInkBrush");
        SetResourceReference(EdgeProperty, "Kit.MonoEdgeBrush");
        SetResourceReference(HoverFillProperty, "Kit.MonoHoverFillBrush");
        SetResourceReference(HoverInkProperty, "Kit.MonoHoverInkBrush");
        SetResourceReference(HoverEdgeProperty, "Kit.MonoHoverEdgeBrush");
        SetResourceReference(FocusEdgeProperty, "Kit.AccentBrush");
        SetResourceReference(FontFamilyProperty, "Kit.UiFont");

        Template = BuildTemplate();
        ApplyMetrics();
    }

    public PillSize Size
    {
        get => (PillSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public CornerRadius Radius
    {
        get => (CornerRadius)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    public Brush Ink
    {
        get => (Brush)GetValue(InkProperty);
        set => SetValue(InkProperty, value);
    }

    public Brush Edge
    {
        get => (Brush)GetValue(EdgeProperty);
        set => SetValue(EdgeProperty, value);
    }

    public Brush HoverFill
    {
        get => (Brush)GetValue(HoverFillProperty);
        set => SetValue(HoverFillProperty, value);
    }

    public Brush HoverInk
    {
        get => (Brush)GetValue(HoverInkProperty);
        set => SetValue(HoverInkProperty, value);
    }

    public Brush HoverEdge
    {
        get => (Brush)GetValue(HoverEdgeProperty);
        set => SetValue(HoverEdgeProperty, value);
    }

    public Brush FocusEdge
    {
        get => (Brush)GetValue(FocusEdgeProperty);
        set => SetValue(FocusEdgeProperty, value);
    }

    private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PillButton)d).ApplyMetrics();
    }

    private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PillButton)d).Refresh();
    }

    private static ControlTemplate BuildTemplate()
    {
        var chrome = new FrameworkElementFactory(typeof(Border), "Chrome");
        chrome.SetValue(Border.BorderThicknessProperty, new Thickness(1.0));

        var presenter = new FrameworkElementFactory(typeof(ContentPresenter));
        presenter.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        presenter.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

        chrome.AppendChild(presenter);

        var pressed = new Trigger { Property = ButtonBase.IsPressedProperty, Value = true };
        pressed.Setters.Add(new Setter(UIElement.OpacityProperty, 0.82, "Chrome"));

        var disabled = new Trigger { Property = UIElement.IsEnabledProperty, Value = false };
        disabled.Setters.Add(new Setter(UIElement.OpacityProperty, 0.4, "Chrome"));

        var template = new ControlTemplate(typeof(PillButton)) { VisualTree = chrome };
        template.Triggers.Add(pressed);
        template.Triggers.Add(disabled);
        return template;
    }

    private void ApplyMetrics()
    {
        var metrics = MetricsFor(Size);
        Height = metrics.Height;
        MinWidth = metrics.MinWidth;
        FontSize = metrics.FontSize;
        FontWeight = FontWeights.SemiBold;
        Padding = new Thickness(metrics.PadX, metrics.PadY, metrics.PadX, metrics.PadY);
        Radius = new CornerRadius(metrics.Height / 2.0);
        Refresh();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _chrome = GetTemplateChild("Chrome") as Border;
        Refresh();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);

        if (sizeInfo.NewSize.Height > 0)
        {
            Radius = new CornerRadius(sizeInfo.NewSize.Height / 2.0);
        }

        Refresh();
    }

    protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        Refresh();
    }

    protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        Refresh();
    }

    protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
        base.OnGotKeyboardFocus(e);
        Refresh();
    }

    protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
        base.OnLostKeyboardFocus(e);
        Refresh();
    }

    /// <summary>
    /// Paints the chrome by hand instead of through triggers: the brushes are
    /// dependency properties, so a theme swap and a per-button override both land here.
    /// </summary>
    private void Refresh()
    {
        if (_chrome is null)
        {
            return;
        }

        var hover = IsMouseOver && IsEnabled;

        _chrome.Background = hover ? HoverFill : Fill;
        _chrome.BorderBrush = IsKeyboardFocused ? FocusEdge : hover ? HoverEdge : Edge;
        _chrome.CornerRadius = Radius;

        // Ink rides the text tree, so string content, a TextBlock and an icon all inherit it.
        TextElement.SetForeground(_chrome, hover ? HoverInk : Ink);
    }
}
}

namespace Kit.Controls
{
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


/// <summary>
/// A button you colour. PillButton is the six-size black-and-white pill; this one
/// takes a design's own palette: any fill (solid or gradient), one 1px edge, any
/// corner radius, and a hover state you set instead of one KIT guesses.
/// </summary>
public class FlatButton : Button
{
    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
        nameof(Fill),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty InkProperty = DependencyProperty.Register(
        nameof(Ink),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty EdgeProperty = DependencyProperty.Register(
        nameof(Edge),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty HoverFillProperty = DependencyProperty.Register(
        nameof(HoverFill),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty HoverInkProperty = DependencyProperty.Register(
        nameof(HoverInk),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty HoverEdgeProperty = DependencyProperty.Register(
        nameof(HoverEdge),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty FocusEdgeProperty = DependencyProperty.Register(
        nameof(FocusEdge),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.DodgerBlue, OnBrushChanged));

    /// <summary>Any radius: 0 is a tile, half the height is a pill.</summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
        nameof(Radius),
        typeof(CornerRadius),
        typeof(FlatButton),
        new PropertyMetadata(new CornerRadius(10.0), OnBrushChanged));

    private Border? _chrome;

    public FlatButton()
    {
        Cursor = Cursors.Hand;
        SnapsToDevicePixels = true;
        UseLayoutRounding = true;

        // Theme defaults first, so an unthemed FlatButton still belongs in KIT.
        SetResourceReference(FontFamilyProperty, "Kit.UiFont");
        SetResourceReference(FillProperty, "Kit.CardBrush");
        SetResourceReference(InkProperty, "Kit.TextBrush");
        SetResourceReference(EdgeProperty, "Kit.OutlineBrush");
        SetResourceReference(HoverFillProperty, "Kit.HoverBrush");
        SetResourceReference(HoverInkProperty, "Kit.TextBrush");
        SetResourceReference(HoverEdgeProperty, "Kit.AccentBrush");
        SetResourceReference(FocusEdgeProperty, "Kit.AccentBrush");

        Template = BuildTemplate();
    }

    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    public Brush Ink
    {
        get => (Brush)GetValue(InkProperty);
        set => SetValue(InkProperty, value);
    }

    public Brush Edge
    {
        get => (Brush)GetValue(EdgeProperty);
        set => SetValue(EdgeProperty, value);
    }

    public Brush HoverFill
    {
        get => (Brush)GetValue(HoverFillProperty);
        set => SetValue(HoverFillProperty, value);
    }

    public Brush HoverInk
    {
        get => (Brush)GetValue(HoverInkProperty);
        set => SetValue(HoverInkProperty, value);
    }

    public Brush HoverEdge
    {
        get => (Brush)GetValue(HoverEdgeProperty);
        set => SetValue(HoverEdgeProperty, value);
    }

    public Brush FocusEdge
    {
        get => (Brush)GetValue(FocusEdgeProperty);
        set => SetValue(FocusEdgeProperty, value);
    }

    public CornerRadius Radius
    {
        get => (CornerRadius)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((FlatButton)d).Refresh();
    }

    private static ControlTemplate BuildTemplate()
    {
        var chrome = new FrameworkElementFactory(typeof(Border), "Chrome");
        chrome.SetValue(Border.BorderThicknessProperty, new Thickness(1.0));

        // Padding is part of the button's shape: without it, a coloured pill's label
        // runs out over its own edge.
        chrome.SetValue(Border.PaddingProperty, new TemplateBindingExtension(Control.PaddingProperty));

        var presenter = new FrameworkElementFactory(typeof(ContentPresenter));
        presenter.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        presenter.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

        chrome.AppendChild(presenter);

        var pressed = new Trigger { Property = ButtonBase.IsPressedProperty, Value = true };
        pressed.Setters.Add(new Setter(UIElement.OpacityProperty, 0.85, "Chrome"));

        var disabled = new Trigger { Property = UIElement.IsEnabledProperty, Value = false };
        disabled.Setters.Add(new Setter(UIElement.OpacityProperty, 0.45, "Chrome"));

        var template = new ControlTemplate(typeof(FlatButton)) { VisualTree = chrome };
        template.Triggers.Add(pressed);
        template.Triggers.Add(disabled);
        return template;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _chrome = GetTemplateChild("Chrome") as Border;
        Refresh();
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        Refresh();
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        Refresh();
    }

    protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
        base.OnGotKeyboardFocus(e);
        Refresh();
    }

    protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
        base.OnLostKeyboardFocus(e);
        Refresh();
    }

    /// <summary>
    /// Painted by hand like PillButton: every brush is a dependency property, so a
    /// design's palette and a theme swap land in the same place.
    /// </summary>
    private void Refresh()
    {
        if (_chrome is null)
        {
            return;
        }

        var hover = IsMouseOver && IsEnabled;

        _chrome.Background = hover ? HoverFill : Fill;
        _chrome.BorderBrush = IsKeyboardFocused ? FocusEdge : hover ? HoverEdge : Edge;
        _chrome.CornerRadius = Radius;

        // Ink rides the text tree, so a string, a TextBlock and an icon all follow it.
        TextElement.SetForeground(_chrome, hover ? HoverInk : Ink);
    }
}
}

namespace Kit.Controls
{
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


/// <summary>
/// Short cuts for KIT's theme, so a snippet can show a design instead of six setters
/// per element. Every brush here is a resource key rather than a hex, which means a
/// design built from these helpers follows the accent and the light/dark switch live.
/// </summary>
public static class KitUi
{
    /// <summary>Points one property at a theme resource, so it keeps following the theme.</summary>
    public static T Skin<T>(T element, DependencyProperty property, string key)
        where T : FrameworkElement
    {
        element.SetResourceReference(property, key);
        return element;
    }

    /// <summary>Applies one of the named styles from Themes/Kit.xaml.</summary>
    public static T Use<T>(T control, string key)
        where T : FrameworkElement
    {
        if (Application.Current?.TryFindResource(key) is Style style)
        {
            control.Style = style;
        }

        return control;
    }

    public static TextBlock Text(string text, double size = 12.5, string brush = "Kit.TextBrush") =>
        Skin(
            new TextBlock { Text = text, FontSize = size, TextTrimming = TextTrimming.CharacterEllipsis },
            TextBlock.ForegroundProperty,
            brush);

    public static TextBlock Muted(string text, double size = 11.5) => Text(text, size, "Kit.MutedBrush");

    public static TextBlock Head(string text, double size = 14)
    {
        var head = Text(text, size);
        head.FontWeight = FontWeights.Bold;
        return head;
    }

    public static TextBlock Mono(string text, double size = 11.5, string brush = "Kit.TextBrush")
    {
        var label = Text(text, size, brush);
        Skin(label, TextBlock.FontFamilyProperty, "Kit.MonoFont");
        return label;
    }

    public static StackPanel Col(double gap = 8, params UIElement[] children) => Stack(false, gap, children);

    public static StackPanel Row(double gap = 8, params UIElement[] children) => Stack(true, gap, children);

    private static StackPanel Stack(bool horizontal, double gap, UIElement[] children)
    {
        var panel = new StackPanel
        {
            Orientation = horizontal ? Orientation.Horizontal : Orientation.Vertical
        };

        for (var i = 0; i < children.Length; i++)
        {
            // Add to the margin the caller already set instead of replacing it: an
            // indent or a custom offset is part of the design, the gap is not.
            if (children[i] is FrameworkElement element && i < children.Length - 1)
            {
                var margin = element.Margin;

                element.Margin = horizontal
                    ? new Thickness(margin.Left, margin.Top, margin.Right + gap, margin.Bottom)
                    : new Thickness(margin.Left, margin.Top, margin.Right, margin.Bottom + gap);
            }

            panel.Children.Add(children[i]);
        }

        return panel;
    }

    public static Border Surface(
        UIElement child,
        double pad = 12,
        double radius = 8,
        string fill = "Kit.CardBrush",
        string edge = "Kit.BorderBrush")
    {
        var border = new Border
        {
            Padding = new Thickness(pad),
            CornerRadius = new CornerRadius(radius),
            BorderThickness = new Thickness(1),
            Child = child
        };

        Skin(border, Border.BackgroundProperty, fill);
        Skin(border, Border.BorderBrushProperty, edge);
        return border;
    }

    public static Border Rule(double thickness = 1) =>
        Skin(new Border { Height = thickness }, Border.BackgroundProperty, "Kit.BorderBrush");

    public static TextBox Input(string text, double height = 30, double size = 12)
    {
        var box = Skin(
            new TextBox
            {
                Text = text,
                Height = height,
                FontSize = size,
                Padding = new Thickness(9, 0, 9, 0),
                VerticalContentAlignment = VerticalAlignment.Center,
                BorderThickness = new Thickness(1)
            },
            TextBox.BackgroundProperty,
            "Kit.FieldBrush");

        Skin(box, TextBox.ForegroundProperty, "Kit.TextBrush");
        Skin(box, TextBox.BorderBrushProperty, "Kit.BorderBrush");
        Skin(box, TextBox.CaretBrushProperty, "Kit.AccentBrush");
        return box;
    }

    /// <summary>A pill. Active is accent on dark ink; quiet is panel on text ink.</summary>
    public static FlatButton Pill(string text, bool active = false, double height = 30)
    {
        var pill = new FlatButton
        {
            Content = text,
            Height = height,
            Radius = new CornerRadius(height / 2.0),
            Padding = new Thickness(14, 0, 14, 0),
            FontSize = 12,
            FontWeight = FontWeights.SemiBold
        };

        if (active)
        {
            Skin(pill, FlatButton.FillProperty, "Kit.AccentBrush");
            Skin(pill, FlatButton.InkProperty, "Kit.OnAccentBrush");
            Skin(pill, FlatButton.EdgeProperty, "Kit.AccentBrush");
            Skin(pill, FlatButton.HoverFillProperty, "Kit.AccentDeepBrush");
            Skin(pill, FlatButton.HoverInkProperty, "Kit.OnAccentBrush");
            Skin(pill, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
        }
        else
        {
            Skin(pill, FlatButton.FillProperty, "Kit.PanelBrush");
            Skin(pill, FlatButton.InkProperty, "Kit.TextBrush");
            Skin(pill, FlatButton.EdgeProperty, "Kit.BorderBrush");
            Skin(pill, FlatButton.HoverFillProperty, "Kit.HoverBrush");
            Skin(pill, FlatButton.HoverInkProperty, "Kit.TextBrush");
            Skin(pill, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
        }

        return pill;
    }

    /// <summary>A borderless action: an icon, a glyph, a quiet "close".</summary>
    public static FlatButton Quiet(string text, double height = 26, double width = double.NaN)
    {
        var quiet = new FlatButton
        {
            Content = text,
            Height = height,
            MinWidth = 0,
            Padding = new Thickness(6, 0, 6, 0),
            FontSize = 11,
            Radius = new CornerRadius(5),
            Fill = Brushes.Transparent,
            Edge = Brushes.Transparent,
            HoverFill = Brushes.Transparent
        };

        if (!double.IsNaN(width))
        {
            quiet.Width = width;
        }

        Skin(quiet, FlatButton.InkProperty, "Kit.MutedBrush");
        Skin(quiet, FlatButton.HoverInkProperty, "Kit.TextBrush");
        Skin(quiet, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
        return quiet;
    }

    public static Border Dot(double size = 8, string brush = "Kit.AccentBrush") =>
        Skin(
            new Border
            {
                Width = size,
                Height = size,
                CornerRadius = new CornerRadius(size / 2.0),
                VerticalAlignment = VerticalAlignment.Center
            },
            Border.BackgroundProperty,
            brush);

    public static Border Chip(string text, string fill = "Kit.RowBrush", string ink = "Kit.MutedBrush")
    {
        var chip = new Border
        {
            CornerRadius = new CornerRadius(4),
            Padding = new Thickness(8, 2, 8, 2),
            VerticalAlignment = VerticalAlignment.Center,
            Child = Text(text, 10.5, ink)
        };

        Skin(chip, Border.BackgroundProperty, fill);
        return chip;
    }

    /// <summary>A keycap: mono text in a small outlined box.</summary>
    public static Border Keycap(string text)
    {
        var cap = new Border
        {
            CornerRadius = new CornerRadius(4),
            BorderThickness = new Thickness(1),
            Padding = new Thickness(6, 2, 6, 2),
            VerticalAlignment = VerticalAlignment.Center,
            Child = Mono(text, 10)
        };

        Skin(cap, Border.BackgroundProperty, "Kit.FieldBrush");
        Skin(cap, Border.BorderBrushProperty, "Kit.BorderBrush");
        return cap;
    }

    public static Border Swatch(string hex, double size = 22) =>
        new()
        {
            Width = size,
            Height = size,
            CornerRadius = new CornerRadius(size / 2.0),
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex)),
            BorderThickness = new Thickness(1.5),
            BorderBrush = Brushes.White,
            Margin = new Thickness(0, 0, 8, 0)
        };
}
}

namespace Kit.Controls
{
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


/// <summary>What a pasted snippet produced: a control, some text, or errors.</summary>
public sealed record SnippetResult(UIElement? Preview, string? Text, IReadOnlyList<string> Messages)
{
    public bool Ok => Messages.Count == 0;
}

/// <summary>
/// Compiles pasted C# and hands back the control it builds. KIT is a design tool, so
/// this is deliberately shallow: one class, one Build() entry point, no files on disk,
/// no assembly saved anywhere. The code runs in this process, exactly like a code-behind
/// file would, which is why it can use PillButton and WPF directly.
/// </summary>
public static class SnippetRunner
{
    private static List<MetadataReference>? _references;

    /// <summary>The smallest thing that works, shown in the Sample flyout.</summary>
    public const string OnePill = """
        return new PillButton
        {
            Content = "Save",
            Size = PillSize.Large
        };
        """;

    /// <summary>Pasted into the box when KIT starts, so the contract is visible.</summary>
    public const string Sample = """
        var row = new StackPanel { Orientation = Orientation.Horizontal };

        row.Children.Add(new PillButton
        {
            Content = "Save",
            Size = PillSize.Medium,
            Margin = new Thickness(0, 0, 8, 0)
        });

        row.Children.Add(new PillButton { Content = "Cancel", Size = PillSize.Small });

        return row;
        """;

    private const string Header = """
        // KIT runs this as the body of Snippet.Build().
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Windows;
        using System.Windows.Controls;
        using System.Windows.Input;
        using System.Windows.Media;
        using Kit.Controls;

        public static class Snippet
        {
            public static object Build()
            {

        """;

    private const string Footer = """
            }
        }
        """;

    public static SnippetResult Run(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return new SnippetResult(null, null, ["Nothing to run yet — paste some code."]);
        }

        var wholeFile = code.Contains("class ", StringComparison.Ordinal);
        var source = wholeFile ? code : Header + Normalize(code) + "\n" + Footer;
        var headerLines = wholeFile ? 0 : Header.Count(c => c == '\n');

        var tree = CSharpSyntaxTree.ParseText(
            source,
            new CSharpParseOptions(LanguageVersion.Latest),
            path: "snippet.cs");

        var compilation = CSharpCompilation.Create(
            "Kit.Snippet",
            [tree],
            References(),
            new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Debug));

        using var stream = new MemoryStream();
        var emitted = compilation.Emit(stream);

        if (!emitted.Success)
        {
            var errors = new List<string>();

            if (!wholeFile && !Balanced(Normalize(code)))
            {
                errors.Add(
                    "This looks like part of a file, not a snippet: the curly braces do not match. " +
                    "Paste the whole thing, or paste something that returns a control.");
            }

            var diagnostics = emitted.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => Describe(d, headerLines))
                .ToList();

            errors.AddRange(diagnostics.Take(5));

            if (diagnostics.Count > 5)
            {
                errors.Add($"...and {diagnostics.Count - 5} more.");
            }

            return new SnippetResult(null, null, errors);
        }

        stream.Position = 0;
        var assembly = Assembly.Load(stream.ToArray());
        var entry = FindBuild(assembly);

        if (entry is null)
        {
            // A whole control class pasted in: build the first one that can stand alone.
            var control = FindControl(assembly);
            if (control is null)
            {
                return new SnippetResult(null, null, [
                    "No Build() method and no control class to show. Return a control, or paste a class with a parameterless constructor."]);
            }

            try
            {
                return new SnippetResult(Activator.CreateInstance(control) as UIElement, null, []);
            }
            catch (Exception ex)
            {
                return new SnippetResult(null, null, [Describe(ex)]);
            }
        }

        object? value;
        try
        {
            value = entry.Invoke(null, null);
        }
        catch (TargetInvocationException ex)
        {
            return new SnippetResult(null, null, [Describe(ex.InnerException ?? ex)]);
        }
        catch (Exception ex)
        {
            return new SnippetResult(null, null, [Describe(ex)]);
        }

        return value switch
        {
            null => new SnippetResult(null, null, ["Build() returned null."]),
            UIElement element => new SnippetResult(element, null, []),
            _ => new SnippetResult(null, value.ToString(), [])
        };
    }

    /// <summary>
    /// People paste a snippet, not a method. If there is no return yet, make the obvious
    /// one: a bare `new PillButton {...}` gets returned, and a `var save = new ...`
    /// returns the last thing it built, so the preview shows the thing they just wrote.
    /// </summary>
    private static string Normalize(string code)
    {
        var body = code.TrimEnd();

        if (body.Length == 0 || body.Contains("return", StringComparison.Ordinal))
        {
            return body;
        }

        if (body.TrimStart().StartsWith("new ", StringComparison.Ordinal)
            && body.EndsWith(";", StringComparison.Ordinal)
            && body.Count(c => c == ';') == 1)
        {
            return "return " + body[..^1].TrimEnd() + ";";
        }

        var assignments = Regex.Matches(body, @"\b(\w+)\s*=\s*new\b");
        if (assignments.Count > 0)
        {
            var name = assignments[assignments.Count - 1].Groups[1].Value;
            return body + Environment.NewLine + "return " + name + ";";
        }

        return body + Environment.NewLine + "return null;";
    }

    /// <summary>Braces inside strings and comments are ignored; this is a shape check.</summary>
    private static bool Balanced(string code)
    {
        var depth = 0;
        var inString = false;
        var inChar = false;
        var escaped = false;

        foreach (var c in code)
        {
            if (escaped)
            {
                escaped = false;
                continue;
            }

            if (c == '\\' && (inString || inChar))
            {
                escaped = true;
                continue;
            }

            if (!inChar && c == '"')
            {
                inString = !inString;
                continue;
            }

            if (!inString && c == '\'')
            {
                inChar = !inChar;
                continue;
            }

            if (inString || inChar)
            {
                continue;
            }

            if (c == '{')
            {
                depth++;
            }
            else if (c == '}')
            {
                depth--;
                if (depth < 0)
                {
                    return false;
                }
            }
        }

        return depth == 0;
    }

    private static MethodInfo? FindBuild(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            var method = type.GetMethod(
                "Build",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
                Type.EmptyTypes);

            if (method is not null)
            {
                return method;
            }
        }

        return null;
    }

    private static Type? FindControl(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (typeof(UIElement).IsAssignableFrom(type)
                && !type.IsAbstract
                && type.GetConstructor(Type.EmptyTypes) is not null)
            {
                return type;
            }
        }

        return null;
    }

    private static string Describe(Diagnostic diagnostic, int headerLines)
    {
        var line = diagnostic.Location.GetLineSpan().StartLinePosition.Line + 1 - headerLines;
        var column = diagnostic.Location.GetLineSpan().StartLinePosition.Character + 1;

        if (headerLines == 0 || line < 1)
        {
            return $"{diagnostic.Id}: {diagnostic.GetMessage()}";
        }

        return $"{diagnostic.Id} (line {line}, col {column}): {diagnostic.GetMessage()}";
    }

    private static string Describe(Exception ex) => $"{ex.GetType().Name}: {ex.Message}";

    /// <summary>
    /// Everything WPF and KIT can see, plus the shared framework, so a snippet can use
    /// Brushes, Thickness and PillSize without extra using blocks of its own.
    /// </summary>
    private static List<MetadataReference> References()
    {
        if (_references is not null)
        {
            return _references;
        }

        var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") is string trusted)
        {
            foreach (var path in trusted.Split(Path.PathSeparator))
            {
                paths.Add(path);
            }
        }

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location))
            {
                paths.Add(assembly.Location);
            }
        }

        _references = paths
            .Where(File.Exists)
            .Select(path => (MetadataReference)MetadataReference.CreateFromFile(path))
            .ToList();

        return _references;
    }
}
}

namespace Kit
{
    /// <summary>The shipped library, in rail order: header rows and the designs under them.</summary>
    public static class StarterCatalog
    {
        public static List<DesignRecord> Read()
        {
            var records = new List<DesignRecord>(Designs.Rail.Length);

            foreach (var row in Designs.Rail)
            {
                records.Add(new DesignRecord(row.Name, row.Code, row.IsHeader));
            }

            return records;
        }
    }
}

