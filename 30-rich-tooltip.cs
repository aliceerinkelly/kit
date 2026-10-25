// Tooltip: a trigger with a card under it - title, one sentence, and the shortcut.
var trigger = KitUi.Pill("Compile", height: 30);
trigger.HorizontalAlignment = HorizontalAlignment.Left;

var caret = KitUi.Muted("\u25B2", 9);
caret.HorizontalAlignment = HorizontalAlignment.Left;
caret.Margin = new Thickness(22, 0, 0, 4);

var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
var title = KitUi.Text("Compile the snippet", 11.5);
title.FontWeight = FontWeights.SemiBold;
var cap = KitUi.Keycap("Ctrl+Enter");
cap.Margin = new Thickness(10, 0, 0, 0);
head.Children.Add(title);
head.Children.Add(cap);

var copy = KitUi.Muted(
    "Runs the code in this design and paints the result in the preview panel.",
    11);
copy.TextWrapping = TextWrapping.Wrap;
copy.Margin = new Thickness(0, 5, 0, 0);

var card = KitUi.Surface(KitUi.Col(0, head, copy), 11, 7, "Kit.CardBrush", "Kit.BorderBrush");
card.Width = 300;
card.HorizontalAlignment = HorizontalAlignment.Left;
card.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 16,
    ShadowDepth = 4,
    Opacity = 0.24,
    Color = Colors.Black
};

var stack = KitUi.Col(0, trigger, caret, card);
stack.HorizontalAlignment = HorizontalAlignment.Left;
return stack;
