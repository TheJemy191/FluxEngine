using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Flux.Engine;

public static class GameEngineExtension
{
    public static IServiceCollection AddSilkInput(this IServiceCollection services) =>
        services.AddSingleton(p => p.GetRequiredService<IWindow>().CreateInput());
}