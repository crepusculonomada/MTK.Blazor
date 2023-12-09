using System.Reflection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace MTK.Blazor;

public static class MtkExtensions
{
    
    public static void AddMtk(this IServiceCollection services)
    {
        services.AddSingleton<IMessenger>(_ => WeakReferenceMessenger.Default);
    }
    
    public static void RegisterMtkViewModels(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(x => x.IsSubclassOf(typeof(MtkViewModel))).ToList();
        
        foreach (var type in types) 
            services.AddTransient(type);
    }
}