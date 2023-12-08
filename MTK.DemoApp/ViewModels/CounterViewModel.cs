using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MTK.DemoApp.ViewModels;

using MTK.Blazor;

public partial class CounterViewModel : MtkViewModelBase
{
    private readonly Random random;
    
    public CounterViewModel()
    {
        random = new Random();
    }
    
    public override Task InitializeAsync()
    {
        CurrentCount = random.Next(100);
        return Task.CompletedTask;
    }

    [ObservableProperty] private int _currentCount;
    
    [RelayCommand]
    public void IncrementCount()
    {
        CurrentCount++;
    }
    
    [RelayCommand]
    public void DecrementCount()
    {
        CurrentCount--;
    }

    public override async Task TerminateAsync()
    {
        await Task.Delay(500);
        CurrentCount = 0;
    }
}