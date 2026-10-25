// Toggle: 48 by 24 track, 20px thumb, and the thumb travels instead of fading.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

Border Toggle(bool on, string label)
{
    var thumb = new Border
    {
        Width = 20,
        Height = 20,
        CornerRadius = new CornerRadius(10),
        Background = Brushes.White,
        HorizontalAlignment = on ? HorizontalAlignment.Right : HorizontalAlignment.Left,
        Margin = new Thickness(2)
    };

    var track = new Border
    {
        Width = 48,
        Height = 24,
        CornerRadius = new CornerRadius(12),
        Background = on ? Hex("#38a169") : Hex("#cbd5e0"),
        Child = thumb,
        VerticalAlignment = VerticalAlignment.Center
    };

    var text = KitUi.Text(label, 12);
    text.VerticalAlignment = VerticalAlignment.Center;
    text.Margin = new Thickness(10, 0, 0, 0);

    var row = new StackPanel { Orientation = Orientation.Horizontal };
    row.Children.Add(track);
    row.Children.Add(text);
    return new Border { Child = row };
}

var card = KitUi.Surface(
    KitUi.Col(
        14,
        Toggle(true, "Hardware acceleration"),
        Toggle(false, "Write protection")),
    16,
    8,
    "Kit.PanelBrush",
    "Kit.BorderBrush");

card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
