// Breadcrumb trail: chevrons between crumbs, the last one carrying the accent.
var crumbs = new[] { "Workspace", "Projects", "Kit.sln", "Snippets" };
var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };

for (var i = 0; i < crumbs.Length; i++)
{
    var last = i == crumbs.Length - 1;

    if (i > 0)
    {
        var separator = KitUi.Text("\u203A", 12.5, "Kit.BorderBrush");
        separator.Margin = new Thickness(7, 0, 7, 0);
        separator.VerticalAlignment = VerticalAlignment.Center;
        row.Children.Add(separator);
    }

    var crumb = KitUi.Text(crumbs[i], 12.5, last ? "Kit.AccentBrush" : "Kit.MutedBrush");

    if (last)
    {
        crumb.FontWeight = FontWeights.SemiBold;
    }

    row.Children.Add(crumb);
}

return KitUi.Surface(row, 11, 6, "Kit.RowBrush", "Kit.BorderBrush");
