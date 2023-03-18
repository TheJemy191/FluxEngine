using System.Numerics;

namespace Flux.MathAddon;

public static class MathExtension
{
    public static Vector3 QuaternionToEuler(this Quaternion q)
    {
        // Extract the pitch (x), yaw (y), and roll (z) angles from the quaternion

        return new Vector3
        {
            X = (float)Math.Atan2(2.0f * (q.W * q.Y + q.X * q.Z), 1.0f - 2.0f * (q.Y * q.Y + q.Z * q.Z)),
            Y = (float)Math.Asin(2.0f * (q.W * q.X - q.Y * q.Z)),
            Z = (float)Math.Atan2(2.0f * (q.W * q.Z + q.X * q.Y), 1.0f - 2.0f * (q.Z * q.Z + q.Y * q.Y))
        };
    }


    public static Vector3 Forward(this Quaternion rotation) => Vector3.Transform(Vector3.UnitZ, rotation);
    public static Vector3 Up(this Quaternion rotation) => Vector3.Transform(Vector3.UnitY, rotation);
    public static Vector3 Right(this Quaternion rotation) => Vector3.Transform(Vector3.UnitX, rotation);
}
