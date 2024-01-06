using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MTK.Blazor.Tests.ViewModels;

public partial class CounterViewModel : MtkViewModel
{
    [ObservableProperty] private int _currentCount;
    
    [RelayCommand]
    private void IncrementCount()
    {
        CurrentCount++;
    }
    
    [RelayCommand]
    private void DecrementCount()
    {
        CurrentCount--;
    }

    [RelayCommand]
    private void ResetCount()
    {
        CurrentCount = 0;
    }
}