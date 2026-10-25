// Links: the colour says what happens, the underline says it is clickable at all.
TextBlock Link(string text, string brush, bool underline)
{
    var link = new TextBlock
    {
        Text = text,
        FontSize = 12.5,
        TextDecorations = underline ? TextDecorations.Underline : null
    };

    link.SetResourceReference(TextBlock.ForegroundProperty, brush);
    return link;
}

var stack = new StackPanel { Width = 340 };

void Row(TextBlock link, string note)
{
    var caption = KitUi.Muted(note, 10);
    caption.Margin = new Thickness(0, 2, 0, 12);

    stack.Children.Add(link);
    stack.Children.Add(caption);
}

Row(Link("Open the snippet folder", "Kit.AccentBrush", true), "navigates - accent, underlined");
Row(Link("Copy this code", "Kit.TextBrush", true), "acts on what you are looking at - text ink, underlined");
Row(Link("Delete this design", "Kit.AccentDeepBrush", false), "destroys something - deep accent, no underline until hover");
Row(Link("Learn more", "Kit.MutedBrush", false), "leaves KIT - quiet, so it never competes with an action");

var card = KitUi.Surface(stack, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
