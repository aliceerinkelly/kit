// Tree node: fold bullets, and one open branch with its children indented under it.
StackPanel Node(string label, int depth, bool open)
{
    var bullet = KitUi.Muted(open ? "\u25BE" : "\u2022", 11);
    bullet.Width = 14;
    bullet.VerticalAlignment = VerticalAlignment.Center;

    var text = KitUi.Text(label, 12.5, open ? "Kit.TextBrush" : "Kit.MutedBrush");
    text.VerticalAlignment = VerticalAlignment.Center;

    var row = new StackPanel
    {
        Orientation = Orientation.Horizontal,
        Margin = new Thickness(depth * 16, 0, 0, 0)
    };

    row.Children.Add(bullet);
    row.Children.Add(text);
    return row;
}

var tree = KitUi.Col(
    7,
    Node("Kit", 0, true),
    Node("Controls", 1, true),
    Node("PillButton", 2, false),
    Node("FlatButton", 2, false),
    Node("KitUi", 2, false),
    Node("Snippets", 1, true),
    Node("catalog", 2, false),
    Node("Views", 1, false));

var card = KitUi.Surface(tree, 13, 8);
card.Width = 280;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
