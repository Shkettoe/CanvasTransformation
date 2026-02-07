using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public PanningLabViewModel PanningLabViewModel { get; } =  new ();
    
    [ObservableProperty] private ObservableCollection<Avalonia.Point> _points =
    [
        new(75, 0), new(90, 45), new(150, 45), new(100, 75), new(120, 120), new(75, 90), new(30, 120), new(50, 75),
        new(0, 45), new(60, 45)
    ];

    [ObservableProperty] private ObservableCollection<Avalonia.Point> _triangle =
    [
        new(40, 12), new(20, 15), new(25, 50)
    ];
}