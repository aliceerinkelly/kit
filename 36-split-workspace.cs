// Split workspace: rail, a draggable seam, then the editor surface.
var rail = KitUi.Col(
    6,
    KitUi.Muted("RAIL", 9.5),
    KitUi.Pill("Buttons", active: true, height: 26),
    KitUi.Pill("Fields", height: 26),
    KitUi.Pill("Charts", height: 26));

rail.Margin = new Thickness(12);

var seam = new Border { Width = 3, Cursor = Cursors.SizeWE };
seam.SetResourceReference(Border.BackgroundProperty, "Kit.BorderBrush");

var editor = KitUi.Col(
    8,
    KitUi.Head("Snippet.cs", 12),
    KitUi.Mono("var save = new PillButton", 11, "Kit.MutedBrush"),
    KitUi.Mono("{ Content = \"Save\" };", 11, "Kit.MutedBrush"));

editor.Margin = new Thickness(14);

var grid = new Grid { Width = 520, Height = 190 };
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });
grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4) });
grid.ColumnDefinitions.Add(new ColumnDefinition());

Grid.SetColumn(seam, 1);
Grid.SetColumn(editor, 2);

var railHost = new Border { Child = rail };
railHost.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

grid.Children.Add(railHost);
grid.Children.Add(seam);
grid.Children.Add(editor);

var frame = KitUi.Surface(grid, pad: 0, radius: 8, fill: "Kit.PanelBrush");
frame.HorizontalAlignment = HorizontalAlignment.Left;
return frame;
