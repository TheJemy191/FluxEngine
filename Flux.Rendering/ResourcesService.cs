using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using Flux.Rendering.Resources;

namespace Flux.Rendering;

public class ResourcesService : IDisposable
{
    readonly GL gl;
    readonly ModelLoaderService modelLoaderService;
    readonly TexturesManager texturesManager;
    readonly List<IDisposable> resources = new();


    public ResourcesService(GL gl, ModelLoaderService modelLoaderService, TexturesManager texturesManager)
    {
        this.gl = gl;
        this.modelLoaderService = modelLoaderService;
        this.texturesManager = texturesManager;
    }

    public Shader LoadShader(Path vertexPath, Path fragmentPath)
    {
        var shader = new Shader(gl, LoadAssetFile(vertexPath), LoadAssetFile(fragmentPath));
        resources.Add(shader);
        return shader;
    }
    public ResourceHandle<Texture> LoadTexture(Path path)
    {
        var texture = texturesManager.Get(path);
        return texture;
    }

    public Model LoadModel(Path path, Material material)
    {
        var meshes = modelLoaderService.LoadMeshes(ToAssetPath(path));
        resources.AddRange(meshes.Cast<IDisposable>());
        return new(meshes, material);
    }

    static string ToAssetPath(Path path) => System.IO.Path.Combine("Assets", path);
    static string LoadAssetFile(Path path) => File.ReadAllText(ToAssetPath(path));

    public void Dispose()
    {
        foreach (var resource in resources)
        {
            resource.Dispose();
        }
    }
}