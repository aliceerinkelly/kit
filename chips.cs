// Chips: the pill at Tiny, quiet until you hover it.
var muted = (Brush)Application.Current.Resources["Kit.MutedBrush"];
var text = (Brush)Application.Current.Resources["Kit.TextBrush"];
var hover = (Brush)Application.Current.Resources["Kit.HoverBrush"];

var wrap = new WrapPanel { Orientation = Orientation.Horizontal };

var tags = new[] { "usb", "fat32", "iso", "pe", "signed" };

foreach (var tag in tags)
{
    wrap.Children.Add(new PillButton
    {
        Content = tag,
        Size = PillSize.Tiny,
        Margin = new Thickness(0, 0, 6, 6),
        Fill = Brushes.Transparent,
        Ink = muted,
        Edge = muted,
        HoverFill = hover,
        HoverInk = text,
        HoverEdge = text
    });
}

return wrap;
