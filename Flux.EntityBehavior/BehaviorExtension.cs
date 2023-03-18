using DefaultEcs;
using Flux.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Flux.EntityBehavior;

public static class BehaviorExtension
{
    public static BehaviorComponent AddBehavior(this Entity entity, IInjectionService injectionServices)
    {
        var behavior = new BehaviorComponent(entity, injectionServices);
        entity.Set(behavior);
        entity.Set<IUIRenderComponent>(behavior);
        return behavior;
    }

    public static IServiceCollection AddBehaviorServices(this IServiceCollection services) =>
        services.AddSingleton<BehaviorService>();

    public static IGameEngine AddBehaviorSystem(this IGameEngine engine) =>
        engine.AddUpdateSystem<BehaviorSystem>();
}
