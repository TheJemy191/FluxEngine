
namespace MinecraftClone;

static class ChunkMath
{
    public const int ChunkSize = 16;
    public const int BlockInChunk = ChunkSize * ChunkSize * ChunkSize;

    public static Vector3Int BlockIndexToChunkPosition(int index) => new()
    {
        X = (index % ChunkSize),
        Y = (index / ChunkSize) % ChunkSize,
        Z = (index / (ChunkSize * ChunkSize))
    };

    public static int ChunkPositionToIndex(int x, int y, int z) => (x * ChunkSize * ChunkSize) + (y * ChunkSize) + z;
    public static int ChunkPositionToIndex(Vector3Int pos) => ChunkPositionToIndex(pos.X, pos.Y, pos.Z);
}
