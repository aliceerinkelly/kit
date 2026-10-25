// Numeric stepper: a minus, the value, a plus, all inside one outlined control.
var minus = KitUi.Quiet("\u2212", 26, 28);

var value = KitUi.Mono("10", 13);
value.Width = 46;
value.TextAlignment = TextAlignment.Center;
value.VerticalAlignment = VerticalAlignment.Center;

var plus = KitUi.Quiet("+", 26, 28);

var row = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center
};

row.Children.Add(minus);
row.Children.Add(new Border { Width = 2 });
row.Children.Add(value);
row.Children.Add(new Border { Width = 2 });
row.Children.Add(plus);

var stepper = KitUi.Surface(row, 3, 8, "Kit.FieldBrush", "Kit.BorderBrush");
stepper.Width = 150;
stepper.HorizontalAlignment = HorizontalAlignment.Left;

var label = KitUi.Muted("RETRIES", 10);
label.Margin = new Thickness(2, 0, 0, 5);

return KitUi.Col(0, label, stepper);
