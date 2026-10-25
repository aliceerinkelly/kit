// Search field: leading glyph, live text, and a clear action that sits inside the field.
var glyph = KitUi.Muted("\u2315", 15);
glyph.Margin = new Thickness(0, 0, 8, 0);
glyph.VerticalAlignment = VerticalAlignment.Center;

var box = new TextBox
{
    Text = "ahci",
    BorderThickness = new Thickness(0),
    Background = Brushes.Transparent,
    FontSize = 12,
    VerticalContentAlignment = VerticalAlignment.Center,
    Width = 214
};

box.SetResourceReference(TextBox.ForegroundProperty, "Kit.TextBrush");
box.SetResourceReference(TextBox.CaretBrushProperty, "Kit.AccentBrush");

var clear = KitUi.Quiet("\u00D7", 22, 22);

var row = new StackPanel
{
    Orientation = Orientation.Horizontal,
    VerticalAlignment = VerticalAlignment.Center
};

row.Children.Add(glyph);
row.Children.Add(box);
row.Children.Add(clear);

var field = KitUi.Surface(row, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
field.Width = 320;
field.HorizontalAlignment = HorizontalAlignment.Left;
return field;
