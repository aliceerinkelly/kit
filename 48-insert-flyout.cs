// Insert flyout: a floating card with the template, then insert or copy.
var head = new StackPanel { Orientation = Orientation.Horizontal };
var title = KitUi.Text("Save bar", 12);
title.FontWeight = FontWeights.SemiBold;
var tag = KitUi.Chip("WPF \u00B7 C#", "Kit.AccentSoftBrush", "Kit.AccentInkBrush");
tag.Margin = new Thickness(9, 0, 0, 0);
head.Children.Add(title);
head.Children.Add(tag);

var preview = KitUi.Col(
    2,
    KitUi.Mono("var row = KitUi.Row(8,", 10.5, "Kit.MutedBrush"),
    KitUi.Mono("    KitUi.Pill(\"Save\", active: true),", 10.5, "Kit.MutedBrush"),
    KitUi.Mono("    KitUi.Pill(\"Cancel\"));", 10.5, "Kit.MutedBrush"));

var well = KitUi.Surface(preview, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
well.Margin = new Thickness(0, 9, 0, 0);

var insert = KitUi.Pill("Insert", active: true, height: 28);
var copy = KitUi.Pill("Copy code", height: 28);

var actions = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
actions.Children.Add(insert);
actions.Children.Add(new Border { Width = 8 });
actions.Children.Add(copy);

var card = KitUi.Surface(KitUi.Col(0, head, well, actions), 13, 9, "Kit.CardBrush", "Kit.BorderBrush");
card.Width = 300;
card.HorizontalAlignment = HorizontalAlignment.Left;
card.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 20,
    ShadowDepth = 5,
    Opacity = 0.28,
    Color = Colors.Black
};

return card;
