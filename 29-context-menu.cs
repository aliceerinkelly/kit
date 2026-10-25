// Context menu: shortcuts sit right-aligned against the action, separator between groups.
var items = new StackPanel { Width = 210 };
var entries = new (string Name, string Key, bool Dangerous)[]
{
    ("Rename", "F2", false),
    ("Duplicate", "Ctrl+D", false),
    ("Copy code", "Ctrl+C", false),
    ("Delete", "Del", true)
};

var index = 0;

foreach (var (name, key, dangerous) in entries)
{
    if (name == "Delete")
    {
        var rule = KitUi.Rule();
        rule.Margin = new Thickness(0, 4, 0, 4);
        items.Children.Add(rule);
    }

    var label = KitUi.Text(name, 12, dangerous ? "Kit.AccentBrush" : "Kit.TextBrush");
    label.VerticalAlignment = VerticalAlignment.Center;

    var cap = KitUi.Keycap(key);
    cap.HorizontalAlignment = HorizontalAlignment.Right;

    var line = new Grid();
    line.ColumnDefinitions.Add(new ColumnDefinition());
    line.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
    line.Children.Add(label);
    Grid.SetColumn(cap, 1);
    line.Children.Add(cap);

    var row = new Border
    {
        Padding = new Thickness(9, 6, 9, 6),
        CornerRadius = new CornerRadius(4),
        Child = line
    };

    if (index == 0)
    {
        row.SetResourceReference(Border.BackgroundProperty, "Kit.HoverBrush");
    }

    items.Children.Add(row);
    index++;
}

var menu = KitUi.Surface(items, 6, 8, "Kit.CardBrush", "Kit.BorderBrush");
menu.HorizontalAlignment = HorizontalAlignment.Left;
menu.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 18,
    ShadowDepth = 5,
    Opacity = 0.25,
    Color = Colors.Black
};

return menu;
