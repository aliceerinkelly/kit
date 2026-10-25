# Contributing

One file is a whole contribution. You do not need to touch the app, the theme, or the rail —
copy a template, write the design, send the file.

## Add a design

1. Copy `snippets/_template.cs` to the shelf that fits:

   | Shelf | Folder | Numbers taken | Start at |
   | --- | --- | --- | --- |
   | Parts of a tool | `snippets/catalog/` | 01–50 | `51-your-design.cs` |
   | Button and table styles | `snippets/styles/` | 01–25 | `26-your-style.cs` |
   | Type styles | `snippets/type/` | 01–10 | `11-your-style.cs` |

   The file name is `<number>-<kebab-name>.cs`, lower case. The number picks the rail
   category and the rest becomes the row name, so `51-inline-diff.cs` reads as
   **inline diff**.

2. Write the snippet and run it (below).
3. Load it into the rail:

   ```powershell
   powershell -ExecutionPolicy Bypass -File tools\seed-catalog.ps1 -Refresh
   ```

4. Open a pull request with that one file. `-Refresh` is how a change to the code of a
   design already in the rail gets picked up without deleting the row first.

A number past the last declared range still lands, under a **new designs** category (or
**new styles**, **new type**), so a first-timer's file is never silently dropped — a
maintainer folds it into a real category afterwards. Only a brand-new shelf needs one
line in the `$sets` table of `tools/seed-catalog.ps1`.

## How a snippet works

- **No usings.** `System`, `System.Windows`, `System.Windows.Controls`,
  `System.Windows.Media` and `Kit.Controls` are already imported.
- The file runs as the body of `Build()`, so it has to **return one control**.
- Local functions are fine, and so is a whole class pasted in — KIT builds the first
  control it finds with a parameterless constructor.
- `Controls/KitUi.cs` is the short hand: `Text`, `Muted`, `Head`, `Mono`, `Col`, `Row`,
  `Surface`, `Rule`, `Input`, `Pill`, `Quiet`, `Dot`, `Chip`, `Keycap`.

## Rules that actually get checked

- **Theme it.** Point at `Kit.*` resource keys — `Kit.TextBrush`, `Kit.MutedBrush`,
  `Kit.BorderBrush`, `Kit.RowBrush`, `Kit.CardBrush`, `Kit.PanelBrush`, `Kit.FieldBrush`,
  `Kit.HoverBrush`, `Kit.AccentBrush`. Where a helper doesn't cover it, use
  `SetResourceReference`. A fixed hex is allowed only when the design owns its whole
  palette (the collected looks do), and then it still has to hold up in light mode.
- **Check it in light, in dark, and on all five accents** before sending. The most
  common reason a design gets sent back is that it disappears in light mode.
- **Give it a state.** Hover, pressed, focused, disabled, empty — a control that can't
  change is a picture of a control.
- **1px edges, 10px radius, coder density.** No marketing whitespace, no drop shadows
  standing in for hierarchy.
- **One idea per file.** Two ideas is two files.
- **First line is a comment** naming the design and the decision behind it. It is the
  first thing a human reads:
  `// Metric card: one number, its delta, and a quiet scale under it.`
- **Name it for what it does**, never for the app it came from. No product names, no
  personal names, no screenshots of someone's tool.
- **WPF only.** No packages. If WPF can draw it, draw it.

## Test it before you send it

```powershell
dotnet run
```

In KIT: **Open** your file (or `Ctrl+O`), press **Run**, and the preview appears under
the code. Errors are reported against your line numbers. Then switch theme with the
mode pill and click through the five accents — that is the review.

After that, seed and confirm it appears under the right category in the rail.

## What gets turned away

- Decoration with no state behind it.
- A palette that breaks in light or dark.
- A design that needs a package to compile.
- A file that doesn't compile — a design is not done until the preview draws.

## License

KIT is MIT. Contributions come in under the same licence, which is what lets the next
person use your design for free.
