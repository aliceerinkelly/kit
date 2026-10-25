// Workspace switcher: the active solution in the bar, the others one click below.
var glyph = KitUi.Muted("\u25B1", 13);
glyph.Margin = new Thickness(0, 0, 8, 0);
glyph.VerticalAlignment = VerticalAlignment.Center;

var name = KitUi.Text("Kit.sln", 12);
name.FontWeight = FontWeights.SemiBold;
name.VerticalAlignment = VerticalAlignment.Center;

var live = KitUi.Chip("active", "Kit.AccentSoftBrush", "Kit.AccentInkBrush");
live.Margin = new Thickness(8, 0, 0, 0);

var chevron = KitUi.Muted("\u25BE", 10);
chevron.Margin = new Thickness(10, 0, 0, 0);
chevron.VerticalAlignment = VerticalAlignment.Center;

var barRow = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
barRow.Children.Add(glyph);
barRow.Children.Add(name);
barRow.Children.Add(live);
barRow.Children.Add(chevron);

var bar = KitUi.Surface(barRow, 10, 6, "Kit.RowBrush", "Kit.BorderBrush");
bar.HorizontalAlignment = HorizontalAlignment.Left;

var list = new StackPanel();

foreach (var (workspace, when) in new[] { ("Kit.sln", "now"), ("Catalog.sln", "yesterday"), ("Sandbox.sln", "3 days ago") })
{
    var label = KitUi.Text(workspace, 11.5);
    label.Width = 130;
    label.VerticalAlignment = VerticalAlignment.Center;

    var stamp = KitUi.Mono(when, 10, "Kit.MutedBrush");
    stamp.VerticalAlignment = VerticalAlignment.Center;

    var row = new StackPanel { Orientation = Orientation.Horizontal };
    row.Children.Add(label);
    row.Children.Add(stamp);

    list.Children.Add(new Border { Padding = new Thickness(10, 6, 10, 6), Child = row });
}

var menu = KitUi.Surface(list, 4, 8, "Kit.CardBrush", "Kit.BorderBrush");
menu.HorizontalAlignment = HorizontalAlignment.Left;
menu.Margin = new Thickness(0, 6, 0, 0);

return KitUi.Col(0, bar, menu);
