// Range slider: a slim track, an accent fill and a readout on the right.
var track = new Border { Height = 4, CornerRadius = new CornerRadius(2), VerticalAlignment = VerticalAlignment.Center };
track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

var fill = new Border
{
    Width = 132,
    Height = 4,
    CornerRadius = new CornerRadius(2),
    HorizontalAlignment = HorizontalAlignment.Left,
    VerticalAlignment = VerticalAlignment.Center
};
fill.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");

var thumb = new Border
{
    Width = 14,
    Height = 14,
    CornerRadius = new CornerRadius(7),
    HorizontalAlignment = HorizontalAlignment.Left,
    VerticalAlignment = VerticalAlignment.Center,
    Margin = new Thickness(125, 0, 0, 0),
    BorderThickness = new Thickness(2)
};
thumb.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
thumb.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");

var scale = new Grid { Width = 220, Height = 22 };
scale.Children.Add(track);
scale.Children.Add(fill);
scale.Children.Add(thumb);

var value = KitUi.Mono("55", 12);
value.VerticalAlignment = VerticalAlignment.Center;

var row = new StackPanel { Orientation = Orientation.Horizontal };
row.Children.Add(scale);
row.Children.Add(new Border { Width = 12 });
row.Children.Add(value);

var ticks = new StackPanel { Orientation = Orientation.Horizontal, Width = 250, Margin = new Thickness(0, 4, 0, 0) };

foreach (var tick in new[] { "0", "25", "50", "75", "100" })
{
    var label = KitUi.Mono(tick, 9.5, "Kit.MutedBrush");
    label.Width = 50;
    ticks.Children.Add(label);
}

var card = KitUi.Surface(
    KitUi.Col(0, KitUi.Muted("OPACITY", 10), row, ticks),
    14,
    8,
    "Kit.PanelBrush");

card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
