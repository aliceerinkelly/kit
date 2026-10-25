// Floating label field: the caption rides the top of the field, the value sits under it.
Border Field(string caption, string value)
{
    var label = KitUi.Text(caption, 9.5, "Kit.AccentBrush");
    label.FontWeight = FontWeights.SemiBold;

    var input = new TextBox
    {
        Text = value,
        BorderThickness = new Thickness(0),
        Background = Brushes.Transparent,
        FontSize = 12.5,
        Padding = new Thickness(0, 3, 0, 0)
    };

    input.SetResourceReference(TextBox.ForegroundProperty, "Kit.TextBrush");
    input.SetResourceReference(TextBox.CaretBrushProperty, "Kit.AccentBrush");

    var field = KitUi.Surface(KitUi.Col(0, label, input), 10, 6, "Kit.FieldBrush", "Kit.AccentBrush");
    field.Width = 320;
    field.HorizontalAlignment = HorizontalAlignment.Left;
    return field;
}

return KitUi.Col(0, Field("DATABASE CONNECTION STRING", "Server=localhost;Port=5432"), Field("DATABASE NAME", "kit_catalog"));
