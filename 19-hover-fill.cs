// Hover fill: outlined at rest, solid under the pointer. The whole state change is the
// button answering the mouse before the click.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var outline = new FlatButton
{
    Content = "Connect",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(4),
    Fill = Brushes.Transparent,
    Edge = Hex("#3182ce"),
    Ink = Hex("#3182ce"),
    HoverFill = Hex("#3182ce"),
    HoverEdge = Hex("#3182ce"),
    HoverInk = Brushes.White
};

var filled = new FlatButton
{
    Content = "Connect",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(4),
    Fill = Hex("#3182ce"),
    Edge = Hex("#3182ce"),
    Ink = Brushes.White,
    HoverFill = Hex("#2b6cb0"),
    HoverEdge = Hex("#2b6cb0"),
    HoverInk = Brushes.White,
    Margin = new Thickness(10, 0, 0, 0)
};

var states = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
states.Children.Add(outline);
states.Children.Add(filled);

var caption = KitUi.Muted("left: rest   \u00B7   right: what the pointer sees", 10.5);
caption.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, states, caption);
