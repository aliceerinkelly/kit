// Inline code: the run stays in the sentence, only the box and the family change.
Border Code(string text)
{
    var box = new Border
    {
        CornerRadius = new CornerRadius(4),
        Padding = new Thickness(5, 1, 5, 2),
        Margin = new Thickness(0, 0, 3, 0),
        VerticalAlignment = VerticalAlignment.Center,
        Child = KitUi.Mono(text, 11.5, "Kit.TextBrush")
    };

    box.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
    box.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
    box.BorderThickness = new Thickness(1);
    return box;
}

TextBlock Word(string text)
{
    var block = KitUi.Text(text, 12.5);
    block.VerticalAlignment = VerticalAlignment.Center;
    return block;
}

var line = new WrapPanel { Width = 460 };
line.Children.Add(Word("Run "));
line.Children.Add(Code("dotnet build"));
line.Children.Add(Word(" first, then open "));
line.Children.Add(Code("Kit.sln"));
line.Children.Add(Word(" and press "));
line.Children.Add(Code("F5"));
line.Children.Add(Word(" to attach the debugger."));

var caption = KitUi.Muted("Padding 5/1/5/2 keeps the box off the baseline.", 10.5);
caption.Margin = new Thickness(0, 10, 0, 0);

var card = KitUi.Surface(KitUi.Col(0, line, caption), 14, 8, "Kit.PanelBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
