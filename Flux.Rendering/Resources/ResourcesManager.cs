namespace Flux.Rendering.Resources;

public abstract class ResourcesManager<T> : IResourceManager
{
    readonly Dictionary<Path, ResourceHandle<T>> resources = new();
    readonly Dictionary<Path, uint> refCount = new();

    public ResourceHandle<T> Get(Path path)
    {
        if (!resources.TryGetValue(path, out var resource))
        {
            resource = new (path, Load(path), this);
            resources[path] = resource;
            refCount[path] = 0;
        }

        refCount[path] += 1;
        return resource;
    }

    internal void Return(Path path)
    {
        if (!refCount.ContainsKey(path))
            return;

        refCount[path] -= 1;
    }

    public uint GetRefCount(Path path)
    {
        if (!refCount.ContainsKey(path))
            return 0;

        return refCount[path];
    }

    public void Purge()
    {
        foreach (var path in refCount.Where(r => r.Value == 0).Select(r => r.Key))
        {
            var handle = resources[path];
            Unload(handle.Value);
            handle.Dispose();
            resources.Remove(path);
            refCount.Remove(path);
        }
    }

    protected abstract T Load(Path path);
    protected abstract void Unload(T resource);

    protected static string ToAssetPath(Path path) => System.IO.Path.Combine("Assets", path);
    protected static string LoadAssetFile(Path path) => File.ReadAllText(ToAssetPath(path));
}