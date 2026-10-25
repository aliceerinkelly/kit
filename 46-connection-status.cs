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
