// Keycaps: each key its own box, a plus between them, the action on the left.
FrameworkElement Shortcut(string action, string[] keys)
{
    var label = KitUi.Text(action, 11.5);
    label.Width = 130;
    label.VerticalAlignment = VerticalAlignment.Center;

    var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
    row.Children.Add(label);

    for (var i = 0; i < keys.Length; i++)
    {
        if (i > 0)
        {
            var plus = KitUi.Muted("+", 9.5);
            plus.Margin = new Thickness(5, 0, 5, 0);
            plus.VerticalAlignment = VerticalAlignment.Center;
            row.Children.Add(plus);
        }

        row.Children.Add(KitUi.Keycap(keys[i]));
    }

    return row;
}

var rows = KitUi.Col(
    8,
    Shortcut("Run the snippet", ["Ctrl", "Enter"]),
    Shortcut("Add a design", ["Ctrl", "N"]),
    Shortcut("Open a file", ["Ctrl", "O"]),
    Shortcut("Rename", ["F2"]));

var card = KitUi.Surface(rows, 13, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
