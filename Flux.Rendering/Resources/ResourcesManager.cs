namespace Flux.Rendering.Resources;

public abstract class ResourcesManager<T>
{
    readonly Dictionary<Path, T> resources = new();

    public T Get(Path path)
    {
        if (!resources.TryGetValue(path, out var resource))
        {
            resource = Load(path);
            resources[path] = resource;
        }

        return resource;
    }

    public abstract T Load(Path path);

    internal void Decrement<T>(Resource<T> resource)
    {
    }

    internal void Increment<T>(Resource<T> resource)
    {
    }


    protected static string ToAssetPath(Path path) => System.IO.Path.Combine("Assets", path);
    protected static string LoadAssetFile(Path path) => File.ReadAllText(ToAssetPath(path));
}
