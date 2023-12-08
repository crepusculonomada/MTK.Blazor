using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace MTK.Blazor;

public abstract class MtkComponent<T> : ComponentBase, IAsyncDisposable where T : MtkViewModel
{
    [Inject] public IMessenger? Messenger { get; set; }
    [Inject] public T? ViewModel { get; private set; }
    [CascadingParameter(Name = "Layout")] public MtkLayout? Layout { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await InitializeAsync();
        Messenger?.RegisterAll(this);
        if (ViewModel != null)
        {
            ViewModel.PropertyChanged += VM_PropertyChanged;
            Messenger?.RegisterAll(ViewModel);
            await ViewModel.InitializeAsync();
        }
    }

    private void VM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyUpdated(e.PropertyName);
        if (Layout == null) InvokeAsync(StateHasChanged);
        else Layout.Refresh();
    }
    
    public virtual void PropertyUpdated(string? propertyName){}

    public virtual Task InitializeAsync() => Task.CompletedTask;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (ViewModel is { IsVisible: true }) base.BuildRenderTree(builder);
    }
    
    public async ValueTask DisposeAsync()
    {
        Messenger?.UnregisterAll(this);
        if (ViewModel != null)
        {
            Messenger?.UnregisterAll(ViewModel);
            ViewModel.PropertyChanged -= VM_PropertyChanged;
            await ViewModel.TerminateAsync();
        }
    }
}