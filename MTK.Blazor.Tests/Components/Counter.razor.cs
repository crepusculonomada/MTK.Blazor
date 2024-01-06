namespace MTK.Blazor.Tests.Components;

public partial class Counter
{
    public bool CurrentCounterPropertyUpdateInvoked { get; set; }

    public override void PropertyUpdated(string? propertyName)
    {
        if (propertyName == nameof(ViewModel.CurrentCount))
        {
            CurrentCounterPropertyUpdateInvoked = true;
        }
    }
}