namespace Flux.Rendering;

public class Uniform<T> : Uniform
{
    public T value;

    public Uniform(string name, T value) : base(name)
    {
        this.value = value;
    }

    public Uniform(string name) : base(name)
    {
        value = default;
    }
}

public abstract class Uniform
{
    public readonly string name;

    protected Uniform(string name)
    {
        this.name = name;
    }
}