using CommunityToolkit.Mvvm.ComponentModel;

namespace MTK.Blazor.Tests.ViewModels;

public partial class TestViewModel : MtkViewModel
{
    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private string _text = string.Empty;
}