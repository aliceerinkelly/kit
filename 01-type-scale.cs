// Type scale: the five sizes KIT uses, each shown at its own size with its spec.
var rows = new (string Name, double Size, string Weight, string Sample)[]
{
    ("display", 26, "Bold", "Kit"),
    ("heading", 18, "Bold", "Design library"),
    ("body", 12.5, "Regular", "Dense rows, mono values, visible focus."),
    ("caption", 10.5, "Regular", "1px outlines, 10px radius"),
    ("micro", 9.5, "SemiBold", "SECTION LABEL")
};

var list = new StackPanel { Width = 460 };

foreach (var (name, size, weight, sample) in rows)
{
    var grid = new Grid { Margin = new Thickness(0, 0, 0, 10) };
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(96) });
    grid.ColumnDefinitions.Add(new ColumnDefinition());

    var spec = KitUi.Mono($"{name} {size:0.#}", 10, "Kit.MutedBrush");
    spec.VerticalAlignment = VerticalAlignment.Center;

    var text = KitUi.Text(sample, size, "Kit.TextBrush");
    text.TextTrimming = TextTrimming.None;
    text.TextWrapping = TextWrapping.NoWrap;

    if (weight == "Bold")
    {
        text.FontWeight = FontWeights.Bold;
    }
    else if (weight == "SemiBold")
    {
        text.FontWeight = FontWeights.SemiBold;
    }

    Grid.SetColumn(text, 1);
    grid.Children.Add(spec);
    grid.Children.Add(text);
    list.Children.Add(grid);
}

var card = KitUi.Surface(list, 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
