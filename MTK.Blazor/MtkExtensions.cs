using Microsoft.Extensions.DependencyInjection;

namespace MTK.Blazor;

public static class MtkExtensions
{
    public static void AddMtk(this IServiceCollection services)
    {
        services.AddScoped<MtkViewModelBase>();
        services.AddScoped<MtkComponentBase<MtkViewModelBase>>();
    }
}