// Raised 3D: a light face on a darker base, so the button looks like it sits on the page.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var face = new FlatButton
{
    Content = "Edit entry",
    Height = 34,
    Padding = new Thickness(14, 0, 14, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(4),
    Fill = Hex("#e2e8f0"),
    Edge = Hex("#a0aec0"),
    Ink = Hex("#2d3748"),
    HoverFill = Hex("#edf2f7"),
    HoverEdge = Hex("#a0aec0"),
    HoverInk = Hex("#1a202c")
};

var basePlate = new Border
{
    Background = Hex("#a0aec0"),
    CornerRadius = new CornerRadius(4.5),
    Padding = new Thickness(0, 0, 0, 2),
    Child = face,
    HorizontalAlignment = HorizontalAlignment.Left
};

var caption = KitUi.Muted("The 2px base is the pressed depth.", 10.5);
caption.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, basePlate, caption);
