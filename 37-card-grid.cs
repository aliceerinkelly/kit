// Card grid: modular tiles that wrap, each with a name, a count and a swatch.
var wrap = new WrapPanel { Width = 480 };

var tiles = new (string Name, string Count, string Hex)[]
{
    ("Buttons", "18 designs", "#FF2E88"),
    ("Fields", "9 designs", "#22D3FF"),
    ("Charts", "6 designs", "#B14BFF"),
    ("Layouts", "12 designs", "#00FF9C")
};

foreach (var (name, count, hex) in tiles)
{
    var swatch = KitUi.Swatch(hex, 10);

    var label = KitUi.Text(name, 12.5);
    label.FontWeight = FontWeights.SemiBold;

    var top = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
    top.Children.Add(swatch);
    top.Children.Add(label);

    var body = KitUi.Col(3, top, KitUi.Muted(count, 10.5));
    var tile = KitUi.Surface(body, 12, 8, "Kit.CardBrush", "Kit.BorderBrush");
    tile.Width = 148;
    tile.Margin = new Thickness(0, 0, 9, 9);
    wrap.Children.Add(tile);
}

var host = KitUi.Surface(wrap, 10, 8, "Kit.PanelBrush");
host.HorizontalAlignment = HorizontalAlignment.Left;
return host;
