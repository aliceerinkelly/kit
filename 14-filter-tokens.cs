// Filter tokens: active query parameters as chips that carry their own remove badge.
Border Token(string text)
{
    var label = KitUi.Text(text, 11, "Kit.TextBrush");
    label.Margin = new Thickness(0, 0, 6, 0);
    label.VerticalAlignment = VerticalAlignment.Center;

    var close = KitUi.Quiet("\u00D7", 20, 18);

    var content = new StackPanel { Orientation = Orientation.Horizontal };
    content.Children.Add(label);
    content.Children.Add(close);

    var chip = new Border
    {
        CornerRadius = new CornerRadius(12),
        BorderThickness = new Thickness(1),
        Padding = new Thickness(10, 2, 4, 2),
        Margin = new Thickness(0, 0, 6, 6),
        Child = content
    };

    chip.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
    chip.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
    return chip;
}

var tokens = new WrapPanel { Width = 460 };
tokens.Children.Add(Token("status: failed"));
tokens.Children.Add(Token("pool: storage"));
tokens.Children.Add(Token("since: 24h"));
tokens.Children.Add(Token("tag: nightly"));

var add = KitUi.Quiet("+ Add filter", 24);
add.FontSize = 11.5;
add.Margin = new Thickness(2, 0, 0, 6);
tokens.Children.Add(add);

var card = KitUi.Surface(KitUi.Col(0, KitUi.Muted("4 filters active", 10.5), tokens), 12, 8, "Kit.PanelBrush");
card.Width = 480;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
