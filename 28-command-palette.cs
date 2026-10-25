// Command palette: a search line and a keyboard-navigable list, one row already lit.
var search = KitUi.Input("run", 30, 12);
search.Margin = new Thickness(0, 0, 0, 8);

var rows = new StackPanel();
var commands = new (string Name, string Key, bool Active)[]
{
    ("Run the snippet", "Ctrl+Enter", true),
    ("Add a design", "Ctrl+N", false),
    ("Open a file", "Ctrl+O", false),
    ("Copy this code", "Ctrl+Shift+C", false)
};

foreach (var (name, key, active) in commands)
{
    var label = KitUi.Text(name, 12, active ? "Kit.TextBrush" : "Kit.MutedBrush");
    label.VerticalAlignment = VerticalAlignment.Center;

    var cap = KitUi.Keycap(key);
    cap.HorizontalAlignment = HorizontalAlignment.Right;

    var line = new Grid();
    line.ColumnDefinitions.Add(new ColumnDefinition());
    line.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
    line.Children.Add(label);
    Grid.SetColumn(cap, 1);
    line.Children.Add(cap);

    var row = new Border { Padding = new Thickness(10, 7, 10, 7), CornerRadius = new CornerRadius(5), Child = line };

    if (active)
    {
        row.SetResourceReference(Border.BackgroundProperty, "Kit.AccentSoftBrush");
    }

    rows.Children.Add(row);
}

var card = KitUi.Surface(KitUi.Col(0, search, rows), 10, 10, "Kit.CardBrush", "Kit.BorderBrush");
card.Width = 460;
card.HorizontalAlignment = HorizontalAlignment.Left;
card.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 24,
    ShadowDepth = 6,
    Opacity = 0.3,
    Color = Colors.Black
};

return card;
