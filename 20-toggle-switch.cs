// Toggle switch: a sliding indicator, and the sentence that explains what it does.
FrameworkElement Toggle(string title, string detail, bool on)
{
    var track = new Border
    {
        Width = 38,
        Height = 20,
        CornerRadius = new CornerRadius(10),
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(0, 0, 11, 0)
    };

    var knob = new Border
    {
        Width = 14,
        Height = 14,
        CornerRadius = new CornerRadius(7),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = on ? HorizontalAlignment.Right : HorizontalAlignment.Left,
        Margin = new Thickness(3, 0, 3, 0)
    };

    knob.SetResourceReference(Border.BackgroundProperty, on ? "Kit.OnAccentBrush" : "Kit.MutedBrush");

    if (on)
    {
        track.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
    }
    else
    {
        track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
        track.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
        track.BorderThickness = new Thickness(1);
    }

    track.Child = knob;

    var copy = KitUi.Col(2, KitUi.Text(title, 12), KitUi.Muted(detail, 10.5));
    copy.VerticalAlignment = VerticalAlignment.Center;

    var row = new StackPanel { Orientation = Orientation.Horizontal };
    row.Children.Add(track);
    row.Children.Add(copy);
    return row;
}

var card = KitUi.Surface(
    KitUi.Col(
        14,
        Toggle("Hardware acceleration", "Uses the GPU for the live preview", true),
        Toggle("Write protection", "Blocks sector writes on removable disks", false)),
    14,
    8,
    "Kit.PanelBrush");

card.Width = 360;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
