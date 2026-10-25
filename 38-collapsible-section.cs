// Collapsible section: a quiet header bar over the settings it owns.
var chevron = KitUi.Muted("\u25BE", 10);
chevron.Margin = new Thickness(0, 0, 8, 0);
chevron.VerticalAlignment = VerticalAlignment.Center;

var title = KitUi.Muted("CONFIGURATION", 10);
title.VerticalAlignment = VerticalAlignment.Center;

var hint = KitUi.Mono("3 keys", 9.5, "Kit.BorderBrush");
hint.VerticalAlignment = VerticalAlignment.Center;

var head = new Grid();
head.ColumnDefinitions.Add(new ColumnDefinition());
head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

var left = new StackPanel { Orientation = Orientation.Horizontal };
left.Children.Add(chevron);
left.Children.Add(title);
head.Children.Add(left);
Grid.SetColumn(hint, 1);
head.Children.Add(hint);

var bar = new Border { Padding = new Thickness(13, 9, 13, 9), Child = head };
bar.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

var body = new Border
{
    Padding = new Thickness(13, 11, 13, 12),
    Child = KitUi.Col(
        7,
        KitUi.Mono("timeout = 5000", 11.5, "Kit.TextBrush"),
        KitUi.Mono("retries = 2", 11.5, "Kit.TextBrush"),
        KitUi.Mono("verbose = false", 11.5, "Kit.MutedBrush"))
};

var panel = KitUi.Surface(KitUi.Col(0, bar, KitUi.Rule(), body), pad: 0, radius: 8, fill: "Kit.PanelBrush");
panel.Width = 340;
panel.HorizontalAlignment = HorizontalAlignment.Left;
return panel;
