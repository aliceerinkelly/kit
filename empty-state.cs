// An empty state: one dot, one line of what happened, one way out.
var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];

var stack = new StackPanel
{
    HorizontalAlignment = HorizontalAlignment.Center,
    VerticalAlignment = VerticalAlignment.Center
};

stack.Children.Add(new System.Windows.Shapes.Ellipse
{
    Width = 10,
    Height = 10,
    Fill = accent,
    HorizontalAlignment = HorizontalAlignment.Center,
    Margin = new Thickness(0, 0, 0, 12)
});

stack.Children.Add(new TextBlock
{
    Text = "No sticks yet",
    Style = (Style)Application.Current.Resources["Kit.H2"],
    HorizontalAlignment = HorizontalAlignment.Center
});

stack.Children.Add(new TextBlock
{
    Text = "Plug one in and KIT will pick it up.",
    Style = (Style)Application.Current.Resources["Kit.MutedText"],
    HorizontalAlignment = HorizontalAlignment.Center,
    Margin = new Thickness(0, 6, 0, 14)
});

stack.Children.Add(new PillButton
{
    Content = "Scan",
    Size = PillSize.Small,
    HorizontalAlignment = HorizontalAlignment.Center
});

return stack;
