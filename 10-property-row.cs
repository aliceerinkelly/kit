// Property inspector rows: bold keys left, editable mono values right.
var body = new StackPanel { Width = 420 };

void Row(string key, string value, bool mono)
{
    var grid = new Grid { Margin = new Thickness(0, 6, 0, 6) };
    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(130) });
    grid.ColumnDefinitions.Add(new ColumnDefinition());

    var label = KitUi.Text(key, 11.5, "Kit.MutedBrush");
    label.VerticalAlignment = VerticalAlignment.Center;

    var field = KitUi.Input(value, 28, mono ? 11 : 12);

    if (mono)
    {
        field.SetResourceReference(TextBox.FontFamilyProperty, "Kit.MonoFont");
    }

    Grid.SetColumn(field, 1);
    grid.Children.Add(label);
    grid.Children.Add(field);
    body.Children.Add(grid);
}

Row("Timeout (ms)", "5000", true);
Row("Worker threads", "8", true);
Row("Output path", "C:\\Workspace\\build", true);
Row("Keep symbols", "true", false);

var card = KitUi.Surface(body, 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
