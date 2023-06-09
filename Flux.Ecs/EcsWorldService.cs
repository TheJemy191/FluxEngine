﻿using DefaultEcs;

namespace Flux.Ecs;

public class EcsWorldService : IEcsWorldService
{
    public World World { get; }

    public EcsWorldService() => World = new();

    public void SetGlobal<T>() => World.Set<T>();

    public void Dispose() => World.Dispose();
}
