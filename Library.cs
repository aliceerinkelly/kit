using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Kit;

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
