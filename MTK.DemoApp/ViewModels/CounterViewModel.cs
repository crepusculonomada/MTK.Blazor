using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MTK.Blazor;

namespace MTK.DemoApp.ViewModels;

public partial class CounterViewModel : MtkViewModel
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