// Drop zone: a dashed outline that invites a file, with the accepted types under it.
var icon = KitUi.Text("\u21E9", 22, "Kit.AccentBrush");
icon.HorizontalAlignment = HorizontalAlignment.Center;

var title = KitUi.Text("Drop a preset here", 12.5);
title.HorizontalAlignment = HorizontalAlignment.Center;
title.Margin = new Thickness(0, 4, 0, 0);

var hint = KitUi.Muted("JSON or XML up to 2 MB", 10.5);
hint.HorizontalAlignment = HorizontalAlignment.Center;
hint.Margin = new Thickness(0, 3, 0, 0);

var content = KitUi.Col(0, icon, title, hint);
content.VerticalAlignment = VerticalAlignment.Center;

var outline = new System.Windows.Shapes.Rectangle
{
    StrokeThickness = 1.5,
    RadiusX = 8,
    RadiusY = 8,
    StrokeDashArray = new DoubleCollection { 4, 3 }
};
outline.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.AccentBrush");

var frame = new Grid { Height = 120 };
frame.Children.Add(outline);
frame.Children.Add(new Border { Child = content });

frame.Width = 320;
frame.HorizontalAlignment = HorizontalAlignment.Left;
return frame;
