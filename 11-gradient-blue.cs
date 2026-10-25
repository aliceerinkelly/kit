// Gradient blue: #4299e1 to #3182ce on the diagonal, the same pair the PDF names.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var face = new LinearGradientBrush(
    (Color)ColorConverter.ConvertFromString("#4299e1"),
    (Color)ColorConverter.ConvertFromString("#3182ce"),
    45.0);

var deeper = new LinearGradientBrush(
    (Color)ColorConverter.ConvertFromString("#3182ce"),
    (Color)ColorConverter.ConvertFromString("#2c5282"),
    45.0);

var upload = new FlatButton
{
    Content = "Upload build",
    Height = 36,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 12.5,
    FontWeight = FontWeights.SemiBold,
    Radius = new CornerRadius(6),
    Fill = face,
    Edge = Hex("#3182ce"),
    Ink = Brushes.White,
    HoverFill = deeper,
    HoverEdge = Hex("#2c5282"),
    HoverInk = Brushes.White
};

upload.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 12,
    ShadowDepth = 3,
    Opacity = 0.3,
    Color = (Color)ColorConverter.ConvertFromString("#3182ce")
};

var caption = KitUi.Muted("The gradient goes down-right, not top-down.", 10.5);
caption.Margin = new Thickness(0, 10, 0, 0);

return KitUi.Col(0, upload, caption);
