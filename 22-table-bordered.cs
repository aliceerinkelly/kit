// Bordered table: a line between every cell, a box around the whole thing.
var rows = new (string Name, string Cpu, string Mem)[]
{
    ("PipelineWorker", "12.4%", "184 MB"),
    ("Indexer", "8.1%", "96 MB"),
    ("Watcher", "3.7%", "42 MB")
};

var table = new StackPanel { Width = 440 };

Grid Line(double height, bool header)
{
    var grid = new Grid { Height = height };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

    if (header)
    {
        var band = new Border();
        band.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
        band.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
        band.BorderThickness = new Thickness(0, 0, 0, 1);
        Grid.SetColumnSpan(band, 3);
        grid.Children.Add(band);
    }

    return grid;
}

void Put(Grid grid, FrameworkElement element, int column)
{
    element.Margin = new Thickness(8, 0, 8, 0);
    element.VerticalAlignment = VerticalAlignment.Center;
    Grid.SetColumn(element, column);
    grid.Children.Add(element);
}

var head = Line(24, true);
Put(head, KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush"), 0);
Put(head, KitUi.Mono("CPU", 9.5, "Kit.MutedBrush"), 1);
Put(head, KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush"), 2);
table.Children.Add(head);

foreach (var (name, cpu, mem) in rows)
{
    var row = Line(28, false);
    Put(row, KitUi.Text(name, 12), 0);
    Put(row, KitUi.Mono(cpu, 11), 1);
    Put(row, KitUi.Mono(mem, 11, "Kit.MutedBrush"), 2);
    table.Children.Add(row);
    table.Children.Add(KitUi.Rule());
}

var frame = new Border
{
    BorderThickness = new Thickness(1),
    CornerRadius = new CornerRadius(6),
    Child = table
};

frame.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
frame.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
frame.HorizontalAlignment = HorizontalAlignment.Left;
return frame;
