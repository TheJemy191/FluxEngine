using Microsoft.Extensions.DependencyInjection;

namespace Flux.Engine.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddMultipleInterfaceSingleton<TService, TService2, TImplementation>(this IServiceCollection services)
        where TService : class
        where TService2 : class
        where TImplementation : class, TService, TService2 =>
        services.AddMultipleInterface<TService, TService2, TImplementation>(ServiceLifetime.Singleton);

    public static IServiceCollection AddMultipleInterfaceScoped<TService, TService2, TImplementation>(this IServiceCollection services)
        where TService : class
        where TService2 : class
        where TImplementation : class, TService, TService2 =>
        services.AddMultipleInterface<TService, TService2, TImplementation>(ServiceLifetime.Scoped);


    public static IServiceCollection AddMultipleInterfaceTransient<TService, TService2, TImplementation>(this IServiceCollection services)
        where TService : class
        where TService2 : class
        where TImplementation : class, TService, TService2 =>
        services.AddMultipleInterface<TService, TService2, TImplementation>(ServiceLifetime.Transient);

    public static IServiceCollection AddMultipleInterface<TService, TService2, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
        where TService : class
        where TService2 : class
        where TImplementation : class, TService, TService2
    {
        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService2), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }
}
