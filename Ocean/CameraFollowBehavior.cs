using System.Numerics;
using DefaultEcs;
using Flux.EntityBehavior;
using Flux.MathAddon;
using Flux.Rendering;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Ocean;

class CameraFollowBehavior : Behavior, IUpdatable
{
    public float lookSensitivity = 0.1f;
    public float moveSpeed = 7f;

    Vector2 LastMousePosition;

    readonly IInputContext input;
    readonly IKeyboard primaryKeyboard;
    readonly IMouse primaryMouse;

    Vector2 rotation;
    Entity target;

    public CameraFollowBehavior(IInputContext input, IWindow window)
    {
        this.input = input;
        primaryKeyboard = input.Keyboards[0];
        primaryMouse = input.Mice[0];

        window.Resize += OnResize;
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
        ref var transform = ref GetComponent<Transform>();
        var targetTransform = this.target.Get<Transform>();
        transform.Position = Follow(transform.Position, targetTransform, 0);
    }

    void OnResize(Vector2D<int> size) => GetComponent<Camera>().aspectRatio = size.X / (float)size.Y;

    public void Update(float deltatime)
    {
        return;
        ref var transform = ref GetComponent<Transform>();
        var target = this.target.Get<Transform>();

        transform.Position = Follow(transform.Position, target, 0.8f);
        transform.Rotation = target.Rotation * Quaternion.CreateFromAxisAngle(Vector3.UnitX, Angle.FromDegrees(20).Radians);
    }

    Vector3 Follow(Vector3 current, Transform target, float smoothing)
    {
        var targetPos = target.Position - target.Forward * 3 + Vector3.UnitY * 2;

        return Vector3.Lerp(targetPos, current, smoothing);
    }
}
