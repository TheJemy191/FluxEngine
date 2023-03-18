using System.Numerics;

namespace Flux.MathAddon;

public struct Transform
{
    public Vector3 Position;
    public Vector3 Scale = Vector3.One;
    public Quaternion Rotation = Quaternion.Identity;

    public Vector3 Forward => Rotation.Forward();
    public Vector3 Up => Rotation.Up();
    public Vector3 Right => Rotation.Right();

    public Matrix4x4 ModelMatrix => Matrix4x4.Identity
                                   * Matrix4x4.CreateFromQuaternion(Rotation)
                                   * Matrix4x4.CreateScale(Scale)
                                   * Matrix4x4.CreateTranslation(Position);

    public override string? ToString() => $"Pos: {Position}\nScale: {Scale}\nRotation: {Rotation.QuaternionToEuler()}";

    public Transform()
    {
    }
}