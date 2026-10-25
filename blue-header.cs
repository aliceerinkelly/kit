// Blue capsule: title left, then the glass pill, the theme dots and the mode pill.
Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var grid = new Grid { Width = 556 };
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

var title = new StackPanel { VerticalAlignment = VerticalAlignment.Center };

title.Children.Add(new TextBlock
{
    Text = "DISKTOOL",
    FontSize = 21,
    FontWeight = FontWeights.Bold,
    Foreground = C("#F2F7FF")
});

title.Children.Add(new TextBlock
{
    Text = "v2.2.0 · partition suite",
    FontSize = 11,
    Foreground = C("#8FAFD2")
});

var right = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center
};

var read = new FlatButton
{
    Content = "Read all devices",
    Height = 34,
    Padding = new Thickness(18, 0, 18, 0),
    FontSize = 12.5,
    FontWeight = FontWeights.SemiBold,
    Radius = new CornerRadius(17),
    Fill = new LinearGradientBrush(
        Color.FromRgb(0xE4, 0xEF, 0xFD),
        Color.FromRgb(0x9C, 0xBB, 0xE2),
        90.0),
    Ink = C("#0E1C2E"),
    Edge = C("#D6E6FA"),
    HoverFill = new LinearGradientBrush(
        Color.FromRgb(0xF6, 0xFA, 0xFF),
        Color.FromRgb(0xB8, 0xD2, 0xF2),
        90.0),
    HoverInk = C("#0E1C2E"),
    HoverEdge = C("#FFFFFF"),
    Margin = new Thickness(0, 0, 14, 0)
};

var dots = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center,
    Margin = new Thickness(0, 0, 14, 0)
};

var swatches = new[]
{
    ("#45E0A0", false),
    ("#FF5C7A", false),
    ("#4F7BFF", true),
    ("#B18CFF", false),
    ("#FFC94D", false)
};

foreach (var (hex, selected) in swatches)
{
    dots.Children.Add(new System.Windows.Shapes.Ellipse
    {
        Width = 15,
        Height = 15,
        Fill = C(hex),
        Stroke = selected ? C("#FFFFFF") : Brushes.Transparent,
        StrokeThickness = selected ? 2.5 : 0,
        Margin = new Thickness(4, 0, 4, 0)
    });
}

var mode = new FlatButton
{
    Content = "Light mode",
    Width = 128,
    Height = 34,
    FontSize = 12.5,
    Radius = new CornerRadius(17),
    Fill = C("#1B3355"),
    Ink = C("#EAF3FF"),
    Edge = C("#D7E6F7"),
    HoverFill = C("#25446D"),
    HoverInk = C("#FFFFFF"),
    HoverEdge = C("#FFFFFF")
};

right.Children.Add(read);
right.Children.Add(dots);
right.Children.Add(mode);

Grid.SetColumn(right, 1);
grid.Children.Add(title);
grid.Children.Add(right);

// The strip is the app's own navy, so the white title survives a light host.
return new Border
{
    Background = C("#101E33"),
    CornerRadius = new CornerRadius(12),
    Padding = new Thickness(16, 12, 16, 12),
    HorizontalAlignment = HorizontalAlignment.Left,
    Child = grid
};
