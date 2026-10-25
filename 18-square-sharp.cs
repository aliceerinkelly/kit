// Square sharp: no radius at all. At this corner it has to look deliberate, not broken.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var run = new FlatButton
{
    Content = "RUN",
    Height = 34,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 11.5,
    FontWeight = FontWeights.Bold,
    Radius = new CornerRadius(0),
    Fill = Hex("#2f855a"),
    Edge = Hex("#2f855a"),
    Ink = Brushes.White,
    HoverFill = Hex("#276749"),
    HoverEdge = Hex("#276749"),
    HoverInk = Brushes.White
};

var stop = new FlatButton
{
    Content = "STOP",
    Height = 34,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 11.5,
    FontWeight = FontWeights.Bold,
    Radius = new CornerRadius(0),
    Fill = Hex("#2d3748"),
    Edge = Hex("#2d3748"),
    Ink = Brushes.White,
    HoverFill = Hex("#1a202c"),
    HoverEdge = Hex("#1a202c"),
    HoverInk = Brushes.White,
    Margin = new Thickness(8, 0, 0, 0)
};

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(run);
row.Children.Add(stop);

var caption = KitUi.Muted("Radius 0 reads as a command, not a choice.", 10.5);
caption.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, row, caption);
