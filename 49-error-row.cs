// Diagnostic row: the code, the sentence, and where to go and fix it.
var code = KitUi.Mono("CS0103", 11, "Kit.TextBrush");
code.Width = 62;
code.VerticalAlignment = VerticalAlignment.Center;

var message = KitUi.Text("The name 'InitializeComponent' does not exist in the current context", 11.5);
message.VerticalAlignment = VerticalAlignment.Center;

var where = KitUi.Mono("MainWindow.xaml.cs:42", 10, "Kit.MutedBrush");
where.VerticalAlignment = VerticalAlignment.Center;
where.Margin = new Thickness(12, 0, 0, 0);

var badge = new Border
{
    CornerRadius = new CornerRadius(4),
    Padding = new Thickness(6, 2, 6, 2),
    Margin = new Thickness(0, 0, 10, 0),
    Background = new SolidColorBrush(Color.FromArgb(30, 0xEF, 0x44, 0x44)),
    VerticalAlignment = VerticalAlignment.Center,
    Child = code
};

var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
row.Children.Add(badge);
row.Children.Add(message);

var grid = new Grid { Width = 500 };
grid.ColumnDefinitions.Add(new ColumnDefinition());
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
grid.Children.Add(row);
Grid.SetColumn(where, 1);
grid.Children.Add(where);

var line = KitUi.Surface(grid, 10, 6, "Kit.PanelBrush", "Kit.BorderBrush");
line.HorizontalAlignment = HorizontalAlignment.Left;

var second = KitUi.Muted("1 error \u00B7 0 warnings \u00B7 last build 04:12", 10.5);
second.Margin = new Thickness(2, 8, 0, 0);

return KitUi.Col(0, line, second);
