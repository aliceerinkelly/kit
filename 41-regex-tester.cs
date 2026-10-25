// Regex tester: the pattern in mono, the verdict beside it, the hits under it.
var pattern = KitUi.Mono("^[a-z0-9_]+$", 12);
pattern.Width = 250;
pattern.VerticalAlignment = VerticalAlignment.Center;

var verdict = KitUi.Chip("3 matches", "Kit.AccentSoftBrush", "Kit.AccentInkBrush");
verdict.HorizontalAlignment = HorizontalAlignment.Right;

var field = new Grid();
field.ColumnDefinitions.Add(new ColumnDefinition());
field.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
field.Children.Add(pattern);
Grid.SetColumn(verdict, 1);
field.Children.Add(verdict);

var frame = KitUi.Surface(field, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
frame.Width = 340;

var hits = new WrapPanel { Width = 340, Margin = new Thickness(0, 9, 0, 0) };

foreach (var hit in new[] { "panel_header", "rail_item", "chip_style" })
{
    var pill = new Border
    {
        CornerRadius = new CornerRadius(4),
        Padding = new Thickness(7, 2, 7, 2),
        Margin = new Thickness(0, 0, 6, 6),
        Child = KitUi.Mono(hit, 10.5)
    };

    pill.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
    hits.Children.Add(pill);
}

var block = KitUi.Col(0, KitUi.Muted("PATTERN", 10), frame, hits);
block.HorizontalAlignment = HorizontalAlignment.Left;
return block;
