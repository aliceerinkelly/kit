using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Kit.Controls;
using Kit.Theming;

namespace Kit;

public partial class MainWindow : Window
{
    private KitAccent _accent = KitThemes.Accents[0];
    private bool _dark = true;
    private string? _copiedName;
    private string? _copiedCode;
    private bool _copiedHeader;
    private DesignEntry? _shown;

    public ObservableCollection<DesignEntry> Designs { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        var stored = DesignStore.Load();

        // First run on a machine with no saved rail: open with the shipped library
        // rather than an empty canvas. Only when no file exists, so deleting every row
        // stays deleted instead of resurrecting on the next start.
        var seeded = false;
        if (stored.Count == 0 && !DesignStore.Exists)
        {
            stored = StarterCatalog.Read();
            seeded = stored.Count > 0;
        }

        foreach (var record in stored)
        {
            if (!string.IsNullOrWhiteSpace(record.Name))
            {
                Designs.Add(new DesignEntry(record.Name, custom: true, record.Code, record.IsHeader));
            }
        }

        if (seeded)
        {
            DesignStore.Save(Designs);
        }

        Rail.Reindex(Designs);

        DataContext = this;
        EmptyHint.Visibility = Designs.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        UpdateChrome();

        if (Designs.Count > 0)
        {
            Rail.Select(Designs, Designs[0]);
            ShowEntry(Designs[0]);
        }
        else
        {
            ShowEntry(null);
        }
    }

    private void Accent_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string name })
        {
            _accent = Array.Find(KitThemes.Accents, a => a.Name == name) ?? _accent;
            ThemeManager.Apply(_accent, _dark);
            UpdateChrome();
        }
    }

    private void Mode_Click(object sender, RoutedEventArgs e)
    {
        _dark = !_dark;
        ThemeManager.Apply(_accent, _dark);
        UpdateChrome();
    }

    private void UpdateChrome()
    {
        ThemeName.Text = $"{_accent.Name} · {(_dark ? "Dark" : "Light")}";
        ModeButton.Content = _dark ? "Light mode" : "Dark mode";
    }

    private void Design_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton { DataContext: DesignEntry entry })
        {
            ShowEntry(entry);
        }
    }

    private void AddLine_Click(object sender, RoutedEventArgs e)
    {
        // An empty box still adds something: a design you can name later beats a
        // button that looks broken.
        var name = NewLineBox.Text.Trim();
        if (name.Length == 0)
        {
            name = "New design";
        }

        var entry = Rail.AddDesign(Designs, name);
        NewLineBox.Clear();
        EmptyHint.Visibility = Visibility.Collapsed;
        ShowEntry(entry);
        DesignStore.Save(Designs);

        // Queue the focus: the click that got us here would otherwise keep it on the button.
        Dispatcher.BeginInvoke(new Action(() => CodeBox.Focus()), System.Windows.Threading.DispatcherPriority.Input);
    }

    private void AddHeader_Click(object sender, RoutedEventArgs e)
    {
        var name = NewLineBox.Text.Trim();
        if (name.Length == 0)
        {
            name = "New category";
        }

        var entry = Rail.AddHeader(Designs, name);
        NewLineBox.Clear();
        EmptyHint.Visibility = Visibility.Collapsed;
        ShowEntry(entry);
        DesignStore.Save(Designs);
    }

    private void ButtonSizes_Click(object sender, RoutedEventArgs e)
    {
        var window = new ButtonSizesWindow { Owner = this };
        window.Show();
    }

    /// <summary>
    /// The label is the unit: selecting it loads its own code on the right and shows
    /// whatever that code builds underneath. No label, no editor.
    /// </summary>
    private void ShowEntry(DesignEntry? entry)
    {
        // Switching labels keeps what you typed: the editor is a draft of the design
        // it was opened on, whether or not you pressed Run.
        StashEditor();
        _shown = entry;

        if (CanvasTitle is null)
        {
            return;
        }

        CanvasTitle.Text = entry?.Name ?? "No line selected";
        CanvasHint.Text = entry is null
            ? "Add a design on the left to start."
            : entry.IsHeader
                ? "Category. Select a design under it, or add one."
                : "Paste C#, press Run, and the control appears below.";

        CodeBox.IsEnabled = entry is not null && !entry.IsHeader;
        RunButton.IsEnabled = entry is not null && !entry.IsHeader;
        SampleButton.IsEnabled = entry is not null && !entry.IsHeader;
        RunErrors.Visibility = Visibility.Collapsed;
        RunStatus.Text = string.Empty;
        PreviewHost.Content = null;

        if (entry is null)
        {
            CodeBox.Text = string.Empty;
            RunStatus.Text = "No line selected";
            PreviewHint.Text = "Add a design on the left to start.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        if (entry.IsHeader)
        {
            var count = Rail.ChildCount(Designs, entry);
            CodeBox.Text = string.Empty;
            RunStatus.Text = count == 1 ? "1 design" : $"{count} designs";
            PreviewHint.Text = "A category holds designs. Select one to see its code.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        CodeBox.Text = entry.Code ?? string.Empty;
        CodeBox.ScrollToHome();

        if (string.IsNullOrWhiteSpace(entry.Code))
        {
            PreviewHint.Text = "This line has no code yet. Paste some and press Run.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        RunSnippet(save: false);
    }

    /// <summary>
    /// Writes the editor back onto the design it belongs to. Without this, selecting
    /// another label threw away code that had not been run yet.
    /// </summary>
    private void StashEditor()
    {
        if (_shown is not { IsHeader: false } leaving || CodeBox is null)
        {
            return;
        }

        var text = CodeBox.Text ?? string.Empty;
        if (string.Equals(leaving.Code ?? string.Empty, text, StringComparison.Ordinal))
        {
            return;
        }

        leaving.Code = text;
        DesignStore.Save(Designs);
    }

    // --- Rail: move, copy, paste, delete ---------------------------------

    private void Line_RightClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is RadioButton { DataContext: DesignEntry entry })
        {
            Rail.Select(Designs, entry);
            ShowEntry(entry);
        }
    }

    // --- Rename ----------------------------------------------------------

    private void Line_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is RadioButton { DataContext: DesignEntry entry } row)
        {
            BeginRename(row, entry);
            e.Handled = true;
        }
    }

    private void RenameLine_Click(object sender, RoutedEventArgs e)
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        var row = LinesList.ItemContainerGenerator.ContainerFromItem(entry) as ContentPresenter;
        var button = row is null ? null : FindChild<RadioButton>(row);

        if (button is not null)
        {
            BeginRename(button, entry);
        }
        else
        {
            entry.IsEditing = true;
        }
    }

    private void BeginRename(RadioButton row, DesignEntry entry)
    {
        // One row at a time: another row still editing gets committed first.
        foreach (var other in Designs.Where(line => line.IsEditing && !ReferenceEquals(line, entry)))
        {
            other.IsEditing = false;
        }

        Rail.Select(Designs, entry);
        ShowEntry(entry);
        entry.IsEditing = true;

        row.UpdateLayout();

        var box = FindChild<TextBox>(row);
        if (box is null)
        {
            return;
        }

        Dispatcher.BeginInvoke(new Action(() =>
        {
            box.Focus();
            box.SelectAll();
        }), DispatcherPriority.Input);
    }

    private void Rename_KeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not TextBox box)
        {
            return;
        }

        if (e.Key == Key.Enter)
        {
            CommitRename(box);
            e.Handled = true;
        }
        else if (e.Key == Key.Escape)
        {
            CancelRename(box);
            e.Handled = true;
        }
    }

    private void Rename_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (sender is TextBox box)
        {
            CommitRename(box);
        }
    }

    private void CommitRename(TextBox box)
    {
        if (box.DataContext is not DesignEntry entry || !entry.IsEditing)
        {
            return;
        }

        entry.IsEditing = false;

        var name = Rail.Rename(Designs, entry, box.Text);
        box.Text = name;

        if (entry.IsSelected)
        {
            CanvasTitle.Text = name;
            ShowEntry(entry);
        }

        DesignStore.Save(Designs);
    }

    private void CancelRename(TextBox box)
    {
        if (box.DataContext is DesignEntry entry)
        {
            box.Text = entry.Name;
            entry.IsEditing = false;
        }
    }

    private static T? FindChild<T>(DependencyObject root) where T : DependencyObject
    {
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
        {
            var child = VisualTreeHelper.GetChild(root, i);

            if (child is T match)
            {
                return match;
            }

            var inner = FindChild<T>(child);
            if (inner is not null)
            {
                return inner;
            }
        }

        return null;
    }

    private void MoveUp_Click(object sender, RoutedEventArgs e) => MoveLine(-1);

    private void MoveDown_Click(object sender, RoutedEventArgs e) => MoveLine(1);

    private void CopyLine_Click(object sender, RoutedEventArgs e) => CopyLine();

    private void PasteLine_Click(object sender, RoutedEventArgs e) => PasteLine();

    private void DeleteLine_Click(object sender, RoutedEventArgs e) => DeleteLine();

    private void LinesList_KeyDown(object sender, KeyEventArgs e)
    {
        var control = Keyboard.Modifiers == ModifierKeys.Control;

        if (e.Key == Key.F2)
        {
            RenameLine_Click(sender, e);
            e.Handled = true;
        }
        else if (e.Key == Key.Delete)
        {
            DeleteLine();
            e.Handled = true;
        }
        else if (control && e.Key == Key.Up)
        {
            MoveLine(-1);
            e.Handled = true;
        }
        else if (control && e.Key == Key.Down)
        {
            MoveLine(1);
            e.Handled = true;
        }
        else if (control && e.Key == Key.C)
        {
            CopyLine();
            e.Handled = true;
        }
        else if (control && e.Key == Key.V)
        {
            PasteLine();
            e.Handled = true;
        }
    }

    private void MoveLine(int delta)
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        if (Rail.Move(Designs, entry, delta) >= 0)
        {
            DesignStore.Save(Designs);
        }
    }

    private void CopyLine()
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        _copiedName = Rail.CopyName(entry);
        _copiedCode = entry.Code;
        _copiedHeader = entry.IsHeader;

        try
        {
            Clipboard.SetText(_copiedName);
        }
        catch
        {
            // The rail copy still works when the system clipboard is locked.
        }
    }

    private void PasteLine()
    {
        var name = _copiedName;

        if (string.IsNullOrWhiteSpace(name))
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    name = Clipboard.GetText();
                }
            }
            catch
            {
                name = null;
            }
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var entry = Rail.Paste(Designs, name, _copiedCode, _copiedHeader);
        _copiedCode = null;
        _copiedName = null;
        _copiedHeader = false;

        EmptyHint.Visibility = Visibility.Collapsed;
        ShowEntry(entry);
        DesignStore.Save(Designs);
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
        {
            OpenSnippet_Click(sender, e);
            e.Handled = true;
        }
    }

    private void DeleteLine()
    {
        if (Rail.Selected(Designs) is not { } entry)
        {
            return;
        }

        if (entry.IsHeader)
        {
            var children = Rail.ChildCount(Designs, entry);
            var question = children == 0
                ? $"Delete the category \"{entry.Name}\"?"
                : $"Delete the category \"{entry.Name}\" and its {children} design{(children == 1 ? "" : "s")}?";

            if (MessageBox.Show(this, question, "KIT", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
        }

        var next = Rail.Delete(Designs, entry);
        EmptyHint.Visibility = Designs.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        ShowEntry(next);
        DesignStore.Save(Designs);
    }

    private void Run_Click(object sender, RoutedEventArgs e)
    {
        RunSnippet();
    }

    /// <summary>
    /// The no-paste path: open a snippet file, run it, and keep it on the selected label.
    /// </summary>
    private void OpenSnippet_Click(object sender, RoutedEventArgs e)
    {
        if (Rail.Selected(Designs) is null)
        {
            return;
        }

        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Title = "Open a snippet",
            Filter = "C# snippets (*.cs)|*.cs|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            CheckFileExists = true
        };

        if (dialog.ShowDialog(this) != true)
        {
            return;
        }

        try
        {
            var code = System.IO.File.ReadAllText(dialog.FileName);
            var target = Rail.Selected(Designs);

            // A category cannot hold code: the file becomes a design named after itself.
            if (target is null || target.IsHeader)
            {
                target = Rail.AddDesign(Designs, System.IO.Path.GetFileNameWithoutExtension(dialog.FileName));
                EmptyHint.Visibility = Visibility.Collapsed;
            }
            else if (!string.IsNullOrWhiteSpace(target.Code)
                     && !string.Equals(target.Code.Trim(), code.Trim(), StringComparison.Ordinal))
            {
                var replace = MessageBox.Show(
                    this,
                    $"Replace the code in \"{target.Name}\"?",
                    "KIT",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (replace != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            target.Code = code;
            Rail.Select(Designs, target);
            ShowEntry(target);
            DesignStore.Save(Designs);
            RunStatus.Text = $"Loaded {System.IO.Path.GetFileName(dialog.FileName)}";
        }
        catch (Exception ex)
        {
            RunStatus.Text = "Could not open file";
            RunErrors.Text = ex.Message;
            RunErrors.Visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Sample belongs to the design you are in: a flyout with the code for one button,
    /// nothing added to the rail and nothing overwritten until you press Insert.
    /// </summary>
    private void Sample_Click(object sender, RoutedEventArgs e)
    {
        if (sender is UIElement anchor)
        {
            SampleFlyout.PlacementTarget = anchor;
        }

        // The flyout is its own transparent window, so paint it from live resources
        // rather than relying on dynamic lookup inside the popup tree.
        if (TryFindResource("Kit.RowBrush") is System.Windows.Media.Brush card)
        {
            SampleCard.Background = card;
        }

        if (TryFindResource("Kit.OutlineBrush") is System.Windows.Media.Brush edge)
        {
            SampleCard.BorderBrush = edge;
        }

        SampleCode.Text = SnippetRunner.OnePill;
        SampleFlyout.IsOpen = true;
    }

    private void InsertSample_Click(object sender, RoutedEventArgs e)
    {
        SampleFlyout.IsOpen = false;

        if (Rail.Selected(Designs) is null || Rail.Selected(Designs)!.IsHeader)
        {
            RunStatus.Text = "Select a design first";
            return;
        }

        CodeBox.Text = SnippetRunner.OnePill;
        CodeBox.Focus();
        RunStatus.Text = "One-button sample in the editor — press Run";
    }

    private void CopySample_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Clipboard.SetText(SnippetRunner.OnePill);
            RunStatus.Text = "One-button sample copied";
        }
        catch
        {
            RunStatus.Text = "Clipboard is busy";
        }

        SampleFlyout.IsOpen = false;
    }

    private void CodeBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
        {
            RunSnippet();
            e.Handled = true;
        }
    }

    /// <summary>
    /// Compiles the editor and drops the result into the preview. A successful run also
    /// saves the code onto the selected label, so each line keeps its own snippet.
    /// Errors land under the Run button, next to the code that caused them.
    /// </summary>
    private void RunSnippet(bool save = true)
    {
        var entry = Rail.Selected(Designs);
        var result = SnippetRunner.Run(CodeBox.Text ?? string.Empty);
        PreviewHost.Content = null;
        RunErrors.Visibility = Visibility.Collapsed;

        if (result.Messages.Count > 0)
        {
            RunStatus.Text = "Did not run";
            RunErrors.Text = string.Join(Environment.NewLine, result.Messages);
            RunErrors.Visibility = Visibility.Visible;
            PreviewHint.Text = "Fix the code above and press Run.";
            PreviewHint.Visibility = Visibility.Visible;
            return;
        }

        if (result.Preview is not { } element)
        {
            PreviewHint.Text = result.Text ?? "Build() returned nothing to show.";
            PreviewHint.Visibility = Visibility.Visible;
            RunStatus.Text = "Nothing to render";
            return;
        }

        PreviewHost.Content = element;
        PreviewHint.Visibility = Visibility.Collapsed;
        RunStatus.Text = save
            ? $"Rendered {element.GetType().Name} · saved to {entry?.Name}"
            : $"Rendered {element.GetType().Name} · auto from {entry?.Name}";

        if (save && entry is not null)
        {
            entry.Code = CodeBox.Text;
            DesignStore.Save(Designs);
        }
    }

    private void NewLineBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            AddLine_Click(sender, e);
            e.Handled = true;
        }
    }

}