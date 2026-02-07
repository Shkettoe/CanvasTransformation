using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;

namespace AvaloniaApplication1.Controls;

public class Panvas : ItemsControl
{
    public static readonly StyledProperty<double> CanvasScaleXProperty = AvaloniaProperty.Register<Panvas, double>(
        nameof(CanvasScaleX), 1.0);

    public double CanvasScaleX
    {
        get => GetValue(CanvasScaleXProperty);
        set => SetValue(CanvasScaleXProperty, value);
    }

    public static readonly StyledProperty<double> CanvasScaleYProperty = AvaloniaProperty.Register<Panvas, double>(
        nameof(CanvasScaleY), 1.0);

    public double CanvasScaleY
    {
        get => GetValue(CanvasScaleYProperty);
        set => SetValue(CanvasScaleYProperty, value);
    }

    public static readonly StyledProperty<double> CanvasTranslateXProperty = AvaloniaProperty.Register<Panvas, double>(
        /*Set the default values for testing purposes. It appears that this binding has no effect in the XAML transform configuration.*/
        nameof(CanvasTranslateX));

    public double CanvasTranslateX
    {
        get => GetValue(CanvasTranslateXProperty);
        set => SetValue(CanvasTranslateXProperty, value);
    }

    public static readonly StyledProperty<double> CanvasTranslateYProperty = AvaloniaProperty.Register<Panvas, double>(
        nameof(CanvasTranslateY));

    public double CanvasTranslateY
    {
        get => GetValue(CanvasTranslateYProperty);
        set => SetValue(CanvasTranslateYProperty, value);
    }

    private bool _isDragging;
    private Point _lastPosition;


    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        _isDragging = true;
        _lastPosition = e.GetPosition(this);
        e.Pointer.Capture(this);
        base.OnPointerPressed(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        var currentPosition = e.GetPosition(this);
        if (!_isDragging) return;
        var deltaX = currentPosition.X - _lastPosition.X;
        var deltaY = currentPosition.Y - _lastPosition.Y;
        CanvasTranslateX += deltaX;
        CanvasTranslateY += deltaY;
        _lastPosition = currentPosition;
        base.OnPointerMoved(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        _isDragging = false;
        e.Pointer.Capture(this);
        base.OnPointerReleased(e);
    }

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        var delta = e.Delta.Y;
        var center = e.GetPosition(this);
        var factor = delta > 0 ? 1.1 : 0.9;
        var (x, y) = new Avalonia.Point(center.X - Width / 2.0, center.Y - Height / 2.0);
        if (CanvasScaleX * factor > 200 || CanvasScaleY * factor > 200 || CanvasScaleX * factor < 1 ||
            CanvasScaleY * factor < 1) return;
        CanvasScaleX *= factor;
        CanvasScaleY *= factor;
        CanvasTranslateX = x - (x - CanvasTranslateX) * factor;
        CanvasTranslateY = y - (y - CanvasTranslateY) * factor;
        base.OnPointerWheelChanged(e);
    }

    static Panvas()
    {
        ItemsPanelProperty.OverrideDefaultValue<Panvas>(
            new FuncTemplate<Panel>(() => new Canvas())!);
    }
}