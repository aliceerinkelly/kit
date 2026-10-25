// One pill, three lives: normal, disabled, and filled with the live accent.
var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];

var row = new StackPanel { Orientation = Orientation.Horizontal };

row.Children.Add(new PillButton
{
    Content = "Normal",
    Size = PillSize.Medium,
    Margin = new Thickness(0, 0, 8, 0)
});

row.Children.Add(new PillButton
{
    Content = "Disabled",
    Size = PillSize.Medium,
    IsEnabled = false,
    Margin = new Thickness(0, 0, 8, 0)
});

row.Children.Add(new PillButton
{
    Content = "Accent",
    Size = PillSize.Medium,
    Fill = accent,
    Ink = Brushes.Black,
    Edge = accent,
    HoverFill = Brushes.White,
    HoverInk = Brushes.Black,
    HoverEdge = accent
});

return row;
