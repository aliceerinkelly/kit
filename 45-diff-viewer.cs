// Diff viewer: removed lines left, added lines right, tinted rather than boxed.
StackPanel Pane(string heading, string[] lines, Color tint, string marker)
{
    var body = new StackPanel();

    foreach (var line in lines)
    {
        var text = new TextBlock
        {
            Text = marker + " " + line,
            FontFamily = new FontFamily("Consolas"),
            FontSize = 10.5,
            Foreground = new SolidColorBrush(tint),
            Margin = new Thickness(0, 1, 0, 1)
        };

        body.Children.Add(text);
    }

    var host = new Border
    {
        Background = new SolidColorBrush(Color.FromArgb(20, tint.R, tint.G, tint.B)),
        Padding = new Thickness(9, 7, 9, 7),
        Child = body
    };

    var pane = new StackPanel();
    var header = KitUi.Mono(heading, 9.5, "Kit.MutedBrush");
    header.Margin = new Thickness(0, 0, 0, 6);
    pane.Children.Add(header);
    pane.Children.Add(host);
    return pane;
}

var left = Pane(
    "v1.4.9",
    ["var w = 160;", "Radius = 8;", "Text = \"Save\";"],
    Color.FromRgb(0xEF, 0x44, 0x44),
    "\u2212");

var right = Pane(
    "v1.5.0",
    ["var w = 200;", "Radius = 10;", "Text = \"Save & close\";"],
    Color.FromRgb(0x10, 0xB9, 0x81),
    "+");

var grid = new Grid { Width = 440 };
grid.ColumnDefinitions.Add(new ColumnDefinition());
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8) });
grid.ColumnDefinitions.Add(new ColumnDefinition());

Grid.SetColumn(left, 0);
Grid.SetColumn(right, 2);
grid.Children.Add(left);
grid.Children.Add(right);

var card = KitUi.Surface(grid, 12, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
