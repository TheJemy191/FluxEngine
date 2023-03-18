using DefaultEcs;
using Flux.Ecs;
using Flux.Rendering;

namespace MinecraftClone;

class ChunkService
{
    readonly IEcsWorldService ecsService;
    readonly GL gl;
    readonly TerrainService terrainService;
    readonly Shader globalShader;

    public ChunkService(IEcsWorldService ecsService, GL gl, ResourcesService resourcesService, TerrainService terrainService)
    {
        this.ecsService = ecsService;
        this.gl = gl;
        this.terrainService = terrainService;
        globalShader = resourcesService.LoadShader("Chunk.vert", "Chunk.frag");
    }

    public Entity LoadChunk(Vector2Int chunkPosition)
    {
        var blocks = GenerateChunkData(chunkPosition, terrainService);

        var vertexs = new List<float>(16 * 16 * 16 * 6);
        var faces = new List<uint[]>();

        for (int x = 0; x < ChunkMath.ChunkSize; x++)
        {
            for (int y = 0; y < ChunkMath.ChunkSize; y++)
            {
                for (int z = 0; z < ChunkMath.ChunkSize; z++)
                {
                    var isFull = blocks[x, y, z];

                    if (!isFull)
                        continue;

                    var blockPosition = new Vector3Int(x, y, z);

                    vertexs.AddRange(BlockData.OffsetFace(BlockData.XFaceVertex, blockPosition));
                    faces.Add(BlockData.XFaceIndice);

                    vertexs.AddRange(BlockData.OffsetFace(BlockData.NegXFaceVertex, blockPosition));
                    faces.Add(BlockData.NegXFaceIndice);

                    vertexs.AddRange(BlockData.OffsetFace(BlockData.YFaceVertex, blockPosition));
                    faces.Add(BlockData.YFaceIndice);

                    vertexs.AddRange(BlockData.OffsetFace(BlockData.NegYFaceVertex, blockPosition));
                    faces.Add(BlockData.NegYFaceIndice);

                    vertexs.AddRange(BlockData.OffsetFace(BlockData.ZFaceVertex, blockPosition));
                    faces.Add(BlockData.ZFaceIndice);

                    vertexs.AddRange(BlockData.OffsetFace(BlockData.NegZFaceVertex, blockPosition));
                    faces.Add(BlockData.NegZFaceIndice);

                }
            }
        }


        var indexes = ComputeIndexes(faces);

        var entity = ecsService.World.CreateEntity();
        entity.Set(chunkPosition);
        entity.Set(new Chunk(gl, globalShader, vertexs.ToArray(), indexes));
        return entity;
    }

    private static uint[] ComputeIndexes(List<uint[]> faces)
    {
        var indices = new uint[faces.Count * 6];
        for (int i = 0; i < faces.Count; i++)
        {
            var face = faces[i];
            uint indiceOffset = (uint)i * 6;
            uint vertexOffset = (uint)i * 4;
            for (int ii = 0; ii < 6; ii++)
            {
                indices[indiceOffset + ii] = face[ii] + vertexOffset;
            }
        }

        return indices;
    }

    static bool[,,] GenerateChunkData(Vector2Int chunkPosition, TerrainService terrainService)
    {
        var blocks = new bool[ChunkMath.ChunkSize, ChunkMath.ChunkSize, ChunkMath.ChunkSize];

        for (int x = 0; x < ChunkMath.ChunkSize; x++)
        {
            for (int y = 0; y < ChunkMath.ChunkSize; y++)
            {
                for (int z = 0; z < ChunkMath.ChunkSize; z++)
                {
                    var blockX = x + (chunkPosition.X * ChunkMath.ChunkSize);
                    var blockZ = z + (chunkPosition.Y * ChunkMath.ChunkSize);
                    var isFull = y < terrainService.GetHeight(blockX, blockZ, 16);

                    blocks[x, y, z] = isFull;
                }
            }
        }

        return blocks;
    }
}