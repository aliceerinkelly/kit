// Compact table: 24px rows, no lines, 4px of padding. Density is the style.
var rows = new (string Name, string Cpu, string Mem)[]
{
    ("PipelineWorker", "12.4%", "184 MB"),
    ("Indexer", "8.1%", "96 MB"),
    ("Watcher", "3.7%", "42 MB"),
    ("Renderer", "22.9%", "512 MB"),
    ("Scheduler", "1.2%", "18 MB")
};

var table = new StackPanel { Width = 420, Margin = new Thickness(4) };

foreach (var (name, cpu, mem) in rows)
{
    var grid = new Grid { Height = 24 };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(72) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });

    var label = KitUi.Text(name, 11.5);
    label.VerticalAlignment = VerticalAlignment.Center;

    var load = KitUi.Mono(cpu, 10.5);
    load.VerticalAlignment = VerticalAlignment.Center;

    var memory = KitUi.Mono(mem, 10.5, "Kit.MutedBrush");
    memory.VerticalAlignment = VerticalAlignment.Center;

    Grid.SetColumn(load, 1);
    Grid.SetColumn(memory, 2);

    grid.Children.Add(label);
    grid.Children.Add(load);
    grid.Children.Add(memory);
    table.Children.Add(grid);
}

var card = KitUi.Surface(table, 4, 6, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
