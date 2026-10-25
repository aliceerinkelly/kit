// Rounded primary: the PDF's ButtonPrimaryRounded, with a real Border radius instead
// of the CornerRadius setter WPF's Button does not have.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var save = new FlatButton
{
    Content = "Save changes",
    Height = 36,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 12.5,
    FontWeight = FontWeights.SemiBold,
    Radius = new CornerRadius(8),
    Fill = Hex("#3182ce"),
    Edge = Hex("#3182ce"),
    Ink = Brushes.White,
    HoverFill = Hex("#2b6cb0"),
    HoverEdge = Hex("#2b6cb0"),
    HoverInk = Brushes.White
};

var publish = new FlatButton
{
    Content = "Publish",
    Height = 36,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(8),
    IsEnabled = false,
    Fill = Hex("#3182ce"),
    Edge = Hex("#3182ce"),
    Ink = Brushes.White,
    HoverFill = Hex("#3182ce"),
    HoverEdge = Hex("#3182ce"),
    HoverInk = Brushes.White
};

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(save);
row.Children.Add(new Border { Width = 10 });
row.Children.Add(publish);
return row;
