using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MTK.Blazor;

public partial class MtkViewModel : ObservableObject
{
    
    [ObservableProperty] private bool _isVisible = true;
    
    public virtual Task InitializeAsync() => Task.CompletedTask;

    public virtual Task TerminateAsync() => Task.CompletedTask;
}