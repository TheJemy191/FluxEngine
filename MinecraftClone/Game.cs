using DefaultEcs;
using Flux.Ecs;
using Flux.MathAddon;
using Flux.Rendering;
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Numerics;
using TestApp;

namespace MinecraftClone;

class Game
{
    readonly IWindow window;
    readonly IEcsWorldService ecsService;
    readonly ChunkService chunkService;
    readonly ImGuiController imGui;

    private readonly EntitySet cameraSet;
    float cameraSpeed = 100;
    private static CameraController? cameraController;

    public Game(IInputContext input, IWindow window, IEcsWorldService ecsService, ChunkService chunkService, ImGuiController imGui, ModelEntityBuilderService modelEntityBuilder)
    {
        this.window = window;
        this.ecsService = ecsService;
        this.chunkService = chunkService;
        this.imGui = imGui;
        for (var i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += KeyDown;
        }

        //window.Update += OnUpdate;
        window.Render += OnRender;

        CreateCamera(window, ecsService);

        //gl.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        //gl.Disable(EnableCap.CullFace);

        modelEntityBuilder
            .Name("Cube")
            .Vertex("shader.vert")
            .Fragment("suzane.frag")
            .Mesh("Cube.fbx")
            .Position(new Vector3(0, 0, 0))
            .Scale(Vector3.One * 0.5f)
            .Create();

        modelEntityBuilder
            .Position(new Vector3(2, 0, 0))
            .Create();

        modelEntityBuilder
            .Position(new Vector3(0, 0, 2))
            .Create();

        modelEntityBuilder
            .Position(new Vector3(0, 2, 0))
            .Create();

        cameraSet = ecsService.World.GetEntities()
            .With<Camera>()
            .With<Transform>()
            .AsSet();
    }

    private void OnRender(double obj)
    {
        imGui.Update((float)obj);

        if (ImGui.SliderFloat("CameraSpeed", ref cameraSpeed, 32, 320))
            cameraController.moveSpeed = cameraSpeed;

        var cameraPosition = cameraSet.GetEntities()[0].Get<Transform>().Position;
        ImGui.Text(cameraPosition.ToString("n2"));

        var cameraChunkPosition = new Vector2Int
        {
            X = (int)Math.Floor(cameraPosition.X / 16),
            Y = (int)Math.Floor(cameraPosition.Z / 16)
        };
        ImGui.Text(cameraChunkPosition.ToString());

        imGui.Render();
    }

    void CreateCamera(IWindow window, IEcsWorldService ecsService)
    {
        // Camera
        var viewportSize = window.Size;
        var cam = new Camera
        {
            aspectRatio = viewportSize.X / (float)viewportSize.Y,
            fov = Angle.FromDegrees(60f),
            farPlane = 1000
        };

        var (camera, cameraBehaviors) = ecsService.CreateBehaviorEntity();

        var transform = new Transform
        {
            Position = new Vector3(0, 0, -20)
        };
        camera.Set(transform);
        camera.Set(cam);

        cameraBehaviors.AddBehavior<CameraController>();
        cameraController = cameraBehaviors.GetBehavior<CameraController>();
        cameraController.moveSpeed = cameraSpeed;
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