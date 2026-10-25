// Floating action menu: an accent orb that opens a stack of high-frequency actions.
var actions = KitUi.Col(
    8,
    KitUi.Pill("New design", height: 30),
    KitUi.Pill("Import code", height: 30),
    KitUi.Pill("Export library", height: 30));

var orb = new FlatButton
{
    Content = "+",
    Width = 48,
    Height = 48,
    Radius = new CornerRadius(24),
    FontSize = 22,
    FontWeight = FontWeights.Light,
    HorizontalAlignment = HorizontalAlignment.Right
};

KitUi.Skin(orb, FlatButton.FillProperty, "Kit.AccentBrush");
KitUi.Skin(orb, FlatButton.InkProperty, "Kit.OnAccentBrush");
KitUi.Skin(orb, FlatButton.EdgeProperty, "Kit.AccentBrush");
KitUi.Skin(orb, FlatButton.HoverFillProperty, "Kit.AccentDeepBrush");
KitUi.Skin(orb, FlatButton.HoverInkProperty, "Kit.OnAccentBrush");
KitUi.Skin(orb, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");

orb.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 22,
    ShadowDepth = 4,
    Opacity = 0.35,
    Color = Colors.Black
};

var stack = KitUi.Col(12, actions, orb);
stack.HorizontalAlignment = HorizontalAlignment.Right;
stack.VerticalAlignment = VerticalAlignment.Bottom;

var stage = new Grid { Width = 320, Height = 240, HorizontalAlignment = HorizontalAlignment.Left };
stage.Children.Add(KitUi.Surface(new Grid(), pad: 0, radius: 10, fill: "Kit.PanelBrush"));
stage.Children.Add(new Border
{
    Padding = new Thickness(16),
    Child = stack,
    HorizontalAlignment = HorizontalAlignment.Right,
    VerticalAlignment = VerticalAlignment.Bottom
});

return stage;
