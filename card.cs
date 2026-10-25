// A card: 1px outline, heading, one line of copy, one action.
var card = (Style)Application.Current.Resources["Kit.CardStyle"];

var stack = new StackPanel();

stack.Children.Add(new TextBlock
{
    Text = "Storage",
    Style = (Style)Application.Current.Resources["Kit.H2"]
});

stack.Children.Add(new TextBlock
{
    Text = "Three sticks were written this morning. The oldest one is nearly full.",
    Style = (Style)Application.Current.Resources["Kit.Body"],
    Margin = new Thickness(0, 6, 0, 12)
});

stack.Children.Add(new PillButton
{
    Content = "Open log",
    Size = PillSize.Small,
    HorizontalAlignment = HorizontalAlignment.Left
});

return new Border
{
    Style = card,
    Child = stack,
    Width = 340,
    HorizontalAlignment = HorizontalAlignment.Left
};
