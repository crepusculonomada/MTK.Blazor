using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MTK.Blazor.Tests.Components;
using MTK.Blazor.Tests.ViewModels;
using NSubstitute;

namespace MTK.Blazor.Tests;

public class CounterTests : TestContext
{
    public CounterTests()
    {
        Services.AddMtk();
        Services.RegisterMtkViewModels(typeof(CounterTests).Assembly);
    }
    
    [Fact]
    public void ShouldRenderCounterAndIncrementOnClick()
    {
        var cut = RenderComponent<Counter>();
        cut.Find("title").TextContent.Should().Be("Counter Component");
        cut.Find("#counterDiv").TextContent.Should().Be("0");
        cut.Find("#incrementButton").Click();
        cut.Find("#counterDiv").TextContent.Should().Be("1");
        cut.Find("#incrementButton").Click();
        cut.Find("#counterDiv").TextContent.Should().Be("2");
    }
    
    [Fact]
    public void ShouldInvokePropertyChanged()
    {
        var cut = RenderComponent<Counter>();
        cut.Instance.CurrentCounterPropertyUpdateInvoked.Should().BeFalse();
        cut.Find("#counterDiv").TextContent.Should().Be("0");
        cut.Find("#incrementButton").Click();
        cut.Find("#counterDiv").TextContent.Should().Be("1");
        cut.Instance.CurrentCounterPropertyUpdateInvoked.Should().BeTrue();
    }
    
    [Fact]
    public void HostCounterComponentAndCheckCounterUpdates()
    {
        var cut = RenderComponent<HostingCounterComponent>();
        // cut.Find("#hostCounterDiv").TextContent.Should().Be("0");
        cut.Find("#componentCounterDiv").TextContent.Should().Be("0");
        cut.Find("#hostIncButton").Click();
        cut.Find("#componentCounterDiv").TextContent.Should().Be("1");
        cut.Find("#upButton").Click();
        cut.Find("#componentCounterDiv").TextContent.Should().Be("2");
        // One way Binding, so not backward updated
        cut.Find("#hostIncButton").Click();
        cut.Find("#componentCounterDiv").TextContent.Should().Be("2");
    }
    
}