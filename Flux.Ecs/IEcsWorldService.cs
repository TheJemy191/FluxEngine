using DefaultEcs;

namespace Flux.Ecs;

public interface IEcsWorldService : IDisposable
{
    World World { get; }

    void SetGlobal<T>();
}