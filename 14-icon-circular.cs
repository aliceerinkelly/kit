// Icon circular: 40 by 40, no label, the glyph is the whole button.
Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

FlatButton Circle(string glyph)
{
    return new FlatButton
    {
        Content = glyph,
        Width = 40,
        Height = 40,
        MinWidth = 0,
        FontSize = 15,
        Radius = new CornerRadius(20),
        Fill = Hex("#edf2f7"),
        Edge = Hex("#cbd5e0"),
        Ink = Hex("#2d3748"),
        HoverFill = Hex("#e2e8f0"),
        HoverEdge = Hex("#a0aec0"),
        HoverInk = Hex("#1a202c")
    };
}

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(Circle("\u21BB"));
row.Children.Add(new Border { Width = 10 });
row.Children.Add(Circle("\u2699"));
row.Children.Add(new Border { Width = 10 });
row.Children.Add(Circle("\u2715"));
return row;
