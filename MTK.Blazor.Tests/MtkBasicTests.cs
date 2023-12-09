using Bunit;
using FluentAssertions;
using MTK.Blazor.Tests.Components;

namespace MTK.Blazor.Tests;

public class MtkBasicTests : TestContext
{
    public MtkBasicTests()
    {
        Services.AddMtk();
        Services.RegisterMtkViewModels(typeof(MtkBasicTests).Assembly);
    }
    
    [Fact]
    public void TitleShouldUpdateOnViewModelSet()
    {
        var cut = RenderComponent<BasicTest>();
        cut.Instance.ViewModel.Should().NotBeNull();
        const string? expected = "Title of the First Test";
        cut.Instance.ViewModel!.Title = expected;
        cut.Instance.ViewModel!.Title.Should().Be(expected);
        var title = cut.Find("title");
        title.TextContent.Should().Be(expected);
    }

    [Fact]
    public void ShouldUpdateViewModelWhenTextInputChanges()
    {
        var cut = RenderComponent<BasicTest>();
        var textField = cut.Find("#textField");
        textField.Change("New Title");
        cut.Instance.ViewModel!.Text.Should().Be("New Title");
    }
    
    [Fact]
    public void ShouldUpdateTitleWithTextWhenPressButton()
    {
        var cut = RenderComponent<BasicTest>();
        var title = cut.Find("title");
        title.TextContent.Should().NotBe("New Title");
        var textField = cut.Find("#textField");
        textField.Change("New Title");
        cut.Instance.ViewModel!.Text.Should().Be("New Title");
        var button = cut.Find("#testButton");
        button.Click();
        title.TextContent.Should().Be("New Title");
        cut.Instance.ViewModel!.Title.Should().Be("New Title");
    }
}