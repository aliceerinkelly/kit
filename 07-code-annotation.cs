// Code annotation: a numbered badge on the code line, the sentence that explains it.
var notes = new (string Code, string Note)[]
{
    ("var rows = KitUi.Col(8,", "Eight between rows - the same gap everywhere in KIT."),
    ("    KitUi.Head(\"Sizes\"),", "Head is the only helper that sets a weight."),
    ("    KitUi.Muted(\"20 / 12 / 9.5\"));", "Muted never drops below 10.5 for readability.")
};

var body = new StackPanel { Width = 470 };

for (var i = 0; i < notes.Length; i++)
{
    var (code, note) = notes[i];

    var badge = new Border
    {
        Width = 18,
        Height = 18,
        CornerRadius = new CornerRadius(9),
        Background = Brushes.Transparent,
        BorderThickness = new Thickness(1),
        Margin = new Thickness(0, 0, 10, 0),
        VerticalAlignment = VerticalAlignment.Center,
        Child = new TextBlock
        {
            Text = (i + 1).ToString(),
            FontSize = 9.5,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        }
    };

    badge.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");
    ((TextBlock)badge.Child).SetResourceReference(TextBlock.ForegroundProperty, "Kit.AccentBrush");

    var line = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 4) };
    line.Children.Add(badge);
    line.Children.Add(KitUi.Mono(code, 11.5, "Kit.TextBrush"));
    body.Children.Add(line);

    var explanation = KitUi.Muted(note, 10.5);
    explanation.Margin = new Thickness(28, 0, 0, 10);
    body.Children.Add(explanation);
}

var card = KitUi.Surface(body, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
