using System.Numerics;

namespace Flux.Rendering;

public readonly struct Vertex
{
    public readonly Vector3 Position;
    public readonly Vector3 Normal;
    public readonly Vector3 Tangent;
    public readonly Vector3 Bitangent;
    public readonly Vector2 TexCoords;
}
