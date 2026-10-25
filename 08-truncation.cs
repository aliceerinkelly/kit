// Truncation: three ways to lose text, and the one place each belongs.
const string Long = "Bluetooth Device (RFCOMM Protocol TDI) \u00B7 Device\\{4d36e966-e325-11ce-bfc1-08002be10318}\\0000 \u00B7 registry value";

var stack = new StackPanel { Width = 300 };

void Row(string caption, TextTrimming trimming, TextWrapping wrapping, bool clip)
{
    var head = KitUi.Mono(caption, 9.5, "Kit.MutedBrush");
    head.Margin = new Thickness(0, 0, 0, 4);

    var label = KitUi.Text(Long, 12, "Kit.TextBrush");
    label.TextTrimming = trimming;
    label.TextWrapping = wrapping;

    if (clip)
    {
        label.Height = 18;
        label.ClipToBounds = true;
    }

    stack.Children.Add(head);
    stack.Children.Add(label);
    stack.Children.Add(new Border { Height = 12 });
}

Row("ellipsis \u00B7  rows and breadcrumbs", TextTrimming.CharacterEllipsis, TextWrapping.NoWrap, false);
Row("wrap \u00B7  descriptions and help", TextTrimming.None, TextWrapping.Wrap, false);
Row("clip \u00B7  never for text a person must read", TextTrimming.None, TextWrapping.NoWrap, true);

var card = KitUi.Surface(stack, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush");
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
