// Tabbed inspector: Design, Properties, Raw - the live tab carries the accent rule.
var tabs = new StackPanel { Orientation = Orientation.Horizontal };
var names = new[] { "Design", "Properties", "Raw code" };

for (var i = 0; i < names.Length; i++)
{
    var active = i == 1;
    var label = KitUi.Text(names[i], 11.5, active ? "Kit.TextBrush" : "Kit.MutedBrush");
    label.HorizontalAlignment = HorizontalAlignment.Center;
    label.Margin = new Thickness(0, 0, 0, 7);

    var underline = new Border { Height = 2, Background = Brushes.Transparent };

    if (active)
    {
        underline.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
    }

    var cell = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
    cell.Children.Add(label);
    cell.Children.Add(underline);
    tabs.Children.Add(cell);
}

var body = new StackPanel { Margin = new Thickness(0, 12, 0, 0) };

string[] keys = ["Width", "Height", "Radius", "Fill"];

foreach (var key in keys)
{
    var grid = new Grid { Margin = new Thickness(0, 3, 0, 3) };
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });
    grid.ColumnDefinitions.Add(new ColumnDefinition());

    var label = KitUi.Mono(key, 11, "Kit.MutedBrush");
    label.VerticalAlignment = VerticalAlignment.Center;

    var value = KitUi.Mono(key == "Fill" ? "#FF2E88" : "32", 11);

    Grid.SetColumn(value, 1);
    grid.Children.Add(label);
    grid.Children.Add(value);
    body.Children.Add(grid);
}

var card = KitUi.Surface(KitUi.Col(0, tabs, body), 14, 8, "Kit.PanelBrush");
card.Width = 320;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
