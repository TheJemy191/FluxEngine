using System.Numerics;
using DefaultEcs;
using DefaultEcs.System;
using Flux.Ecs;
using Flux.MathAddon;
using Silk.NET.Windowing;

namespace Flux.Rendering;

public class ModelRenderSystem : AEntitySetSystem<float>
{
    readonly EntitySet cameraSet;

    Angle lightYaw = Angle.FromDegrees(180);
    readonly Uniform<Matrix4x4> viewUniform;
    readonly Uniform<Matrix4x4> projectionUniform;
    readonly Uniform<Vector3> lightDirectionUniform;
    readonly Uniform<float> timeUniform;
    readonly IWindow window;

    public ModelRenderSystem(IEcsWorldService ecsService, IWindow window)
        : base(ecsService.World.GetEntities().With<Transform>().With<Model>().AsSet(), false)
    {
        this.window = window;

        cameraSet = ecsService.World
            .GetEntities()
            .With<Camera>()
            .With<Transform>()
        .AsSet();

        viewUniform = new("uView");
        projectionUniform = new("uProjection");
        //viewPosUniform = new("viewPos");
        lightDirectionUniform = new("lightDirection");
        timeUniform = new("uTime");
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

        lightYaw += Angle.FromDegrees(20 * deltatime);

        viewUniform.value = camera.ComputeViewMatrix(cameraTransform);
        projectionUniform.value = camera.ComputeProjectionMatrix();
        //viewPosUniform.value = cameraTransform.Position;
        lightDirectionUniform.value = Quaternion.CreateFromYawPitchRoll(lightYaw.Radians, Angle.FromDegrees(-45).Radians, 0).Forward();
        timeUniform.value = (float)window.Time;
    }

    protected override void Update(float deltatime, in Entity modelEntity)
    {
        var modelTransform = modelEntity.Get<Transform>();
        var model = modelEntity.Get<Model>();

        var uniforms = new Uniform[]
        {
            new Uniform<Matrix4x4>("uModel", modelTransform.ModelMatrix),
            viewUniform,
            projectionUniform,
            //viewPosUniform,
            lightDirectionUniform,
            timeUniform,
        };

        model.Draw(uniforms);
    }

    public override void Dispose()
    {
        base.Dispose();
        cameraSet.Dispose();
    }
}