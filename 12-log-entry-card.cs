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
