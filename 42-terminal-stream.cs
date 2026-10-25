// Terminal stream: auto-scrolled mono output in a well that stays dark in both modes.
var output = new StackPanel();

void Line(string stamp, string tag, string text, Color ink)
{
    var row = new StackPanel { Orientation = Orientation.Horizontal };

    var time = new TextBlock { Text = stamp, FontSize = 11, Foreground = new SolidColorBrush(Color.FromRgb(0x5A, 0x64, 0x72)), Width = 74 };
    var level = new TextBlock { Text = tag, FontSize = 11, Foreground = new SolidColorBrush(ink), Width = 62 };
    var body = new TextBlock { Text = text, FontSize = 11, Foreground = new SolidColorBrush(Color.FromRgb(0xD5, 0xDB, 0xE3)) };

    time.FontFamily = new FontFamily("Consolas");
    level.FontFamily = new FontFamily("Consolas");
    body.FontFamily = new FontFamily("Consolas");

    row.Children.Add(time);
    row.Children.Add(level);
    row.Children.Add(body);
    output.Children.Add(row);
}

Line("04:11:58", "info", "snippet catalog: 84 designs loaded", Color.FromRgb(0x4B, 0xE0, 0xA6));
Line("04:12:01", "build", "Kit.dll rebuilt in 1.4s", Color.FromRgb(0x4B, 0xE0, 0xA6));
Line("04:12:03", "warn", "This well stays dark in light mode on purpose", Color.FromRgb(0xF5, 0xC4, 0x51));
Line("04:12:04", "done", "ready for input", Color.FromRgb(0x8F, 0x9B, 0xAB));

var prompt = new TextBlock
{
    Text = "> _",
    FontFamily = new FontFamily("Consolas"),
    FontSize = 11,
    Foreground = new SolidColorBrush(Color.FromRgb(0x4B, 0xE0, 0xA6)),
    Margin = new Thickness(74, 6, 0, 0)
};

output.Children.Add(prompt);

var well = new Border
{
    Width = 480,
    HorizontalAlignment = HorizontalAlignment.Left,
    Background = new SolidColorBrush(Color.FromRgb(0x0B, 0x0D, 0x12)),
    BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x33)),
    BorderThickness = new Thickness(1),
    CornerRadius = new CornerRadius(8),
    Padding = new Thickness(12, 10, 12, 12),
    Child = output
};

return well;
