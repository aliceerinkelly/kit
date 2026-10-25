// Neon green: the outline carries the colour and the glow does the rest.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var neon = new FlatButton
{
    Content = "ARM WRITES",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 11.5,
    FontWeight = FontWeights.Bold,
    Radius = new CornerRadius(6),
    Fill = Hex("#1448BB78"),
    Edge = Hex("#48bb78"),
    Ink = Hex("#48bb78"),
    HoverFill = Hex("#3348BB78"),
    HoverEdge = Hex("#68d391"),
    HoverInk = Hex("#9ae6b4")
};

neon.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 16,
    ShadowDepth = 0,
    Opacity = 0.55,
    Color = (Color)ColorConverter.ConvertFromString("#48bb78")
};

var armed = KitUi.Muted("armed  \u00B7  writes pass through", 10.5);
armed.Margin = new Thickness(0, 10, 0, 0);

return KitUi.Col(0, neon, armed);
