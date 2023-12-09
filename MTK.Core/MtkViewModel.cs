using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MTK.Blazor;

public partial class MtkViewModel : ObservableObject
{
    
    [ObservableProperty] private bool _isVisible = true;
    
    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
    
    public virtual Task TerminateAsync() => Task.CompletedTask;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName != null) OnPropertyChanged(e.PropertyName);
    }
    
    public virtual void OnPropertyChanged(string propertyName) { }
}