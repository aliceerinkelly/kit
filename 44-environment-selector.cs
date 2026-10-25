// Environment selector: the closed control, then the list it opens with status dots.
var selectedDot = new Border
{
    Width = 8,
    Height = 8,
    CornerRadius = new CornerRadius(4),
    Background = new SolidColorBrush(Color.FromRgb(0xEF, 0x44, 0x44)),
    Margin = new Thickness(0, 0, 8, 0),
    VerticalAlignment = VerticalAlignment.Center
};

var selected = KitUi.Text("Production \u00B7 US-East", 11.5);
selected.VerticalAlignment = VerticalAlignment.Center;

var chevron = KitUi.Muted("\u25BE", 10);
chevron.Margin = new Thickness(10, 0, 0, 0);
chevron.VerticalAlignment = VerticalAlignment.Center;

var closedRow = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
closedRow.Children.Add(selectedDot);
closedRow.Children.Add(selected);
closedRow.Children.Add(chevron);

var closed = KitUi.Surface(closedRow, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
closed.Width = 250;
closed.HorizontalAlignment = HorizontalAlignment.Left;

var menu = new StackPanel { Margin = new Thickness(0, 6, 0, 0) };

var options = new (string Name, Color Tint, bool Live)[]
{
    ("Production \u00B7 US-East", Color.FromRgb(0xEF, 0x44, 0x44), true),
    ("Staging \u00B7 EU-West", Color.FromRgb(0xF5, 0x9E, 0x0B), false),
    ("Development \u00B7 local", Color.FromRgb(0x10, 0xB9, 0x81), false)
};

foreach (var (name, tint, live) in options)
{
    var dot = new Border
    {
        Width = 8,
        Height = 8,
        CornerRadius = new CornerRadius(4),
        Background = new SolidColorBrush(tint),
        Margin = new Thickness(0, 0, 9, 0),
        VerticalAlignment = VerticalAlignment.Center
    };

    var label = KitUi.Text(name, 11.5, live ? "Kit.TextBrush" : "Kit.MutedBrush");
    label.VerticalAlignment = VerticalAlignment.Center;

    var row = new StackPanel { Orientation = Orientation.Horizontal };
    row.Children.Add(dot);
    row.Children.Add(label);

    var item = new Border { Padding = new Thickness(9, 6, 9, 6), CornerRadius = new CornerRadius(5), Child = row };

    if (live)
    {
        item.SetResourceReference(Border.BackgroundProperty, "Kit.HoverBrush");
    }

    menu.Children.Add(item);
}

var open = KitUi.Surface(menu, 4, 8, "Kit.CardBrush", "Kit.BorderBrush");
open.Width = 250;
open.HorizontalAlignment = HorizontalAlignment.Left;
open.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 16,
    ShadowDepth = 4,
    Opacity = 0.25,
    Color = Colors.Black
};

return KitUi.Col(0, KitUi.Muted("TARGET", 10), closed, open);
