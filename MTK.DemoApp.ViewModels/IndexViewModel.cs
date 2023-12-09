using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MTK.Blazor;

namespace MTK.DemoApp.ViewModels;

public partial class IndexViewModel : MtkViewModel
{
    [ObservableProperty] private string _title = "MTK Demo App";
    
    [ObservableProperty] private string _head = "Hello World!";
    
    [RelayCommand]
    private void SetTitle(string title)
    {
        Title = title;
    }
}