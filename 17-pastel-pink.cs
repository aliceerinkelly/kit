// Pastel pink: a soft fill with a deep ink, the one style that is deliberately gentle.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var soft = new FlatButton
{
    Content = "Archive",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Hex("#fed7d7"),
    Edge = Hex("#fed7d7"),
    Ink = Hex("#c53030"),
    HoverFill = Hex("#feb2b2"),
    HoverEdge = Hex("#fc8181"),
    HoverInk = Hex("#9b2c2c")
};

var blue = new FlatButton
{
    Content = "Mute",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Hex("#bee3f8"),
    Edge = Hex("#bee3f8"),
    Ink = Hex("#2b6cb0"),
    HoverFill = Hex("#90cdf4"),
    HoverEdge = Hex("#63b3ed"),
    HoverInk = Hex("#2c5282"),
    Margin = new Thickness(10, 0, 0, 0)
};

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(soft);
row.Children.Add(blue);
return row;
