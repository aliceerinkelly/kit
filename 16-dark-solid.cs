// Dark solid: a slate face that belongs on either surface, white ink throughout.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var commit = new FlatButton
{
    Content = "Commit changes",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Hex("#2d3748"),
    Edge = Hex("#2d3748"),
    Ink = Brushes.White,
    HoverFill = Hex("#1a202c"),
    HoverEdge = Hex("#4a5568"),
    HoverInk = Brushes.White
};

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(commit);

var ghost = new FlatButton
{
    Content = "Stash",
    Height = 34,
    Padding = new Thickness(16, 0, 16, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Brushes.Transparent,
    Edge = Hex("#4a5568"),
    Ink = Hex("#cbd5e0"),
    HoverFill = Hex("#2d3748"),
    HoverEdge = Hex("#718096"),
    HoverInk = Brushes.White,
    Margin = new Thickness(10, 0, 0, 0)
};

row.Children.Add(ghost);
return row;
