// Hover highlight: rows stay quiet until the pointer finds one, shown here mid-hover.
var rows = new (string Name, string Cpu, string Mem)[]
{
    ("PipelineWorker", "12.4%", "184 MB"),
    ("Indexer", "8.1%", "96 MB"),
    ("Watcher", "3.7%", "42 MB"),
    ("Renderer", "22.9%", "512 MB")
};

var table = new StackPanel { Width = 440 };

var head = new Grid { Height = 22, Margin = new Thickness(8, 0, 8, 0) };
head.ColumnDefinitions.Add(new ColumnDefinition());
head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

var headProcess = KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush");
var headCpu = KitUi.Mono("CPU", 9.5, "Kit.MutedBrush");
var headMem = KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush");

Grid.SetColumn(headCpu, 1);
Grid.SetColumn(headMem, 2);

head.Children.Add(headProcess);
head.Children.Add(headCpu);
head.Children.Add(headMem);
table.Children.Add(head);

for (var i = 0; i < rows.Length; i++)
{
    var (name, cpu, mem) = rows[i];

    var grid = new Grid { Height = 28 };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

    var label = KitUi.Text(name, 12);
    label.VerticalAlignment = VerticalAlignment.Center;
    label.Margin = new Thickness(8, 0, 0, 0);

    var load = KitUi.Mono(cpu, 11);
    load.VerticalAlignment = VerticalAlignment.Center;

    var memory = KitUi.Mono(mem, 11, "Kit.MutedBrush");
    memory.VerticalAlignment = VerticalAlignment.Center;

    Grid.SetColumn(load, 1);
    Grid.SetColumn(memory, 2);

    grid.Children.Add(label);
    grid.Children.Add(load);
    grid.Children.Add(memory);

    var band = new Border { CornerRadius = new CornerRadius(4), Child = grid };

    if (i == 1)
    {
        band.SetResourceReference(Border.BackgroundProperty, "Kit.AccentSoftBrush");
    }

    table.Children.Add(band);
}

var card = KitUi.Surface(table, 8, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
