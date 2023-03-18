using System.Numerics;
using DefaultEcs;
using DefaultEcs.System;
using Flux.Ecs;
using Flux.MathAddon;
using Flux.Rendering;

namespace MinecraftClone;

class ChunkRendererSystem : AEntitySetSystem<float>
{
    readonly EntitySet cameraSet;
    Matrix4x4 view;
    Matrix4x4 projection;

    Transform lightTransform;
    Angle lightYaw = Angle.FromDegrees(180);

    public ChunkRendererSystem(IEcsWorldService ecsService) :
        base(ecsService.World.GetEntities().With<Vector2Int>().With<Chunk>().AsSet(), false)
    {
        cameraSet = ecsService.World
            .GetEntities()
            .With<Camera>()
            .With<Transform>()
            .AsSet();

        lightTransform.Rotation = Quaternion.CreateFromYawPitchRoll(lightYaw.Radians, Angle.FromDegrees(-45).Radians, 0);
    }

    protected override void PreUpdate(float deltatime)
    {
        if (cameraSet.Count == 0)
        {
            Console.WriteLine("No camera in the scene.");
            return;
        }

        var cameraEntity = cameraSet.GetEntities()[0];
        var camera = cameraEntity.Get<Camera>();
        var cameraTransform = cameraEntity.Get<Transform>();

        view = camera.ComputeViewMatrix(cameraTransform);
        projection = camera.ComputeProjectionMatrix();

        //lightYaw += Angle.FromDegrees(20 * deltatime);
        //lightTransform.Rotation = Quaternion.CreateFromYawPitchRoll(lightYaw.Radians, Angle.FromDegrees(-45).Radians, 0);
    }

    protected override void Update(float state, in Entity entity)
    {
        var chunkPosition = entity.Get<Vector2Int>();
        var chunk = entity.Get<Chunk>();
        chunk.Draw(view, projection, ComputeModelMatrix(chunkPosition * ChunkMath.ChunkSize), lightTransform.Forward);
        base.Update(state, entity);
    }

    Matrix4x4 ComputeModelMatrix(Vector2Int position) => Matrix4x4.Identity
                               * Matrix4x4.CreateTranslation(position.X, 0, position.Y);
}