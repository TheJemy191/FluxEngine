using Flux.Rendering.Resources;

namespace Flux.Resources.Test;

class TestResourceManager : ResourcesManager<TestClassData>
{
    protected override TestClassData Load(Core.Path path)
    {
        return new TestClassData(path);
    }

    protected override void Unload(TestClassData resource)
    {
        resource.Unloaded = true;
    }
}

public class TestClassData
{
    public string Name { get; set; }
    public bool Unloaded { get; set; }

    public TestClassData(string name)
    {
        Name = name;
    }
}
