// Segmented switch: three view modes in one track, the live one filled with the accent.
var grid = new System.Windows.Controls.Primitives.UniformGrid { Columns = 3, Height = 32 };
var labels = new[] { "Grid", "List", "Raw" };

for (var i = 0; i < labels.Length; i++)
{
    var segment = KitUi.Pill(labels[i], active: i == 0, height: 26);
    segment.Margin = new Thickness(3);
    segment.Radius = new CornerRadius(5);
    grid.Children.Add(segment);
}

var track = KitUi.Surface(grid, pad: 0, radius: 8, fill: "Kit.RowBrush");
track.Width = 300;
track.HorizontalAlignment = HorizontalAlignment.Left;
return track;
