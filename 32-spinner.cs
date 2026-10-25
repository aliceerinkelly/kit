// Spinner: a broken ring that turns, with the queue depth beside it.
var ring = new System.Windows.Shapes.Ellipse
{
    Width = 26,
    Height = 26,
    StrokeThickness = 3,
    StrokeDashArray = new DoubleCollection { 3, 1.6 },
    StrokeStartLineCap = PenLineCap.Round,
    StrokeEndLineCap = PenLineCap.Round,
    RenderTransformOrigin = new Point(0.5, 0.5)
};

ring.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.AccentBrush");

ring.RenderTransform = new RotateTransform(-20);

var text = KitUi.Col(1, KitUi.Text("Compiling 3 designs", 12), KitUi.Muted("keeps running while you browse", 10.5));
text.VerticalAlignment = VerticalAlignment.Center;
text.Margin = new Thickness(12, 0, 0, 0);

var row = new StackPanel { Orientation = Orientation.Horizontal };
row.Children.Add(ring);
row.Children.Add(text);

var card = KitUi.Surface(row, 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
