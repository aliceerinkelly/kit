// Pill: the PDF asks for radius 999; WPF takes half the height, which is a pill at
// every size instead of only at one.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var pill = new FlatButton
{
    Content = "Continue",
    Height = 38,
    Padding = new Thickness(24, 0, 24, 0),
    FontSize = 12.5,
    FontWeight = FontWeights.SemiBold,
    Radius = new CornerRadius(19),
    Fill = Hex("#805ad5"),
    Edge = Hex("#805ad5"),
    Ink = Brushes.White,
    HoverFill = Hex("#6b46c1"),
    HoverEdge = Hex("#6b46c1"),
    HoverInk = Brushes.White
};

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(pill);

var outline = new FlatButton
{
    Content = "Skip",
    Height = 38,
    Padding = new Thickness(22, 0, 22, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(19),
    Fill = Brushes.Transparent,
    Edge = Hex("#805ad5"),
    Ink = Hex("#9f7aea"),
    HoverFill = Hex("#805ad5"),
    HoverEdge = Hex("#805ad5"),
    HoverInk = Brushes.White,
    Margin = new Thickness(10, 0, 0, 0)
};

row.Children.Add(outline);
return row;
