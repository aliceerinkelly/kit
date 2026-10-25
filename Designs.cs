// The design library: every shipped design, as code, in rail order.
// A header row starts a category; the rows after it belong to it.

namespace Kit;

public static class Designs
{
    public readonly record struct Row(bool IsHeader, string Name, string? Code);

    public static readonly Row[] Rail =
    [
        new(true, "chrome & nav", null),
        new(false, "expander tree", """
            // Expander tree: a category header you can fold, with quiet rail rows under it.
            var header = new Border
            {
                Padding = new Thickness(14, 8, 14, 8),
                Child = KitUi.Row(8, KitUi.Muted("\u25BE", 12), KitUi.Head("Core Modules", 13))
            };

            header.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

            var body = new Border
            {
                Padding = new Thickness(14, 9, 14, 10),
                Child = KitUi.Col(
                    3,
                    KitUi.Pill("Dashboard", active: true, height: 28),
                    KitUi.Pill("Diagnostics", height: 28),
                    KitUi.Pill("Change log", height: 28))
            };

            var card = KitUi.Surface(KitUi.Col(0, header, body), pad: 0, radius: 8, fill: "Kit.PanelBrush");
            card.Width = 300;
            card.HorizontalAlignment = HorizontalAlignment.Left;

            return card;

            """),
        new(false, "breadcrumb trail", """
            // Breadcrumb trail: chevrons between crumbs, the last one carrying the accent.
            var crumbs = new[] { "Workspace", "Projects", "Kit.sln", "Snippets" };
            var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };

            for (var i = 0; i < crumbs.Length; i++)
            {
                var last = i == crumbs.Length - 1;

                if (i > 0)
                {
                    var separator = KitUi.Text("\u203A", 12.5, "Kit.BorderBrush");
                    separator.Margin = new Thickness(7, 0, 7, 0);
                    separator.VerticalAlignment = VerticalAlignment.Center;
                    row.Children.Add(separator);
                }

                var crumb = KitUi.Text(crumbs[i], 12.5, last ? "Kit.AccentBrush" : "Kit.MutedBrush");

                if (last)
                {
                    crumb.FontWeight = FontWeights.SemiBold;
                }

                row.Children.Add(crumb);
            }

            return KitUi.Surface(row, 11, 6, "Kit.RowBrush", "Kit.BorderBrush");

            """),
        new(false, "tab strip", """
            // Tab strip: file tabs, the live one carrying a bottom accent line and a close badge.
            var tabs = new StackPanel { Orientation = Orientation.Horizontal };
            var names = new[] { "ServerMonitor.cs", "Theme.xaml", "Program.cs" };

            for (var i = 0; i < names.Length; i++)
            {
                var active = i == 0;

                var label = KitUi.Text(names[i], 11.5, active ? "Kit.TextBrush" : "Kit.MutedBrush");
                label.Margin = new Thickness(0, 0, 9, 0);

                var row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center
                };

                row.Children.Add(label);

                if (active)
                {
                    row.Children.Add(KitUi.Quiet("\u00D7"));
                }

                var cell = new Border
                {
                    Padding = new Thickness(12, 8, 8, 8),
                    Background = Brushes.Transparent,
                    Child = row
                };

                if (active)
                {
                    cell.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
                }

                var underline = new Border { Height = 2 };
                underline.SetResourceReference(Border.BackgroundProperty, active ? "Kit.AccentBrush" : "Kit.PanelBrush");

                var column = new StackPanel();
                column.Children.Add(cell);
                column.Children.Add(underline);
                tabs.Children.Add(column);
            }

            var strip = KitUi.Surface(tabs, pad: 0, radius: 8, fill: "Kit.PanelBrush");
            strip.HorizontalAlignment = HorizontalAlignment.Left;
            return strip;

            """),
        new(false, "floating action menu", """
            // Floating action menu: an accent orb that opens a stack of high-frequency actions.
            var actions = KitUi.Col(
                8,
                KitUi.Pill("New design", height: 30),
                KitUi.Pill("Import code", height: 30),
                KitUi.Pill("Export library", height: 30));

            var orb = new FlatButton
            {
                Content = "+",
                Width = 48,
                Height = 48,
                Radius = new CornerRadius(24),
                FontSize = 22,
                FontWeight = FontWeights.Light,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            KitUi.Skin(orb, FlatButton.FillProperty, "Kit.AccentBrush");
            KitUi.Skin(orb, FlatButton.InkProperty, "Kit.OnAccentBrush");
            KitUi.Skin(orb, FlatButton.EdgeProperty, "Kit.AccentBrush");
            KitUi.Skin(orb, FlatButton.HoverFillProperty, "Kit.AccentDeepBrush");
            KitUi.Skin(orb, FlatButton.HoverInkProperty, "Kit.OnAccentBrush");
            KitUi.Skin(orb, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");

            orb.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 22,
                ShadowDepth = 4,
                Opacity = 0.35,
                Color = Colors.Black
            };

            var stack = KitUi.Col(12, actions, orb);
            stack.HorizontalAlignment = HorizontalAlignment.Right;
            stack.VerticalAlignment = VerticalAlignment.Bottom;

            var stage = new Grid { Width = 320, Height = 240, HorizontalAlignment = HorizontalAlignment.Left };
            stage.Children.Add(KitUi.Surface(new Grid(), pad: 0, radius: 10, fill: "Kit.PanelBrush"));
            stage.Children.Add(new Border
            {
                Padding = new Thickness(16),
                Child = stack,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom
            });

            return stage;

            """),
        new(false, "tree node", """
            // Tree node: fold bullets, and one open branch with its children indented under it.
            StackPanel Node(string label, int depth, bool open)
            {
                var bullet = KitUi.Muted(open ? "\u25BE" : "\u2022", 11);
                bullet.Width = 14;
                bullet.VerticalAlignment = VerticalAlignment.Center;

                var text = KitUi.Text(label, 12.5, open ? "Kit.TextBrush" : "Kit.MutedBrush");
                text.VerticalAlignment = VerticalAlignment.Center;

                var row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(depth * 16, 0, 0, 0)
                };

                row.Children.Add(bullet);
                row.Children.Add(text);
                return row;
            }

            var tree = KitUi.Col(
                7,
                Node("Kit", 0, true),
                Node("Controls", 1, true),
                Node("PillButton", 2, false),
                Node("FlatButton", 2, false),
                Node("KitUi", 2, false),
                Node("Snippets", 1, true),
                Node("catalog", 2, false),
                Node("Views", 1, false));

            var card = KitUi.Surface(tree, 13, 8);
            card.Width = 280;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "split pane header", """
            // Split pane header: a panel title with a pin toggle, over the panel body.
            var title = KitUi.Head("Properties Inspector", 13);
            title.VerticalAlignment = VerticalAlignment.Center;

            var pin = KitUi.Quiet("\uD83D\uDCCC", 26, 30);

            var header = new Grid();
            header.ColumnDefinitions.Add(new ColumnDefinition());
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            header.Children.Add(title);
            Grid.SetColumn(pin, 1);
            header.Children.Add(pin);

            var bar = new Border { Padding = new Thickness(13, 8, 8, 8), Child = header };
            bar.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

            var body = new Border
            {
                Padding = new Thickness(13, 12, 13, 14),
                Child = KitUi.Col(
                    6,
                    KitUi.Mono("Width", 11, "Kit.MutedBrush"),
                    KitUi.Muted("340 px  \u00B7  pinned to the right edge", 11.5))
            };

            var panel = KitUi.Surface(KitUi.Col(0, bar, KitUi.Rule(), body), pad: 0, radius: 8, fill: "Kit.PanelBrush");
            panel.Width = 340;
            panel.HorizontalAlignment = HorizontalAlignment.Left;
            return panel;

            """),
        new(false, "segmented switch", """
            // Segmented switch: three view modes in one track, the live one filled with the accent.
            var grid = new System.Windows.Controls.Primitives.UniformGrid { Columns = 3, Height = 32 };
            var labels = new[] { "Grid", "List", "Raw" };

            for (var i = 0; i < labels.Length; i++)
            {
                var segment = KitUi.Pill(labels[i], active: i == 0, height: 26);
                segment.Margin = new Thickness(3);
                segment.Radius = new CornerRadius(5);
                grid.Children.Add(segment);
            }

            var track = KitUi.Surface(grid, pad: 0, radius: 8, fill: "Kit.RowBrush");
            track.Width = 300;
            track.HorizontalAlignment = HorizontalAlignment.Left;
            return track;

            """),
        new(false, "step tracker", """
            // Step tracker: numbered nodes for done, current and pending, joined by accent rules.
            var steps = new[] { "Plan", "Build", "Review", "Ship" };
            var track = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Top };

            for (var i = 0; i < steps.Length; i++)
            {
                var done = i == 0;
                var active = i == 1;

                var number = KitUi.Text((i + 1).ToString(), 11);
                number.HorizontalAlignment = HorizontalAlignment.Center;
                number.VerticalAlignment = VerticalAlignment.Center;
                number.FontWeight = FontWeights.Bold;

                var node = new Border
                {
                    Width = 24,
                    Height = 24,
                    CornerRadius = new CornerRadius(12),
                    BorderThickness = new Thickness(1),
                    Child = number
                };

                if (done)
                {
                    node.SetResourceReference(Border.BackgroundProperty, "Kit.AccentSoftBrush");
                    node.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");
                    number.SetResourceReference(TextBlock.ForegroundProperty, "Kit.AccentInkBrush");
                }
                else if (active)
                {
                    node.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
                    node.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");
                    number.SetResourceReference(TextBlock.ForegroundProperty, "Kit.OnAccentBrush");
                }
                else
                {
                    node.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
                    node.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
                    number.SetResourceReference(TextBlock.ForegroundProperty, "Kit.MutedBrush");
                }

                var label = KitUi.Text(steps[i], 10.5, active || done ? "Kit.TextBrush" : "Kit.MutedBrush");
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Margin = new Thickness(0, 6, 0, 0);

                var step = new StackPanel { Margin = new Thickness(0, 0, 10, 0) };
                step.Children.Add(node);
                step.Children.Add(label);
                track.Children.Add(step);

                if (i < steps.Length - 1)
                {
                    var rule = new Border
                    {
                        Height = 2,
                        Width = 34,
                        Margin = new Thickness(0, 11, 10, 0),
                        VerticalAlignment = VerticalAlignment.Top
                    };

                    rule.SetResourceReference(Border.BackgroundProperty, done ? "Kit.AccentBrush" : "Kit.BorderBrush");
                    track.Children.Add(rule);
                }
            }

            var card = KitUi.Surface(track, 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(true, "data display", null),
        new(false, "dense data grid", """
            // Dense data grid: 24px rows, mono numbers and a load bar in the last column.
            var rows = new (string Name, string Cpu, string Mem, double Load)[]
            {
                ("PipelineWorker", "12.4%", "184 MB", 0.34),
                ("Indexer", "8.1%", "96 MB", 0.22),
                ("Watcher", "3.7%", "42 MB", 0.11),
                ("Renderer", "22.9%", "512 MB", 0.61)
            };

            var table = new StackPanel { Width = 520 };

            Grid Line(double height)
            {
                var grid = new Grid { Height = height };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(68) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(78) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                return grid;
            }

            void Put(Grid grid, UIElement element, int column)
            {
                Grid.SetColumn(element, column);
                grid.Children.Add(element);
            }

            var head = Line(20);
            Put(head, KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush"), 0);
            Put(head, KitUi.Mono("CPU", 9.5, "Kit.MutedBrush"), 1);
            Put(head, KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush"), 2);
            Put(head, KitUi.Mono("LOAD", 9.5, "Kit.MutedBrush"), 3);
            table.Children.Add(head);

            foreach (var (name, cpu, mem, load) in rows)
            {
                var row = Line(26);
                Put(row, KitUi.Text(name, 12), 0);
                Put(row, KitUi.Mono(cpu, 11), 1);
                Put(row, KitUi.Mono(mem, 11, "Kit.MutedBrush"), 2);

                var track = new Border { Height = 4, CornerRadius = new CornerRadius(2), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 8, 0) };
                track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

                var fill = new Border { Width = 64 * load, Height = 4, CornerRadius = new CornerRadius(2), HorizontalAlignment = HorizontalAlignment.Left };
                fill.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");

                var bar = new Grid();
                bar.Children.Add(track);
                bar.Children.Add(fill);
                Put(row, bar, 3);

                table.Children.Add(row);
                table.Children.Add(KitUi.Rule());
            }

            var card = KitUi.Surface(table, 12, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "property row", """
            // Property inspector rows: bold keys left, editable mono values right.
            var body = new StackPanel { Width = 420 };

            void Row(string key, string value, bool mono)
            {
                var grid = new Grid { Margin = new Thickness(0, 6, 0, 6) };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(130) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                var label = KitUi.Text(key, 11.5, "Kit.MutedBrush");
                label.VerticalAlignment = VerticalAlignment.Center;

                var field = KitUi.Input(value, 28, mono ? 11 : 12);

                if (mono)
                {
                    field.SetResourceReference(TextBox.FontFamilyProperty, "Kit.MonoFont");
                }

                Grid.SetColumn(field, 1);
                grid.Children.Add(label);
                grid.Children.Add(field);
                body.Children.Add(grid);
            }

            Row("Timeout (ms)", "5000", true);
            Row("Worker threads", "8", true);
            Row("Output path", "C:\\Workspace\\build", true);
            Row("Keep symbols", "true", false);

            var card = KitUi.Surface(body, 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "status badges", """
            // Status badges: semantic pills with a soft fill, a dot and bold small caps.
            Border Badge(string text, Color tint)
            {
                var ink = new SolidColorBrush(tint);

                var dot = new Border
                {
                    Width = 6,
                    Height = 6,
                    CornerRadius = new CornerRadius(3),
                    Background = ink,
                    Margin = new Thickness(0, 0, 6, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };

                var label = new TextBlock
                {
                    Text = text,
                    FontSize = 10,
                    FontWeight = FontWeights.Bold,
                    Foreground = ink
                };

                var content = new StackPanel { Orientation = Orientation.Horizontal };
                content.Children.Add(dot);
                content.Children.Add(label);

                return new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(34, tint.R, tint.G, tint.B)),
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(8, 3, 8, 3),
                    Margin = new Thickness(0, 0, 8, 0),
                    Child = content
                };
            }

            var row = new StackPanel { Orientation = Orientation.Horizontal };
            row.Children.Add(Badge("ONLINE", Color.FromRgb(0x10, 0xB9, 0x81)));
            row.Children.Add(Badge("DEGRADED", Color.FromRgb(0xF5, 0x9E, 0x0B)));
            row.Children.Add(Badge("CRITICAL", Color.FromRgb(0xEF, 0x44, 0x44)));

            var caption = KitUi.Muted("Six pools reporting \u00B7 last poll 2s ago", 11);
            caption.Margin = new Thickness(0, 10, 0, 0);

            var card = KitUi.Surface(KitUi.Col(0, row, caption), 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "log entry card", """
            // Log entry card: timestamp, level, summary, and the stack trace under it.
            var level = KitUi.Chip("ERROR", "Kit.RowBrush", "Kit.TextBrush");
            level.Padding = new Thickness(7, 2, 7, 2);

            var line = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            var stamp = KitUi.Mono("[11:02:45]", 11, "Kit.MutedBrush");
            stamp.Margin = new Thickness(0, 0, 9, 0);
            stamp.VerticalAlignment = VerticalAlignment.Center;
            line.Children.Add(stamp);
            line.Children.Add(level);

            var summary = KitUi.Text("Exception thrown in PipelineWorker", 11.5, "Kit.TextBrush");
            summary.Margin = new Thickness(0, 0, 0, 8);

            var trace = KitUi.Col(
                3,
                KitUi.Mono("System.NullReferenceException: Object reference", 11, "Kit.MutedBrush"),
                KitUi.Mono("   at PipelineWorker.Run(Job job)", 11, "Kit.MutedBrush"),
                KitUi.Mono("   at Scheduler.Tick()", 11, "Kit.MutedBrush"));

            var well = KitUi.Surface(trace, 10, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            well.Margin = new Thickness(0, 0, 0, 0);

            var chevron = KitUi.Row(
                6,
                KitUi.Muted("\u25BE", 10),
                KitUi.Muted("Stack trace", 10.5));

            var card = KitUi.Surface(KitUi.Col(0, line, summary, well, chevron), 12, 8, "Kit.CardBrush");
            card.Width = 460;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "metric card", """
            // Metric card: one number, its delta, and a quiet scale under it.
            var value = KitUi.Text("42.8%", 26, "Kit.TextBrush");
            value.FontWeight = FontWeights.Bold;
            value.Margin = new Thickness(0, 3, 0, 0);

            var deltaRow = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 0) };
            deltaRow.Children.Add(new TextBlock
            {
                Text = "\u2191",
                FontSize = 10.5,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x10, 0xB9, 0x81)),
                Margin = new Thickness(0, 0, 4, 0)
            });
            deltaRow.Children.Add(new TextBlock
            {
                Text = "3.2% from baseline",
                FontSize = 10.5,
                Foreground = new SolidColorBrush(Color.FromRgb(0x10, 0xB9, 0x81))
            });

            var spark = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
            double[] heights = [4, 7, 5, 10, 8, 12, 9, 14, 11, 16];

            foreach (var height in heights)
            {
                var bar = new Border
                {
                    Width = 6,
                    Height = height,
                    CornerRadius = new CornerRadius(2),
                    Margin = new Thickness(0, 0, 3, 0),
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                bar.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
                spark.Children.Add(bar);
            }

            var card = KitUi.Surface(
                KitUi.Col(0, KitUi.Muted("CPU USAGE", 10), value, deltaRow, spark),
                14,
                8,
                "Kit.CardBrush");

            card.Width = 190;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "filter tokens", """
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

            """),
        new(false, "timeline feed", """
            // Timeline feed: one hairline down the left, an accent dot per event.
            var events = new (string When, string What, bool Live)[]
            {
                ("09:12", "v1.5.0 tagged and built", true),
                ("09:04", "Snippet catalog compiled clean", false),
                ("08:47", "Rail gained seven categories", false),
                ("08:20", "FlatButton padding fixed", false)
            };

            var feed = new StackPanel { Width = 420 };

            for (var i = 0; i < events.Length; i++)
            {
                var (when, what, live) = events[i];

                var dot = new Border
                {
                    Width = 9,
                    Height = 9,
                    CornerRadius = new CornerRadius(5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 4, 0, 0),
                    BorderThickness = new Thickness(2)
                };

                dot.SetResourceReference(Border.BackgroundProperty, live ? "Kit.AccentBrush" : "Kit.CardBrush");
                dot.SetResourceReference(Border.BorderBrushProperty, live ? "Kit.AccentBrush" : "Kit.BorderBrush");

                var line = new Border { Width = 1, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(4, 0, 0, 0) };
                line.SetResourceReference(Border.BackgroundProperty, "Kit.BorderBrush");

                var text = new StackPanel { Margin = new Thickness(14, 0, 0, 0) };
                text.Children.Add(KitUi.Text(what, 12, live ? "Kit.TextBrush" : "Kit.MutedBrush"));

                var stamp = KitUi.Mono(when, 10, "Kit.MutedBrush");
                stamp.Margin = new Thickness(0, 2, 0, 0);
                text.Children.Add(stamp);

                var cell = new Grid { Margin = new Thickness(0, 0, 0, i == events.Length - 1 ? 0 : 10) };
                cell.Children.Add(line);
                cell.Children.Add(dot);
                cell.Children.Add(text);
                feed.Children.Add(cell);
            }

            var card = KitUi.Surface(feed, 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "code well", """
            // Code well: a mono block with line numbers and a copy badge pinned to the corner.
            var lines = new StackPanel();

            string[] source =
            [
                "{",
                "  \"name\": \"KIT\",",
                "  \"version\": \"1.5.0\",",
                "  \"target\": \"net8.0-windows\",",
                "  \"designs\": 84",
                "}"
            ];

            for (var i = 0; i < source.Length; i++)
            {
                var row = new StackPanel { Orientation = Orientation.Horizontal };

                var number = KitUi.Mono((i + 1).ToString(), 10.5, "Kit.BorderBrush");
                number.Width = 22;

                row.Children.Add(number);
                row.Children.Add(KitUi.Mono(source[i], 11.5, "Kit.TextBrush"));
                lines.Children.Add(row);
            }

            var copy = KitUi.Quiet("Copy", 22);
            copy.HorizontalAlignment = HorizontalAlignment.Right;
            copy.VerticalAlignment = VerticalAlignment.Top;

            var frame = new Grid();
            frame.Children.Add(lines);
            frame.Children.Add(copy);

            var well = KitUi.Surface(frame, 12, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            well.Width = 360;
            well.HorizontalAlignment = HorizontalAlignment.Left;
            return well;

            """),
        new(true, "inputs", null),
        new(false, "search with clear", """
            // Search field: leading glyph, live text, and a clear action that sits inside the field.
            var glyph = KitUi.Muted("\u2315", 15);
            glyph.Margin = new Thickness(0, 0, 8, 0);
            glyph.VerticalAlignment = VerticalAlignment.Center;

            var box = new TextBox
            {
                Text = "ahci",
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                FontSize = 12,
                VerticalContentAlignment = VerticalAlignment.Center,
                Width = 214
            };

            box.SetResourceReference(TextBox.ForegroundProperty, "Kit.TextBrush");
            box.SetResourceReference(TextBox.CaretBrushProperty, "Kit.AccentBrush");

            var clear = KitUi.Quiet("\u00D7", 22, 22);

            var row = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            row.Children.Add(glyph);
            row.Children.Add(box);
            row.Children.Add(clear);

            var field = KitUi.Surface(row, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            field.Width = 320;
            field.HorizontalAlignment = HorizontalAlignment.Left;
            return field;

            """),
        new(false, "floating label field", """
            // Floating label field: the caption rides the top of the field, the value sits under it.
            Border Field(string caption, string value)
            {
                var label = KitUi.Text(caption, 9.5, "Kit.AccentBrush");
                label.FontWeight = FontWeights.SemiBold;

                var input = new TextBox
                {
                    Text = value,
                    BorderThickness = new Thickness(0),
                    Background = Brushes.Transparent,
                    FontSize = 12.5,
                    Padding = new Thickness(0, 3, 0, 0)
                };

                input.SetResourceReference(TextBox.ForegroundProperty, "Kit.TextBrush");
                input.SetResourceReference(TextBox.CaretBrushProperty, "Kit.AccentBrush");

                var field = KitUi.Surface(KitUi.Col(0, label, input), 10, 6, "Kit.FieldBrush", "Kit.AccentBrush");
                field.Width = 320;
                field.HorizontalAlignment = HorizontalAlignment.Left;
                return field;
            }

            return KitUi.Col(0, Field("DATABASE CONNECTION STRING", "Server=localhost;Port=5432"), Field("DATABASE NAME", "kit_catalog"));

            """),
        new(false, "range slider", """
            // Range slider: a slim track, an accent fill and a readout on the right.
            var track = new Border { Height = 4, CornerRadius = new CornerRadius(2), VerticalAlignment = VerticalAlignment.Center };
            track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

            var fill = new Border
            {
                Width = 132,
                Height = 4,
                CornerRadius = new CornerRadius(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            fill.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");

            var thumb = new Border
            {
                Width = 14,
                Height = 14,
                CornerRadius = new CornerRadius(7),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(125, 0, 0, 0),
                BorderThickness = new Thickness(2)
            };
            thumb.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
            thumb.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");

            var scale = new Grid { Width = 220, Height = 22 };
            scale.Children.Add(track);
            scale.Children.Add(fill);
            scale.Children.Add(thumb);

            var value = KitUi.Mono("55", 12);
            value.VerticalAlignment = VerticalAlignment.Center;

            var row = new StackPanel { Orientation = Orientation.Horizontal };
            row.Children.Add(scale);
            row.Children.Add(new Border { Width = 12 });
            row.Children.Add(value);

            var ticks = new StackPanel { Orientation = Orientation.Horizontal, Width = 250, Margin = new Thickness(0, 4, 0, 0) };

            foreach (var tick in new[] { "0", "25", "50", "75", "100" })
            {
                var label = KitUi.Mono(tick, 9.5, "Kit.MutedBrush");
                label.Width = 50;
                ticks.Children.Add(label);
            }

            var card = KitUi.Surface(
                KitUi.Col(0, KitUi.Muted("OPACITY", 10), row, ticks),
                14,
                8,
                "Kit.PanelBrush");

            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "toggle switch", """
            // Toggle switch: a sliding indicator, and the sentence that explains what it does.
            FrameworkElement Toggle(string title, string detail, bool on)
            {
                var track = new Border
                {
                    Width = 38,
                    Height = 20,
                    CornerRadius = new CornerRadius(10),
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 11, 0)
                };

                var knob = new Border
                {
                    Width = 14,
                    Height = 14,
                    CornerRadius = new CornerRadius(7),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = on ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                    Margin = new Thickness(3, 0, 3, 0)
                };

                knob.SetResourceReference(Border.BackgroundProperty, on ? "Kit.OnAccentBrush" : "Kit.MutedBrush");

                if (on)
                {
                    track.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
                }
                else
                {
                    track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
                    track.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
                    track.BorderThickness = new Thickness(1);
                }

                track.Child = knob;

                var copy = KitUi.Col(2, KitUi.Text(title, 12), KitUi.Muted(detail, 10.5));
                copy.VerticalAlignment = VerticalAlignment.Center;

                var row = new StackPanel { Orientation = Orientation.Horizontal };
                row.Children.Add(track);
                row.Children.Add(copy);
                return row;
            }

            var card = KitUi.Surface(
                KitUi.Col(
                    14,
                    Toggle("Hardware acceleration", "Uses the GPU for the live preview", true),
                    Toggle("Write protection", "Blocks sector writes on removable disks", false)),
                14,
                8,
                "Kit.PanelBrush");

            card.Width = 360;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "colour orbs", """
            // Colour orbs: the six accents as circles, the live one wearing a glow ring.
            var orbs = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            var palette = new (string Name, string Hex)[] 
            {
                ("Emerald", "#00FF9C"),
                ("Rose", "#FF2E88"),
                ("Sapphire", "#22D3FF"),
                ("Amethyst", "#B14BFF"),
                ("Amber", "#FFE600")
            };

            foreach (var (name, hex) in palette)
            {
                var selected = name == "Sapphire";
                var ring = KitUi.Swatch(hex, 24);
                ring.BorderBrush = selected ? Brushes.White : Brushes.Transparent;

                if (selected)
                {
                    ring.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        BlurRadius = 14,
                        ShadowDepth = 0,
                        Opacity = 0.5,
                        Color = (Color)ColorConverter.ConvertFromString(hex)
                    };
                }

                var cell = KitUi.Col(5, ring, KitUi.Muted(name, 9.5));
                cell.Margin = new Thickness(0, 0, 14, 0);
                cell.HorizontalAlignment = HorizontalAlignment.Center;
                orbs.Children.Add(cell);
            }

            var card = KitUi.Surface(KitUi.Col(0, KitUi.Muted("ACCENT", 10), orbs), 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "drop zone", """
            // Drop zone: a dashed outline that invites a file, with the accepted types under it.
            var icon = KitUi.Text("\u21E9", 22, "Kit.AccentBrush");
            icon.HorizontalAlignment = HorizontalAlignment.Center;

            var title = KitUi.Text("Drop a preset here", 12.5);
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.Margin = new Thickness(0, 4, 0, 0);

            var hint = KitUi.Muted("JSON or XML up to 2 MB", 10.5);
            hint.HorizontalAlignment = HorizontalAlignment.Center;
            hint.Margin = new Thickness(0, 3, 0, 0);

            var content = KitUi.Col(0, icon, title, hint);
            content.VerticalAlignment = VerticalAlignment.Center;

            var outline = new System.Windows.Shapes.Rectangle
            {
                StrokeThickness = 1.5,
                RadiusX = 8,
                RadiusY = 8,
                StrokeDashArray = new DoubleCollection { 4, 3 }
            };
            outline.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.AccentBrush");

            var frame = new Grid { Height = 120 };
            frame.Children.Add(outline);
            frame.Children.Add(new Border { Child = content });

            frame.Width = 320;
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            return frame;

            """),
        new(false, "password reveal", """
            // Password field: the value stays masked, the reveal toggle lives inside the edge.
            var masked = KitUi.Mono("\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022", 14);
            masked.VerticalAlignment = VerticalAlignment.Center;
            masked.Width = 232;

            var reveal = KitUi.Quiet("\u25C9", 24, 26);

            var row = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            row.Children.Add(masked);
            row.Children.Add(reveal);

            var field = KitUi.Surface(row, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            field.Width = 320;

            var note = KitUi.Muted("12 characters \u00B7 last changed 6 days ago", 10.5);
            note.Margin = new Thickness(2, 6, 0, 0);

            var block = KitUi.Col(0, KitUi.Muted("SIGNING KEY", 10), field, note);
            block.HorizontalAlignment = HorizontalAlignment.Left;
            return block;

            """),
        new(false, "numeric stepper", """
            // Numeric stepper: a minus, the value, a plus, all inside one outlined control.
            var minus = KitUi.Quiet("\u2212", 26, 28);

            var value = KitUi.Mono("10", 13);
            value.Width = 46;
            value.TextAlignment = TextAlignment.Center;
            value.VerticalAlignment = VerticalAlignment.Center;

            var plus = KitUi.Quiet("+", 26, 28);

            var row = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            row.Children.Add(minus);
            row.Children.Add(new Border { Width = 2 });
            row.Children.Add(value);
            row.Children.Add(new Border { Width = 2 });
            row.Children.Add(plus);

            var stepper = KitUi.Surface(row, 3, 8, "Kit.FieldBrush", "Kit.BorderBrush");
            stepper.Width = 150;
            stepper.HorizontalAlignment = HorizontalAlignment.Left;

            var label = KitUi.Muted("RETRIES", 10);
            label.Margin = new Thickness(2, 0, 0, 5);

            return KitUi.Col(0, label, stepper);

            """),
        new(true, "overlays", null),
        new(false, "banner alert", """
            // Banner alert: a status stripe with an icon, the message, and a dismiss action.
            var icon = new Border
            {
                Width = 20,
                Height = 20,
                CornerRadius = new CornerRadius(10),
                Background = new SolidColorBrush(Color.FromArgb(40, 0xF5, 0x9E, 0x0B)),
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock
                {
                    Text = "!",
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(0xF5, 0x9E, 0x0B)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };

            var message = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
            message.Children.Add(KitUi.Text("Unsaved workspace changes detected", 12));
            message.Children.Add(KitUi.Muted("Three designs are only in this session.", 10.5));

            var text = new StackPanel { Orientation = Orientation.Horizontal };
            text.Children.Add(icon);
            text.Children.Add(message);

            var dismiss = KitUi.Quiet("\u00D7", 24, 24);
            dismiss.HorizontalAlignment = HorizontalAlignment.Right;

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.Children.Add(text);
            Grid.SetColumn(dismiss, 1);
            grid.Children.Add(dismiss);

            var banner = new Border
            {
                Width = 460,
                HorizontalAlignment = HorizontalAlignment.Left,
                Padding = new Thickness(12, 9, 8, 9),
                CornerRadius = new CornerRadius(6),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromArgb(24, 0xF5, 0x9E, 0x0B)),
                BorderBrush = new SolidColorBrush(Color.FromArgb(80, 0xF5, 0x9E, 0x0B)),
                Child = grid
            };

            return banner;

            """),
        new(false, "toast", """
            // Toast: a card that fades in at the corner, then leaves on its own.
            var dot = KitUi.Dot(8, "Kit.AccentBrush");
            dot.Margin = new Thickness(0, 0, 9, 0);

            var title = KitUi.Text("Build succeeded in 1.4s", 12);
            title.FontWeight = FontWeights.SemiBold;

            var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            head.Children.Add(dot);
            head.Children.Add(title);

            var detail = KitUi.Muted("84 designs compiled \u00B7 no warnings", 10.5);
            detail.Margin = new Thickness(0, 3, 0, 0);

            var action = KitUi.Quiet("View log", 22);
            action.HorizontalAlignment = HorizontalAlignment.Left;
            action.Margin = new Thickness(0, 7, 0, 0);

            var card = KitUi.Surface(KitUi.Col(0, head, detail, action), 13, 8, "Kit.CardBrush", "Kit.BorderBrush");
            card.Width = 300;
            card.HorizontalAlignment = HorizontalAlignment.Right;
            card.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 18,
                ShadowDepth = 4,
                Opacity = 0.28,
                Color = Colors.Black
            };

            var stage = new Grid { Width = 380, Height = 140 };
            stage.Children.Add(card);
            ((FrameworkElement)card).VerticalAlignment = VerticalAlignment.Bottom;
            return stage;

            """),
        new(false, "modal scrim", """
            // Modal: a scrim covers the page, the dialog sits centred on it with two decisions.
            var title = KitUi.Head("Delete three designs?", 15);

            var copy = KitUi.Muted(
                "The code stays on disk in snippets/, but the rail rows are removed from this workspace.",
                11.5);
            copy.TextWrapping = TextWrapping.Wrap;
            copy.Margin = new Thickness(0, 6, 0, 0);

            var cancel = KitUi.Pill("Cancel", height: 30);
            var confirm = KitUi.Pill("Delete", active: true, height: 30);

            var actions = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 16, 0, 0)
            };

            actions.Children.Add(cancel);
            actions.Children.Add(new Border { Width = 8 });
            actions.Children.Add(confirm);

            var dialog = KitUi.Surface(KitUi.Col(0, title, copy, actions), 18, 10, "Kit.CardBrush", "Kit.BorderBrush");
            dialog.Width = 400;
            dialog.HorizontalAlignment = HorizontalAlignment.Center;
            dialog.VerticalAlignment = VerticalAlignment.Center;
            dialog.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 30,
                ShadowDepth = 8,
                Opacity = 0.4,
                Color = Colors.Black
            };

            var stage = new Grid { Width = 520, Height = 260 };
            stage.Children.Add(KitUi.Surface(new Grid(), pad: 0, radius: 10, fill: "Kit.PanelBrush"));
            stage.Children.Add(new Border { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) });
            stage.Children.Add(dialog);

            stage.HorizontalAlignment = HorizontalAlignment.Left;
            return stage;

            """),
        new(false, "command palette", """
            // Command palette: a search line and a keyboard-navigable list, one row already lit.
            var search = KitUi.Input("run", 30, 12);
            search.Margin = new Thickness(0, 0, 0, 8);

            var rows = new StackPanel();
            var commands = new (string Name, string Key, bool Active)[]
            {
                ("Run the snippet", "Ctrl+Enter", true),
                ("Add a design", "Ctrl+N", false),
                ("Open a file", "Ctrl+O", false),
                ("Copy this code", "Ctrl+Shift+C", false)
            };

            foreach (var (name, key, active) in commands)
            {
                var label = KitUi.Text(name, 12, active ? "Kit.TextBrush" : "Kit.MutedBrush");
                label.VerticalAlignment = VerticalAlignment.Center;

                var cap = KitUi.Keycap(key);
                cap.HorizontalAlignment = HorizontalAlignment.Right;

                var line = new Grid();
                line.ColumnDefinitions.Add(new ColumnDefinition());
                line.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                line.Children.Add(label);
                Grid.SetColumn(cap, 1);
                line.Children.Add(cap);

                var row = new Border { Padding = new Thickness(10, 7, 10, 7), CornerRadius = new CornerRadius(5), Child = line };

                if (active)
                {
                    row.SetResourceReference(Border.BackgroundProperty, "Kit.AccentSoftBrush");
                }

                rows.Children.Add(row);
            }

            var card = KitUi.Surface(KitUi.Col(0, search, rows), 10, 10, "Kit.CardBrush", "Kit.BorderBrush");
            card.Width = 460;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            card.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 24,
                ShadowDepth = 6,
                Opacity = 0.3,
                Color = Colors.Black
            };

            return card;

            """),
        new(false, "context menu", """
            // Context menu: shortcuts sit right-aligned against the action, separator between groups.
            var items = new StackPanel { Width = 210 };
            var entries = new (string Name, string Key, bool Dangerous)[]
            {
                ("Rename", "F2", false),
                ("Duplicate", "Ctrl+D", false),
                ("Copy code", "Ctrl+C", false),
                ("Delete", "Del", true)
            };

            var index = 0;

            foreach (var (name, key, dangerous) in entries)
            {
                if (name == "Delete")
                {
                    var rule = KitUi.Rule();
                    rule.Margin = new Thickness(0, 4, 0, 4);
                    items.Children.Add(rule);
                }

                var label = KitUi.Text(name, 12, dangerous ? "Kit.AccentBrush" : "Kit.TextBrush");
                label.VerticalAlignment = VerticalAlignment.Center;

                var cap = KitUi.Keycap(key);
                cap.HorizontalAlignment = HorizontalAlignment.Right;

                var line = new Grid();
                line.ColumnDefinitions.Add(new ColumnDefinition());
                line.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                line.Children.Add(label);
                Grid.SetColumn(cap, 1);
                line.Children.Add(cap);

                var row = new Border
                {
                    Padding = new Thickness(9, 6, 9, 6),
                    CornerRadius = new CornerRadius(4),
                    Child = line
                };

                if (index == 0)
                {
                    row.SetResourceReference(Border.BackgroundProperty, "Kit.HoverBrush");
                }

                items.Children.Add(row);
                index++;
            }

            var menu = KitUi.Surface(items, 6, 8, "Kit.CardBrush", "Kit.BorderBrush");
            menu.HorizontalAlignment = HorizontalAlignment.Left;
            menu.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 18,
                ShadowDepth = 5,
                Opacity = 0.25,
                Color = Colors.Black
            };

            return menu;

            """),
        new(false, "rich tooltip", """
            // Tooltip: a trigger with a card under it - title, one sentence, and the shortcut.
            var trigger = KitUi.Pill("Compile", height: 30);
            trigger.HorizontalAlignment = HorizontalAlignment.Left;

            var caret = KitUi.Muted("\u25B2", 9);
            caret.HorizontalAlignment = HorizontalAlignment.Left;
            caret.Margin = new Thickness(22, 0, 0, 4);

            var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            var title = KitUi.Text("Compile the snippet", 11.5);
            title.FontWeight = FontWeights.SemiBold;
            var cap = KitUi.Keycap("Ctrl+Enter");
            cap.Margin = new Thickness(10, 0, 0, 0);
            head.Children.Add(title);
            head.Children.Add(cap);

            var copy = KitUi.Muted(
                "Runs the code in this design and paints the result in the preview panel.",
                11);
            copy.TextWrapping = TextWrapping.Wrap;
            copy.Margin = new Thickness(0, 5, 0, 0);

            var card = KitUi.Surface(KitUi.Col(0, head, copy), 11, 7, "Kit.CardBrush", "Kit.BorderBrush");
            card.Width = 300;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            card.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 16,
                ShadowDepth = 4,
                Opacity = 0.24,
                Color = Colors.Black
            };

            var stack = KitUi.Col(0, trigger, caret, card);
            stack.HorizontalAlignment = HorizontalAlignment.Left;
            return stack;

            """),
        new(true, "gauges", null),
        new(false, "progress bar", """
            // Progress bar: rounded ends, a gradient fill and the readout above the track.
            var label = KitUi.Text("Packing image", 12);
            var count = KitUi.Mono("75%", 11, "Kit.MutedBrush");
            count.HorizontalAlignment = HorizontalAlignment.Right;

            var head = new Grid();
            head.ColumnDefinitions.Add(new ColumnDefinition());
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            head.Children.Add(label);
            Grid.SetColumn(count, 1);
            head.Children.Add(count);

            var track = new Border { Height = 5, CornerRadius = new CornerRadius(3), Margin = new Thickness(0, 8, 0, 0) };
            track.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

            var fill = new Border
            {
                Height = 5,
                Width = 225,
                CornerRadius = new CornerRadius(3),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 8, 0, 0)
            };

            var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];
            var deep = (Brush)Application.Current.Resources["Kit.AccentDeepBrush"];

            fill.Background = new LinearGradientBrush(
                ((SolidColorBrush)accent).Color,
                ((SolidColorBrush)deep).Color,
                0.0);

            fill.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 12,
                ShadowDepth = 0,
                Opacity = 0.5,
                Color = ((SolidColorBrush)accent).Color
            };

            var bars = new Grid { Width = 300, Height = 9 };
            bars.Children.Add(track);
            bars.Children.Add(fill);

            var block = new StackPanel { Width = 300 };
            block.Children.Add(head);
            block.Children.Add(bars);

            var caption = KitUi.Muted("1.9 GB of 2.5 GB written", 10.5);
            caption.Margin = new Thickness(0, 7, 0, 0);
            block.Children.Add(caption);

            block.HorizontalAlignment = HorizontalAlignment.Left;
            return block;

            """),
        new(false, "spinner", """
            // Spinner: a broken ring that turns, with the queue depth beside it.
            var ring = new System.Windows.Shapes.Ellipse
            {
                Width = 26,
                Height = 26,
                StrokeThickness = 3,
                StrokeDashArray = new DoubleCollection { 3, 1.6 },
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            ring.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.AccentBrush");

            ring.RenderTransform = new RotateTransform(-20);

            var text = KitUi.Col(1, KitUi.Text("Compiling 3 designs", 12), KitUi.Muted("keeps running while you browse", 10.5));
            text.VerticalAlignment = VerticalAlignment.Center;
            text.Margin = new Thickness(12, 0, 0, 0);

            var row = new StackPanel { Orientation = Orientation.Horizontal };
            row.Children.Add(ring);
            row.Children.Add(text);

            var card = KitUi.Surface(row, 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "activity grid", """
            // Activity grid: a week across, four rows down, opacity carrying the density.
            var grid = new System.Windows.Controls.Primitives.UniformGrid { Columns = 12, Rows = 4, Width = 240 };

            var density = new double[]
            {
                0.15, 0.35, 0.9, 0.2, 0.55, 0.1, 0.4, 0.75, 0.25, 0.6, 0.3, 0.95,
                0.45, 0.2, 0.65, 0.85, 0.3, 0.5, 0.15, 0.7, 0.4, 0.2, 0.55, 0.35,
                0.1, 0.6, 0.25, 0.4, 0.9, 0.35, 0.2, 0.5, 0.8, 0.15, 0.45, 0.3,
                0.3, 0.5, 0.7, 0.15, 0.35, 0.6, 0.9, 0.25, 0.45, 0.2, 0.5, 0.4
            };

            foreach (var level in density)
            {
                var cell = KitUi.Dot(14, "Kit.AccentBrush");
                cell.CornerRadius = new CornerRadius(3);
                cell.Opacity = level;
                cell.Margin = new Thickness(2);
                grid.Children.Add(cell);
            }

            var caption = KitUi.Muted("48 days \u00B7 214 designs saved", 10.5);
            caption.Margin = new Thickness(2, 8, 0, 0);

            var card = KitUi.Surface(KitUi.Col(0, KitUi.Muted("ACTIVITY", 10), grid, caption), 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "ring gauge", """
            // Ring gauge: a full circle behind, an accent arc to 88%, the number in the middle.
            var back = new System.Windows.Shapes.Ellipse
            {
                Width = 88,
                Height = 88,
                StrokeThickness = 7
            };
            back.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.BorderBrush");

            var arc = new System.Windows.Shapes.Path
            {
                StrokeThickness = 7,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                Data = Geometry.Parse("M 44,4 A 40,40 0 1 1 16.7,14.8")
            };
            arc.SetResourceReference(System.Windows.Shapes.Shape.StrokeProperty, "Kit.AccentBrush");

            var value = KitUi.Text("88%", 20);
            value.FontWeight = FontWeights.Bold;
            value.HorizontalAlignment = HorizontalAlignment.Center;
            value.VerticalAlignment = VerticalAlignment.Center;

            var caption = KitUi.Muted("coverage", 9.5);
            caption.HorizontalAlignment = HorizontalAlignment.Center;
            caption.VerticalAlignment = VerticalAlignment.Center;
            caption.Margin = new Thickness(0, 26, 0, 0);

            var dial = new Grid { Width = 88, Height = 88 };
            dial.Children.Add(back);
            dial.Children.Add(arc);
            dial.Children.Add(value);
            dial.Children.Add(caption);

            var text = KitUi.Col(3, KitUi.Head("Snippet coverage", 13), KitUi.Muted("78 of 84 designs compile clean", 11));
            text.VerticalAlignment = VerticalAlignment.Center;
            text.Margin = new Thickness(16, 0, 0, 0);

            var row = new StackPanel { Orientation = Orientation.Horizontal };
            row.Children.Add(dial);
            row.Children.Add(text);

            var card = KitUi.Surface(row, 14, 10, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "spark bars", """
            // Spark bars: the last twelve samples, with the current one called out in accent.
            var samples = new double[] { 0.4, 0.55, 0.3, 0.62, 0.48, 0.7, 0.52, 0.8, 0.66, 0.9, 0.72, 0.85 };
            var graph = new StackPanel { Orientation = Orientation.Horizontal, Height = 48, VerticalAlignment = VerticalAlignment.Bottom };

            for (var i = 0; i < samples.Length; i++)
            {
                var bar = new Border
                {
                    Width = 9,
                    Height = 44 * samples[i],
                    CornerRadius = new CornerRadius(2),
                    Margin = new Thickness(0, 0, 3, 0),
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Opacity = i == samples.Length - 1 ? 1.0 : 0.55
                };

                bar.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
                graph.Children.Add(bar);
            }

            var value = KitUi.Mono("85%", 11);
            var name = KitUi.Muted("CPU", 10);

            var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            head.Children.Add(name);
            var gap = new Border { Width = 8 };
            head.Children.Add(gap);
            head.Children.Add(value);

            var card = KitUi.Surface(KitUi.Col(0, head, graph), 12, 8, "Kit.PanelBrush");
            card.Width = 190;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(true, "layout", null),
        new(false, "split workspace", """
            // Split workspace: rail, a draggable seam, then the editor surface.
            var rail = KitUi.Col(
                6,
                KitUi.Muted("RAIL", 9.5),
                KitUi.Pill("Buttons", active: true, height: 26),
                KitUi.Pill("Fields", height: 26),
                KitUi.Pill("Charts", height: 26));

            rail.Margin = new Thickness(12);

            var seam = new Border { Width = 3, Cursor = Cursors.SizeWE };
            seam.SetResourceReference(Border.BackgroundProperty, "Kit.BorderBrush");

            var editor = KitUi.Col(
                8,
                KitUi.Head("Snippet.cs", 12),
                KitUi.Mono("var save = new PillButton", 11, "Kit.MutedBrush"),
                KitUi.Mono("{ Content = \"Save\" };", 11, "Kit.MutedBrush"));

            editor.Margin = new Thickness(14);

            var grid = new Grid { Width = 520, Height = 190 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Grid.SetColumn(seam, 1);
            Grid.SetColumn(editor, 2);

            var railHost = new Border { Child = rail };
            railHost.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

            grid.Children.Add(railHost);
            grid.Children.Add(seam);
            grid.Children.Add(editor);

            var frame = KitUi.Surface(grid, pad: 0, radius: 8, fill: "Kit.PanelBrush");
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            return frame;

            """),
        new(false, "card grid", """
            // Card grid: modular tiles that wrap, each with a name, a count and a swatch.
            var wrap = new WrapPanel { Width = 480 };

            var tiles = new (string Name, string Count, string Hex)[]
            {
                ("Buttons", "18 designs", "#FF2E88"),
                ("Fields", "9 designs", "#22D3FF"),
                ("Charts", "6 designs", "#B14BFF"),
                ("Layouts", "12 designs", "#00FF9C")
            };

            foreach (var (name, count, hex) in tiles)
            {
                var swatch = KitUi.Swatch(hex, 10);

                var label = KitUi.Text(name, 12.5);
                label.FontWeight = FontWeights.SemiBold;

                var top = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
                top.Children.Add(swatch);
                top.Children.Add(label);

                var body = KitUi.Col(3, top, KitUi.Muted(count, 10.5));
                var tile = KitUi.Surface(body, 12, 8, "Kit.CardBrush", "Kit.BorderBrush");
                tile.Width = 148;
                tile.Margin = new Thickness(0, 0, 9, 9);
                wrap.Children.Add(tile);
            }

            var host = KitUi.Surface(wrap, 10, 8, "Kit.PanelBrush");
            host.HorizontalAlignment = HorizontalAlignment.Left;
            return host;

            """),
        new(false, "collapsible section", """
            // Collapsible section: a quiet header bar over the settings it owns.
            var chevron = KitUi.Muted("\u25BE", 10);
            chevron.Margin = new Thickness(0, 0, 8, 0);
            chevron.VerticalAlignment = VerticalAlignment.Center;

            var title = KitUi.Muted("CONFIGURATION", 10);
            title.VerticalAlignment = VerticalAlignment.Center;

            var hint = KitUi.Mono("3 keys", 9.5, "Kit.BorderBrush");
            hint.VerticalAlignment = VerticalAlignment.Center;

            var head = new Grid();
            head.ColumnDefinitions.Add(new ColumnDefinition());
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var left = new StackPanel { Orientation = Orientation.Horizontal };
            left.Children.Add(chevron);
            left.Children.Add(title);
            head.Children.Add(left);
            Grid.SetColumn(hint, 1);
            head.Children.Add(hint);

            var bar = new Border { Padding = new Thickness(13, 9, 13, 9), Child = head };
            bar.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");

            var body = new Border
            {
                Padding = new Thickness(13, 11, 13, 12),
                Child = KitUi.Col(
                    7,
                    KitUi.Mono("timeout = 5000", 11.5, "Kit.TextBrush"),
                    KitUi.Mono("retries = 2", 11.5, "Kit.TextBrush"),
                    KitUi.Mono("verbose = false", 11.5, "Kit.MutedBrush"))
            };

            var panel = KitUi.Surface(KitUi.Col(0, bar, KitUi.Rule(), body), pad: 0, radius: 8, fill: "Kit.PanelBrush");
            panel.Width = 340;
            panel.HorizontalAlignment = HorizontalAlignment.Left;
            return panel;

            """),
        new(false, "status footer", """
            // Status footer: readiness on the left, the build and the pending change on the right.
            var ready = KitUi.Dot(7, "Kit.AccentBrush");

            var state = KitUi.Text("Ready", 10.5);
            state.Margin = new Thickness(7, 0, 0, 0);
            state.VerticalAlignment = VerticalAlignment.Center;

            var left = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(12, 0, 0, 0)
            };

            left.Children.Add(ready);
            left.Children.Add(state);
            var build = KitUi.Muted("\u00B7  v1.5.0  \u00B7  net8.0", 10);
            build.Margin = new Thickness(8, 0, 0, 0);
            build.VerticalAlignment = VerticalAlignment.Center;
            left.Children.Add(build);

            var pending = KitUi.Text("1 change pending", 10.5, "Kit.AccentBrush");
            pending.VerticalAlignment = VerticalAlignment.Center;
            pending.Margin = new Thickness(0, 0, 14, 0);

            var stamp = KitUi.Mono("build 04:12", 10, "Kit.MutedBrush");
            stamp.VerticalAlignment = VerticalAlignment.Center;
            stamp.Margin = new Thickness(0, 0, 14, 0);

            var right = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 12, 0)
            };

            right.Children.Add(pending);
            right.Children.Add(stamp);

            var grid = new Grid { Height = 28 };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.Children.Add(left);
            Grid.SetColumn(right, 1);
            grid.Children.Add(right);

            var bar = new Border { Width = 520, Child = grid, HorizontalAlignment = HorizontalAlignment.Left, BorderThickness = new Thickness(1) };
            bar.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
            bar.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
            return bar;

            """),
        new(false, "tabbed inspector", """
            // Tabbed inspector: Design, Properties, Raw - the live tab carries the accent rule.
            var tabs = new StackPanel { Orientation = Orientation.Horizontal };
            var names = new[] { "Design", "Properties", "Raw code" };

            for (var i = 0; i < names.Length; i++)
            {
                var active = i == 1;
                var label = KitUi.Text(names[i], 11.5, active ? "Kit.TextBrush" : "Kit.MutedBrush");
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Margin = new Thickness(0, 0, 0, 7);

                var underline = new Border { Height = 2, Background = Brushes.Transparent };

                if (active)
                {
                    underline.SetResourceReference(Border.BackgroundProperty, "Kit.AccentBrush");
                }

                var cell = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
                cell.Children.Add(label);
                cell.Children.Add(underline);
                tabs.Children.Add(cell);
            }

            var body = new StackPanel { Margin = new Thickness(0, 12, 0, 0) };

            string[] keys = ["Width", "Height", "Radius", "Fill"];

            foreach (var key in keys)
            {
                var grid = new Grid { Margin = new Thickness(0, 3, 0, 3) };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                var label = KitUi.Mono(key, 11, "Kit.MutedBrush");
                label.VerticalAlignment = VerticalAlignment.Center;

                var value = KitUi.Mono(key == "Fill" ? "#FF2E88" : "32", 11);

                Grid.SetColumn(value, 1);
                grid.Children.Add(label);
                grid.Children.Add(value);
                body.Children.Add(grid);
            }

            var card = KitUi.Surface(KitUi.Col(0, tabs, body), 14, 8, "Kit.PanelBrush");
            card.Width = 320;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(true, "dev tools", null),
        new(false, "regex tester", """
            // Regex tester: the pattern in mono, the verdict beside it, the hits under it.
            var pattern = KitUi.Mono("^[a-z0-9_]+$", 12);
            pattern.Width = 250;
            pattern.VerticalAlignment = VerticalAlignment.Center;

            var verdict = KitUi.Chip("3 matches", "Kit.AccentSoftBrush", "Kit.AccentInkBrush");
            verdict.HorizontalAlignment = HorizontalAlignment.Right;

            var field = new Grid();
            field.ColumnDefinitions.Add(new ColumnDefinition());
            field.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            field.Children.Add(pattern);
            Grid.SetColumn(verdict, 1);
            field.Children.Add(verdict);

            var frame = KitUi.Surface(field, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            frame.Width = 340;

            var hits = new WrapPanel { Width = 340, Margin = new Thickness(0, 9, 0, 0) };

            foreach (var hit in new[] { "panel_header", "rail_item", "chip_style" })
            {
                var pill = new Border
                {
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(7, 2, 7, 2),
                    Margin = new Thickness(0, 0, 6, 6),
                    Child = KitUi.Mono(hit, 10.5)
                };

                pill.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
                hits.Children.Add(pill);
            }

            var block = KitUi.Col(0, KitUi.Muted("PATTERN", 10), frame, hits);
            block.HorizontalAlignment = HorizontalAlignment.Left;
            return block;

            """),
        new(false, "terminal stream", """
            // Terminal stream: auto-scrolled mono output in a well that stays dark in both modes.
            var output = new StackPanel();

            void Line(string stamp, string tag, string text, Color ink)
            {
                var row = new StackPanel { Orientation = Orientation.Horizontal };

                var time = new TextBlock { Text = stamp, FontSize = 11, Foreground = new SolidColorBrush(Color.FromRgb(0x5A, 0x64, 0x72)), Width = 74 };
                var level = new TextBlock { Text = tag, FontSize = 11, Foreground = new SolidColorBrush(ink), Width = 62 };
                var body = new TextBlock { Text = text, FontSize = 11, Foreground = new SolidColorBrush(Color.FromRgb(0xD5, 0xDB, 0xE3)) };

                time.FontFamily = new FontFamily("Consolas");
                level.FontFamily = new FontFamily("Consolas");
                body.FontFamily = new FontFamily("Consolas");

                row.Children.Add(time);
                row.Children.Add(level);
                row.Children.Add(body);
                output.Children.Add(row);
            }

            Line("04:11:58", "info", "snippet catalog: 84 designs loaded", Color.FromRgb(0x4B, 0xE0, 0xA6));
            Line("04:12:01", "build", "Kit.dll rebuilt in 1.4s", Color.FromRgb(0x4B, 0xE0, 0xA6));
            Line("04:12:03", "warn", "This well stays dark in light mode on purpose", Color.FromRgb(0xF5, 0xC4, 0x51));
            Line("04:12:04", "done", "ready for input", Color.FromRgb(0x8F, 0x9B, 0xAB));

            var prompt = new TextBlock
            {
                Text = "> _",
                FontFamily = new FontFamily("Consolas"),
                FontSize = 11,
                Foreground = new SolidColorBrush(Color.FromRgb(0x4B, 0xE0, 0xA6)),
                Margin = new Thickness(74, 6, 0, 0)
            };

            output.Children.Add(prompt);

            var well = new Border
            {
                Width = 480,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = new SolidColorBrush(Color.FromRgb(0x0B, 0x0D, 0x12)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x33)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12, 10, 12, 12),
                Child = output
            };

            return well;

            """),
        new(false, "http verb badges", """
            // HTTP verbs: each method keeps its own colour, the route stays mono and quiet.
            Border Verb(string verb, Color tint)
            {
                return new Border
                {
                    Width = 54,
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(6, 2, 6, 2),
                    Margin = new Thickness(0, 0, 10, 0),
                    Background = new SolidColorBrush(Color.FromArgb(28, tint.R, tint.G, tint.B)),
                    Child = new TextBlock
                    {
                        Text = verb,
                        FontSize = 10,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(tint),
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                };
            }

            var list = new StackPanel { Width = 440 };

            var routes = new (string Verb, Color Tint, string Path, string Note)[]
            {
                ("GET", Color.FromRgb(0x3B, 0x82, 0xF6), "/v1/devices", "list attached disks"),
                ("POST", Color.FromRgb(0x10, 0xB9, 0x81), "/v1/packages", "stage an MSIX build"),
                ("PUT", Color.FromRgb(0xF5, 0x9E, 0x0B), "/v1/sessions/{id}", "replace a session"),
                ("DEL", Color.FromRgb(0xEF, 0x44, 0x44), "/v1/sessions/{id}", "drop the session")
            };

            foreach (var (verb, tint, path, note) in routes)
            {
                var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
                row.Children.Add(Verb(verb, tint));

                var route = KitUi.Mono(path, 11.5);
                route.VerticalAlignment = VerticalAlignment.Center;
                route.Width = 176;
                row.Children.Add(route);

                var caption = KitUi.Muted(note, 10.5);
                caption.VerticalAlignment = VerticalAlignment.Center;
                row.Children.Add(caption);

                var line = new Border { Padding = new Thickness(0, 7, 0, 7), Child = row };
                list.Children.Add(line);
                list.Children.Add(KitUi.Rule());
            }

            var card = KitUi.Surface(list, 13, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "environment selector", """
            // Environment selector: the closed control, then the list it opens with status dots.
            var selectedDot = new Border
            {
                Width = 8,
                Height = 8,
                CornerRadius = new CornerRadius(4),
                Background = new SolidColorBrush(Color.FromRgb(0xEF, 0x44, 0x44)),
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            var selected = KitUi.Text("Production \u00B7 US-East", 11.5);
            selected.VerticalAlignment = VerticalAlignment.Center;

            var chevron = KitUi.Muted("\u25BE", 10);
            chevron.Margin = new Thickness(10, 0, 0, 0);
            chevron.VerticalAlignment = VerticalAlignment.Center;

            var closedRow = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            closedRow.Children.Add(selectedDot);
            closedRow.Children.Add(selected);
            closedRow.Children.Add(chevron);

            var closed = KitUi.Surface(closedRow, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            closed.Width = 250;
            closed.HorizontalAlignment = HorizontalAlignment.Left;

            var menu = new StackPanel { Margin = new Thickness(0, 6, 0, 0) };

            var options = new (string Name, Color Tint, bool Live)[]
            {
                ("Production \u00B7 US-East", Color.FromRgb(0xEF, 0x44, 0x44), true),
                ("Staging \u00B7 EU-West", Color.FromRgb(0xF5, 0x9E, 0x0B), false),
                ("Development \u00B7 local", Color.FromRgb(0x10, 0xB9, 0x81), false)
            };

            foreach (var (name, tint, live) in options)
            {
                var dot = new Border
                {
                    Width = 8,
                    Height = 8,
                    CornerRadius = new CornerRadius(4),
                    Background = new SolidColorBrush(tint),
                    Margin = new Thickness(0, 0, 9, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };

                var label = KitUi.Text(name, 11.5, live ? "Kit.TextBrush" : "Kit.MutedBrush");
                label.VerticalAlignment = VerticalAlignment.Center;

                var row = new StackPanel { Orientation = Orientation.Horizontal };
                row.Children.Add(dot);
                row.Children.Add(label);

                var item = new Border { Padding = new Thickness(9, 6, 9, 6), CornerRadius = new CornerRadius(5), Child = row };

                if (live)
                {
                    item.SetResourceReference(Border.BackgroundProperty, "Kit.HoverBrush");
                }

                menu.Children.Add(item);
            }

            var open = KitUi.Surface(menu, 4, 8, "Kit.CardBrush", "Kit.BorderBrush");
            open.Width = 250;
            open.HorizontalAlignment = HorizontalAlignment.Left;
            open.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 16,
                ShadowDepth = 4,
                Opacity = 0.25,
                Color = Colors.Black
            };

            return KitUi.Col(0, KitUi.Muted("TARGET", 10), closed, open);

            """),
        new(false, "diff viewer", """
            // Diff viewer: removed lines left, added lines right, tinted rather than boxed.
            StackPanel Pane(string heading, string[] lines, Color tint, string marker)
            {
                var body = new StackPanel();

                foreach (var line in lines)
                {
                    var text = new TextBlock
                    {
                        Text = marker + " " + line,
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 10.5,
                        Foreground = new SolidColorBrush(tint),
                        Margin = new Thickness(0, 1, 0, 1)
                    };

                    body.Children.Add(text);
                }

                var host = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(20, tint.R, tint.G, tint.B)),
                    Padding = new Thickness(9, 7, 9, 7),
                    Child = body
                };

                var pane = new StackPanel();
                var header = KitUi.Mono(heading, 9.5, "Kit.MutedBrush");
                header.Margin = new Thickness(0, 0, 0, 6);
                pane.Children.Add(header);
                pane.Children.Add(host);
                return pane;
            }

            var left = Pane(
                "v1.4.9",
                ["var w = 160;", "Radius = 8;", "Text = \"Save\";"],
                Color.FromRgb(0xEF, 0x44, 0x44),
                "\u2212");

            var right = Pane(
                "v1.5.0",
                ["var w = 200;", "Radius = 10;", "Text = \"Save & close\";"],
                Color.FromRgb(0x10, 0xB9, 0x81),
                "+");

            var grid = new Grid { Width = 440 };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Grid.SetColumn(left, 0);
            Grid.SetColumn(right, 2);
            grid.Children.Add(left);
            grid.Children.Add(right);

            var card = KitUi.Surface(grid, 12, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "connection status", """
            // Connection status: the dot, the latency, the pool, and one way to retry.
            var dot = KitUi.Dot(9, "Kit.AccentBrush");
            dot.Margin = new Thickness(0, 0, 9, 0);

            var state = KitUi.Text("Connected", 12);
            state.FontWeight = FontWeights.SemiBold;
            state.VerticalAlignment = VerticalAlignment.Center;

            var latency = KitUi.Mono("12 ms", 11, "Kit.MutedBrush");
            latency.Margin = new Thickness(9, 0, 0, 0);
            latency.VerticalAlignment = VerticalAlignment.Center;

            var head = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            head.Children.Add(dot);
            head.Children.Add(state);
            head.Children.Add(latency);

            var pool = KitUi.Muted("pool 4/16 \u00B7 kit_catalog @ localhost:5432", 10.5);
            pool.Margin = new Thickness(0, 5, 0, 0);

            var retry = KitUi.Quiet("Reconnect \u21BB", 24);
            retry.HorizontalAlignment = HorizontalAlignment.Left;
            retry.Margin = new Thickness(0, 8, 0, 0);

            var card = KitUi.Surface(KitUi.Col(0, head, pool, retry), 12, 8, "Kit.PanelBrush");
            card.Width = 320;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "keycap badge", """
            // Keycaps: each key its own box, a plus between them, the action on the left.
            FrameworkElement Shortcut(string action, string[] keys)
            {
                var label = KitUi.Text(action, 11.5);
                label.Width = 130;
                label.VerticalAlignment = VerticalAlignment.Center;

                var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
                row.Children.Add(label);

                for (var i = 0; i < keys.Length; i++)
                {
                    if (i > 0)
                    {
                        var plus = KitUi.Muted("+", 9.5);
                        plus.Margin = new Thickness(5, 0, 5, 0);
                        plus.VerticalAlignment = VerticalAlignment.Center;
                        row.Children.Add(plus);
                    }

                    row.Children.Add(KitUi.Keycap(keys[i]));
                }

                return row;
            }

            var rows = KitUi.Col(
                8,
                Shortcut("Run the snippet", ["Ctrl", "Enter"]),
                Shortcut("Add a design", ["Ctrl", "N"]),
                Shortcut("Open a file", ["Ctrl", "O"]),
                Shortcut("Rename", ["F2"]));

            var card = KitUi.Surface(rows, 13, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "insert flyout", """
            // Insert flyout: a floating card with the template, then insert or copy.
            var head = new StackPanel { Orientation = Orientation.Horizontal };
            var title = KitUi.Text("Save bar", 12);
            title.FontWeight = FontWeights.SemiBold;
            var tag = KitUi.Chip("WPF \u00B7 C#", "Kit.AccentSoftBrush", "Kit.AccentInkBrush");
            tag.Margin = new Thickness(9, 0, 0, 0);
            head.Children.Add(title);
            head.Children.Add(tag);

            var preview = KitUi.Col(
                2,
                KitUi.Mono("var row = KitUi.Row(8,", 10.5, "Kit.MutedBrush"),
                KitUi.Mono("    KitUi.Pill(\"Save\", active: true),", 10.5, "Kit.MutedBrush"),
                KitUi.Mono("    KitUi.Pill(\"Cancel\"));", 10.5, "Kit.MutedBrush"));

            var well = KitUi.Surface(preview, 9, 6, "Kit.FieldBrush", "Kit.BorderBrush");
            well.Margin = new Thickness(0, 9, 0, 0);

            var insert = KitUi.Pill("Insert", active: true, height: 28);
            var copy = KitUi.Pill("Copy code", height: 28);

            var actions = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
            actions.Children.Add(insert);
            actions.Children.Add(new Border { Width = 8 });
            actions.Children.Add(copy);

            var card = KitUi.Surface(KitUi.Col(0, head, well, actions), 13, 9, "Kit.CardBrush", "Kit.BorderBrush");
            card.Width = 300;
            card.HorizontalAlignment = HorizontalAlignment.Left;
            card.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 20,
                ShadowDepth = 5,
                Opacity = 0.28,
                Color = Colors.Black
            };

            return card;

            """),
        new(false, "error row", """
            // Diagnostic row: the code, the sentence, and where to go and fix it.
            var code = KitUi.Mono("CS0103", 11, "Kit.TextBrush");
            code.Width = 62;
            code.VerticalAlignment = VerticalAlignment.Center;

            var message = KitUi.Text("The name 'InitializeComponent' does not exist in the current context", 11.5);
            message.VerticalAlignment = VerticalAlignment.Center;

            var where = KitUi.Mono("MainWindow.xaml.cs:42", 10, "Kit.MutedBrush");
            where.VerticalAlignment = VerticalAlignment.Center;
            where.Margin = new Thickness(12, 0, 0, 0);

            var badge = new Border
            {
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(6, 2, 6, 2),
                Margin = new Thickness(0, 0, 10, 0),
                Background = new SolidColorBrush(Color.FromArgb(30, 0xEF, 0x44, 0x44)),
                VerticalAlignment = VerticalAlignment.Center,
                Child = code
            };

            var row = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            row.Children.Add(badge);
            row.Children.Add(message);

            var grid = new Grid { Width = 500 };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.Children.Add(row);
            Grid.SetColumn(where, 1);
            grid.Children.Add(where);

            var line = KitUi.Surface(grid, 10, 6, "Kit.PanelBrush", "Kit.BorderBrush");
            line.HorizontalAlignment = HorizontalAlignment.Left;

            var second = KitUi.Muted("1 error \u00B7 0 warnings \u00B7 last build 04:12", 10.5);
            second.Margin = new Thickness(2, 8, 0, 0);

            return KitUi.Col(0, line, second);

            """),
        new(false, "workspace switcher", """
            // Workspace switcher: the active solution in the bar, the others one click below.
            var glyph = KitUi.Muted("\u25B1", 13);
            glyph.Margin = new Thickness(0, 0, 8, 0);
            glyph.VerticalAlignment = VerticalAlignment.Center;

            var name = KitUi.Text("Kit.sln", 12);
            name.FontWeight = FontWeights.SemiBold;
            name.VerticalAlignment = VerticalAlignment.Center;

            var live = KitUi.Chip("active", "Kit.AccentSoftBrush", "Kit.AccentInkBrush");
            live.Margin = new Thickness(8, 0, 0, 0);

            var chevron = KitUi.Muted("\u25BE", 10);
            chevron.Margin = new Thickness(10, 0, 0, 0);
            chevron.VerticalAlignment = VerticalAlignment.Center;

            var barRow = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            barRow.Children.Add(glyph);
            barRow.Children.Add(name);
            barRow.Children.Add(live);
            barRow.Children.Add(chevron);

            var bar = KitUi.Surface(barRow, 10, 6, "Kit.RowBrush", "Kit.BorderBrush");
            bar.HorizontalAlignment = HorizontalAlignment.Left;

            var list = new StackPanel();

            foreach (var (workspace, when) in new[] { ("Kit.sln", "now"), ("Catalog.sln", "yesterday"), ("Sandbox.sln", "3 days ago") })
            {
                var label = KitUi.Text(workspace, 11.5);
                label.Width = 130;
                label.VerticalAlignment = VerticalAlignment.Center;

                var stamp = KitUi.Mono(when, 10, "Kit.MutedBrush");
                stamp.VerticalAlignment = VerticalAlignment.Center;

                var row = new StackPanel { Orientation = Orientation.Horizontal };
                row.Children.Add(label);
                row.Children.Add(stamp);

                list.Children.Add(new Border { Padding = new Thickness(10, 6, 10, 6), Child = row });
            }

            var menu = KitUi.Surface(list, 4, 8, "Kit.CardBrush", "Kit.BorderBrush");
            menu.HorizontalAlignment = HorizontalAlignment.Left;
            menu.Margin = new Thickness(0, 6, 0, 0);

            return KitUi.Col(0, bar, menu);

            """),
        new(true, "button styles", null),
        new(false, "minimal flat", """
            // Minimal flat: no fill, no border, the label does the work. The ink is the theme's
            // own rather than the PDF's fixed #2d3748, so it reads in light and dark.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            FlatButton Flat(string text, bool accent)
            {
                var button = new FlatButton
                {
                    Content = text,
                    Height = 32,
                    Padding = new Thickness(10, 0, 10, 0),
                    FontSize = 12.5,
                    Radius = new CornerRadius(6),
                    Fill = Brushes.Transparent,
                    Ink = Brushes.Transparent,
                    Edge = Brushes.Transparent,
                    HoverFill = Brushes.Transparent
                };

                button.SetResourceReference(FlatButton.InkProperty, accent ? "Kit.AccentBrush" : "Kit.TextBrush");
                button.SetResourceReference(FlatButton.HoverInkProperty, "Kit.AccentBrush");
                button.SetResourceReference(FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
                return button;
            }

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(Flat("Save changes", false));
            row.Children.Add(new Border { Width = 10 });
            row.Children.Add(Flat("Dismiss", true));
            return row;

            """),
        new(false, "rounded primary", """
            // Rounded primary: the PDF's ButtonPrimaryRounded, with a real Border radius instead
            // of the CornerRadius setter WPF's Button does not have.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var save = new FlatButton
            {
                Content = "Save changes",
                Height = 36,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Radius = new CornerRadius(8),
                Fill = Hex("#3182ce"),
                Edge = Hex("#3182ce"),
                Ink = Brushes.White,
                HoverFill = Hex("#2b6cb0"),
                HoverEdge = Hex("#2b6cb0"),
                HoverInk = Brushes.White
            };

            var publish = new FlatButton
            {
                Content = "Publish",
                Height = 36,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(8),
                IsEnabled = false,
                Fill = Hex("#3182ce"),
                Edge = Hex("#3182ce"),
                Ink = Brushes.White,
                HoverFill = Hex("#3182ce"),
                HoverEdge = Hex("#3182ce"),
                HoverInk = Brushes.White
            };

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(save);
            row.Children.Add(new Border { Width = 10 });
            row.Children.Add(publish);
            return row;

            """),
        new(false, "outlined gray", """
            // Outlined gray: a hairline edge, transparent fill. The label uses the theme's text
            // ink because #4a5568 disappears on a dark surface.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var cancel = new FlatButton
            {
                Content = "Cancel",
                Height = 34,
                Padding = new Thickness(14, 0, 14, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Brushes.Transparent,
                Edge = Hex("#a0aec0"),
                Ink = Brushes.Transparent,
                HoverFill = Hex("#22A0AEC0"),
                HoverEdge = Hex("#a0aec0"),
                HoverInk = Brushes.Transparent
            };

            cancel.SetResourceReference(FlatButton.InkProperty, "Kit.TextBrush");
            cancel.SetResourceReference(FlatButton.HoverInkProperty, "Kit.TextBrush");

            var later = new FlatButton
            {
                Content = "Later",
                Height = 34,
                Padding = new Thickness(14, 0, 14, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Brushes.Transparent,
                Edge = Hex("#a0aec088"),
                Ink = Brushes.Transparent,
                HoverFill = Hex("#22A0AEC0"),
                HoverEdge = Hex("#a0aec0"),
                HoverInk = Brushes.Transparent
            };

            later.SetResourceReference(FlatButton.InkProperty, "Kit.MutedBrush");
            later.SetResourceReference(FlatButton.HoverInkProperty, "Kit.TextBrush");

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(cancel);
            row.Children.Add(new Border { Width = 10 });
            row.Children.Add(later);
            return row;

            """),
        new(false, "outlined success", """
            // Outlined success: green edge, transparent fill. The ink is the brighter green so it
            // still reads against a dark surface; the PDF's #2f855a is a light-mode colour.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var apply = new FlatButton
            {
                Content = "Apply preset",
                Height = 34,
                Padding = new Thickness(14, 0, 14, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Brushes.Transparent,
                Edge = Hex("#38a169"),
                Ink = Hex("#48bb78"),
                HoverFill = Hex("#1F38A169"),
                HoverEdge = Hex("#48bb78"),
                HoverInk = Brushes.White
            };

            var hint = KitUi.Muted("border 1.5  \u00B7  radius 6  \u00B7  fills on hover", 10.5);
            hint.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, apply, hint);

            """),
        new(false, "danger solid", """
            // Danger solid: the one style that should never be quiet about what it does.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var delete = new FlatButton
            {
                Content = "Delete partition",
                Height = 34,
                Padding = new Thickness(14, 0, 14, 0),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Radius = new CornerRadius(6),
                Fill = Hex("#e53e3e"),
                Edge = Hex("#e53e3e"),
                Ink = Brushes.White,
                HoverFill = Hex("#c53030"),
                HoverEdge = Hex("#c53030"),
                HoverInk = Brushes.White
            };

            var note = KitUi.Muted("Hover deepens the red; it never turns soft.", 10.5);
            note.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, delete, note);

            """),
        new(false, "glass", """
            // Glass: translucent fill over a dark surface, which is the only place glass reads.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var glass = new FlatButton
            {
                Content = "Restore",
                Height = 36,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(8),
                Fill = Hex("#33FFFFFF"),
                Edge = Hex("#66FFFFFF"),
                Ink = Brushes.White,
                HoverFill = Hex("#4DFFFFFF"),
                HoverEdge = Brushes.White,
                HoverInk = Brushes.White
            };

            glass.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 14,
                ShadowDepth = 2,
                Opacity = 0.35,
                Color = Colors.Black
            };

            var stage = new Border
            {
                Background = new LinearGradientBrush(
                    Color.FromRgb(0x12, 0x18, 0x26),
                    Color.FromRgb(0x26, 0x12, 0x1E),
                    45.0),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = glass
            };

            var caption = KitUi.Muted("Glass needs something behind it to be glass.", 10.5);
            caption.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, stage, caption);

            """),
        new(false, "raised 3d", """
            // Raised 3D: a light face on a darker base, so the button looks like it sits on the page.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var face = new FlatButton
            {
                Content = "Edit entry",
                Height = 34,
                Padding = new Thickness(14, 0, 14, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(4),
                Fill = Hex("#e2e8f0"),
                Edge = Hex("#a0aec0"),
                Ink = Hex("#2d3748"),
                HoverFill = Hex("#edf2f7"),
                HoverEdge = Hex("#a0aec0"),
                HoverInk = Hex("#1a202c")
            };

            var basePlate = new Border
            {
                Background = Hex("#a0aec0"),
                CornerRadius = new CornerRadius(4.5),
                Padding = new Thickness(0, 0, 0, 2),
                Child = face,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var caption = KitUi.Muted("The 2px base is the pressed depth.", 10.5);
            caption.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, basePlate, caption);

            """),
        new(false, "pill", """
            // Pill: the PDF asks for radius 999; WPF takes half the height, which is a pill at
            // every size instead of only at one.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var pill = new FlatButton
            {
                Content = "Continue",
                Height = 38,
                Padding = new Thickness(24, 0, 24, 0),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Radius = new CornerRadius(19),
                Fill = Hex("#805ad5"),
                Edge = Hex("#805ad5"),
                Ink = Brushes.White,
                HoverFill = Hex("#6b46c1"),
                HoverEdge = Hex("#6b46c1"),
                HoverInk = Brushes.White
            };

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(pill);

            var outline = new FlatButton
            {
                Content = "Skip",
                Height = 38,
                Padding = new Thickness(22, 0, 22, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(19),
                Fill = Brushes.Transparent,
                Edge = Hex("#805ad5"),
                Ink = Hex("#9f7aea"),
                HoverFill = Hex("#805ad5"),
                HoverEdge = Hex("#805ad5"),
                HoverInk = Brushes.White,
                Margin = new Thickness(10, 0, 0, 0)
            };

            row.Children.Add(outline);
            return row;

            """),
        new(false, "text underline", """
            // Text underline: a link that happens to be a button. The line belongs to the text,
            // which is where WPF keeps text decoration - not on the Button itself.
            Button Link(string text, string brush)
            {
                var label = new TextBlock
                {
                    Text = text,
                    FontSize = 12.5,
                    TextDecorations = TextDecorations.Underline
                };

                label.SetResourceReference(TextBlock.ForegroundProperty, brush);

                return new FlatButton
                {
                    Content = label,
                    Height = 26,
                    Padding = new Thickness(4, 0, 4, 0),
                    Radius = new CornerRadius(3),
                    Fill = Brushes.Transparent,
                    Edge = Brushes.Transparent,
                    HoverFill = Brushes.Transparent,
                    HoverEdge = Brushes.Transparent
                };
            }

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(Link("View the log", "Kit.AccentBrush"));
            row.Children.Add(new Border { Width = 14 });
            row.Children.Add(Link("Clear filters", "Kit.MutedBrush"));
            return row;

            """),
        new(false, "disabled", """
            // Disabled: the PDF sets IsEnabled in a style, which freezes the button. Here the
            // state is a property of the instance, so the same style can be enabled again.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            FlatButton State(string text, bool enabled)
            {
                return new FlatButton
                {
                    Content = text,
                    Height = 34,
                    Padding = new Thickness(14, 0, 14, 0),
                    FontSize = 12.5,
                    Radius = new CornerRadius(6),
                    IsEnabled = enabled,
                    Fill = enabled ? Hex("#3182ce") : Hex("#e2e8f0"),
                    Edge = enabled ? Hex("#3182ce") : Hex("#e2e8f0"),
                    Ink = enabled ? Brushes.White : Hex("#a0aec0"),
                    HoverFill = enabled ? Hex("#2b6cb0") : Hex("#e2e8f0"),
                    HoverEdge = enabled ? Hex("#2b6cb0") : Hex("#e2e8f0"),
                    HoverInk = enabled ? Brushes.White : Hex("#a0aec0")
                };
            }

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(State("Commit", true));
            row.Children.Add(new Border { Width = 10 });
            row.Children.Add(State("Commit", false));

            var caption = KitUi.Muted("0.4 opacity is the disabled state, not a colour.", 10.5);
            caption.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, row, caption);

            """),
        new(false, "gradient blue", """
            // Gradient blue: #4299e1 to #3182ce on the diagonal, the same pair the PDF names.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var face = new LinearGradientBrush(
                (Color)ColorConverter.ConvertFromString("#4299e1"),
                (Color)ColorConverter.ConvertFromString("#3182ce"),
                45.0);

            var deeper = new LinearGradientBrush(
                (Color)ColorConverter.ConvertFromString("#3182ce"),
                (Color)ColorConverter.ConvertFromString("#2c5282"),
                45.0);

            var upload = new FlatButton
            {
                Content = "Upload build",
                Height = 36,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Radius = new CornerRadius(6),
                Fill = face,
                Edge = Hex("#3182ce"),
                Ink = Brushes.White,
                HoverFill = deeper,
                HoverEdge = Hex("#2c5282"),
                HoverInk = Brushes.White
            };

            upload.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 12,
                ShadowDepth = 3,
                Opacity = 0.3,
                Color = (Color)ColorConverter.ConvertFromString("#3182ce")
            };

            var caption = KitUi.Muted("The gradient goes down-right, not top-down.", 10.5);
            caption.Margin = new Thickness(0, 10, 0, 0);

            return KitUi.Col(0, upload, caption);

            """),
        new(false, "gradient purple", """
            // Gradient purple: #9f7aea into #805ad5, with the same diagonal as the blue pair.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var face = new LinearGradientBrush(
                (Color)ColorConverter.ConvertFromString("#9f7aea"),
                (Color)ColorConverter.ConvertFromString("#805ad5"),
                45.0);

            var deeper = new LinearGradientBrush(
                (Color)ColorConverter.ConvertFromString("#805ad5"),
                (Color)ColorConverter.ConvertFromString("#553c9a"),
                45.0);

            var upgrade = new FlatButton
            {
                Content = "Upgrade workspace",
                Height = 36,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Radius = new CornerRadius(6),
                Fill = face,
                Edge = Hex("#805ad5"),
                Ink = Brushes.White,
                HoverFill = deeper,
                HoverEdge = Hex("#6b46c1"),
                HoverInk = Brushes.White
            };

            var caption = KitUi.Muted("Purple is the quiet cousin of the neon styles.", 10.5);
            caption.Margin = new Thickness(0, 10, 0, 0);

            return KitUi.Col(0, upgrade, caption);

            """),
        new(false, "neon green", """
            // Neon green: the outline carries the colour and the glow does the rest.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var neon = new FlatButton
            {
                Content = "ARM WRITES",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 11.5,
                FontWeight = FontWeights.Bold,
                Radius = new CornerRadius(6),
                Fill = Hex("#1448BB78"),
                Edge = Hex("#48bb78"),
                Ink = Hex("#48bb78"),
                HoverFill = Hex("#3348BB78"),
                HoverEdge = Hex("#68d391"),
                HoverInk = Hex("#9ae6b4")
            };

            neon.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 16,
                ShadowDepth = 0,
                Opacity = 0.55,
                Color = (Color)ColorConverter.ConvertFromString("#48bb78")
            };

            var armed = KitUi.Muted("armed  \u00B7  writes pass through", 10.5);
            armed.Margin = new Thickness(0, 10, 0, 0);

            return KitUi.Col(0, neon, armed);

            """),
        new(false, "icon circular", """
            // Icon circular: 40 by 40, no label, the glyph is the whole button.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            FlatButton Circle(string glyph)
            {
                return new FlatButton
                {
                    Content = glyph,
                    Width = 40,
                    Height = 40,
                    MinWidth = 0,
                    FontSize = 15,
                    Radius = new CornerRadius(20),
                    Fill = Hex("#edf2f7"),
                    Edge = Hex("#cbd5e0"),
                    Ink = Hex("#2d3748"),
                    HoverFill = Hex("#e2e8f0"),
                    HoverEdge = Hex("#a0aec0"),
                    HoverInk = Hex("#1a202c")
                };
            }

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(Circle("\u21BB"));
            row.Children.Add(new Border { Width = 10 });
            row.Children.Add(Circle("\u2699"));
            row.Children.Add(new Border { Width = 10 });
            row.Children.Add(Circle("\u2715"));
            return row;

            """),
        new(false, "floating action", """
            // Floating action: 56 by 56, the one button that is allowed to sit above the page.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var fab = new FlatButton
            {
                Content = "+",
                Width = 56,
                Height = 56,
                MinWidth = 0,
                FontSize = 26,
                FontWeight = FontWeights.Light,
                Radius = new CornerRadius(28),
                Fill = Hex("#f56565"),
                Edge = Hex("#f56565"),
                Ink = Brushes.White,
                HoverFill = Hex("#e53e3e"),
                HoverEdge = Hex("#e53e3e"),
                HoverInk = Brushes.White
            };

            fab.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 20,
                ShadowDepth = 5,
                Opacity = 0.35,
                Color = Colors.Black
            };

            var stage = new Border
            {
                Background = Hex("#12161F"),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(18),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = fab
            };

            var caption = KitUi.Muted("The shadow is what lifts it off the surface.", 10.5);
            caption.Margin = new Thickness(0, 10, 0, 0);

            return KitUi.Col(0, stage, caption);

            """),
        new(false, "dark solid", """
            // Dark solid: a slate face that belongs on either surface, white ink throughout.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var commit = new FlatButton
            {
                Content = "Commit changes",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Hex("#2d3748"),
                Edge = Hex("#2d3748"),
                Ink = Brushes.White,
                HoverFill = Hex("#1a202c"),
                HoverEdge = Hex("#4a5568"),
                HoverInk = Brushes.White
            };

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(commit);

            var ghost = new FlatButton
            {
                Content = "Stash",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Brushes.Transparent,
                Edge = Hex("#4a5568"),
                Ink = Hex("#cbd5e0"),
                HoverFill = Hex("#2d3748"),
                HoverEdge = Hex("#718096"),
                HoverInk = Brushes.White,
                Margin = new Thickness(10, 0, 0, 0)
            };

            row.Children.Add(ghost);
            return row;

            """),
        new(false, "pastel pink", """
            // Pastel pink: a soft fill with a deep ink, the one style that is deliberately gentle.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var soft = new FlatButton
            {
                Content = "Archive",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Hex("#fed7d7"),
                Edge = Hex("#fed7d7"),
                Ink = Hex("#c53030"),
                HoverFill = Hex("#feb2b2"),
                HoverEdge = Hex("#fc8181"),
                HoverInk = Hex("#9b2c2c")
            };

            var blue = new FlatButton
            {
                Content = "Mute",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(6),
                Fill = Hex("#bee3f8"),
                Edge = Hex("#bee3f8"),
                Ink = Hex("#2b6cb0"),
                HoverFill = Hex("#90cdf4"),
                HoverEdge = Hex("#63b3ed"),
                HoverInk = Hex("#2c5282"),
                Margin = new Thickness(10, 0, 0, 0)
            };

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(soft);
            row.Children.Add(blue);
            return row;

            """),
        new(false, "square sharp", """
            // Square sharp: no radius at all. At this corner it has to look deliberate, not broken.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var run = new FlatButton
            {
                Content = "RUN",
                Height = 34,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 11.5,
                FontWeight = FontWeights.Bold,
                Radius = new CornerRadius(0),
                Fill = Hex("#2f855a"),
                Edge = Hex("#2f855a"),
                Ink = Brushes.White,
                HoverFill = Hex("#276749"),
                HoverEdge = Hex("#276749"),
                HoverInk = Brushes.White
            };

            var stop = new FlatButton
            {
                Content = "STOP",
                Height = 34,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 11.5,
                FontWeight = FontWeights.Bold,
                Radius = new CornerRadius(0),
                Fill = Hex("#2d3748"),
                Edge = Hex("#2d3748"),
                Ink = Brushes.White,
                HoverFill = Hex("#1a202c"),
                HoverEdge = Hex("#1a202c"),
                HoverInk = Brushes.White,
                Margin = new Thickness(8, 0, 0, 0)
            };

            var row = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            row.Children.Add(run);
            row.Children.Add(stop);

            var caption = KitUi.Muted("Radius 0 reads as a command, not a choice.", 10.5);
            caption.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, row, caption);

            """),
        new(false, "hover fill", """
            // Hover fill: outlined at rest, solid under the pointer. The whole state change is the
            // button answering the mouse before the click.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var outline = new FlatButton
            {
                Content = "Connect",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(4),
                Fill = Brushes.Transparent,
                Edge = Hex("#3182ce"),
                Ink = Hex("#3182ce"),
                HoverFill = Hex("#3182ce"),
                HoverEdge = Hex("#3182ce"),
                HoverInk = Brushes.White
            };

            var filled = new FlatButton
            {
                Content = "Connect",
                Height = 34,
                Padding = new Thickness(16, 0, 16, 0),
                FontSize = 12.5,
                Radius = new CornerRadius(4),
                Fill = Hex("#3182ce"),
                Edge = Hex("#3182ce"),
                Ink = Brushes.White,
                HoverFill = Hex("#2b6cb0"),
                HoverEdge = Hex("#2b6cb0"),
                HoverInk = Brushes.White,
                Margin = new Thickness(10, 0, 0, 0)
            };

            var states = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
            states.Children.Add(outline);
            states.Children.Add(filled);

            var caption = KitUi.Muted("left: rest   \u00B7   right: what the pointer sees", 10.5);
            caption.Margin = new Thickness(0, 9, 0, 0);

            return KitUi.Col(0, states, caption);

            """),
        new(false, "toggle button", """
            // Toggle: 48 by 24 track, 20px thumb, and the thumb travels instead of fading.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            Border Toggle(bool on, string label)
            {
                var thumb = new Border
                {
                    Width = 20,
                    Height = 20,
                    CornerRadius = new CornerRadius(10),
                    Background = Brushes.White,
                    HorizontalAlignment = on ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                    Margin = new Thickness(2)
                };

                var track = new Border
                {
                    Width = 48,
                    Height = 24,
                    CornerRadius = new CornerRadius(12),
                    Background = on ? Hex("#38a169") : Hex("#cbd5e0"),
                    Child = thumb,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var text = KitUi.Text(label, 12);
                text.VerticalAlignment = VerticalAlignment.Center;
                text.Margin = new Thickness(10, 0, 0, 0);

                var row = new StackPanel { Orientation = Orientation.Horizontal };
                row.Children.Add(track);
                row.Children.Add(text);
                return new Border { Child = row };
            }

            var card = KitUi.Surface(
                KitUi.Col(
                    14,
                    Toggle(true, "Hardware acceleration"),
                    Toggle(false, "Write protection")),
                16,
                8,
                "Kit.PanelBrush",
                "Kit.BorderBrush");

            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(true, "table styles", null),
        new(false, "table minimal", """
            // Minimal table: no outer border, horizontal hairlines only, a quiet header.
            var rows = new (string Name, string Cpu, string Mem)[]
            {
                ("PipelineWorker", "12.4%", "184 MB"),
                ("Indexer", "8.1%", "96 MB"),
                ("Watcher", "3.7%", "42 MB"),
                ("Renderer", "22.9%", "512 MB")
            };

            var table = new StackPanel { Width = 440 };

            Grid Line(double height)
            {
                var grid = new Grid { Height = height };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });
                return grid;
            }

            void Put(Grid grid, UIElement element, int column)
            {
                Grid.SetColumn(element, column);
                grid.Children.Add(element);
            }

            var head = Line(22);
            Put(head, KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush"), 0);
            Put(head, KitUi.Mono("CPU", 9.5, "Kit.MutedBrush"), 1);
            Put(head, KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush"), 2);
            table.Children.Add(head);

            foreach (var (name, cpu, mem) in rows)
            {
                var row = Line(28);
                Put(row, KitUi.Text(name, 12), 0);
                Put(row, KitUi.Mono(cpu, 11), 1);
                Put(row, KitUi.Mono(mem, 11, "Kit.MutedBrush"), 2);
                table.Children.Add(row);
                table.Children.Add(KitUi.Rule());
            }

            var card = KitUi.Surface(table, 12, 8, "Kit.PanelBrush", "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "table bordered", """
            // Bordered table: a line between every cell, a box around the whole thing.
            var rows = new (string Name, string Cpu, string Mem)[]
            {
                ("PipelineWorker", "12.4%", "184 MB"),
                ("Indexer", "8.1%", "96 MB"),
                ("Watcher", "3.7%", "42 MB")
            };

            var table = new StackPanel { Width = 440 };

            Grid Line(double height, bool header)
            {
                var grid = new Grid { Height = height };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

                if (header)
                {
                    var band = new Border();
                    band.SetResourceReference(Border.BackgroundProperty, "Kit.RowBrush");
                    band.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
                    band.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumnSpan(band, 3);
                    grid.Children.Add(band);
                }

                return grid;
            }

            void Put(Grid grid, FrameworkElement element, int column)
            {
                element.Margin = new Thickness(8, 0, 8, 0);
                element.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(element, column);
                grid.Children.Add(element);
            }

            var head = Line(24, true);
            Put(head, KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush"), 0);
            Put(head, KitUi.Mono("CPU", 9.5, "Kit.MutedBrush"), 1);
            Put(head, KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush"), 2);
            table.Children.Add(head);

            foreach (var (name, cpu, mem) in rows)
            {
                var row = Line(28, false);
                Put(row, KitUi.Text(name, 12), 0);
                Put(row, KitUi.Mono(cpu, 11), 1);
                Put(row, KitUi.Mono(mem, 11, "Kit.MutedBrush"), 2);
                table.Children.Add(row);
                table.Children.Add(KitUi.Rule());
            }

            var frame = new Border
            {
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6),
                Child = table
            };

            frame.SetResourceReference(Border.BorderBrushProperty, "Kit.BorderBrush");
            frame.SetResourceReference(Border.BackgroundProperty, "Kit.CardBrush");
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            return frame;

            """),
        new(false, "table dark striped", """
            // Dark striped: this table brings its own dark surface, so it reads the same in both modes.
            Brush Hex(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var rows = new (string Name, string Cpu, string Mem)[]
            {
                ("PipelineWorker", "12.4%", "184 MB"),
                ("Indexer", "8.1%", "96 MB"),
                ("Watcher", "3.7%", "42 MB"),
                ("Renderer", "22.9%", "512 MB")
            };

            var table = new StackPanel();

            for (var i = 0; i < rows.Length; i++)
            {
                var (name, cpu, mem) = rows[i];

                var grid = new Grid { Height = 28 };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

                var label = new TextBlock { Text = name, FontSize = 12, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 0, 0, 0) };
                var load = new TextBlock { Text = cpu, FontFamily = new FontFamily("Consolas"), FontSize = 11, Foreground = Hex("#cbd5e0"), VerticalAlignment = VerticalAlignment.Center };
                var memory = new TextBlock { Text = mem, FontFamily = new FontFamily("Consolas"), FontSize = 11, Foreground = Hex("#a0aec0"), VerticalAlignment = VerticalAlignment.Center };

                Grid.SetColumn(load, 1);
                Grid.SetColumn(memory, 2);

                grid.Children.Add(label);
                grid.Children.Add(load);
                grid.Children.Add(memory);

                var band = new Border
                {
                    Background = i % 2 == 0 ? Hex("#2d3748") : Hex("#4a5568"),
                    Child = grid
                };

                table.Children.Add(band);
            }

            var frame = new Border
            {
                Background = Hex("#2d3748"),
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(0, 0, 0, 0),
                Child = table,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 440,
                ClipToBounds = true
            };

            return frame;

            """),
        new(false, "table compact", """
            // Compact table: 24px rows, no lines, 4px of padding. Density is the style.
            var rows = new (string Name, string Cpu, string Mem)[]
            {
                ("PipelineWorker", "12.4%", "184 MB"),
                ("Indexer", "8.1%", "96 MB"),
                ("Watcher", "3.7%", "42 MB"),
                ("Renderer", "22.9%", "512 MB"),
                ("Scheduler", "1.2%", "18 MB")
            };

            var table = new StackPanel { Width = 420, Margin = new Thickness(4) };

            foreach (var (name, cpu, mem) in rows)
            {
                var grid = new Grid { Height = 24 };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(72) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });

                var label = KitUi.Text(name, 11.5);
                label.VerticalAlignment = VerticalAlignment.Center;

                var load = KitUi.Mono(cpu, 10.5);
                load.VerticalAlignment = VerticalAlignment.Center;

                var memory = KitUi.Mono(mem, 10.5, "Kit.MutedBrush");
                memory.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetColumn(load, 1);
                Grid.SetColumn(memory, 2);

                grid.Children.Add(label);
                grid.Children.Add(load);
                grid.Children.Add(memory);
                table.Children.Add(grid);
            }

            var card = KitUi.Surface(table, 4, 6, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "table hover", """
            // Hover highlight: rows stay quiet until the pointer finds one, shown here mid-hover.
            var rows = new (string Name, string Cpu, string Mem)[]
            {
                ("PipelineWorker", "12.4%", "184 MB"),
                ("Indexer", "8.1%", "96 MB"),
                ("Watcher", "3.7%", "42 MB"),
                ("Renderer", "22.9%", "512 MB")
            };

            var table = new StackPanel { Width = 440 };

            var head = new Grid { Height = 22, Margin = new Thickness(8, 0, 8, 0) };
            head.ColumnDefinitions.Add(new ColumnDefinition());
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

            var headProcess = KitUi.Mono("PROCESS", 9.5, "Kit.MutedBrush");
            var headCpu = KitUi.Mono("CPU", 9.5, "Kit.MutedBrush");
            var headMem = KitUi.Mono("MEMORY", 9.5, "Kit.MutedBrush");

            Grid.SetColumn(headCpu, 1);
            Grid.SetColumn(headMem, 2);

            head.Children.Add(headProcess);
            head.Children.Add(headCpu);
            head.Children.Add(headMem);
            table.Children.Add(head);

            for (var i = 0; i < rows.Length; i++)
            {
                var (name, cpu, mem) = rows[i];

                var grid = new Grid { Height = 28 };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });

                var label = KitUi.Text(name, 12);
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Margin = new Thickness(8, 0, 0, 0);

                var load = KitUi.Mono(cpu, 11);
                load.VerticalAlignment = VerticalAlignment.Center;

                var memory = KitUi.Mono(mem, 11, "Kit.MutedBrush");
                memory.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetColumn(load, 1);
                Grid.SetColumn(memory, 2);

                grid.Children.Add(label);
                grid.Children.Add(load);
                grid.Children.Add(memory);

                var band = new Border { CornerRadius = new CornerRadius(4), Child = grid };

                if (i == 1)
                {
                    band.SetResourceReference(Border.BackgroundProperty, "Kit.AccentSoftBrush");
                }

                table.Children.Add(band);
            }

            var card = KitUi.Surface(table, 8, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(true, "type styles", null),
        new(false, "type scale", """
            // Type scale: the five sizes KIT uses, each shown at its own size with its spec.
            var rows = new (string Name, double Size, string Weight, string Sample)[]
            {
                ("display", 26, "Bold", "Kit"),
                ("heading", 18, "Bold", "Design library"),
                ("body", 12.5, "Regular", "Dense rows, mono values, visible focus."),
                ("caption", 10.5, "Regular", "1px outlines, 10px radius"),
                ("micro", 9.5, "SemiBold", "SECTION LABEL")
            };

            var list = new StackPanel { Width = 460 };

            foreach (var (name, size, weight, sample) in rows)
            {
                var grid = new Grid { Margin = new Thickness(0, 0, 0, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(96) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                var spec = KitUi.Mono($"{name} {size:0.#}", 10, "Kit.MutedBrush");
                spec.VerticalAlignment = VerticalAlignment.Center;

                var text = KitUi.Text(sample, size, "Kit.TextBrush");
                text.TextTrimming = TextTrimming.None;
                text.TextWrapping = TextWrapping.NoWrap;

                if (weight == "Bold")
                {
                    text.FontWeight = FontWeights.Bold;
                }
                else if (weight == "SemiBold")
                {
                    text.FontWeight = FontWeights.SemiBold;
                }

                Grid.SetColumn(text, 1);
                grid.Children.Add(spec);
                grid.Children.Add(text);
                list.Children.Add(grid);
            }

            var card = KitUi.Surface(list, 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "inline code", """
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

            """),
        new(false, "label pair", """
            // Label pair: the three lines that belong around a field, and what each one is for.
            var stack = new StackPanel { Width = 320 };

            var label = KitUi.Text("TIMEOUT (MS)", 9.5, "Kit.TextBrush");
            label.FontWeight = FontWeights.SemiBold;
            stack.Children.Add(label);

            var field = KitUi.Input("5000", 32);
            field.Margin = new Thickness(0, 5, 0, 0);
            stack.Children.Add(field);

            var hint = KitUi.Muted("Blank means the client default of 3000.", 10.5);
            hint.Margin = new Thickness(1, 5, 0, 0);
            stack.Children.Add(hint);

            var danger = new SolidColorBrush(Color.FromRgb(0xEF, 0x44, 0x44));
            var error = new TextBlock { Text = "Must be between 100 and 60000.", FontSize = 10.5, Foreground = danger };
            error.Margin = new Thickness(1, 9, 0, 0);
            stack.Children.Add(error);

            var errorLabel = KitUi.Text("OUTPUT PATH", 9.5, "Kit.TextBrush");
            errorLabel.FontWeight = FontWeights.SemiBold;
            errorLabel.Margin = new Thickness(0, 16, 0, 0);
            stack.Children.Add(errorLabel);

            var badField = KitUi.Input("C:\\Workspace\\build", 32);
            badField.Margin = new Thickness(0, 5, 0, 0);
            badField.BorderBrush = danger;
            stack.Children.Add(badField);

            var badHint = KitUi.Muted("Paths outside the workspace are refused.", 10.5);
            badHint.Margin = new Thickness(1, 5, 0, 0);
            stack.Children.Add(badHint);

            var card = KitUi.Surface(stack, 14, 8, "Kit.PanelBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "terminal prompt", """
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

            """),
        new(false, "tabular numbers", """
            // Tabular numbers: right-aligned mono so the decimal points line up down the column.
            var rows = new (string Label, string Value, string Unit)[]
            {
                ("Build time", "1.42", "s"),
                ("Package size", "63.4", "MB"),
                ("Designs", "96", ""),
                ("Warnings", "0", "")
            };

            var table = new StackPanel { Width = 320 };

            foreach (var (label, value, unit) in rows)
            {
                var grid = new Grid { Height = 28 };
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(34) });

                var name = KitUi.Muted(label, 11.5);
                name.VerticalAlignment = VerticalAlignment.Center;

                var number = KitUi.Mono(value, 13, "Kit.TextBrush");
                number.TextAlignment = TextAlignment.Right;
                number.VerticalAlignment = VerticalAlignment.Center;

                var suffix = KitUi.Muted(unit, 10);
                suffix.VerticalAlignment = VerticalAlignment.Center;
                suffix.Margin = new Thickness(6, 0, 0, 0);

                Grid.SetColumn(number, 1);
                Grid.SetColumn(suffix, 2);

                grid.Children.Add(name);
                grid.Children.Add(number);
                grid.Children.Add(suffix);
                table.Children.Add(grid);
                table.Children.Add(KitUi.Rule());
            }

            var card = KitUi.Surface(table, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "kicker block", """
            // Kicker block: three sizes doing three jobs - context, claim, and the sentence that
            // stops the claim from being a slogan.
            var kicker = KitUi.Text("DESIGN LIBRARY", 9.5, "Kit.AccentBrush");
            kicker.FontWeight = FontWeights.Bold;

            var heading = KitUi.Text("A place for the designs you keep rewriting", 20, "Kit.TextBrush");
            heading.FontWeight = FontWeights.Bold;
            heading.TextWrapping = TextWrapping.Wrap;
            heading.Margin = new Thickness(0, 6, 0, 0);

            var deck = KitUi.Muted(
                "KIT holds the buttons, rows and fields a programmer tool needs, as code you can copy into the next project.",
                12);
            deck.TextWrapping = TextWrapping.Wrap;
            deck.Margin = new Thickness(0, 8, 0, 0);

            var block = new StackPanel { Width = 420 };
            block.Children.Add(kicker);
            block.Children.Add(heading);
            block.Children.Add(deck);

            var caption = KitUi.Muted("20 / 12 / 9.5, one and a half steps apart.", 10);
            caption.Margin = new Thickness(0, 14, 0, 0);

            return KitUi.Col(0, block, caption);

            """),
        new(false, "code annotation", """
            // Code annotation: a numbered badge on the code line, the sentence that explains it.
            var notes = new (string Code, string Note)[]
            {
                ("var rows = KitUi.Col(8,", "Eight between rows - the same gap everywhere in KIT."),
                ("    KitUi.Head(\"Sizes\"),", "Head is the only helper that sets a weight."),
                ("    KitUi.Muted(\"20 / 12 / 9.5\"));", "Muted never drops below 10.5 for readability.")
            };

            var body = new StackPanel { Width = 470 };

            for (var i = 0; i < notes.Length; i++)
            {
                var (code, note) = notes[i];

                var badge = new Border
                {
                    Width = 18,
                    Height = 18,
                    CornerRadius = new CornerRadius(9),
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    Child = new TextBlock
                    {
                        Text = (i + 1).ToString(),
                        FontSize = 9.5,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };

                badge.SetResourceReference(Border.BorderBrushProperty, "Kit.AccentBrush");
                ((TextBlock)badge.Child).SetResourceReference(TextBlock.ForegroundProperty, "Kit.AccentBrush");

                var line = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 4) };
                line.Children.Add(badge);
                line.Children.Add(KitUi.Mono(code, 11.5, "Kit.TextBrush"));
                body.Children.Add(line);

                var explanation = KitUi.Muted(note, 10.5);
                explanation.Margin = new Thickness(28, 0, 0, 10);
                body.Children.Add(explanation);
            }

            var card = KitUi.Surface(body, 14, 8, "Kit.PanelBrush", "Kit.BorderBrush");
            card.HorizontalAlignment = HorizontalAlignment.Left;
            return card;

            """),
        new(false, "truncation", """
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

            """),
        new(false, "micro labels", """
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

            """),
        new(false, "link styles", """
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

            """),
        new(true, "collected looks", null),
        new(false, "one pill", """
            // One pill. The smallest thing that works.
            return new PillButton
            {
                Content = "Save",
                Size = PillSize.Large
            };

            """),
        new(false, "six sizes", """
            // Six pills, one template. Paste this whole file into KIT and press Run.
            var sizes = new[]
            {
                PillSize.Tiny,
                PillSize.Small,
                PillSize.Medium,
                PillSize.Large,
                PillSize.XLarge,
                PillSize.Huge
            };

            var row = new WrapPanel { Orientation = Orientation.Horizontal };

            foreach (var size in sizes)
            {
                row.Children.Add(new PillButton
                {
                    Content = size.ToString(),
                    Size = size,
                    Margin = new Thickness(0, 0, 8, 8)
                });
            }

            return row;

            """),
        new(false, "themed pill", """
            // The same control, rethemed per button: no template edits, just brushes.
            var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];

            return new PillButton
            {
                Content = "Themed",
                Size = PillSize.XLarge,
                Fill = accent,
                Ink = Brushes.Black,
                Edge = accent,
                HoverFill = Brushes.White,
                HoverInk = Brushes.Black,
                HoverEdge = Brushes.Black
            };

            """),
        new(false, "button states", """
            // One pill, three lives: normal, disabled, and filled with the live accent.
            var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];

            var row = new StackPanel { Orientation = Orientation.Horizontal };

            row.Children.Add(new PillButton
            {
                Content = "Normal",
                Size = PillSize.Medium,
                Margin = new Thickness(0, 0, 8, 0)
            });

            row.Children.Add(new PillButton
            {
                Content = "Disabled",
                Size = PillSize.Medium,
                IsEnabled = false,
                Margin = new Thickness(0, 0, 8, 0)
            });

            row.Children.Add(new PillButton
            {
                Content = "Accent",
                Size = PillSize.Medium,
                Fill = accent,
                Ink = Brushes.Black,
                Edge = accent,
                HoverFill = Brushes.White,
                HoverInk = Brushes.Black,
                HoverEdge = accent
            });

            return row;

            """),
        new(false, "segmented pill group", """
            // A segmented control built from the same pill: one active, two quiet.
            var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];
            var text = (Brush)Application.Current.Resources["Kit.TextBrush"];
            var muted = (Brush)Application.Current.Resources["Kit.MutedBrush"];
            var hover = (Brush)Application.Current.Resources["Kit.HoverBrush"];

            var group = new StackPanel { Orientation = Orientation.Horizontal };
            var labels = new[] { "Design", "Preview", "Code" };

            for (var i = 0; i < labels.Length; i++)
            {
                var active = i == 0;

                group.Children.Add(new PillButton
                {
                    Content = labels[i],
                    Size = PillSize.Small,
                    Margin = new Thickness(0, 0, 6, 0),
                    Fill = active ? accent : Brushes.Transparent,
                    Ink = active ? Brushes.Black : text,
                    Edge = active ? accent : muted,
                    HoverFill = active ? accent : hover,
                    HoverInk = active ? Brushes.Black : text,
                    HoverEdge = active ? accent : text
                });
            }

            return group;

            """),
        new(false, "field", """
            // A labelled field: mono value, small caps label, 1px outline.
            var field = (Style)Application.Current.Resources["Kit.FieldStyle"];

            var panel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Left };

            panel.Children.Add(new TextBlock
            {
                Text = "HOSTNAME",
                FontSize = 10,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["Kit.MutedBrush"],
                Margin = new Thickness(2, 0, 0, 6)
            });

            panel.Children.Add(new TextBox
            {
                Text = "build.local",
                Style = field,
                Width = 280
            });

            return panel;

            """),
        new(false, "card", """
            // A card: 1px outline, heading, one line of copy, one action.
            var card = (Style)Application.Current.Resources["Kit.CardStyle"];

            var stack = new StackPanel();

            stack.Children.Add(new TextBlock
            {
                Text = "Storage",
                Style = (Style)Application.Current.Resources["Kit.H2"]
            });

            stack.Children.Add(new TextBlock
            {
                Text = "Three sticks were written this morning. The oldest one is nearly full.",
                Style = (Style)Application.Current.Resources["Kit.Body"],
                Margin = new Thickness(0, 6, 0, 12)
            });

            stack.Children.Add(new PillButton
            {
                Content = "Open log",
                Size = PillSize.Small,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            return new Border
            {
                Style = card,
                Child = stack,
                Width = 340,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            """),
        new(false, "stat row", """
            // A dense property row: label left, mono value right, hairline under it.
            var mono = (FontFamily)Application.Current.Resources["Kit.MonoFont"];
            var text = (Brush)Application.Current.Resources["Kit.TextBrush"];
            var muted = (Brush)Application.Current.Resources["Kit.MutedBrush"];
            var line = (Brush)Application.Current.Resources["Kit.OutlineBrush"];

            var stack = new StackPanel { Width = 360 };

            var rows = new (string Label, string Value)[]
            {
                ("Device", "Sandisk Extreme"),
                ("Capacity", "58.6 GB"),
                ("Written", "12 sticks"),
                ("Last write", "11:42")
            };

            foreach (var (label, value) in rows)
            {
                var grid = new Grid { Margin = new Thickness(0, 6, 0, 6) };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var left = new TextBlock { Text = label, FontSize = 12, Foreground = muted };
                var right = new TextBlock
                {
                    Text = value,
                    FontFamily = mono,
                    FontSize = 12,
                    Foreground = text
                };

                Grid.SetColumn(right, 1);
                grid.Children.Add(left);
                grid.Children.Add(right);

                stack.Children.Add(grid);
                stack.Children.Add(new Border { Height = 1, Background = line });
            }

            return stack;

            """),
        new(false, "empty state", """
            // An empty state: one dot, one line of what happened, one way out.
            var accent = (Brush)Application.Current.Resources["Kit.AccentBrush"];

            var stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            stack.Children.Add(new System.Windows.Shapes.Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = accent,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 12)
            });

            stack.Children.Add(new TextBlock
            {
                Text = "No sticks yet",
                Style = (Style)Application.Current.Resources["Kit.H2"],
                HorizontalAlignment = HorizontalAlignment.Center
            });

            stack.Children.Add(new TextBlock
            {
                Text = "Plug one in and KIT will pick it up.",
                Style = (Style)Application.Current.Resources["Kit.MutedText"],
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 6, 0, 14)
            });

            stack.Children.Add(new PillButton
            {
                Content = "Scan",
                Size = PillSize.Small,
                HorizontalAlignment = HorizontalAlignment.Center
            });

            return stack;

            """),
        new(false, "chips", """
            // Chips: the pill at Tiny, quiet until you hover it.
            var muted = (Brush)Application.Current.Resources["Kit.MutedBrush"];
            var text = (Brush)Application.Current.Resources["Kit.TextBrush"];
            var hover = (Brush)Application.Current.Resources["Kit.HoverBrush"];

            var wrap = new WrapPanel { Orientation = Orientation.Horizontal };

            var tags = new[] { "usb", "fat32", "iso", "pe", "signed" };

            foreach (var tag in tags)
            {
                wrap.Children.Add(new PillButton
                {
                    Content = tag,
                    Size = PillSize.Tiny,
                    Margin = new Thickness(0, 0, 6, 6),
                    Fill = Brushes.Transparent,
                    Ink = muted,
                    Edge = muted,
                    HoverFill = hover,
                    HoverInk = text,
                    HoverEdge = text
                });
            }

            return wrap;

            """),
        new(false, "greeting", """
            // cookies: a welcome card built from KIT styles, with one action.
            var card = (Style)Application.Current.Resources["Kit.CardStyle"];

            var stack = new StackPanel();

            stack.Children.Add(new TextBlock
            {
                Text = "i like cookies",
                Style = (Style)Application.Current.Resources["Kit.H1"]
            });

            stack.Children.Add(new TextBlock
            {
                Text = "Hello, friend. Every label on the left keeps its own snippet; this one is just a card.",
                Style = (Style)Application.Current.Resources["Kit.Body"],
                Margin = new Thickness(0, 6, 0, 14)
            });

            stack.Children.Add(new PillButton
            {
                Content = "Got it",
                Size = PillSize.Medium,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            return new Border
            {
                Style = card,
                Child = stack,
                Width = 360,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            """),
        new(false, "mint tabs", """
            // Mint outline: the selected tab is a filled mint tile, the others sit back in slate.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var row = new StackPanel { Orientation = Orientation.Horizontal };

            var tabs = new (string Label, bool Active)[]
            {
                ("1. Overview", true),
                ("2. Live telemetry", false),
                ("3. Changes & search", false)
            };

            foreach (var (label, active) in tabs)
            {
                row.Children.Add(new FlatButton
                {
                    Content = label,
                    Height = 48,
                    Padding = new Thickness(22, 0, 22, 0),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Radius = new CornerRadius(11),
                    Margin = new Thickness(0, 0, 9, 0),
                    Fill = active ? C("#22D48C") : C("#1A2B2E"),
                    Ink = active ? C("#06130E") : C("#CBD8D8"),
                    Edge = active ? C("#45F0A8") : C("#344A4E"),
                    HoverFill = active ? C("#3BEAA0") : C("#24383C"),
                    HoverInk = active ? C("#06130E") : C("#FFFFFF"),
                    HoverEdge = active ? C("#7CFFC9") : C("#63838A")
                });
            }

            // The strip keeps the row readable on a light surface too.
            return new Border
            {
                Background = C("#08120F"),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(9),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = row
            };

            """),
        new(false, "mint list", """
            // Mint outline: a mint-outlined search field, then full-width outlined rows.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var stack = new StackPanel { Width = 520 };

            stack.Children.Add(new Border
            {
                Background = C("#0C1F1B"),
                BorderBrush = C("#2E9C74"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(11, 8, 11, 8),
                Child = new TextBlock
                {
                    Text = "Search name / ID / hardware key",
                    FontSize = 13,
                    Foreground = C("#7EA79A")
                }
            });

            stack.Children.Add(new TextBlock
            {
                Text = "158 entries with registry values - click one to decode it",
                FontSize = 11.5,
                Foreground = C("#9FB5AE"),
                Margin = new Thickness(2, 7, 0, 9)
            });

            var names = new[]
            {
                "ACPI x64-based PC",
                "Bluetooth Device (Personal Area Network)",
                "CD-ROM Drive (1 of 4)"
            };

            foreach (var name in names)
            {
                stack.Children.Add(new FlatButton
                {
                    Content = name,
                    Height = 42,
                    Margin = new Thickness(0, 0, 0, 6),
                    FontSize = 14.5,
                    FontWeight = FontWeights.Bold,
                    Radius = new CornerRadius(21),
                    Fill = C("#0F241F"),
                    Ink = C("#EAF6F0"),
                    Edge = C("#2E9C74"),
                    HoverFill = C("#163A2F"),
                    HoverInk = C("#FFFFFF"),
                    HoverEdge = C("#4BE0A6")
                });
            }

            return stack;

            """),
        new(false, "mint flyout", """
            // Mint outline detail: a flyout beside a row, with its value editor stacked right.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);
            var mono = (FontFamily)Application.Current.Resources["Kit.MonoFont"];

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(196) });

            var left = new StackPanel { Margin = new Thickness(0, 0, 18, 0) };

            left.Children.Add(new TextBlock
            {
                Text = "Resource picker tags",
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = C("#EAF6F0")
            });

            left.Children.Add(new TextBlock
            {
                Text = "No description is declared for this value in the definition or its parameter tree.",
                FontSize = 11.5,
                FontStyle = FontStyles.Italic,
                Foreground = C("#9FB5AE"),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 4, 0, 10)
            });

            left.Children.Add(new TextBlock
            {
                Text = "Key: HKLM:\\SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e966-e325-11ce-bfc1-08002be10318}\\0000",
                FontFamily = mono,
                FontSize = 10.5,
                Foreground = C("#7FBFA6"),
                TextWrapping = TextWrapping.Wrap
            });

            var right = new StackPanel();

            right.Children.Add(new Border
            {
                Background = C("#0A1A16"),
                BorderBrush = C("#2E9C74"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(10, 7, 10, 7),
                Margin = new Thickness(0, 0, 0, 8),
                Child = new TextBlock
                {
                    Text = "HAL",
                    FontSize = 12,
                    Foreground = C("#EAF6F0")
                }
            });

            right.Children.Add(new FlatButton
            {
                Content = "Apply value",
                Height = 32,
                FontSize = 12,
                Radius = new CornerRadius(8),
                Margin = new Thickness(0, 0, 0, 7),
                Fill = C("#1B3B33"),
                Ink = C("#EAF6F0"),
                Edge = C("#2E9C74"),
                HoverFill = C("#245146"),
                HoverInk = C("#FFFFFF"),
                HoverEdge = C("#4BE0A6")
            });

            right.Children.Add(new FlatButton
            {
                Content = "Revert to backup",
                Height = 32,
                FontSize = 12,
                Radius = new CornerRadius(8),
                IsEnabled = false,
                Fill = C("#0D1B18"),
                Ink = C("#5F7A72"),
                Edge = C("#24413A"),
                HoverFill = C("#0D1B18"),
                HoverInk = C("#5F7A72"),
                HoverEdge = C("#24413A")
            });

            Grid.SetColumn(right, 1);
            grid.Children.Add(left);
            grid.Children.Add(right);

            return new Border
            {
                Width = 560,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = C("#0E1F1D"),
                BorderBrush = C("#2A6F5C"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(16),
                Child = grid
            };

            """),
        new(false, "mint rescan", """
            // Mint outline footer: the full-width secondary button under a list.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            return new StackPanel
            {
                Width = 520,
                Children =
                {
                    new FlatButton
                    {
                        Content = "Rescan hardware",
                        Height = 36,
                        FontSize = 12,
                        Radius = new CornerRadius(18),
                        Fill = C("#17292B"),
                        Ink = C("#DCE8E6"),
                        Edge = C("#2E9C74"),
                        HoverFill = C("#1E3A38"),
                        HoverInk = C("#FFFFFF"),
                        HoverEdge = C("#4BE0A6")
                    },
                    new TextBlock
                    {
                        Text = "158 entries read from the live registry this session.",
                        FontSize = 11,
                        Foreground = C("#7E968F"),
                        Margin = new Thickness(2, 9, 0, 0)
                    }
                }
            };

            """),
        new(false, "blue header", """
            // Blue capsule: title left, then the glass pill, the theme dots and the mode pill.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var grid = new Grid { Width = 556 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var title = new StackPanel { VerticalAlignment = VerticalAlignment.Center };

            title.Children.Add(new TextBlock
            {
                Text = "DISKTOOL",
                FontSize = 21,
                FontWeight = FontWeights.Bold,
                Foreground = C("#F2F7FF")
            });

            title.Children.Add(new TextBlock
            {
                Text = "v2.2.0 · partition suite",
                FontSize = 11,
                Foreground = C("#8FAFD2")
            });

            var right = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            var read = new FlatButton
            {
                Content = "Read all devices",
                Height = 34,
                Padding = new Thickness(18, 0, 18, 0),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Radius = new CornerRadius(17),
                Fill = new LinearGradientBrush(
                    Color.FromRgb(0xE4, 0xEF, 0xFD),
                    Color.FromRgb(0x9C, 0xBB, 0xE2),
                    90.0),
                Ink = C("#0E1C2E"),
                Edge = C("#D6E6FA"),
                HoverFill = new LinearGradientBrush(
                    Color.FromRgb(0xF6, 0xFA, 0xFF),
                    Color.FromRgb(0xB8, 0xD2, 0xF2),
                    90.0),
                HoverInk = C("#0E1C2E"),
                HoverEdge = C("#FFFFFF"),
                Margin = new Thickness(0, 0, 14, 0)
            };

            var dots = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 14, 0)
            };

            var swatches = new[]
            {
                ("#45E0A0", false),
                ("#FF5C7A", false),
                ("#4F7BFF", true),
                ("#B18CFF", false),
                ("#FFC94D", false)
            };

            foreach (var (hex, selected) in swatches)
            {
                dots.Children.Add(new System.Windows.Shapes.Ellipse
                {
                    Width = 15,
                    Height = 15,
                    Fill = C(hex),
                    Stroke = selected ? C("#FFFFFF") : Brushes.Transparent,
                    StrokeThickness = selected ? 2.5 : 0,
                    Margin = new Thickness(4, 0, 4, 0)
                });
            }

            var mode = new FlatButton
            {
                Content = "Light mode",
                Width = 128,
                Height = 34,
                FontSize = 12.5,
                Radius = new CornerRadius(17),
                Fill = C("#1B3355"),
                Ink = C("#EAF3FF"),
                Edge = C("#D7E6F7"),
                HoverFill = C("#25446D"),
                HoverInk = C("#FFFFFF"),
                HoverEdge = C("#FFFFFF")
            };

            right.Children.Add(read);
            right.Children.Add(dots);
            right.Children.Add(mode);

            Grid.SetColumn(right, 1);
            grid.Children.Add(title);
            grid.Children.Add(right);

            // The strip is the app's own navy, so the white title survives a light host.
            return new Border
            {
                Background = C("#101E33"),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(16, 12, 16, 12),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = grid
            };

            """),
        new(false, "blue device bar", """
            // Blue capsule: one outlined capsule holds the disk label and its pills.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var pills = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var actions = new (string Label, bool Active)[]
            {
                ("Locked", false),
                ("GPT", true),
                ("MBR", false),
                ("+ Create", false),
                ("Delete", false),
                ("Format", false),
                ("Unmount", false),
                ("Wipe", false)
            };

            foreach (var (label, active) in actions)
            {
                pills.Children.Add(new FlatButton
                {
                    Content = label,
                    Height = 28,
                    Padding = new Thickness(9, 0, 9, 0),
                    FontSize = 10.5,
                    FontWeight = FontWeights.SemiBold,
                    Radius = new CornerRadius(14),
                    Margin = new Thickness(0, 0, 4, 0),
                    Fill = active
                        ? new LinearGradientBrush(
                            Color.FromRgb(0x54, 0x9F, 0xFF),
                            Color.FromRgb(0x2A, 0x67, 0xD8),
                            90.0)
                        : C("#1B3A5C"),
                    Ink = active ? Brushes.White : C("#DCEBFF"),
                    Edge = active ? C("#8FC2FF") : C("#3E7FC4"),
                    HoverFill = active
                        ? new LinearGradientBrush(
                            Color.FromRgb(0x76, 0xB4, 0xFF),
                            Color.FromRgb(0x3A, 0x7B, 0xEE),
                            90.0)
                        : C("#254B72"),
                    HoverInk = Brushes.White,
                    HoverEdge = active ? C("#BFDBFF") : C("#6FA6DE")
                });
            }

            var grid = new Grid();

            grid.Children.Add(new TextBlock
            {
                Text = "DISK 0  ·  476.9 GB  ·  GPT",
                FontFamily = (FontFamily)Application.Current.Resources["Kit.MonoFont"],
                FontSize = 9.5,
                Foreground = C("#93B4D6"),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(6, 0, 10, 0)
            });

            grid.Children.Add(pills);

            return new Border
            {
                Width = 600,
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 46,
                Background = C("#16283F"),
                BorderBrush = C("#2F6FC0"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(23),
                Padding = new Thickness(8, 0, 5, 0),
                Child = grid
            };

            """),
        new(false, "blue cards", """
            // Blue capsule partitions: a card per volume, with its filesystem, size and letter.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);
            var mono = (FontFamily)Application.Current.Resources["Kit.MonoFont"];

            var row = new StackPanel { Orientation = Orientation.Horizontal };

            var parts = new (string Number, string Kind, string Format, string Size, string Letter, double Fill)[]
            {
                ("P1", "Basic data partition", "FAT32", "200 MB", "[ ]", 0.06),
                ("P3", "Basic data partition", "NTFS", "473.8 GB", "[C:]", 0.62),
                ("P4", "Windows Recovery", "NTFS", "892 MB", "[ ]", 0.12)
            };

            foreach (var (number, kind, format, size, letter, fill) in parts)
            {
                var card = new StackPanel();

                var head = new Grid();
                head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                var tag = new TextBlock
                {
                    Text = number,
                    FontFamily = mono,
                    FontSize = 10,
                    Foreground = C("#7FA5C8"),
                    Margin = new Thickness(0, 0, 8, 0)
                };

                var label = new TextBlock
                {
                    Text = kind,
                    FontSize = 10.5,
                    Foreground = C("#A9C3DD"),
                    TextTrimming = TextTrimming.CharacterEllipsis
                };

                Grid.SetColumn(label, 1);
                head.Children.Add(tag);
                head.Children.Add(label);
                card.Children.Add(head);

                card.Children.Add(new TextBlock
                {
                    Text = format,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    Foreground = C("#5FA8FF"),
                    Margin = new Thickness(0, 5, 0, 0)
                });

                card.Children.Add(new TextBlock
                {
                    Text = size,
                    FontFamily = mono,
                    FontSize = 10.5,
                    Foreground = C("#DCEBFF"),
                    Margin = new Thickness(0, 3, 0, 7)
                });

                var track = new Grid { Height = 3 };
                track.Children.Add(new Border { Background = C("#24405F"), CornerRadius = new CornerRadius(2) });
                track.Children.Add(new Border
                {
                    Background = C("#5FA8FF"),
                    CornerRadius = new CornerRadius(2),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 106 * fill
                });

                card.Children.Add(track);

                card.Children.Add(new TextBlock
                {
                    Text = letter,
                    FontFamily = mono,
                    FontSize = 10.5,
                    Foreground = C("#A9C3DD"),
                    Margin = new Thickness(0, 7, 0, 0)
                });

                row.Children.Add(new Border
                {
                    Width = 168,
                    Background = C("#14263C"),
                    BorderBrush = C("#2E6FA8"),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),
                    Padding = new Thickness(11, 9, 11, 9),
                    Margin = new Thickness(0, 0, 9, 0),
                    Child = card
                });
            }

            return row;

            """),
        new(false, "blue action bar", """
            // Blue capsule action bar: a quiet scan action and the write-arm toggle, centred.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var scanText = new StackPanel { Orientation = Orientation.Horizontal };

            scanText.Children.Add(new TextBlock
            {
                Text = "⛶",
                FontSize = 12,
                Margin = new Thickness(0, 0, 7, 0),
                VerticalAlignment = VerticalAlignment.Center
            });

            scanText.Children.Add(new TextBlock
            {
                Text = "Scan sectors",
                FontSize = 12,
                VerticalAlignment = VerticalAlignment.Center
            });

            var armText = new StackPanel { Orientation = Orientation.Horizontal };

            armText.Children.Add(new Border
            {
                Width = 12,
                Height = 12,
                CornerRadius = new CornerRadius(3),
                Background = C("#0C1A2A"),
                BorderBrush = C("#6FA6DE"),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Center
            });

            armText.Children.Add(new TextBlock
            {
                Text = "Arm sector writes",
                FontSize = 12,
                VerticalAlignment = VerticalAlignment.Center
            });

            var row = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            row.Children.Add(new FlatButton
            {
                Content = scanText,
                Height = 30,
                Padding = new Thickness(14, 0, 14, 0),
                Radius = new CornerRadius(15),
                Margin = new Thickness(0, 0, 10, 0),
                Fill = C("#1B3355"),
                Ink = C("#9DBBDA"),
                Edge = C("#2F5A85"),
                HoverFill = C("#25446D"),
                HoverInk = C("#EAF3FF"),
                HoverEdge = C("#5C90C8")
            });

            row.Children.Add(new FlatButton
            {
                Content = armText,
                Height = 30,
                Padding = new Thickness(14, 0, 14, 0),
                Radius = new CornerRadius(15),
                Fill = C("#1B3355"),
                Ink = C("#EAF3FF"),
                Edge = C("#2F5A85"),
                HoverFill = C("#25446D"),
                HoverInk = C("#FFFFFF"),
                HoverEdge = C("#5C90C8")
            });

            return new Border
            {
                Width = 600,
                Height = 46,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = C("#24405F"),
                BorderBrush = C("#2E6FA8"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Child = row
            };

            """),
        new(false, "neon title", """
            // Purple neon title row: the product name, a workspace chip, the build note.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var chipContent = new StackPanel { Orientation = Orientation.Horizontal };

            chipContent.Children.Add(new TextBlock
            {
                Text = "▤",
                FontSize = 12,
                Margin = new Thickness(0, 0, 7, 0),
                VerticalAlignment = VerticalAlignment.Center
            });

            chipContent.Children.Add(new TextBlock
            {
                Text = "Workspace: C:\\Workspace\\ImageBuild",
                FontSize = 11,
                VerticalAlignment = VerticalAlignment.Center
            });

            var chip = new Border
            {
                Background = C("#120A1B"),
                BorderBrush = C("#A93BF5"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(18),
                Padding = new Thickness(14, 6, 14, 6),
                HorizontalAlignment = HorizontalAlignment.Center,
                Child = chipContent
            };

            TextBlock.SetForeground(chip, C("#E4D6F5"));

            var grid = new Grid { Width = 560 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var title = new TextBlock
            {
                Text = "Build Suite v5.1.0",
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Foreground = C("#C084FC"),
                VerticalAlignment = VerticalAlignment.Center
            };

            var note = new TextBlock
            {
                Text = "local build",
                FontSize = 10.5,
                Foreground = C("#8E7AA3"),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(12, 0, 0, 0)
            };

            Grid.SetColumn(chip, 1);
            Grid.SetColumn(note, 2);

            grid.Children.Add(title);
            grid.Children.Add(chip);
            grid.Children.Add(note);

            return new Border
            {
                Background = C("#050208"),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(12, 10, 12, 10),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = grid
            };

            """),
        new(false, "neon tabs", """
            // Purple neon tabs: the live tab wears a purple outline and a bar under it.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var row = new StackPanel { Orientation = Orientation.Horizontal };

            var tabs = new (string Label, bool Active)[]
            {
                ("Image servicing", true),
                ("Driver service", false),
                ("Package removal", false),
                ("Registry policies", false),
                ("Settings", false)
            };

            foreach (var (label, active) in tabs)
            {
                var cell = new StackPanel { Margin = new Thickness(0, 0, 6, 0) };

                cell.Children.Add(new FlatButton
                {
                    Content = label,
                    Height = 36,
                    Padding = new Thickness(12, 0, 12, 0),
                    FontSize = 11.5,
                    FontWeight = active ? FontWeights.SemiBold : FontWeights.Normal,
                    Radius = new CornerRadius(18),
                    Fill = active ? C("#160A20") : Brushes.Transparent,
                    Ink = active ? C("#FFFFFF") : C("#B9A8C9"),
                    Edge = active ? C("#B23BFF") : Brushes.Transparent,
                    HoverFill = active ? C("#22102F") : C("#160A20"),
                    HoverInk = C("#FFFFFF"),
                    HoverEdge = active ? C("#D18AFF") : C("#6B3A8A")
                });

                cell.Children.Add(new Border
                {
                    Height = 3,
                    Width = active ? 100 : 0,
                    Background = C("#A93BF5"),
                    CornerRadius = new CornerRadius(2),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 6, 0, 0)
                });

                row.Children.Add(cell);
            }

            // The strip keeps the quiet tabs legible on a light surface.
            return new Border
            {
                Background = C("#07030B"),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(9),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = row
            };

            """),
        new(false, "neon buttons", """
            // Purple neon buttons: a near-black gradient pill with a purple edge and an icon.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);

            var actions = new (string Glyph, string Label)[]
            {
                ("⤓", "Backup Drivers"),
                ("◉", "Automated ISO Pipeline"),
                ("↺", "Restore Drivers"),
                ("▤", "Browse Image & Slipstream"),
                ("■", "HALT ALL ACTIONS"),
                ("⚙", "Generate answer file")
            };

            var grid = new Grid { Width = 600 };

            for (var column = 0; column < 2; column++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (var line = 0; line < 3; line++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            for (var i = 0; i < actions.Length; i++)
            {
                var (glyph, label) = actions[i];

                var content = new StackPanel { Orientation = Orientation.Horizontal };

                content.Children.Add(new TextBlock
                {
                    Text = glyph,
                    FontSize = 13,
                    Margin = new Thickness(0, 0, 9, 0),
                    VerticalAlignment = VerticalAlignment.Center
                });

                content.Children.Add(new TextBlock
                {
                    Text = label,
                    FontSize = 12,
                    FontWeight = FontWeights.SemiBold,
                    VerticalAlignment = VerticalAlignment.Center
                });

                var button = new FlatButton
                {
                    Content = content,
                    Height = 42,
                    Radius = new CornerRadius(21),
                    Margin = new Thickness(4, 4, 4, 4),
                    Fill = new LinearGradientBrush(
                        Color.FromRgb(0x2C, 0x25, 0x36),
                        Color.FromRgb(0x0B, 0x09, 0x10),
                        90.0),
                    Ink = C("#F3ECFF"),
                    Edge = C("#A93BF5"),
                    HoverFill = new LinearGradientBrush(
                        Color.FromRgb(0x3E, 0x30, 0x50),
                        Color.FromRgb(0x14, 0x0C, 0x1D),
                        90.0),
                    HoverInk = C("#FFFFFF"),
                    HoverEdge = C("#D18AFF")
                };

                Grid.SetRow(button, i / 2);
                Grid.SetColumn(button, i % 2);
                grid.Children.Add(button);
            }

            return grid;

            """),
        new(false, "neon console", """
            // Purple neon console: a near-black well, a purple edge, and mono output lines.
            Brush C(string hex) => (Brush)new BrushConverter().ConvertFromString(hex);
            var mono = (FontFamily)Application.Current.Resources["Kit.MonoFont"];

            var stack = new StackPanel();

            stack.Children.Add(new TextBlock
            {
                Text = "Live terminal stream & hardware diagnostic console",
                FontSize = 13,
                FontWeight = FontWeights.Bold,
                Foreground = C("#E4D6F5"),
                Margin = new Thickness(2, 0, 0, 9)
            });

            var lines = new (string Time, string Message)[]
            {
                ("10:49:18", "Ready. Running with administrator rights."),
                ("10:49:18", "Console v5.1.0 loaded with the hardware baseline deck."),
                ("10:49:18", "Workspace: C:\\Workspace\\ImageBuild")
            };

            foreach (var (time, message) in lines)
            {
                var line = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(2, 1, 2, 1)
                };

                line.Children.Add(new TextBlock
                {
                    Text = "[" + time + "]",
                    FontFamily = mono,
                    FontSize = 11.5,
                    Foreground = C("#C084FC"),
                    Margin = new Thickness(0, 0, 8, 0)
                });

                line.Children.Add(new TextBlock
                {
                    Text = message,
                    FontFamily = mono,
                    FontSize = 11.5,
                    Foreground = C("#CDB6E0"),
                    TextWrapping = TextWrapping.Wrap
                });

                stack.Children.Add(line);
            }

            return new Border
            {
                Width = 580,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = C("#07030B"),
                BorderBrush = C("#7A2BB5"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(9),
                Padding = new Thickness(15, 13, 15, 15),
                Child = stack
            };

            """),
    ];
}

