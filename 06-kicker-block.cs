// Kicker block: three sizes doing three jobs - context, claim, and the sentence that
// stops the claim from being a slogan.
var kicker = KitUi.Text("DESIGN LIBRARY", 9.5, "Kit.AccentBrush");
kicker.FontWeight = FontWeights.Bold;

var heading = KitUi.Text("A place for the designs you keep rewriting", 20, "Kit.TextBrush");
heading.FontWeight = FontWeights.Bold;
heading.TextWrapping = TextWrapping.Wrap;
heading.Margin = new Thickness(0, 6, 0, 0);

var deck = KitUi.Muted(
    "KIT holds the buttons, rows and fields a programmer tool needs, as code you can copy into the next project.",
    12);
deck.TextWrapping = TextWrapping.Wrap;
deck.Margin = new Thickness(0, 8, 0, 0);

var block = new StackPanel { Width = 420 };
block.Children.Add(kicker);
block.Children.Add(heading);
block.Children.Add(deck);

var caption = KitUi.Muted("20 / 12 / 9.5, one and a half steps apart.", 10);
caption.Margin = new Thickness(0, 14, 0, 0);

return KitUi.Col(0, block, caption);
