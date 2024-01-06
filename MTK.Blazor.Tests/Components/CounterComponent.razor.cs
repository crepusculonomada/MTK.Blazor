using MTK.Blazor.Tests.ViewModels;

namespace MTK.Blazor.Tests.Components;

[MtkParam("CurrentCount", typeof(int))]
public partial class CounterComponent : MtkComponent<CounterViewModel>
{
    private void IncrementCount() => ViewModel.IncrementCountCommand.Execute(null);
    private void DecrementCount() => ViewModel.DecrementCountCommand.Execute(null);
    private void ResetCount() => ViewModel.ResetCountCommand.Execute(null);
}