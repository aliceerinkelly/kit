// Expander tree: a category header you can fold, with quiet rail rows under it.
var header = new Border
{
    Padding = new Thickness(14, 8, 14, 8),
    Child = KitUi.Row(8, KitUi.Muted("\u25BE", 12), KitUi.Head("Core Modules", 13))
};

header.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

var body = new Border
{
    Padding = new Thickness(14, 9, 14, 10),
    Child = KitUi.Col(
        3,
        KitUi.Pill("Dashboard", active: true, height: 28),
        KitUi.Pill("Diagnostics", height: 28),
        KitUi.Pill("Change log", height: 28))
};

var card = KitUi.Surface(KitUi.Col(0, header, body), pad: 0, radius: 8, fill: "Kit.PanelBrush");
card.Width = 300;
card.HorizontalAlignment = HorizontalAlignment.Left;

return card;
