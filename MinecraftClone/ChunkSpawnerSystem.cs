using DefaultEcs;
using DefaultEcs.System;
using Flux.Ecs;
using Flux.Rendering;

namespace MinecraftClone
{
    class ChunkSpawnerSystem : ISystem<float>
    {
        readonly ChunkService chunkService;
        readonly IEcsWorldService ecsService;
        private readonly EntitySet cameraSet;

        HashSet<Vector2Int> existingChunk = new();

        Queue<Vector2Int> chunkQueue = new Queue<Vector2Int>();

        const int renderDistance = 16;
        const int halfRenderDistance = renderDistance / 2;

        public bool IsEnabled { get; set; }

        public ChunkSpawnerSystem(ChunkService chunkService, IEcsWorldService ecsService)
        {
            this.chunkService = chunkService;
            this.ecsService = ecsService;

            cameraSet = ecsService.World.GetEntities()
                .With<Camera>()
                .With<Transform>()
                .AsSet();
        }

        public void Update(float state)
        {
            if (chunkQueue.Any())
                chunkService.LoadChunk(chunkQueue.Dequeue());

            var cameraPosition = cameraSet.GetEntities()[0].Get<Transform>().Position;
            var cameraChunkPosition = new Vector2Int
            {
                X = (int)Math.Floor(cameraPosition.X / 16),
                Y = (int)Math.Floor(cameraPosition.Z / 16)
            };

            for (int x = 0; x < renderDistance; x++)
            {
                for (int z = 0; z < renderDistance; z++)
                {

                    var chunkPosition = cameraChunkPosition + new Vector2Int(x - halfRenderDistance, z - halfRenderDistance);

                    if (existingChunk.Contains(chunkPosition))
                        continue;

                    chunkService.LoadChunk(chunkPosition);
                    //chunkQueue.Enqueue(chunkPosition);
                    existingChunk.Add(chunkPosition);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
