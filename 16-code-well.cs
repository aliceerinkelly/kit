// Code well: a mono block with line numbers and a copy badge pinned to the corner.
var lines = new StackPanel();

string[] source =
[
    "{",
    "  \"name\": \"KIT\",",
    "  \"version\": \"1.5.0\",",
    "  \"target\": \"net8.0-windows\",",
    "  \"designs\": 84",
    "}"
];

for (var i = 0; i < source.Length; i++)
{
    var row = new StackPanel { Orientation = Orientation.Horizontal };

    var number = KitUi.Mono((i + 1).ToString(), 10.5, "Kit.BorderBrush");
    number.Width = 22;

    row.Children.Add(number);
    row.Children.Add(KitUi.Mono(source[i], 11.5, "Kit.TextBrush"));
    lines.Children.Add(row);
}

var copy = KitUi.Quiet("Copy", 22);
copy.HorizontalAlignment = HorizontalAlignment.Right;
copy.VerticalAlignment = VerticalAlignment.Top;

var frame = new Grid();
frame.Children.Add(lines);
frame.Children.Add(copy);

var well = KitUi.Surface(frame, 12, 6, "Kit.FieldBrush", "Kit.BorderBrush");
well.Width = 360;
well.HorizontalAlignment = HorizontalAlignment.Left;
return well;
