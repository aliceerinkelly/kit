// Terminal prompt: the prompt glyph is quieter than the command it introduces.
var lines = new StackPanel();

void Line(string prompt, string command, bool output)
{
    var glyph = KitUi.Mono(prompt, 11.5, output ? "Kit.BorderBrush" : "Kit.AccentBrush");
    glyph.Width = 18;

    var text = KitUi.Mono(command, 11.5, output ? "Kit.MutedBrush" : "Kit.TextBrush");

    var row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 4) };
    row.Children.Add(glyph);
    row.Children.Add(text);
    lines.Children.Add(row);
}

Line(">", "dotnet run", false);
Line(" ", "Kit listening on the debug port", true);
Line(">", "git status --short", false);
Line(" ", "M  MainWindow.xaml.cs", true);
Line(">", "_", false);

var well = KitUi.Surface(lines, 12, 6, "Kit.FieldBrush", "Kit.BorderBrush");
well.HorizontalAlignment = HorizontalAlignment.Left;

var caption = KitUi.Muted("The column is 18px, so output lines up under the command.", 10.5);
caption.Margin = new Thickness(0, 10, 0, 0);

return KitUi.Col(0, well, caption);
