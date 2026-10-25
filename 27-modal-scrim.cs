// Modal: a scrim covers the page, the dialog sits centred on it with two decisions.
var title = KitUi.Head("Delete three designs?", 15);

var copy = KitUi.Muted(
    "The code stays on disk in snippets/, but the rail rows are removed from this workspace.",
    11.5);
copy.TextWrapping = TextWrapping.Wrap;
copy.Margin = new Thickness(0, 6, 0, 0);

var cancel = KitUi.Pill("Cancel", height: 30);
var confirm = KitUi.Pill("Delete", active: true, height: 30);

var actions = new StackPanel
{
    Orientation = Orientation.Horizontal,
    HorizontalAlignment = HorizontalAlignment.Right,
    Margin = new Thickness(0, 16, 0, 0)
};

actions.Children.Add(cancel);
actions.Children.Add(new Border { Width = 8 });
actions.Children.Add(confirm);

var dialog = KitUi.Surface(KitUi.Col(0, title, copy, actions), 18, 10, "Kit.CardBrush", "Kit.BorderBrush");
dialog.Width = 400;
dialog.HorizontalAlignment = HorizontalAlignment.Center;
dialog.VerticalAlignment = VerticalAlignment.Center;
dialog.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 30,
    ShadowDepth = 8,
    Opacity = 0.4,
    Color = Colors.Black
};

var stage = new Grid { Width = 520, Height = 260 };
stage.Children.Add(KitUi.Surface(new Grid(), pad: 0, radius: 10, fill: "Kit.PanelBrush"));
stage.Children.Add(new Border { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) });
stage.Children.Add(dialog);

stage.HorizontalAlignment = HorizontalAlignment.Left;
return stage;
