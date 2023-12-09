using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MTK.Blazor;
using MTK.DemoApp;
using MTK.DemoApp.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// MTK
builder.Services.AddSingleton<IMessenger>(_ => WeakReferenceMessenger.Default);
builder.Services.RegisterMtkViewModels(typeof(MTK.DemoApp.ViewModels.IndexViewModel).Assembly);

await builder.Build().RunAsync();