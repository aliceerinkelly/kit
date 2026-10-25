// Spark bars: the last twelve samples, with the current one called out in accent.
var samples = new double[] { 0.4, 0.55, 0.3, 0.62, 0.48, 0.7, 0.52, 0.8, 0.66, 0.9, 0.72, 0.85 };
var graph = new StackPanel { Orientation = Orientation.Horizontal, Height = 48, VerticalAlignment = VerticalAlignment.Bottom };

for (var i = 0; i < samples.Length; i++)
{
    var bar = new Border
    {
        Width = 9,
        Height = 44 * samples[i],
        CornerRadius = new CornerRadius(2),
        Margin = new Thickness(0, 0, 3, 0),
        VerticalAlignment = VerticalAlignment.Bottom,
        Opacity = i == samples.Length - 1 ? 1.0 : 0.55
    };

    bar.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
    graph.Children.Add(bar);
}

var value = KitUi.Mono("85%", 11);
var name = KitUi.Muted("CPU", 10);

var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
head.Children.Add(name);
var gap = new Border { Width = 8 };
head.Children.Add(gap);
head.Children.Add(value);

var card = KitUi.Surface(KitUi.Col(0, head, graph), 12, 8, "Kit.PanelBrush");
card.Width = 190;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
