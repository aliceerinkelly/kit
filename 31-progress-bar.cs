// Progress bar: rounded ends, a gradient fill and the readout above the track.
var label = KitUi.Text("Packing image", 12);
var count = KitUi.Mono("75%", 11, "Kit.MutedBrush");
count.HorizontalAlignment = HorizontalAlignment.Right;

var head = new Grid();
head.ColumnDefinitions.Add(new ColumnDefinition());
head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
head.Children.Add(label);
Grid.SetColumn(count, 1);
head.Children.Add(count);

var track = new Border { Height = 5, CornerRadius = new CornerRadius(3), Margin = new Thickness(0, 8, 0, 0) };
track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

var fill = new Border
{
    Height = 5,
    Width = 225,
    CornerRadius = new CornerRadius(3),
    HorizontalAlignment = HorizontalAlignment.Left,
    Margin = new Thickness(0, 8, 0, 0)
};

var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];
var deep = (Brush)Application.Current.Resources["Kit.AccentDeepBrush"];

fill.Background = new LinearGradientBrush(
    ((SolidColorBrush)accent).Color,
    ((SolidColorBrush)deep).Color,
    0.0);

fill.Effect = new System.Windows.Media.Effects.DropShadowEffect
{
    BlurRadius = 12,
    ShadowDepth = 0,
    Opacity = 0.5,
    Color = ((SolidColorBrush)accent).Color
};

var bars = new Grid { Width = 300, Height = 9 };
bars.Children.Add(track);
bars.Children.Add(fill);

var block = new StackPanel { Width = 300 };
block.Children.Add(head);
block.Children.Add(bars);

var caption = KitUi.Muted("1.9 GB of 2.5 GB written", 10.5);
caption.Margin = new Thickness(0, 7, 0, 0);
block.Children.Add(caption);

block.HorizontalAlignment = HorizontalAlignment.Left;
return block;
