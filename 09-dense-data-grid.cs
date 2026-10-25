// Dense data grid: 24px rows, mono numbers and a load bar in the last column.
var rows = new (string Name, string Cpu, string Mem, double Load)[]
{
    ("PipelineWorker", "12.4%", "184 MB", 0.34),
    ("Indexer", "8.1%", "96 MB", 0.22),
    ("Watcher", "3.7%", "42 MB", 0.11),
    ("Renderer", "22.9%", "512 MB", 0.61)
};

var table = new StackPanel { Width = 520 };

Grid Line(double height)
{
    var grid = new Grid { Height = height };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(68) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(78) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
    return grid;
}

void Put(Grid grid, UIElement element, int column)
{
    Grid.SetColumn(element, column);
    grid.Children.Add(element);
}

var head = Line(20);
Put(head, KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush"), 0);
Put(head, KitUi.Mono("CPU", 9.5, "Kit.MutedBrush"), 1);
Put(head, KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush"), 2);
Put(head, KitUi.Mono("LOAD", 9.5, "Kit.MutedBrush"), 3);
table.Children.Add(head);

foreach (var (name, cpu, mem, load) in rows)
{
    var row = Line(26);
    Put(row, KitUi.Text(name, 12), 0);
    Put(row, KitUi.Mono(cpu, 11), 1);
    Put(row, KitUi.Mono(mem, 11, "Kit.MutedBrush"), 2);

    var track = new Border { Height = 4, CornerRadius = new CornerRadius(2), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 8, 0) };
    track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

    var fill = new Border { Width = 64 * load, Height = 4, CornerRadius = new CornerRadius(2), HorizontalAlignment = HorizontalAlignment.Left };
    fill.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");

    var bar = new Grid();
    bar.Children.Add(track);
    bar.Children.Add(fill);
    Put(row, bar, 3);

    table.Children.Add(row);
    table.Children.Add(KitUi.Rule());
}

var card = KitUi.Surface(table, 12, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
