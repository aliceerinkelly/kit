// Dark striped: this table brings its own dark surface, so it reads the same in both modes.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var rows = new (string Name, string Cpu, string Mem)[]
{
    ("PipelineWorker", "12.4%", "184 MB"),
    ("Indexer", "8.1%", "96 MB"),
    ("Watcher", "3.7%", "42 MB"),
    ("Renderer", "22.9%", "512 MB")
};

var table = new StackPanel();

for (var i = 0; i < rows.Length; i++)
{
    var (name, cpu, mem) = rows[i];

    var grid = new Grid { Height = 28 };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

    var label = new TextBlock { Text = name, FontSize = 12, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 0, 0, 0) };
    var load = new TextBlock { Text = cpu, FontFamily = new FontFamily("Consolas"), FontSize = 11, Foreground = Hex("#cbd5e0"), VerticalAlignment = VerticalAlignment.Center };
    var memory = new TextBlock { Text = mem, FontFamily = new FontFamily("Consolas"), FontSize = 11, Foreground = Hex("#a0aec0"), VerticalAlignment = VerticalAlignment.Center };

    Grid.SetColumn(load, 1);
    Grid.SetColumn(memory, 2);

    grid.Children.Add(label);
    grid.Children.Add(load);
    grid.Children.Add(memory);

    var band = new Border
    {
        Background = i % 2 == 0 ? Hex("#2d3748") : Hex("#4a5568"),
        Child = grid
    };

    table.Children.Add(band);
}

var frame = new Border
{
    Background = Hex("#2d3748"),
    CornerRadius = new CornerRadius(6),
    Padding = new Thickness(0, 0, 0, 0),
    Child = table,
    HorizontalAlignment = HorizontalAlignment.Left,
    Width = 440,
    ClipToBounds = true
};

return frame;
