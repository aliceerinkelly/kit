// Timeline feed: one hairline down the left, an accent dot per event.
var events = new (string When, string What, bool Live)[]
{
    ("09:12", "v1.5.0 tagged and built", true),
    ("09:04", "Snippet catalog compiled clean", false),
    ("08:47", "Rail gained seven categories", false),
    ("08:20", "FlatButton padding fixed", false)
};

var feed = new StackPanel { Width = 420 };

for (var i = 0; i < events.Length; i++)
{
    var (when, what, live) = events[i];

    var dot = new Border
    {
        Width = 9,
        Height = 9,
        CornerRadius = new CornerRadius(5),
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0, 4, 0, 0),
        BorderThickness = new Thickness(2)
    };

    dot.SetResourceReference(Border.BackgroundProperty, live ? "Kit.AccentBrush" : "Kit.CardBrush");
    dot.SetResourceReference(Border.BorderBrushProperty, live ? "Kit.AccentBrush" : "Kit.BorderBrush");

    var line = new Border { Width = 1, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(4, 0, 0, 0) };
    line.SetResourceReference(Border.BackgroundProperty, "Kit.BorderBrush");

    var text = new StackPanel { Margin = new Thickness(14, 0, 0, 0) };
    text.Children.Add(KitUi.Text(what, 12, live ? "Kit.TextBrush" : "Kit.MutedBrush"));

    var stamp = KitUi.Mono(when, 10, "Kit.MutedBrush");
    stamp.Margin = new Thickness(0, 2, 0, 0);
    text.Children.Add(stamp);

    var cell = new Grid { Margin = new Thickness(0, 0, 0, i == events.Length - 1 ? 0 : 10) };
    cell.Children.Add(line);
    cell.Children.Add(dot);
    cell.Children.Add(text);
    feed.Children.Add(cell);
}

var card = KitUi.Surface(feed, 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
