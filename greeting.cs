// cookies: a welcome card built from KIT styles, with one action.
var card = (Style)Application.Current.Resources["Kit.CardStyle"];

var stack = new StackPanel();

stack.Children.Add(new TextBlock
{
    Text = "i like cookies",
    Style = (Style)Application.Current.Resources["Kit.H1"]
});

stack.Children.Add(new TextBlock
{
    Text = "Hello, friend. Every label on the left keeps its own snippet; this one is just a card.",
    Style = (Style)Application.Current.Resources["Kit.Body"],
    Margin = new Thickness(0, 6, 0, 14)
});

stack.Children.Add(new PillButton
{
    Content = "Got it",
    Size = PillSize.Medium,
    HorizontalAlignment = HorizontalAlignment.Left
});

return new Border
{
    Style = card,
    Child = stack,
    Width = 360,
    HorizontalAlignment = HorizontalAlignment.Left
};
