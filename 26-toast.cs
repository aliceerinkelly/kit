// Toast: a card that fades in at the corner, then leaves on its own.
var dot = KitUi.Dot(8, "Kit.AccentBrush");
dot.Margin = new Thickness(0, 0, 9, 0);

var title = KitUi.Text("Build succeeded in 1.4s", 12);
title.FontWeight = FontWeights.SemiBold;

var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
head.Children.Add(dot);
head.Children.Add(title);

var detail = KitUi.Muted("84 designs compiled \u00B7 no warnings", 10.5);
detail.Margin = new Thickness(0, 3, 0, 0);

var action = KitUi.Quiet("View log", 22);
action.HorizontalAlignment = HorizontalAlignment.Left;
action.Margin = new Thickness(0, 7, 0, 0);

var card = KitUi.Surface(KitUi.Col(0, head, detail, action), 13, 8, "Kit.CardBrush", "Kit.BorderBrush");
card.Width = 300;
card.HorizontalAlignment = HorizontalAlignment.Right;
card.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 18,
    ShadowDepth = 4,
    Opacity = 0.28,
    Color = Colors.Black
};

var stage = new Grid { Width = 380, Height = 140 };
stage.Children.Add(card);
((FrameworkElement)card).VerticalAlignment = VerticalAlignment.Bottom;
return stage;
