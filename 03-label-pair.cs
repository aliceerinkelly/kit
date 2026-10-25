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
