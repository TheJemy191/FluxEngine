using System.Numerics;
using Flux.EntityBehavior;
using Flux.MathAddon;
using Flux.Rendering;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Ocean;

class Game
{
    readonly IWindow window;

    public Game(IInputContext input, IWindow window, BehaviorService behaviorService, ModelEntityBuilderService modelBuilder)
    {
        this.window = window;
        for (var i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += KeyDown;
        }

        var oceanTilling = new Uniform<Vector2>("tilling", new Vector2(0.25f, 0.25f));

        var oceanEntity = modelBuilder
            .Name("Ocean")
            .Vertex("Ocean.vert")
            .Fragment("Ocean.frag")
            .Mesh("Ocean.fbx")
            .Texture("albedo", "Ocean.png")
            .Scale(Vector3.One)
            .AddUniform(oceanTilling)
            .Create();

        var boatEntity = modelBuilder
             .Name("Boat")
             .Vertex("Base.vert")
             .Fragment("Texture.frag")
             .Mesh("Boat.fbx")
             .Texture("albedo", "Boat.png")
             .Position(new Vector3(0, 0, 0))
             .Scale(Vector3.One * 0.3f)
             .ClearUniforms()
             .Create();

        boatEntity.Dispose();

        CreateCamera(window, behaviorService, boatEntity);

        behaviorService.AddBehaviorToEntity(boatEntity).AddBehavior<BoatBehavior>();

        behaviorService.CreateSingleBehaviorEntity(new OceanManager(oceanTilling, oceanEntity, boatEntity));
    }

    void CreateCamera(IWindow window, BehaviorService behaviorService, DefaultEcs.Entity boatEntity)
    {
        // Camera
        var viewportSize = window.Size;
        var cam = new Camera
        {
            aspectRatio = viewportSize.X / (float)viewportSize.Y,
            fov = Angle.FromDegrees(60f),
            farPlane = 5000f
        };

        var (camera, behavior) = behaviorService.CreateBehaviorEntity();

        behavior.AddBehavior<CameraFollowBehavior>();
        var cameraFollow = behavior.GetBehavior<CameraFollowBehavior>();

        var transform = new Transform
        {
            Position = new Vector3(0, 5, -10)
        };
        camera.Set(transform);
        camera.Set(cam);

        cameraFollow.SetTarget(boatEntity);
    }

    void KeyDown(IKeyboard keyboard, Key key, int arg)
    {
        if (key == Key.Escape)
            window.Close();
        if (key == Key.F2)
        {
            if (window.WindowState == WindowState.Fullscreen)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
                window.WindowState = WindowState.Fullscreen;
            }
        }
    }
}