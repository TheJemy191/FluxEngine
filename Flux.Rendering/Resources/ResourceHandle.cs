namespace Flux.Rendering.Resources;

public readonly struct ResourceHandle<T> : IDisposable
{
    public readonly T Value;
    readonly ResourcesManager<T> manager;
    readonly Path path;

    public ResourceHandle(Path path, T resource, ResourcesManager<T> manager)
    {
        this.path = path;
        Value = resource;
        this.manager = manager;
    }

    public void Dispose()
    {
        manager.Return(path);
    }
}