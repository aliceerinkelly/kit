// Blue capsule partitions: a card per volume, with its filesystem, size and letter.
Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);
var mono = (FontFamily)Application.Current.Resources["Kit.MonoFont"];

var row = new StackPanel { Orientation = Orientation.Horizontal };

var parts = new (string Number, string Kind, string Format, string Size, string Letter, double Fill)[]
{
    ("P1", "Basic data partition", "FAT32", "200 MB", "[ ]", 0.06),
    ("P3", "Basic data partition", "NTFS", "473.8 GB", "[C:]", 0.62),
    ("P4", "Windows Recovery", "NTFS", "892 MB", "[ ]", 0.12)
};

foreach (var (number, kind, format, size, letter, fill) in parts)
{
    var card = new StackPanel();

    var head = new Grid();
    head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
    head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

    var tag = new TextBlock
    {
        Text = number,
        FontFamily = mono,
        FontSize = 10,
        Foreground = C("#7FA5C8"),
        Margin = new Thickness(0, 0, 8, 0)
    };

    var label = new TextBlock
    {
        Text = kind,
        FontSize = 10.5,
        Foreground = C("#A9C3DD"),
        TextTrimming = TextTrimming.CharacterEllipsis
    };

    Grid.SetColumn(label, 1);
    head.Children.Add(tag);
    head.Children.Add(label);
    card.Children.Add(head);

    card.Children.Add(new TextBlock
    {
        Text = format,
        FontSize = 12,
        FontWeight = FontWeights.Bold,
        Foreground = C("#5FA8FF"),
        Margin = new Thickness(0, 5, 0, 0)
    });

    card.Children.Add(new TextBlock
    {
        Text = size,
        FontFamily = mono,
        FontSize = 10.5,
        Foreground = C("#DCEBFF"),
        Margin = new Thickness(0, 3, 0, 7)
    });

    var track = new Grid { Height = 3 };
    track.Children.Add(new Border { Background = C("#24405F"), CornerRadius = new CornerRadius(2) });
    track.Children.Add(new Border
    {
        Background = C("#5FA8FF"),
        CornerRadius = new CornerRadius(2),
        HorizontalAlignment = HorizontalAlignment.Left,
        Width = 106 * fill
    });

    card.Children.Add(track);

    card.Children.Add(new TextBlock
    {
        Text = letter,
        FontFamily = mono,
        FontSize = 10.5,
        Foreground = C("#A9C3DD"),
        Margin = new Thickness(0, 7, 0, 0)
    });

    row.Children.Add(new Border
    {
        Width = 168,
        Background = C("#14263C"),
        BorderBrush = C("#2E6FA8"),
        BorderThickness = new Thickness(1),
        CornerRadius = new CornerRadius(8),
        Padding = new Thickness(11, 9, 11, 9),
        Margin = new Thickness(0, 0, 9, 0),
        Child = card
    });
}

return row;
