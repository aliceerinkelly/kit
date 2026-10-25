// Micro labels: the small caps family, which is three jobs at one size.
var stack = new StackPanel { Width = 340 };

void Row(string caption, TextBlock label)
{
    var line = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 12) };

    var spec = KitUi.Mono(caption, 9.5, "Kit.MutedBrush");
    spec.Width = 130;
    spec.VerticalAlignment = VerticalAlignment.Center;

    label.VerticalAlignment = VerticalAlignment.Center;
    line.Children.Add(spec);
    line.Children.Add(label);
    stack.Children.Add(line);
}

var section = KitUi.Muted("SECTION", 9.5);
section.FontWeight = FontWeights.SemiBold;

var accent = KitUi.Text("LIVE", 9.5, "Kit.AccentBrush");
accent.FontWeight = FontWeights.Bold;

var quiet = KitUi.Text("IDLE \u00B7 4 MIN", 9.5, "Kit.BorderBrush");
quiet.FontWeight = FontWeights.SemiBold;

Row("section", section);
Row("status, live", accent);
Row("status, quiet", quiet);

var caption = KitUi.Muted("One size, one case, no tracking tricks - weight carries the difference.", 10);
return KitUi.Col(0, KitUi.Surface(stack, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush"), caption);
