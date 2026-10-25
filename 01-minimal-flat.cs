// Minimal flat: no fill, no border, the label does the work. The ink is the theme's
// own rather than the PDF's fixed #2d3748, so it reads in light and dark.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

FlatButton Flat(string text, bool accent)
{
    var button = new FlatButton
    {
        Content = text,
        Height = 32,
        Padding = new Thickness(10, 0, 10, 0),
        FontSize = 12.5,
        Radius = new CornerRadius(6),
        Fill = Brushes.Transparent,
        Ink = Brushes.Transparent,
        Edge = Brushes.Transparent,
        HoverFill = Brushes.Transparent
    };

    button.SetResourceReference(FlatButton.InkProperty, accent ? "Kit.AccentBrush" : "Kit.TextBrush");
    button.SetResourceReference(FlatButton.HoverInkProperty, "Kit.AccentBrush");
    button.SetResourceReference(FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
    return button;
}

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(Flat("Save changes", false));
row.Children.Add(new Border { Width = 10 });
row.Children.Add(Flat("Dismiss", true));
return row;
