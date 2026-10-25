// Banner alert: a status stripe with an icon, the message, and a dismiss action.
var icon = new Border
{
    Width = 20,
    Height = 20,
    CornerRadius = new CornerRadius(10),
    Background = new SolidColorBrush(Color.FromArgb(40, 0xF5, 0x9E, 0x0B)),
    Margin = new Thickness(0, 0, 10, 0),
    VerticalAlignment = VerticalAlignment.Center,
    Child = new TextBlock
    {
        Text = "!",
        FontSize = 11,
        FontWeight = FontWeights.Bold,
        Foreground = new SolidColorBrush(Color.FromRgb(0xF5, 0x9E, 0x0B)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
    }
};

var message = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
message.Children.Add(KitUi.Text("Unsaved workspace changes detected", 12));
message.Children.Add(KitUi.Muted("Three designs are only in this session.", 10.5));

var text = new StackPanel { Orientation = Orientation.Horizontal };
text.Children.Add(icon);
text.Children.Add(message);

var dismiss = KitUi.Quiet("\u00D7", 24, 24);
dismiss.HorizontalAlignment = HorizontalAlignment.Right;

var grid = new Grid();
grid.ColumnDefinitions.Add(new ColumnDefinition());
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
grid.Children.Add(text);
Grid.SetColumn(dismiss, 1);
grid.Children.Add(dismiss);

var banner = new Border
{
    Width = 460,
    HorizontalAlignment = HorizontalAlignment.Left,
    Padding = new Thickness(12, 9, 8, 9),
    CornerRadius = new CornerRadius(6),
    BorderThickness = new Thickness(1),
    Background = new SolidColorBrush(Color.FromArgb(24, 0xF5, 0x9E, 0x0B)),
    BorderBrush = new SolidColorBrush(Color.FromArgb(80, 0xF5, 0x9E, 0x0B)),
    Child = grid
};

return banner;
