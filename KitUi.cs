using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kit.Controls;

/// <summary>
/// Short cuts for KIT's theme, so a snippet can show a design instead of six setters
/// per element. Every brush here is a resource key rather than a hex, which means a
/// design built from these helpers follows the accent and the light/dark switch live.
/// </summary>
public static class KitUi
{
    /// <summary>Points one property at a theme resource, so it keeps following the theme.</summary>
    public static T Skin<T>(T element, DependencyProperty property, string key)
        where T : FrameworkElement
    {
        element.SetResourceReference(property, key);
        return element;
    }

    /// <summary>Applies one of the named styles from Themes/Kit.xaml.</summary>
    public static T Use<T>(T control, string key)
        where T : FrameworkElement
    {
        if (Application.Current?.TryFindResource(key) is Style style)
        {
            control.Style = style;
        }

        return control;
    }

    public static TextBlock Text(string text, double size = 12.5, string brush = "Kit.TextBrush") =>
        Skin(
            new TextBlock { Text = text, FontSize = size, TextTrimming = TextTrimming.CharacterEllipsis },
            TextBlock.ForegroundProperty,
            brush);

    public static TextBlock Muted(string text, double size = 11.5) => Text(text, size, "Kit.MutedBrush");

    public static TextBlock Head(string text, double size = 14)
    {
        var head = Text(text, size);
        head.FontWeight = FontWeights.Bold;
        return head;
    }

    public static TextBlock Mono(string text, double size = 11.5, string brush = "Kit.TextBrush")
    {
        var label = Text(text, size, brush);
        Skin(label, TextBlock.FontFamilyProperty, "Kit.MonoFont");
        return label;
    }

    public static StackPanel Col(double gap = 8, params UIElement[] children) => Stack(false, gap, children);

    public static StackPanel Row(double gap = 8, params UIElement[] children) => Stack(true, gap, children);

    private static StackPanel Stack(bool horizontal, double gap, UIElement[] children)
    {
        var panel = new StackPanel
        {
            Orientation = horizontal ? Orientation.Horizontal : Orientation.Vertical
        };

        for (var i = 0; i < children.Length; i++)
        {
            // Add to the margin the caller already set instead of replacing it: an
            // indent or a custom offset is part of the design, the gap is not.
            if (children[i] is FrameworkElement element && i < children.Length - 1)
            {
                var margin = element.Margin;

                element.Margin = horizontal
                    ? new Thickness(margin.Left, margin.Top, margin.Right + gap, margin.Bottom)
                    : new Thickness(margin.Left, margin.Top, margin.Right, margin.Bottom + gap);
            }

            panel.Children.Add(children[i]);
        }

        return panel;
    }

    public static Border Surface(
        UIElement child,
        double pad = 12,
        double radius = 8,
        string fill = "Kit.CardBrush",
        string edge = "Kit.BorderBrush")
    {
        var border = new Border
        {
            Padding = new Thickness(pad),
            CornerRadius = new CornerRadius(radius),
            BorderThickness = new Thickness(1),
            Child = child
        };

        Skin(border, Border.BackgroundProperty, fill);
        Skin(border, Border.BorderBrushProperty, edge);
        return border;
    }

    public static Border Rule(double thickness = 1) =>
        Skin(new Border { Height = thickness }, Border.BackgroundProperty, "Kit.BorderBrush");

    public static TextBox Input(string text, double height = 30, double size = 12)
    {
        var box = Skin(
            new TextBox
            {
                Text = text,
                Height = height,
                FontSize = size,
                Padding = new Thickness(9, 0, 9, 0),
                VerticalContentAlignment = VerticalAlignment.Center,
                BorderThickness = new Thickness(1)
            },
            TextBox.BackgroundProperty,
            "Kit.FieldBrush");

        Skin(box, TextBox.ForegroundProperty, "Kit.TextBrush");
        Skin(box, TextBox.BorderBrushProperty, "Kit.BorderBrush");
        Skin(box, TextBox.CaretBrushProperty, "Kit.AccentBrush");
        return box;
    }

    /// <summary>A pill. Active is accent on dark ink; quiet is panel on text ink.</summary>
    public static FlatButton Pill(string text, bool active = false, double height = 30)
    {
        var pill = new FlatButton
        {
            Content = text,
            Height = height,
            Radius = new CornerRadius(height / 2.0),
            Padding = new Thickness(14, 0, 14, 0),
            FontSize = 12,
            FontWeight = FontWeights.SemiBold
        };

        if (active)
        {
            Skin(pill, FlatButton.FillProperty, "Kit.AccentBrush");
            Skin(pill, FlatButton.InkProperty, "Kit.OnAccentBrush");
            Skin(pill, FlatButton.EdgeProperty, "Kit.AccentBrush");
            Skin(pill, FlatButton.HoverFillProperty, "Kit.AccentDeepBrush");
            Skin(pill, FlatButton.HoverInkProperty, "Kit.OnAccentBrush");
            Skin(pill, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
        }
        else
        {
            Skin(pill, FlatButton.FillProperty, "Kit.PanelBrush");
            Skin(pill, FlatButton.InkProperty, "Kit.TextBrush");
            Skin(pill, FlatButton.EdgeProperty, "Kit.BorderBrush");
            Skin(pill, FlatButton.HoverFillProperty, "Kit.HoverBrush");
            Skin(pill, FlatButton.HoverInkProperty, "Kit.TextBrush");
            Skin(pill, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
        }

        return pill;
    }

    /// <summary>A borderless action: an icon, a glyph, a quiet "close".</summary>
    public static FlatButton Quiet(string text, double height = 26, double width = double.NaN)
    {
        var quiet = new FlatButton
        {
            Content = text,
            Height = height,
            MinWidth = 0,
            Padding = new Thickness(6, 0, 6, 0),
            FontSize = 11,
            Radius = new CornerRadius(5),
            Fill = Brushes.Transparent,
            Edge = Brushes.Transparent,
            HoverFill = Brushes.Transparent
        };

        if (!double.IsNaN(width))
        {
            quiet.Width = width;
        }

        Skin(quiet, FlatButton.InkProperty, "Kit.MutedBrush");
        Skin(quiet, FlatButton.HoverInkProperty, "Kit.TextBrush");
        Skin(quiet, FlatButton.HoverEdgeProperty, "Kit.AccentBrush");
        return quiet;
    }

    public static Border Dot(double size = 8, string brush = "Kit.AccentBrush") =>
        Skin(
            new Border
            {
                Width = size,
                Height = size,
                CornerRadius = new CornerRadius(size / 2.0),
                VerticalAlignment = VerticalAlignment.Center
            },
            Border.BackgroundProperty,
            brush);

    public static Border Chip(string text, string fill = "Kit.RowBrush", string ink = "Kit.MutedBrush")
    {
        var chip = new Border
        {
            CornerRadius = new CornerRadius(4),
            Padding = new Thickness(8, 2, 8, 2),
            VerticalAlignment = VerticalAlignment.Center,
            Child = Text(text, 10.5, ink)
        };

        Skin(chip, Border.BackgroundProperty, fill);
        return chip;
    }

    /// <summary>A keycap: mono text in a small outlined box.</summary>
    public static Border Keycap(string text)
    {
        var cap = new Border
        {
            CornerRadius = new CornerRadius(4),
            BorderThickness = new Thickness(1),
            Padding = new Thickness(6, 2, 6, 2),
            VerticalAlignment = VerticalAlignment.Center,
            Child = Mono(text, 10)
        };

        Skin(cap, Border.BackgroundProperty, "Kit.FieldBrush");
        Skin(cap, Border.BorderBrushProperty, "Kit.BorderBrush");
        return cap;
    }

    public static Border Swatch(string hex, double size = 22) =>
        new()
        {
            Width = size,
            Height = size,
            CornerRadius = new CornerRadius(size / 2.0),
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex)),
            BorderThickness = new Thickness(1.5),
            BorderBrush = Brushes.White,
            Margin = new Thickness(0, 0, 8, 0)
        };
}
