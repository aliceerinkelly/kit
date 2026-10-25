// Activity grid: a week across, four rows down, opacity carrying the density.
var grid = new System.Windows.Controls.Primitives.UniformGrid { Columns = 12, Rows = 4, Width = 240 };

var density = new double[]
{
    0.15, 0.35, 0.9, 0.2, 0.55, 0.1, 0.4, 0.75, 0.25, 0.6, 0.3, 0.95,
    0.45, 0.2, 0.65, 0.85, 0.3, 0.5, 0.15, 0.7, 0.4, 0.2, 0.55, 0.35,
    0.1, 0.6, 0.25, 0.4, 0.9, 0.35, 0.2, 0.5, 0.8, 0.15, 0.45, 0.3,
    0.3, 0.5, 0.7, 0.15, 0.35, 0.6, 0.9, 0.25, 0.45, 0.2, 0.5, 0.4
};

foreach (var level in density)
{
    var cell = KitUi.Dot(14, "Kit.AccentBrush");
    cell.CornerRadius = new CornerRadius(3);
    cell.Opacity = level;
    cell.Margin = new Thickness(2);
    grid.Children.Add(cell);
}

var caption = KitUi.Muted("48 days \u00B7 214 designs saved", 10.5);
caption.Margin = new Thickness(2, 8, 0, 0);

var card = KitUi.Surface(KitUi.Col(0, KitUi.Muted("ACTIVITY", 10), grid, caption), 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
