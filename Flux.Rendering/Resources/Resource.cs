namespace Flux.Rendering.Resources;

public class Resource<T> : IDisposable
{
    readonly ResourcesManager<T> manager;

    public Resource(ResourcesManager<T> manager)
    {
        this.manager = manager;
        manager.Increment(this);
    }

    public void Dispose()
    {
        manager.Decrement(this);
    }
}
