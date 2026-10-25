// Ring gauge: a full circle behind, an accent arc to 88%, the number in the middle.
var back = new System.Windows.Shapes.Ellipse
{
    Width = 88,
    Height = 88,
    StrokeThickness = 7
};
back.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.BorderBrush");

var arc = new System.Windows.Shapes.Path
{
    StrokeThickness = 7,
    StrokeStartLineCap = PenLineCap.Round,
    StrokeEndLineCap = PenLineCap.Round,
    Data = Geometry.Parse("M 44,4 A 40,40 0 1 1 16.7,14.8")
};
arc.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.AccentBrush");

var value = KitUi.Text("88%", 20);
value.FontWeight = FontWeights.Bold;
value.HorizontalAlignment = HorizontalAlignment.Center;
value.VerticalAlignment = VerticalAlignment.Center;

var caption = KitUi.Muted("coverage", 9.5);
caption.HorizontalAlignment = HorizontalAlignment.Center;
caption.VerticalAlignment = VerticalAlignment.Center;
caption.Margin = new Thickness(0, 26, 0, 0);

var dial = new Grid { Width = 88, Height = 88 };
dial.Children.Add(back);
dial.Children.Add(arc);
dial.Children.Add(value);
dial.Children.Add(caption);

var text = KitUi.Col(3, KitUi.Head("Snippet coverage", 13), KitUi.Muted("78 of 84 designs compile clean", 11));
text.VerticalAlignment = VerticalAlignment.Center;
text.Margin = new Thickness(16, 0, 0, 0);

var row = new StackPanel { Orientation = Orientation.Horizontal };
row.Children.Add(dial);
row.Children.Add(text);

var card = KitUi.Surface(row, 14, 10, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
