// Gradient purple: #9f7aea into #805ad5, with the same diagonal as the blue pair.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var face = new LinearGradientBrush(
    (Color)ColorConverter.ConvertFromString("#9f7aea"),
    (Color)ColorConverter.ConvertFromString("#805ad5"),
    45.0);

var deeper = new LinearGradientBrush(
    (Color)ColorConverter.ConvertFromString("#805ad5"),
    (Color)ColorConverter.ConvertFromString("#553c9a"),
    45.0);

var upgrade = new FlatButton
{
    Content = "Upgrade workspace",
    Height = 36,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 12.5,
    FontWeight = FontWeights.SemiBold,
    Radius = new CornerRadius(6),
    Fill = face,
    Edge = Hex("#805ad5"),
    Ink = Brushes.White,
    HoverFill = deeper,
    HoverEdge = Hex("#6b46c1"),
    HoverInk = Brushes.White
};

var caption = KitUi.Muted("Purple is the quiet cousin of the neon styles.", 10.5);
caption.Margin = new Thickness(0, 10, 0, 0);

return KitUi.Col(0, upgrade, caption);
