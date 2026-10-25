// Disabled: the PDF sets IsEnabled in a style, which freezes the button. Here the
// state is a property of the instance, so the same style can be enabled again.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

FlatButton State(string text, bool enabled)
{
    return new FlatButton
    {
        Content = text,
        Height = 34,
        Padding = new Thickness(14, 0, 14, 0),
        FontSize = 12.5,
        Radius = new CornerRadius(6),
        IsEnabled = enabled,
        Fill = enabled ? Hex("#3182ce") : Hex("#e2e8f0"),
        Edge = enabled ? Hex("#3182ce") : Hex("#e2e8f0"),
        Ink = enabled ? Brushes.White : Hex("#a0aec0"),
        HoverFill = enabled ? Hex("#2b6cb0") : Hex("#e2e8f0"),
        HoverEdge = enabled ? Hex("#2b6cb0") : Hex("#e2e8f0"),
        HoverInk = enabled ? Brushes.White : Hex("#a0aec0")
    };
}

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(State("Commit", true));
row.Children.Add(new Border { Width = 10 });
row.Children.Add(State("Commit", false));

var caption = KitUi.Muted("0.4 opacity is the disabled state, not a colour.", 10.5);
caption.Margin = new Thickness(0, 9, 0, 0);

return KitUi.Col(0, row, caption);
