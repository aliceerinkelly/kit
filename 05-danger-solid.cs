// Danger solid: the one style that should never be quiet about what it does.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var delete = new FlatButton
{
    Content = "Delete partition",
    Height = 34,
    Padding = new Thickness(14, 0, 14, 0),
    FontSize = 12.5,
    FontWeight = FontWeights.SemiBold,
    Radius = new CornerRadius(6),
    Fill = Hex("#e53e3e"),
    Edge = Hex("#e53e3e"),
    Ink = Brushes.White,
    HoverFill = Hex("#c53030"),
    HoverEdge = Hex("#c53030"),
    HoverInk = Brushes.White
};

var note = KitUi.Muted("Hover deepens the red; it never turns soft.", 10.5);
note.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, delete, note);
