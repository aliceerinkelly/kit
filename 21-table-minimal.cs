// Minimal table: no outer border, horizontal hairlines only, a quiet header.
var rows = new (string Name, string Cpu, string Mem)[]
{
    ("PipelineWorker", "12.4%", "184 MB"),
    ("Indexer", "8.1%", "96 MB"),
    ("Watcher", "3.7%", "42 MB"),
    ("Renderer", "22.9%", "512 MB")
};

var table = new StackPanel { Width = 440 };

Grid Line(double height)
{
    var grid = new Grid { Height = height };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });
    return grid;
}

void Put(Grid grid, UIElement element, int column)
{
    Grid.SetColumn(element, column);
    grid.Children.Add(element);
}

var head = Line(22);
Put(head, KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush"), 0);
Put(head, KitUi.Mono("CPU", 9.5, "Kit.MutedBrush"), 1);
Put(head, KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush"), 2);
table.Children.Add(head);

foreach (var (name, cpu, mem) in rows)
{
    var row = Line(28);
    Put(row, KitUi.Text(name, 12), 0);
    Put(row, KitUi.Mono(cpu, 11), 1);
    Put(row, KitUi.Mono(mem, 11, "Kit.MutedBrush"), 2);
    table.Children.Add(row);
    table.Children.Add(KitUi.Rule());
}

var card = KitUi.Surface(table, 12, 8, "Kit.PanelBrush", "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
