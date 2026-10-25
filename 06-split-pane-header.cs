// Split pane header: a panel title with a pin toggle, over the panel body.
var title = KitUi.Head("Properties Inspector", 13);
title.VerticalAlignment = VerticalAlignment.Center;

var pin = KitUi.Quiet("\uD83D\uDCCC", 26, 30);

var header = new Grid();
header.ColumnDefinitions.Add(new ColumnDefinition());
header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
header.Children.Add(title);
Grid.SetColumn(pin, 1);
header.Children.Add(pin);

var bar = new Border { Padding = new Thickness(13, 8, 8, 8), Child = header };
bar.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

var body = new Border
{
    Padding = new Thickness(13, 12, 13, 14),
    Child = KitUi.Col(
        6,
        KitUi.Mono("Width", 11, "Kit.MutedBrush"),
        KitUi.Muted("340 px  \u00B7  pinned to the right edge", 11.5))
};

var panel = KitUi.Surface(KitUi.Col(0, bar, KitUi.Rule(), body), pad: 0, radius: 8, fill: "Kit.PanelBrush");
panel.Width = 340;
panel.HorizontalAlignment = HorizontalAlignment.Left;
return panel;
