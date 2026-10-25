// Text underline: a link that happens to be a button. The line belongs to the text,
// which is where WPF keeps text decoration - not on the Button itself.
Button Link(string text, string brush)
{
    var label = new TextBlock
    {
        Text = text,
        FontSize = 12.5,
        TextDecorations = TextDecorations.Underline
    };

    label.SetResourceReference(TextBlock.ForegroundProperty, brush);

    return new FlatButton
    {
        Content = label,
        Height = 26,
        Padding = new Thickness(4, 0, 4, 0),
        Radius = new CornerRadius(3),
        Fill = Brushes.Transparent,
        Edge = Brushes.Transparent,
        HoverFill = Brushes.Transparent,
        HoverEdge = Brushes.Transparent
    };
}

var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
row.Children.Add(Link("View the log", "Kit.AccentBrush"));
row.Children.Add(new Border { Width = 14 });
row.Children.Add(Link("Clear filters", "Kit.MutedBrush"));
return row;
