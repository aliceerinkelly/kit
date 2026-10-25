// Metric card: one number, its delta, and a quiet scale under it.
var value = KitUi.Text("42.8%", 26, "Kit.TextBrush");
value.FontWeight = FontWeights.Bold;
value.Margin = new Thickness(0, 3, 0, 0);

var deltaRow = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 0) };
deltaRow.Children.Add(new TextBlock
{
    Text = "\u2191",
    FontSize = 10.5,
    FontWeight = FontWeights.Bold,
    Foreground = new SolidColorBrush(Color.FromRgb(0x10, 0xB9, 0x81)),
    Margin = new Thickness(0, 0, 4, 0)
});
deltaRow.Children.Add(new TextBlock
{
    Text = "3.2% from baseline",
    FontSize = 10.5,
    Foreground = new SolidColorBrush(Color.FromRgb(0x10, 0xB9, 0x81))
});

var spark = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
double[] heights = [4, 7, 5, 10, 8, 12, 9, 14, 11, 16];

foreach (var height in heights)
{
    var bar = new Border
    {
        Width = 6,
        Height = height,
        CornerRadius = new CornerRadius(2),
        Margin = new Thickness(0, 0, 3, 0),
        VerticalAlignment = VerticalAlignment.Bottom
    };

    bar.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
    spark.Children.Add(bar);
}

var card = KitUi.Surface(
    KitUi.Col(0, KitUi.Muted("CPU USAGE", 10), value, deltaRow, spark),
    14,
    8,
    "Kit.CardBrush");

card.Width = 190;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
