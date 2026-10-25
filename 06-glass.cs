// Glass: translucent fill over a dark surface, which is the only place glass reads.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var glass = new FlatButton
{
    Content = "Restore",
    Height = 36,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(8),
    Fill = Hex("#33FFFFFF"),
    Edge = Hex("#66FFFFFF"),
    Ink = Brushes.White,
    HoverFill = Hex("#4DFFFFFF"),
    HoverEdge = Brushes.White,
    HoverInk = Brushes.White
};

glass.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 14,
    ShadowDepth = 2,
    Opacity = 0.35,
    Color = Colors.Black
};

var stage = new Border
{
    Background = new LinearGradientBrush(
        Color.FromRgb(0x12, 0x18, 0x26),
        Color.FromRgb(0x26, 0x12, 0x1E),
        45.0),
    CornerRadius = new CornerRadius(10),
    Padding = new Thickness(20),
    HorizontalAlignment = HorizontalAlignment.Left,
    Child = glass
};

var caption = KitUi.Muted("Glass needs something behind it to be glass.", 10.5);
caption.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, stage, caption);
