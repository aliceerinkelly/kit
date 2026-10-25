// Password field: the value stays masked, the reveal toggle lives inside the edge.
var masked = KitUi.Mono("\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022", 14);
masked.VerticalAlignment = VerticalAlignment.Center;
masked.Width = 232;

var reveal = KitUi.Quiet("\u25C9", 24, 26);

var row = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center
};

row.Children.Add(masked);
row.Children.Add(reveal);

var field = KitUi.Surface(row, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
field.Width = 320;

var note = KitUi.Muted("12 characters \u00B7 last changed 6 days ago", 10.5);
note.Margin = new Thickness(2, 6, 0, 0);

var block = KitUi.Col(0, KitUi.Muted("SIGNING KEY", 10), field, note);
block.HorizontalAlignment = HorizontalAlignment.Left;
return block;
