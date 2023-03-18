using System.Numerics;
using Flux.EntityBehavior;
using Flux.MathAddon;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Ocean
{
    public class BoatBehavior : Behavior, IUpdatable
    {
        readonly IInputContext input;
        readonly IWindow window;
        readonly IKeyboard primaryKeyboard;

        Angle rotation = new();
        Angle rotationSpeed = Angle.FromDegrees(45);
        float moveSpeed = 5;

        public BoatBehavior(IInputContext input, IWindow window)
        {
            this.input = input;
            this.window = window;
            primaryKeyboard = input.Keyboards[0];
        }

        public void Update(float deltatime)
        {
            ref var transform = ref GetComponent<Transform>();

            var direction = Vector3.Zero;

            var forward = transform.Forward;

            if (primaryKeyboard.IsKeyPressed(Key.W))
            {
                direction += forward * moveSpeed * deltatime;
            }
            if (primaryKeyboard.IsKeyPressed(Key.S))
            {
                direction -= forward * moveSpeed * deltatime;
            }

            //direction = Vector3.Normalize(direction) * moveSpeed;

            if (primaryKeyboard.IsKeyPressed(Key.D))
            {
                rotation += rotationSpeed * deltatime;
            }
            if (primaryKeyboard.IsKeyPressed(Key.A))
            {
                rotation -= rotationSpeed * deltatime;
            }

            transform.Rotation = Quaternion.CreateFromAxisAngle(transform.Up, rotation.Radians);

            var position = transform.Position;

            position.Y = GetHeight(Convert(position), (float)window.Time);


            var front = GetHeight(Convert(position + transform.Forward * 2), (float)window.Time);
            var back = GetHeight(Convert(position - transform.Forward * 2), (float)window.Time);

            transform.Position = position + direction;
        }

        float GetHeight(Vector2 pos, float time)
        {
            Vector2 wave = new Vector2(0, 0);

            int iteration = 10;
            float speed = 1;
            for (int i = 1; i < iteration + 1; i++)
            {
                float force = 0.05f * i;
                speed = gold_noise(new Vector2(1, 1), speed);
                pos += new Vector2(i + 3.674f, -i + 7.4f) * iteration;
                wave = ComputeWave(pos, wave, force, speed, time * 2) * i * 0.15f;
            }
            wave /= iteration;

            return wave.X + wave.Y;
        }

        Vector2 ComputeWave(Vector2 pos, Vector2 wave, float force, float speed, float time)
        {
            return wave + new Vector2(MathF.Sin(pos.X * force + time * speed), MathF.Sin(pos.Y * force + time * speed));
        }

        const float PHI = 1.61803398874989484820459f;  // Φ = Golden Ratio   

        float gold_noise(Vector2 xy, in float seed)
        {
            var distance = (xy * PHI - xy).Length();
            return Fract(MathF.Tan(distance * seed) * xy.X);
        }

        float Fract(float value) => value - MathF.Floor(value);

        Vector2 Convert(Vector3 value) => new Vector2(value.X, value.Z);
    }
}