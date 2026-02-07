using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels;

public partial class PanningLabViewModel : ViewModelBase
{
    [NotifyPropertyChangedFor(nameof(CanvasWidth))] [ObservableProperty]
    private double _canvasHeight = 500;

    public double CanvasWidth => CanvasHeight * 2;
    [ObservableProperty] private ScaleTransform _scaleTransform = new(1, 1);
    [ObservableProperty] private TranslateTransform _translateTransform = new(0, 0);
    [ObservableProperty] private Point _cursorLocation;

    [ObservableProperty] private ObservableCollection<Point> _points =
    [
        new(75, 0), new(90, 45), new(150, 45), new(100, 75), new(120, 120), new(75, 90), new(30, 120), new(50, 75),
        new(0, 45), new(60, 45)
    ];

    /*[ObservableProperty] private ObservableCollection<Point> _points =
    [
        new(190, 46), new(188, 42), new(191, 45)
    ];*/

    [RelayCommand]
    private void CenterPolygon()
    {
        var maxX = Points.Max(p => p.X);
        var maxY = Points.Max(p => p.Y);
        var minX = Points.Min(p => p.X);
        var minY = Points.Min(p => p.Y);
        var width = maxX + (maxX - (maxX - minX));
        var height = maxY + (maxY - (maxY - minY));
        var scale = Math.Min(CanvasWidth, CanvasHeight) / Math.Min((maxX - minX), (maxY - minY)) / 2;
        ScaleTransform = new ScaleTransform(scale, scale);
        TranslateTransform = new TranslateTransform((CanvasWidth / 2 - width / 2) * scale,
            (CanvasHeight / 2 - height / 2) * scale);
    }

    public void PanCanvas(double deltaX, double deltaY)
    {
        TranslateTransform.X += deltaX;
        TranslateTransform.Y += deltaY;
    }
    
    public void ZoomCanvas(double factor, Point center)
    {
        var (x, y) = new Point(center.X - CanvasWidth / 2.0, center.Y - CanvasHeight / 2.0);
        if (ScaleTransform.ScaleX * factor > 200 || ScaleTransform.ScaleY * factor > 200 || ScaleTransform.ScaleX * factor < 1 ||
            ScaleTransform.ScaleY * factor < 1) return;
        ScaleTransform.ScaleX *= factor;
        ScaleTransform.ScaleY *= factor;
        TranslateTransform.X = x - (x - TranslateTransform.X) * factor;
        TranslateTransform.Y = y - (y - TranslateTransform.Y) * factor;
    }
}