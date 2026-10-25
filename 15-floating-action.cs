// Floating action: 56 by 56, the one button that is allowed to sit above the page.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var fab = new FlatButton
{
    Content = "+",
    Width = 56,
    Height = 56,
    MinWidth = 0,
    FontSize = 26,
    FontWeight = FontWeights.Light,
    Radius = new CornerRadius(28),
    Fill = Hex("#f56565"),
    Edge = Hex("#f56565"),
    Ink = Brushes.White,
    HoverFill = Hex("#e53e3e"),
    HoverEdge = Hex("#e53e3e"),
    HoverInk = Brushes.White
};

fab.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 20,
    ShadowDepth = 5,
    Opacity = 0.35,
    Color = Colors.Black
};

var stage = new Border
{
    Background = Hex("#12161F"),
    CornerRadius = new CornerRadius(10),
    Padding = new Thickness(18),
    HorizontalAlignment = HorizontalAlignment.Left,
    Child = fab
};

var caption = KitUi.Muted("The shadow is what lifts it off the surface.", 10.5);
caption.Margin = new Thickness(0, 10, 0, 0);

return KitUi.Col(0, stage, caption);
