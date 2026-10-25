using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kit.Controls;

namespace Kit;

/// <summary>
/// The gallery for the C# pill button: all six sizes, drawn from the same template.
/// Built in code and free of KIT styles, so it can be pasted into any WPF project.
/// </summary>
public sealed class ButtonSizesWindow : Window
{
    private static readonly PillSize[] Sizes =
    [
        PillSize.Tiny,
        PillSize.Small,
        PillSize.Medium,
        PillSize.Large,
        PillSize.XLarge,
        PillSize.Huge
    ];

    private readonly Brush _text;
    private readonly Brush _muted;
    private readonly Brush _panel;
    private readonly Brush _panelEdge;

    public ButtonSizesWindow()
    {
        _text = Brush("Kit.TextBrush", Brushes.Black);
        _muted = Brush("Kit.MutedBrush", Brushes.Gray);
        _panel = Brush("Kit.PanelBrush", Brushes.Transparent);
        _panelEdge = Brush("Kit.BorderBrush", Brushes.Gray);

        Title = "KIT — Button sizes";
        Width = 520;
        Height = 580;
        MinWidth = 440;
        MinHeight = 480;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        // Uses the KIT font when the host app has it, otherwise WPF's default.
        if (TryFindResource("Kit.UiFont") is FontFamily font)
        {
            FontFamily = font;
        }

        if (TryFindResource("Kit.BackgroundBrush") is Brush page)
        {
            Background = page;
        }
        else
        {
            Background = Brushes.White;
        }

        var root = new Grid { Margin = new Thickness(22.0) };
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var heading = new TextBlock
        {
            Text = "Button sizes",
            FontSize = 20.0,
            FontWeight = FontWeights.SemiBold,
            Foreground = _text
        };

        var subheading = new TextBlock
        {
            Text = "One pill template, six sizes, black and white.",
            FontSize = 12.0,
            Foreground = _text,
            Margin = new Thickness(0.0, 5.0, 0.0, 14.0)
        };

        var header = new StackPanel();
        header.Children.Add(heading);
        header.Children.Add(subheading);
        Grid.SetRow(header, 0);
        root.Children.Add(header);

        var panel = new Border
        {
            Background = _panel,
            BorderBrush = _panelEdge,
            BorderThickness = new Thickness(1.0),
            CornerRadius = new CornerRadius(10.0),
            Padding = new Thickness(12.0)
        };

        var rows = new StackPanel { Margin = new Thickness(8.0, 6.0, 8.0, 6.0) };

        foreach (var size in Sizes)
        {
            rows.Children.Add(BuildRow(size));
        }

        panel.Child = rows;
        Grid.SetRow(panel, 1);
        root.Children.Add(panel);

        var footer = new TextBlock
        {
            Text = "Hover inverts the pill. Each size is 22, 26, 32, 38, 46, 56 tall.",
            FontSize = 11.0,
            Foreground = _muted,
            Margin = new Thickness(2.0, 10.0, 2.0, 0.0)
        };
        Grid.SetRow(footer, 2);
        root.Children.Add(footer);

        Content = root;
    }

    private Brush Brush(string key, Brush fallback) =>
        TryFindResource(key) as Brush ?? fallback;

    private Grid BuildRow(PillSize size)
    {
        var button = new PillButton
        {
            Size = size,
            Content = size.ToString(),
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0.0, 0.0, 0.0, 10.0)
        };

        var label = new TextBlock
        {
            Text = $"{size} · {button.Height:0}",
            FontSize = 11.0,
            Foreground = _muted,
            VerticalAlignment = VerticalAlignment.Center
        };

        var row = new Grid();
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        Grid.SetColumn(button, 1);
        row.Children.Add(label);
        row.Children.Add(button);
        return row;
    }
}
