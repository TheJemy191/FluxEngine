using LibNoise.Primitive;

namespace MinecraftClone;

class TerrainService
{
    readonly SimplexPerlin perlin;

    public TerrainService()
    {
        perlin = new SimplexPerlin();
    }

    public int GetHeight(Vector2Int pos, int maxHeight) => GetHeight(pos.X, pos.Y, maxHeight);
    public int GetHeight(int x, int y, int maxHeight)
    {
        float scale = 0.01f;

        var fx = (float)x + 0.5f;
        var fy = (float)y + 0.5f;

        var height = perlin.GetValue((fx) * scale, (fy) * scale);

        return (int)Math.Ceiling(height * maxHeight);
    }
}
