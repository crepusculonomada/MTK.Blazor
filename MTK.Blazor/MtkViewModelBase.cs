using CommunityToolkit.Mvvm.ComponentModel;

namespace MTK.Blazor;

public partial class MtkViewModelBase : ObservableObject
{
    
    [ObservableProperty] private bool _isVisible = true;
    
    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
    
    public virtual Task TerminateAsync() => Task.CompletedTask;

}