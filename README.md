# KIT

A native WPF design library for programmer tools.

KIT is a blank canvas with a line rail. Add a line, name it, and keep a place for each design. It is deliberately small: one shell, five gem accents, light and dark, no account, and no third-party controls.

## Why

Most UI kits are built for marketing pages. Programmer tools need the opposite: dense rows, mono values, visible focus, and surfaces that stay out of the way.

KIT starts as the place where those designs live. The next layer is the component gallery: type, buttons, property rows, fields, and the coder / friend views.

## What works today

- Blank design canvas with a line rail
- Add a line by typing a name and pressing **Add** or **Enter**
- Categories in the rail, with designs underneath them
- Each design owns its own C# snippet: click it, the code and its preview open on the right
- Three collected look families in the rail: **mint outline**, **blue capsule**, **purple neon**
- A 50-design catalog in seven categories: chrome & nav, data display, inputs, overlays, gauges, layout, dev tools
- A style shelf: 20 button styles, 5 table styles and 10 type styles, rebuilt as valid WPF
- A fresh copy opens with 85 designs already in the rail: the catalog is embedded at build time, so the download is not an empty shelf. The source tree carries 108 in all
- Paste C# into the canvas, press **Run**, and KIT compiles it and shows the control underneath
- Rail operations: move up/down, copy, paste, delete (right-click a row); a category moves with its designs
- Lines and code are saved locally to `%LOCALAPPDATA%\Kit\designs.json`
- Five accents: Emerald, Rose, Sapphire, Amethyst, Amber
- Light and dark themes, switched live
- Native WPF controls; one package, the C# compiler (Roslyn)

## Requirements

- Windows 10 or 11
- .NET 8 SDK
- `Microsoft.CodeAnalysis.CSharp` (restored automatically by `dotnet run`)

## Run

```powershell
dotnet run
```

Or open `Kit.sln` in Visual Studio and press F5.

## Add a line

1. Type a name in the left rail.
2. Press **Enter** or click **Add**.
3. The line appears in the rail and the canvas shows its name.

The list is stored as JSON so it survives restarts. There is no cloud and no account.

## The paste box

Every line is a label plus a snippet. Select a label, and the right side shows that
label's code with its preview underneath. Press **Run** (or **Ctrl+Enter**) to compile
it; a clean run saves the code onto the label. Switching labels keeps the draft you
were typing, so nothing is lost if you move before pressing Run.

```csharp
return new PillButton { Content = "Save", Size = PillSize.Large };
```

A snippet can also be a whole class pasted in — KIT builds the first control it finds
with a parameterless constructor. Errors appear in mono under the Run button, with the
line number of your snippet, not of the wrapper around it.

No copy-paste at all: the `snippets/` folder holds whole runnable files, and **Open**
(or **Open snippet…** in the footer, or `Ctrl+O`) loads one straight into the editor and
runs it. They are excluded from the build, so they can live in the repo without
becoming part of the app.

## Rail

The rail is a small tree. **Add design** puts a design under the selected category,
**+ Category** adds a header. A header owns every row after it, up to the next header.

Rename a row by double-clicking it, pressing **F2**, or choosing **Rename** on the
right-click menu: the label turns into a text box, **Enter** keeps it, **Esc** cancels.
A name another row already uses gets a number, and an empty name is refused.

Right-click a row for **Move up**, **Move down**, **Copy**, **Paste**, **Delete**.
The same operations sit on the keyboard: `Ctrl+Up`, `Ctrl+Down`, `Ctrl+C`, `Ctrl+V`,
and `Delete`. Paste inserts right after the selection, code included, under a unique
name. Moving a category takes its designs along; deleting one removes its designs
after a confirmation.

## Button sizes

One black/white pill template, coded in C# (`Controls/PillButton.cs`), in six sizes. The radius is half the height, so every size is fully rounded, at any height you give it.

```xml
<controls:PillButton Content="Save" Size="Tiny" />
<controls:PillButton Content="Save" Size="Small" />
<controls:PillButton Content="Save" Size="Medium" />
<controls:PillButton Content="Save" Size="Large" />
<controls:PillButton Content="Save" Size="XLarge" />
<controls:PillButton Content="Save" Size="Huge" />
```

Sizes are 22, 26, 32, 38, 46, and 56 pixels tall. They are black on light and white on dark, and hover inverts them.

Theme it two ways: swap the `Kit.Mono*Brush` application resources, or override the brushes on a single button.

```csharp
var save = new PillButton
{
    Size = PillSize.Large,
    Content = "Save",
    Fill = Brushes.Black,
    Ink = Brushes.White,
    HoverFill = Brushes.White,
    HoverInk = Brushes.Black
};
```

Open **Button sizes** in the footer to preview all six.

## FlatButton

`Controls/FlatButton.cs` is the button you colour. Same shape rules, but the fill can
be any brush (solid or gradient), the radius is yours, and the hover state is explicit,
so a design keeps its own palette instead of KIT's black and white.

```csharp
return new FlatButton
{
    Content = "Backup Drivers",
    Height = 42,
    Radius = new CornerRadius(21),
    Fill = new LinearGradientBrush(
        Color.FromRgb(0x2C, 0x25, 0x36),
        Color.FromRgb(0x0B, 0x09, 0x10),
        90.0),
    Ink = Brushes.White,
    Edge = (Brush)new BrushConverter().ConvertFromString("#A93BF5"),
    HoverEdge = Brushes.White
};
```

Every brush is a dependency property: `Fill`, `Ink`, `Edge`, `HoverFill`, `HoverInk`,
`HoverEdge`, `FocusEdge`, plus `Radius`.

## Collected looks

These started as three working tools and were rebuilt here as portable pieces. They
are named for their palette, not for any product, so they can be reused as-is.

| Family | Designs | Files |
| --- | --- | --- |
| mint outline | tab tiles, list rows, detail flyout, rescan bar | `snippets/mint-*.cs` |
| blue capsule | header, device bar, partition cards, action bar | `snippets/blue-*.cs` |
| purple neon | title bar, underline tabs, neon buttons, console | `snippets/neon-*.cs` |

Every one of them is a normal snippet: select it in the rail to read the code, or open
the file with **Open** and paste it into another project. The `snippets/` folder is the
shareable part of KIT — add a file there, open a pull request, and the next person gets
the design for free.

## Catalog

Fifty working designs for the parts a tool actually needs, in the order a tool grows:

| Category | Designs |
| --- | --- |
| chrome & nav | expander tree, breadcrumb trail, tab strip, floating action menu, tree node, split pane header, segmented switch, step tracker |
| data display | dense data grid, property row, status badges, log entry card, metric card, filter tokens, timeline feed, code well |
| inputs | search with clear, floating label field, range slider, toggle switch, colour orbs, drop zone, password reveal, numeric stepper |
| overlays | banner alert, toast, modal scrim, command palette, context menu, rich tooltip |
| gauges | progress bar, spinner, activity grid, ring gauge, spark bars |
| layout | split workspace, card grid, collapsible section, status footer, tabbed inspector |
| dev tools | regex tester, terminal stream, HTTP verb badges, environment selector, diff viewer, connection status, keycap badge, insert flyout, error row, workspace switcher |

They live in `snippets/catalog/` as `01-expander-tree.cs` … `50-workspace-switcher.cs`.
The number picks the category, so adding a design is one file and one re-seed:

```powershell
powershell -ExecutionPolicy Bypass -File tools\seed-catalog.ps1
```

That script appends what is missing and leaves the designs you added by hand alone.

## Contributing

A design is one file. Copy `snippets/_template.cs` into the shelf that fits, number it
past the last one, run it in KIT, and open a pull request with that file alone.
[CONTRIBUTING.md](CONTRIBUTING.md) has the rules that get checked: theme keys instead of
hex, state instead of decoration, and the design still readable in light mode.

## Style shelf

The `WpfControlStyles` list that came in as a PDF is in here as working code. Two things
were worth fixing rather than copying: the printed XAML sets `CornerRadius` and
`TextDecoration` on a `Button`, which WPF does not have (the radius belongs to the
`Border` in the template, the decoration to the `TextBlock`), and every colour was a
fixed light-mode value. The shelf keeps the look and fixes both.

| Category | Designs | Files |
| --- | --- | --- |
| button styles | minimal flat, rounded primary, outlined gray, outlined success, danger solid, glass, raised 3D, pill, text underline, disabled, gradient blue, gradient purple, neon green, icon circular, floating action, dark solid, pastel, square sharp, hover fill, toggle | `snippets/styles/01-20` |
| table styles | minimal, bordered, dark striped, compact, hover highlight | `snippets/styles/21-25` |
| type styles | type scale, inline code, label pair, terminal prompt, tabular numbers, kicker block, code annotation, truncation, micro labels, links | `snippets/type/01-10` |

The 60-odd names the PDF lists after style 20 (`ButtonWarningYellow`, `ButtonCornerCut`
and the rest) have no code behind them, so there was nothing to copy. The type shelf is
the first pass at what a text style set should be: sizes, weights and the three lines
that live around a field.

## KitUi

`Controls/KitUi.cs` is the short hand the catalog is written in: one call for a line of
type, a card, a field, a pill, a chip, a keycap, a dot.

```csharp
return KitUi.Surface(KitUi.Col(
    8,
    KitUi.Head("Packing image"),
    KitUi.Muted("1.9 GB of 2.5 GB written"),
    KitUi.Pill("Cancel")));
```

Every helper points at a `Kit.*` resource key rather than a hex, so a design keeps
following the accent and the light/dark switch after it is built. That is the difference
between a screenshot and a control.

## Project layout

```text
kit/
  App.xaml              application entry and merged styles
  MainWindow.xaml       the shell: rail, blank canvas, theme controls
  MainWindow.xaml.cs    line library, selection, theme switching
  ButtonSizesWindow.xaml.cs  the six-size gallery, built in C#
  Controls/PillButton.cs     the pill button: template, six sizes, themeable brushes
  Controls/FlatButton.cs     a button you colour: any fill, any radius, explicit hover
  Controls/KitUi.cs          theme short hand: type, cards, fields, pills, chips, keycaps
  tools/seed-catalog.ps1     loads snippets/catalog into the rail, idempotently
  Library.cs            DesignEntry (name + code) and the local JSON store
  Rail.cs               move, copy, paste, delete for the line rail
  snippets/             runnable examples for the paste box, loaded with Open
  snippets/catalog/     the 50-design catalog
  snippets/styles/      button and table styles
  snippets/type/        type styles
  Controls/SnippetRunner.cs  compiles a pasted snippet and returns the control
  Theming/Theme.cs      five accents, light/dark palettes, live brushes
  Themes/Kit.xaml       native WPF styles: type, cards, rows, buttons, fields
  Kit.csproj
  Kit.sln
```

## Design rules

- Flat first for KIT's own chrome: solid surfaces, 1px borders, no gradients or shadows. Collected looks keep their own fill.
- Coder-grade density, friend-grade clarity.
- One text colour per light theme: the gem's deep ink, not slate.
- Neon belongs in fills, borders, selection, and the status dot — not in body text.
- 1px outlines, 10px radius, no giant marketing whitespace.
- No packages. If WPF can do it, use WPF.

## Status

Early. `v0.1` is the shell: line library, local persistence, theme switching, and a blank canvas. Component previews come next.

## License

MIT. See [  LICENSE
  CONTRIBUTING.md       how to add one design, and what gets checked
](  LICENSE
  CONTRIBUTING.md       how to add one design, and what gets checked
).
