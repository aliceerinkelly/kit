using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Kit;

/// <summary>
/// The design library as it ships inside the program, so a fresh copy of KIT opens with
/// the shelf full instead of an empty rail. The same files seed the source tree through
/// tools/seed-catalog.ps1; these are embedded copies of those files.
///
/// The category comes from the number in the file name, exactly as the seed script does:
/// catalog 01-50, styles 01-25, type 01-10. Anything past a declared range lands under a
/// "new" header rather than being dropped.
/// </summary>
public static class StarterCatalog
{
    private sealed record Shelf(string Folder, string Group, int From, int To);

    private static readonly Shelf[] Shelves =
    [
        new("catalog", "chrome & nav", 1, 8),
        new("catalog", "data display", 9, 16),
        new("catalog", "inputs", 17, 24),
        new("catalog", "overlays", 25, 30),
        new("catalog", "gauges", 31, 35),
        new("catalog", "layout", 36, 40),
        new("catalog", "dev tools", 41, 50),
        new("catalog", "new designs", 51, int.MaxValue),
        new("styles", "button styles", 1, 20),
        new("styles", "table styles", 21, 25),
        new("styles", "new styles", 26, int.MaxValue),
        new("type", "type styles", 1, 10),
        new("type", "new type", 11, int.MaxValue),
    ];

    private const string Prefix = "snippets/";

    /// <summary>Every shipped design, in rail order: category header, then its designs.</summary>
    public static List<DesignRecord> Read()
    {
        var assembly = typeof(StarterCatalog).Assembly;

        var files = assembly.GetManifestResourceNames()
            .Where(name => name.StartsWith(Prefix, StringComparison.Ordinal))
            .Select(name => (Name: name, Parts: name[Prefix.Length..].Split('/')))
            .Where(item => item.Parts.Length == 2)
            .Select(item => new
            {
                Folder = item.Parts[0],
                File = item.Parts[1],
                Number = LeadingNumber(item.Parts[1]),
                item.Name
            })
            .Where(item => item.Number > 0)
            .ToList();

        if (files.Count == 0)
        {
            return [];
        }

        var records = new List<DesignRecord>();
        var placed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var shelf in Shelves)
        {
            var mine = files
                .Where(file => file.Folder.Equals(shelf.Folder, StringComparison.OrdinalIgnoreCase)
                               && file.Number >= shelf.From
                               && file.Number <= shelf.To)
                .OrderBy(file => file.Number)
                .ThenBy(file => file.File, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (mine.Count == 0)
            {
                continue;
            }

            records.Add(new DesignRecord(shelf.Group, null, true));

            foreach (var file in mine)
            {
                if (!placed.Add(file.Name))
                {
                    continue;
                }

                records.Add(new DesignRecord(RowName(file.File), ReadText(assembly, file.Name), false));
            }
        }

        return records;
    }

    private static int LeadingNumber(string fileName)
    {
        var head = fileName.Split('-')[0];
        return int.TryParse(head, NumberStyles.None, CultureInfo.InvariantCulture, out var number)
            ? number
            : 0;
    }

    private static string RowName(string fileName)
    {
        var withoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var dash = withoutExtension.IndexOf('-');

        if (dash < 0 || dash + 1 >= withoutExtension.Length)
        {
            return withoutExtension;
        }

        return withoutExtension[(dash + 1)..].Replace('-', ' ');
    }

    private static string ReadText(System.Reflection.Assembly assembly, string resource)
    {
        using var stream = assembly.GetManifestResourceStream(resource);
        if (stream is null)
        {
            return string.Empty;
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}
