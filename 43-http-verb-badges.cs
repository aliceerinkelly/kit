// HTTP verbs: each method keeps its own colour, the route stays mono and quiet.
Border Verb(string verb, Color tint)
{
    return new Border
    {
        Width = 54,
        CornerRadius = new CornerRadius(4),
        Padding = new Thickness(6, 2, 6, 2),
        Margin = new Thickness(0, 0, 10, 0),
        Background = new SolidColorBrush(Color.FromArgb(28, tint.R, tint.G, tint.B)),
        Child = new TextBlock
        {
            Text = verb,
            FontSize = 10,
            FontWeight = FontWeights.Bold,
            Foreground = new SolidColorBrush(tint),
            HorizontalAlignment = HorizontalAlignment.Center
        }
    };
}

var list = new StackPanel { Width = 440 };

var routes = new (string Verb, Color Tint, string Path, string Note)[]
{
    ("GET", Color.FromRgb(0x3B, 0x82, 0xF6), "/v1/devices", "list attached disks"),
    ("POST", Color.FromRgb(0x10, 0xB9, 0x81), "/v1/packages", "stage an MSIX build"),
    ("PUT", Color.FromRgb(0xF5, 0x9E, 0x0B), "/v1/sessions/{id}", "replace a session"),
    ("DEL", Color.FromRgb(0xEF, 0x44, 0x44), "/v1/sessions/{id}", "drop the session")
};

foreach (var (verb, tint, path, note) in routes)
{
    var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
    row.Children.Add(Verb(verb, tint));

    var route = KitUi.Mono(path, 11.5);
    route.VerticalAlignment = VerticalAlignment.Center;
    route.Width = 176;
    row.Children.Add(route);

    var caption = KitUi.Muted(note, 10.5);
    caption.VerticalAlignment = VerticalAlignment.Center;
    row.Children.Add(caption);

    var line = new Border { Padding = new Thickness(0, 7, 0, 7), Child = row };
    list.Children.Add(line);
    list.Children.Add(KitUi.Rule());
}

var card = KitUi.Surface(list, 13, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
