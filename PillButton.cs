using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Kit.Controls;

/// <summary>The six pill heights KIT ships.</summary>
public enum PillSize
{
    Tiny,
    Small,
    Medium,
    Large,
    XLarge,
    Huge
}

/// <summary>
/// A pill button written in C#: one template, six sizes, black and white by theme,
/// and every brush exposed as a dependency property so it can be rethemed without
/// touching the template. The radius is always half the height, so it stays a pill
/// at any size a consumer sets.
/// </summary>
public class PillButton : Button
{
    private readonly record struct Metrics(
        double Height,
        double MinWidth,
        double FontSize,
        double PadX,
        double PadY);

    private static Metrics MetricsFor(PillSize size) => size switch
    {
        PillSize.Tiny => new(22.0, 52.0, 10.0, 8.0, 2.0),
        PillSize.Small => new(26.0, 62.0, 10.5, 10.0, 3.0),
        PillSize.Medium => new(32.0, 80.0, 12.0, 14.0, 5.0),
        PillSize.Large => new(38.0, 100.0, 13.0, 18.0, 7.0),
        PillSize.XLarge => new(46.0, 120.0, 15.0, 22.0, 9.0),
        _ => new(56.0, 144.0, 17.0, 28.0, 12.0)
    };

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size),
        typeof(PillSize),
        typeof(PillButton),
        new PropertyMetadata(PillSize.Medium, OnSizeChanged));

    /// <summary>Half the height, kept in sync on every layout pass.</summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
        nameof(Radius),
        typeof(CornerRadius),
        typeof(PillButton),
        new PropertyMetadata(new CornerRadius(0.0), OnBrushChanged));

    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
        nameof(Fill),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty InkProperty = DependencyProperty.Register(
        nameof(Ink),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty EdgeProperty = DependencyProperty.Register(
        nameof(Edge),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty HoverFillProperty = DependencyProperty.Register(
        nameof(HoverFill),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty HoverInkProperty = DependencyProperty.Register(
        nameof(HoverInk),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty HoverEdgeProperty = DependencyProperty.Register(
        nameof(HoverEdge),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.Black, OnBrushChanged));

    public static readonly DependencyProperty FocusEdgeProperty = DependencyProperty.Register(
        nameof(FocusEdge),
        typeof(Brush),
        typeof(PillButton),
        new PropertyMetadata(Brushes.DodgerBlue, OnBrushChanged));

    private Border? _chrome;

    public PillButton()
    {
        Cursor = Cursors.Hand;
        SnapsToDevicePixels = true;
        UseLayoutRounding = true;

        // Theme first: these resolve against the live application resources, so a
        // light/dark switch repaints every pill without rebuilding it.
        SetResourceReference(FillProperty, "Kit.MonoFillBrush");
        SetResourceReference(InkProperty, "Kit.MonoInkBrush");
        SetResourceReference(EdgeProperty, "Kit.MonoEdgeBrush");
        SetResourceReference(HoverFillProperty, "Kit.MonoHoverFillBrush");
        SetResourceReference(HoverInkProperty, "Kit.MonoHoverInkBrush");
        SetResourceReference(HoverEdgeProperty, "Kit.MonoHoverEdgeBrush");
        SetResourceReference(FocusEdgeProperty, "Kit.AccentBrush");
        SetResourceReference(FontFamilyProperty, "Kit.UiFont");

        Template = BuildTemplate();
        ApplyMetrics();
    }

    public PillSize Size
    {
        get => (PillSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public CornerRadius Radius
    {
        get => (CornerRadius)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    public Brush Ink
    {
        get => (Brush)GetValue(InkProperty);
        set => SetValue(InkProperty, value);
    }

    public Brush Edge
    {
        get => (Brush)GetValue(EdgeProperty);
        set => SetValue(EdgeProperty, value);
    }

    public Brush HoverFill
    {
        get => (Brush)GetValue(HoverFillProperty);
        set => SetValue(HoverFillProperty, value);
    }

    public Brush HoverInk
    {
        get => (Brush)GetValue(HoverInkProperty);
        set => SetValue(HoverInkProperty, value);
    }

    public Brush HoverEdge
    {
        get => (Brush)GetValue(HoverEdgeProperty);
        set => SetValue(HoverEdgeProperty, value);
    }

    public Brush FocusEdge
    {
        get => (Brush)GetValue(FocusEdgeProperty);
        set => SetValue(FocusEdgeProperty, value);
    }

    private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PillButton)d).ApplyMetrics();
    }

    private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PillButton)d).Refresh();
    }

    private static ControlTemplate BuildTemplate()
    {
        var chrome = new FrameworkElementFactory(typeof(Border), "Chrome");
        chrome.SetValue(Border.BorderThicknessProperty, new Thickness(1.0));

        var presenter = new FrameworkElementFactory(typeof(ContentPresenter));
        presenter.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        presenter.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

        chrome.AppendChild(presenter);

        var pressed = new Trigger { Property = ButtonBase.IsPressedProperty, Value = true };
        pressed.Setters.Add(new Setter(UIElement.OpacityProperty, 0.82, "Chrome"));

        var disabled = new Trigger { Property = UIElement.IsEnabledProperty, Value = false };
        disabled.Setters.Add(new Setter(UIElement.OpacityProperty, 0.4, "Chrome"));

        var template = new ControlTemplate(typeof(PillButton)) { VisualTree = chrome };
        template.Triggers.Add(pressed);
        template.Triggers.Add(disabled);
        return template;
    }

    private void ApplyMetrics()
    {
        var metrics = MetricsFor(Size);
        Height = metrics.Height;
        MinWidth = metrics.MinWidth;
        FontSize = metrics.FontSize;
        FontWeight = FontWeights.SemiBold;
        Padding = new Thickness(metrics.PadX, metrics.PadY, metrics.PadX, metrics.PadY);
        Radius = new CornerRadius(metrics.Height / 2.0);
        Refresh();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _chrome = GetTemplateChild("Chrome") as Border;
        Refresh();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);

        if (sizeInfo.NewSize.Height > 0)
        {
            Radius = new CornerRadius(sizeInfo.NewSize.Height / 2.0);
        }

        Refresh();
    }

    protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        Refresh();
    }

    protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        Refresh();
    }

    protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
        base.OnGotKeyboardFocus(e);
        Refresh();
    }

    protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
        base.OnLostKeyboardFocus(e);
        Refresh();
    }

    /// <summary>
    /// Paints the chrome by hand instead of through triggers: the brushes are
    /// dependency properties, so a theme swap and a per-button override both land here.
    /// </summary>
    private void Refresh()
    {
        if (_chrome is null)
        {
            return;
        }

        var hover = IsMouseOver && IsEnabled;

        _chrome.Background = hover ? HoverFill : Fill;
        _chrome.BorderBrush = IsKeyboardFocused ? FocusEdge : hover ? HoverEdge : Edge;
        _chrome.CornerRadius = Radius;

        // Ink rides the text tree, so string content, a TextBlock and an icon all inherit it.
        TextElement.SetForeground(_chrome, hover ? HoverInk : Ink);
    }
}
