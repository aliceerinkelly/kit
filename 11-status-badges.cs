// Status badges: semantic pills with a soft fill, a dot and bold small caps.
Border Badge(string text, Color tint)
{
    var ink = new SolidColorBrush(tint);

    var dot = new Border
    {
        Width = 6,
        Height = 6,
        CornerRadius = new CornerRadius(3),
        Background = ink,
        Margin = new Thickness(0, 0, 6, 0),
        VerticalAlignment = VerticalAlignment.Center
    };

    var label = new TextBlock
    {
        Text = text,
        FontSize = 10,
        FontWeight = FontWeights.Bold,
        Foreground = ink
    };

    var content = new StackPanel { Orientation = Orientation.Horizontal };
    content.Children.Add(dot);
    content.Children.Add(label);

    return new Border
    {
        Background = new SolidColorBrush(Color.FromArgb(34, tint.R, tint.G, tint.B)),
        CornerRadius = new CornerRadius(4),
        Padding = new Thickness(8, 3, 8, 3),
        Margin = new Thickness(0, 0, 8, 0),
        Child = content
    };
}

var row = new StackPanel { Orientation = Orientation.Horizontal };
row.Children.Add(Badge("ONLINE", Color.FromRgb(0x10, 0xB9, 0x81)));
row.Children.Add(Badge("DEGRADED", Color.FromRgb(0xF5, 0x9E, 0x0B)));
row.Children.Add(Badge("CRITICAL", Color.FromRgb(0xEF, 0x44, 0x44)));

var caption = KitUi.Muted("Six pools reporting \u00B7 last poll 2s ago", 11);
caption.Margin = new Thickness(0, 10, 0, 0);

var card = KitUi.Surface(KitUi.Col(0, row, caption), 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
