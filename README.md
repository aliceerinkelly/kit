# KIT

A design library for programmer tools.

KIT is a native Windows app full of finished interface pieces — buttons, tables, tabs, tooltips, log rows, gauges, dialogs — and it shows you the C# behind every one of them. Pick a name on the left, read the code on the right, press **Run**, and the real thing draws itself in the preview pane, using the real theme and the real styles.

Nothing here is a picture of a widget. Every entry is code that compiles and runs.

## Requirements

- Windows 10 or 11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Build and run it

```
dotnet build
dotnet run
```

Seven files, no other setup. The first build restores `Microsoft.CodeAnalysis.CSharp`, which is what compiles your snippet live.

To make one self-contained exe that runs on a machine with nothing installed:

```
dotnet publish -c Release -r win-x64 --self-contained true ^
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:EnableCompressionInSingleFile=true -o publish
```

`publish\Kit.exe` is the whole app — about 74 MB, no installer, no runtime download.

## What's in the box

108 designs under 11 categories:

| Category | What's in it |
|---|---|
| chrome & nav | expander trees, breadcrumbs, tab strips, segmented switches, steppers |
| data display | dense tables, property rows, badges, log cards, metric cards, timelines |
| inputs | search fields, floating labels, sliders, toggles, swatch pickers, drop zones |
| overlays | banners, toasts, modal scrims, command palettes, context menus, tooltips |
| gauges | progress bars, spinners, heatmaps, rings, resource bars |
| layout | split panes, card grids, collapsible panels, status bars, inspector tabs |
| dev tools | regex testers, terminals, HTTP badges, env pickers, diffs, shortcut keycaps |
| button styles | six pill sizes, flat buttons, ghost rows, orb and mono looks |
| table styles | row, header and cell treatments |
| type styles | headings, body, muted, monospace and inline code |
| collected looks | whole screens assembled from the pieces above |

## The files

| File | What it holds |
|---|---|
| `Kit.csproj` | project, metadata, the app icon |
| `App.xaml` | every style and brush key, inlined |
| `App.xaml.cs` | startup: theme on, window up |
| `MainWindow.xaml` | the window's shape |
| `Kit.cs` | all C# — theme, rail, store, both button types, helpers, snippet compiler, dialogs |
| `Designs.cs` | the shipped library, as code, in rail order |
| `app.ico` | the mark, seven sizes from 16 to 256 |

## Theme it with brushes, never with colours

A snippet asks the theme for its colours by name, so it follows whichever accent and light/dark mode is on:

```csharp
var card = new Border();
card.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
card.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
```

Brush keys: `Kit.AccentBrush`, `Kit.AccentDeepBrush`, `Kit.AccentSoftBrush`, `Kit.AccentInkBrush`, `Kit.OnAccentBrush`, `Kit.BackgroundBrush`, `Kit.PanelBrush`, `Kit.CardBrush`, `Kit.RowBrush`, `Kit.FieldBrush`, `Kit.HoverBrush`, `Kit.TextBrush`, `Kit.MutedBrush`, `Kit.BorderBrush`, `Kit.OutlineBrush`, `Kit.PrimaryBrush`, `Kit.PillBrush`, `Kit.MonoFillBrush`, `Kit.MonoInkBrush`, `Kit.UiFont`, `Kit.MonoFont`.

Five accents — Emerald, Rose, Sapphire, Amethyst, Amber — each in light and dark.

## How a design is written

A snippet is a plain C# body, not a class. It gets the usings it needs, so it starts straight at `var`. It must return a `UIElement`.

```csharp
// Chips: the pill at Tiny, quiet until you hover it.
var wrap = new WrapPanel();

foreach (var tag in new[] { "usb", "fat32", "iso" })
{
    wrap.Children.Add(new PillButton
    {
        Content = tag,
        Size = PillSize.Tiny,
        Margin = new Thickness(0, 0, 6, 6)
    });
}

return wrap;
```

Helpers save the boilerplate: `KitUi.Text`, `KitUi.Muted`, `KitUi.Head`, `KitUi.Mono`, `KitUi.Col`, `KitUi.Row`, `KitUi.Surface`, `KitUi.Rule`, `KitUi.Chip`, `KitUi.Keycap`, `KitUi.Swatch`, `KitUi.Pill`. Buttons come in `PillButton` with six sizes (`Tiny`, `Small`, `Medium`, `Large`, `XLarge`, `Huge`) and `FlatButton` in black or white mono. `KitUi.Pill(text, active, height)` draws a quiet rail row on top of `FlatButton`, which is what the category rows in the app's own left rail use.

## Adding a design

**In the app.** Add a line in the rail, paste your code on the right, press **Ctrl+Enter** or click **Run**. It compiles, renders, and saves itself onto that line. Your rail lives at `%LOCALAPPDATA%\Kit\designs.json`, so it survives a rebuild.

**In the source, to ship it with KIT.** Add a row to `Designs.cs` in the order you want it to appear:

```csharp
new(false, "my design", """
    // what it is, in one line
    var card = KitUi.Surface(KitUi.Text("hello"), 14, 8, "Kit.CardBrush");
    return card;
    """),
```

A `new(true, "category name", null)` row starts a new category. The rows under it belong to it.

## Sharing

KIT is meant to be copied, forked and added to. If you write a design worth keeping, send it back — one row in `Designs.cs` is all it takes, and everyone who pulls after you gets it.
