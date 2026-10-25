// A labelled field: mono value, small caps label, 1px outline.
var field = (Style)Application.Current.Resources["Kit.FieldStyle"];

var panel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Left };

panel.Children.Add(new TextBlock
{
    Text = "HOSTNAME",
    FontSize = 10,
    FontWeight = FontWeights.SemiBold,
    Foreground = (Brush)Application.Current.Resources["Kit.MutedBrush"],
    Margin = new Thickness(2, 0, 0, 6)
});

panel.Children.Add(new TextBox
{
    Text = "build.local",
    Style = field,
    Width = 280
});

return panel;
