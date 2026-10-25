// Colour orbs: the six accents as circles, the live one wearing a glow ring.
var orbs = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
var palette = new (string Name, string Hex)[] 
{
    ("Emerald", "#00FF9C"),
    ("Rose", "#FF2E88"),
    ("Sapphire", "#22D3FF"),
    ("Amethyst", "#B14BFF"),
    ("Amber", "#FFE600")
};

foreach (var (name, hex) in palette)
{
    var selected = name == "Sapphire";
    var ring = KitUi.Swatch(hex, 24);
    ring.BorderBrush = selected ? Brushes.White : Brushes.Transparent;

    if (selected)
    {
        ring.Effect = new System.Windows.Media.Effects.DropShadowEffect
        {
            BlurRadius = 14,
            ShadowDepth = 0,
            Opacity = 0.5,
            Color = (Color)ColorConverter.ConvertFromString(hex)
        };
    }

    var cell = KitUi.Col(5, ring, KitUi.Muted(name, 9.5));
    cell.Margin = new Thickness(0, 0, 14, 0);
    cell.HorizontalAlignment = HorizontalAlignment.Center;
    orbs.Children.Add(cell);
}

var card = KitUi.Surface(KitUi.Col(0, KitUi.Muted("ACCENT", 10), orbs), 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
