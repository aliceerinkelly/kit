// Outlined gray: a hairline edge, transparent fill. The label uses the theme's text
// ink because #4a5568 disappears on a dark surface.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

var cancel = new FlatButton
{
    Content = "Cancel",
    Height = 34,
    Padding = new Thickness(14, 0, 14, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Brushes.Transparent,
    Edge = Hex("#a0aec0"),
    Ink = Brushes.Transparent,
    HoverFill = Hex("#22A0AEC0"),
    HoverEdge = Hex("#a0aec0"),
    HoverInk = Brushes.Transparent
};

cancel.SetResourceReference(FlatButton.InkProperty, "Kit.TextBrush");
cancel.SetResourceReference(FlatButton.HoverInkProperty, "Kit.TextBrush");

var later = new FlatButton
{
    Content = "Later",
    Height = 34,
    Padding = new Thickness(14, 0, 14, 0),
    FontSize = 12.5,
    Radius = new CornerRadius(6),
    Fill = Brushes.Transparent,
    Edge = Hex("#a0aec088"),
    Ink = Brushes.Transparent,
    HoverFill = Hex("#22A0AEC0"),
    HoverEdge = Hex("#a0aec0"),
    HoverInk = Brushes.Transparent
};

later.SetResourceReference(FlatButton.InkProperty, "Kit.MutedBrush");
later.SetResourceReference(FlatButton.HoverInkProperty, "Kit.TextBrush");

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(cancel);
row.Children.Add(new Border { Width = 10 });
row.Children.Add(later);
return row;
