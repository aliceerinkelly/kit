// Outlined success: green edge, transparent fill. The ink is the brighter green so it
// still reads against a dark surface; the PDF's #2f855a is a light-mode colour.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var apply = new FlatButton
{
    Content = "Apply preset",
    Height = 34,
    Padding = new Thickness(14, 0, 14, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Brushes.Transparent,
    Edge = Hex("#38a169"),
    Ink = Hex("#48bb78"),
    HoverFill = Hex("#1F38A169"),
    HoverEdge = Hex("#48bb78"),
    HoverInk = Brushes.White
};

var hint = KitUi.Muted("border 1.5  \u00B7  radius 6  \u00B7  fills on hover", 10.5);
hint.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, apply, hint);
