// Status footer: readiness on the left, the build and the pending change on the right.
var ready = KitUi.Dot(7, "Kit.AccentBrush");

var state = KitUi.Text("Ready", 10.5);
state.Margin = new Thickness(7, 0, 0, 0);
state.VerticalAlignment = VerticalAlignment.Center;

var left = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center,
    Margin = new Thickness(12, 0, 0, 0)
};

left.Children.Add(ready);
left.Children.Add(state);
var build = KitUi.Muted("\u00B7  v1.5.0  \u00B7  net8.0", 10);
build.Margin = new Thickness(8, 0, 0, 0);
build.VerticalAlignment = VerticalAlignment.Center;
left.Children.Add(build);

var pending = KitUi.Text("1 change pending", 10.5, "Kit.AccentBrush");
pending.VerticalAlignment = VerticalAlignment.Center;
pending.Margin = new Thickness(0, 0, 14, 0);

var stamp = KitUi.Mono("build 04:12", 10, "Kit.MutedBrush");
stamp.VerticalAlignment = VerticalAlignment.Center;
stamp.Margin = new Thickness(0, 0, 14, 0);

var right = new StackPanel
{
    Orientation = Orientation.Horizontal,
    HorizontalAlignment = HorizontalAlignment.Right,
    VerticalAlignment = VerticalAlignment.Center,
    Margin = new Thickness(0, 0, 12, 0)
};

right.Children.Add(pending);
right.Children.Add(stamp);

var grid = new Grid { Height = 28 };
grid.ColumnDefinitions.Add(new ColumnDefinition());
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
grid.Children.Add(left);
Grid.SetColumn(right, 1);
grid.Children.Add(right);

var bar = new Border { Width = 520, Child = grid, HorizontalAlignment = HorizontalAlignment.Left, BorderThickness = new Thickness(1) };
bar.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
bar.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
return bar;
