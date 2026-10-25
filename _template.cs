// Design name: the one decision this file makes, in a sentence.
//
// Copy me to snippets/catalog/51-your-name.cs (styles: 26-, type: 11-), then:
//   dotnet run  ->  Open  ->  Run  ->  light, dark, five accents  ->  seed -Refresh
//
// No usings here: System, System.Windows, System.Windows.Controls, System.Windows.Media
// and Kit.Controls are already imported, and the file runs as the body of Build(), so it
// has to return one control. Theme with Kit.* keys, never a hex. See CONTRIBUTING.md.

var card = KitUi.Surface(
    KitUi.Col(
        6,
        KitUi.Head("Something useful"),
        KitUi.Muted("What it does, in one line.")),
    14,
    8,
    "Kit.CardBrush");

card.Width = 280;
card.HorizontalAlignment = HorizontalAlignment.Left;
return card;
