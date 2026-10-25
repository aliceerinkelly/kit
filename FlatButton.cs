using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Kit.Controls;

/// <summary>
/// A button you colour. PillButton is the six-size black-and-white pill; this one
/// takes a design's own palette: any fill (solid or gradient), one 1px edge, any
/// corner radius, and a hover state you set instead of one KIT guesses.
/// </summary>
public class FlatButton : Button
{
    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
        nameof(Fill),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty InkProperty = DependencyProperty.Register(
        nameof(Ink),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty EdgeProperty = DependencyProperty.Register(
        nameof(Edge),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty HoverFillProperty = DependencyProperty.Register(
        nameof(HoverFill),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty HoverInkProperty = DependencyProperty.Register(
        nameof(HoverInk),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.White, OnBrushChanged));

    public static readonly DependencyProperty HoverEdgeProperty = DependencyProperty.Register(
        nameof(HoverEdge),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.Transparent, OnBrushChanged));

    public static readonly DependencyProperty FocusEdgeProperty = DependencyProperty.Register(
        nameof(FocusEdge),
        typeof(Brush),
        typeof(FlatButton),
        new PropertyMetadata(Brushes.DodgerBlue, OnBrushChanged));

    /// <summary>Any radius: 0 is a tile, half the height is a pill.</summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
        nameof(Radius),
        typeof(CornerRadius),
        typeof(FlatButton),
        new PropertyMetadata(new CornerRadius(10.0), OnBrushChanged));

    private Border? _chrome;

    public FlatButton()
    {
        Cursor = Cursors.Hand;
        SnapsToDevicePixels = true;
        UseLayoutRounding = true;

        // Theme defaults first, so an unthemed FlatButton still belongs in KIT.
        SetResourceReference(FontFamilyProperty, "Kit.UiFont");
        SetResourceReference(FillProperty, "Kit.CardBrush");
        SetResourceReference(InkProperty, "Kit.TextBrush");
        SetResourceReference(EdgeProperty, "Kit.OutlineBrush");
        SetResourceReference(HoverFillProperty, "Kit.HoverBrush");
        SetResourceReference(HoverInkProperty, "Kit.TextBrush");
        SetResourceReference(HoverEdgeProperty, "Kit.AccentBrush");
        SetResourceReference(FocusEdgeProperty, "Kit.AccentBrush");

        Template = BuildTemplate();
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

    public CornerRadius Radius
    {
        get => (CornerRadius)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((FlatButton)d).Refresh();
    }

    private static ControlTemplate BuildTemplate()
    {
        var chrome = new FrameworkElementFactory(typeof(Border), "Chrome");
        chrome.SetValue(Border.BorderThicknessProperty, new Thickness(1.0));

        // Padding is part of the button's shape: without it, a coloured pill's label
        // runs out over its own edge.
        chrome.SetValue(Border.PaddingProperty, new TemplateBindingExtension(Control.PaddingProperty));

        var presenter = new FrameworkElementFactory(typeof(ContentPresenter));
        presenter.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        presenter.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

        chrome.AppendChild(presenter);

        var pressed = new Trigger { Property = ButtonBase.IsPressedProperty, Value = true };
        pressed.Setters.Add(new Setter(UIElement.OpacityProperty, 0.85, "Chrome"));

        var disabled = new Trigger { Property = UIElement.IsEnabledProperty, Value = false };
        disabled.Setters.Add(new Setter(UIElement.OpacityProperty, 0.45, "Chrome"));

        var template = new ControlTemplate(typeof(FlatButton)) { VisualTree = chrome };
        template.Triggers.Add(pressed);
        template.Triggers.Add(disabled);
        return template;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _chrome = GetTemplateChild("Chrome") as Border;
        Refresh();
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        Refresh();
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        Refresh();
    }

    protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
        base.OnGotKeyboardFocus(e);
        Refresh();
    }

    protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
        base.OnLostKeyboardFocus(e);
        Refresh();
    }

    /// <summary>
    /// Painted by hand like PillButton: every brush is a dependency property, so a
    /// design's palette and a theme swap land in the same place.
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

        // Ink rides the text tree, so a string, a TextBlock and an icon all follow it.
        TextElement.SetForeground(_chrome, hover ? HoverInk : Ink);
    }
}
