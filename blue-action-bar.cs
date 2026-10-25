// Blue capsule action bar: a quiet scan action and the write-arm toggle, centred.
Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var scanText = new StackPanel { Orientation = Orientation.Horizontal };

scanText.Children.Add(new TextBlock
{
    Text = "⛶",
    FontSize = 12,
    Margin = new Thickness(0, 0, 7, 0),
    VerticalAlignment = VerticalAlignment.Center
});

scanText.Children.Add(new TextBlock
{
    Text = "Scan sectors",
    FontSize = 12,
    VerticalAlignment = VerticalAlignment.Center
});

var armText = new StackPanel { Orientation = Orientation.Horizontal };

armText.Children.Add(new Border
{
    Width = 12,
    Height = 12,
    CornerRadius = new CornerRadius(3),
    Background = C("#0C1A2A"),
    BorderBrush = C("#6FA6DE"),
    BorderThickness = new Thickness(1),
    Margin = new Thickness(0, 0, 8, 0),
    VerticalAlignment = VerticalAlignment.Center
});

armText.Children.Add(new TextBlock
{
    Text = "Arm sector writes",
    FontSize = 12,
    VerticalAlignment = VerticalAlignment.Center
});

var row = new StackPanel
{
    Orientation = Orientation.Horizontal,
    HorizontalAlignment = HorizontalAlignment.Center,
    VerticalAlignment = VerticalAlignment.Center
};

row.Children.Add(new FlatButton
{
    Content = scanText,
    Height = 30,
    Padding = new Thickness(14, 0, 14, 0),
    Radius = new CornerRadius(15),
    Margin = new Thickness(0, 0, 10, 0),
    Fill = C("#1B3355"),
    Ink = C("#9DBBDA"),
    Edge = C("#2F5A85"),
    HoverFill = C("#25446D"),
    HoverInk = C("#EAF3FF"),
    HoverEdge = C("#5C90C8")
});

row.Children.Add(new FlatButton
{
    Content = armText,
    Height = 30,
    Padding = new Thickness(14, 0, 14, 0),
    Radius = new CornerRadius(15),
    Fill = C("#1B3355"),
    Ink = C("#EAF3FF"),
    Edge = C("#2F5A85"),
    HoverFill = C("#25446D"),
    HoverInk = C("#FFFFFF"),
    HoverEdge = C("#5C90C8")
});

return new Border
{
    Width = 600,
    Height = 46,
    HorizontalAlignment = HorizontalAlignment.Left,
    Background = C("#24405F"),
    BorderBrush = C("#2E6FA8"),
    BorderThickness = new Thickness(1),
    CornerRadius = new CornerRadius(10),
    Child = row
};
