// Tab strip: file tabs, the live one carrying a bottom accent line and a close badge.
var tabs = new StackPanel { Orientation = Orientation.Horizontal };
var names = new[] { "ServerMonitor.cs", "Theme.xaml", "Program.cs" };

for (var i = 0; i < names.Length; i++)
{
    var active = i == 0;

    var label = KitUi.Text(names[i], 11.5, active ? "Kit.TextBrush" : "Kit.MutedBrush");
    label.Margin = new Thickness(0, 0, 9, 0);

    var row = new StackPanel
    {
        Orientation = Orientation.Horizontal,
        VerticalAlignment = VerticalAlignment.Center
    };

    row.Children.Add(label);

    if (active)
    {
        row.Children.Add(KitUi.Quiet("\u00D7"));
    }

    var cell = new Border
    {
        Padding = new Thickness(12, 8, 8, 8),
        Background = Brushes.Transparent,
        Child = row
    };

    if (active)
    {
        cell.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
    }

    var underline = new Border { Height = 2 };
    underline.SetResourceReference(Border.BackgroundProperty, active ? "Kit.AccentBrush" : "Kit.PanelBrush");

    var column = new StackPanel();
    column.Children.Add(cell);
    column.Children.Add(underline);
    tabs.Children.Add(column);
}

var strip = KitUi.Surface(tabs, pad: 0, radius: 8, fill: "Kit.PanelBrush");
strip.HorizontalAlignment = HorizontalAlignment.Left;
return strip;
