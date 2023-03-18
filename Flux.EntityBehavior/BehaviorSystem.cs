using DefaultEcs.System;
using Flux.Ecs;

namespace Flux.EntityBehavior;

public class BehaviorSystem : AComponentSystem<float, BehaviorComponent>
{

    public BehaviorSystem(IEcsWorldService ecsService) : base(ecsService.World)
    {
    }

    protected override void Update(float deltatime, ref BehaviorComponent component)
    {
        component.Update(deltatime);
    }
}
