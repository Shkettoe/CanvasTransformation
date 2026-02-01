using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm || sender is not Panel canvas) return;
        vm.CursorLocation = e.GetPosition(canvas);
    }
}