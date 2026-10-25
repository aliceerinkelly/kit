// Step tracker: numbered nodes for done, current and pending, joined by accent rules.
var steps = new[] { "Plan", "Build", "Review", "Ship" };
var track = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Top };

for (var i = 0; i < steps.Length; i++)
{
    var done = i == 0;
    var active = i == 1;

    var number = KitUi.Text((i + 1).ToString(), 11);
    number.HorizontalAlignment = HorizontalAlignment.Center;
    number.VerticalAlignment = VerticalAlignment.Center;
    number.FontWeight = FontWeights.Bold;

    var node = new Border
    {
        Width = 24,
        Height = 24,
        CornerRadius = new CornerRadius(12),
        BorderThickness = new Thickness(1),
        Child = number
    };

    if (done)
    {
        node.SetResourceReference(Border.BackgroundProperty, "Kit.AccentSoftBrush");
        node.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");
        number.SetResourceReference(TextBlock.ForegroundProperty, "Kit.AccentInkBrush");
    }
    else if (active)
    {
        node.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
        node.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");
        number.SetResourceReference(TextBlock.ForegroundProperty, "Kit.OnAccentBrush");
    }
    else
    {
        node.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
        node.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
        number.SetResourceReference(TextBlock.ForegroundProperty, "Kit.MutedBrush");
    }

    var label = KitUi.Text(steps[i], 10.5, active || done ? "Kit.TextBrush" : "Kit.MutedBrush");
    label.HorizontalAlignment = HorizontalAlignment.Center;
    label.Margin = new Thickness(0, 6, 0, 0);

    var step = new StackPanel { Margin = new Thickness(0, 0, 10, 0) };
    step.Children.Add(node);
    step.Children.Add(label);
    track.Children.Add(step);

    if (i < steps.Length - 1)
    {
        var rule = new Border
        {
            Height = 2,
            Width = 34,
            Margin = new Thickness(0, 11, 10, 0),
            VerticalAlignment = VerticalAlignment.Top
        };

        rule.SetResourceReference(Border.BackgroundProperty, done ? "Kit.AccentBrush" : "Kit.BorderBrush");
        track.Children.Add(rule);
    }
}

var card = KitUi.Surface(track, 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
