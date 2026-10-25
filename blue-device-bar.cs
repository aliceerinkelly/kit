// Blue capsule: one outlined capsule holds the disk label and its pills.
Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var pills = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center,
    HorizontalAlignment = HorizontalAlignment.Right
};

var actions = new (string Label, bool Active)[]
{
    ("Locked", false),
    ("GPT", true),
    ("MBR", false),
    ("+ Create", false),
    ("Delete", false),
    ("Format", false),
    ("Unmount", false),
    ("Wipe", false)
};

foreach (var (label, active) in actions)
{
    pills.Children.Add(new FlatButton
    {
        Content = label,
        Height = 28,
        Padding = new Thickness(9, 0, 9, 0),
        FontSize = 10.5,
        FontWeight = FontWeights.SemiBold,
        Radius = new CornerRadius(14),
        Margin = new Thickness(0, 0, 4, 0),
        Fill = active
            ? new LinearGradientBrush(
                Color.FromRgb(0x54, 0x9F, 0xFF),
                Color.FromRgb(0x2A, 0x67, 0xD8),
                90.0)
            : C("#1B3A5C"),
        Ink = active ? Brushes.White : C("#DCEBFF"),
        Edge = active ? C("#8FC2FF") : C("#3E7FC4"),
        HoverFill = active
            ? new LinearGradientBrush(
                Color.FromRgb(0x76, 0xB4, 0xFF),
                Color.FromRgb(0x3A, 0x7B, 0xEE),
                90.0)
            : C("#254B72"),
        HoverInk = Brushes.White,
        HoverEdge = active ? C("#BFDBFF") : C("#6FA6DE")
    });
}

var grid = new Grid();

grid.Children.Add(new TextBlock
{
    Text = "DISK 0  ·  476.9 GB  ·  GPT",
    FontFamily = (FontFamily)Application.Current.Resources["Kit.MonoFont"],
    FontSize = 9.5,
    Foreground = C("#93B4D6"),
    VerticalAlignment = VerticalAlignment.Center,
    Margin = new Thickness(6, 0, 10, 0)
});

grid.Children.Add(pills);

return new Border
{
    Width = 600,
    HorizontalAlignment = HorizontalAlignment.Left,
    Height = 46,
    Background = C("#16283F"),
    BorderBrush = C("#2F6FC0"),
    BorderThickness = new Thickness(1),
    CornerRadius = new CornerRadius(23),
    Padding = new Thickness(8, 0, 5, 0),
    Child = grid
};
