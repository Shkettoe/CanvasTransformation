using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [NotifyPropertyChangedFor(nameof(CanvasWidth))] [ObservableProperty]
    private double _canvasHeight = 800;

    public double CanvasWidth => CanvasHeight * 2;
    [ObservableProperty] private ScaleTransform _scaleTransform = new(1, 1);
    [ObservableProperty] private TranslateTransform _translateTransform = new(0, 0);
    [ObservableProperty] private Point _cursorLocation;

    [ObservableProperty] private ObservableCollection<Point> _points =
    [
        new(75, 0), new(90, 45), new(150, 45), new(100, 75), new(120, 120), new(75, 90), new(30, 120), new(50, 75),
        new(0, 45), new(60, 45)
    ];

    [RelayCommand]
    private void CenterPolygon()
    {
        var maxX = Points.Max(p => p.X);
        var maxY = Points.Max(p => p.Y);
        var scale = Math.Min(CanvasWidth, CanvasHeight) / Math.Min(maxX, maxY) / 1.05;
        ScaleTransform = new ScaleTransform(scale, scale);
        TranslateTransform = new TranslateTransform((CanvasWidth * scale - maxX * scale) / 2,
            (CanvasHeight * scale - maxY * scale) / 2);
    }
}