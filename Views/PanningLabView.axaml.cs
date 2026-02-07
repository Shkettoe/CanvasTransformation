using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class PanningLabView : UserControl
{
    public PanningLabView()
    {
        InitializeComponent();
    }
    private bool _isDragging;
    private Avalonia.Point _lastPosition;

    private void CanvasPanel_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (DataContext is not PanningLabViewModel vm || sender is not Panel canvas) return;
        var currentPosition = e.GetPosition(canvas);
        vm.CursorLocation = currentPosition;
        if (!_isDragging) return;
        var deltaX = currentPosition.X - _lastPosition.X;
        var deltaY = currentPosition.Y - _lastPosition.Y;
        vm.PanCanvas(deltaX, deltaY);
        _lastPosition = currentPosition;
    }

    private void CanvasPanel_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Panel canvas) return;
        _isDragging = true;
        _lastPosition = e.GetPosition(canvas);
        e.Pointer.Capture(canvas);
    }

    private void CanvasPanel_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is not Panel canvas) return;
        _isDragging = false;
        e.Pointer.Capture(canvas);
    }

    private void CanvasPanel_OnPointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
        if (DataContext is not PanningLabViewModel vm || sender is not Panel canvas) return;
        var delta = e.Delta.Y;
        var pointerPosition = e.GetPosition(canvas);
        var zoomFactor = delta > 0 ? 1.1 : 0.9;
        vm.ZoomCanvas(zoomFactor, pointerPosition);
    }
}