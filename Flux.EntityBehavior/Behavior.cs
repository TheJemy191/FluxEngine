using DefaultEcs;

namespace Flux.EntityBehavior;

public class Behavior
{
    Entity entity;
    public void Attach(Entity entity) => this.entity = entity;
    public ref T GetComponent<T>() => ref entity.Get<T>();
}
