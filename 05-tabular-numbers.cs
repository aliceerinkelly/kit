// Tabular numbers: right-aligned mono so the decimal points line up down the column.
var rows = new (string Label, string Value, string Unit)[]
{
    ("Build time", "1.42", "s"),
    ("Package size", "63.4", "MB"),
    ("Designs", "96", ""),
    ("Warnings", "0", "")
};

var table = new StackPanel { Width = 320 };

foreach (var (label, value, unit) in rows)
{
    var grid = new Grid { Height = 28 };
    grid.ColumnDefinitions.Add(new ColumnDefinition());
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(34) });

    var name = KitUi.Muted(label, 11.5);
    name.VerticalAlignment = VerticalAlignment.Center;

    var number = KitUi.Mono(value, 13, "Kit.TextBrush");
    number.TextAlignment = TextAlignment.Right;
    number.VerticalAlignment = VerticalAlignment.Center;

    var suffix = KitUi.Muted(unit, 10);
    suffix.VerticalAlignment = VerticalAlignment.Center;
    suffix.Margin = new Thickness(6, 0, 0, 0);

    Grid.SetColumn(number, 1);
    Grid.SetColumn(suffix, 2);

    grid.Children.Add(name);
    grid.Children.Add(number);
    grid.Children.Add(suffix);
    table.Children.Add(grid);
    table.Children.Add(KitUi.Rule());
}

var card = KitUi.Surface(table, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
