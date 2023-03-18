using System.Numerics;
using Silk.NET.OpenGL;

namespace Flux.Rendering;

public readonly struct Shader : IDisposable
{
    readonly uint handle;
    readonly GL gl;

    public Shader(GL gl, string vertexSource, string fragmentSource)
    {
        this.gl = gl;

        var vertex = SendToGPU(ShaderType.VertexShader, vertexSource);
        var fragment = SendToGPU(ShaderType.FragmentShader, fragmentSource);

        handle = this.gl.CreateProgram();

        this.gl.AttachShader(handle, vertex);
        this.gl.AttachShader(handle, fragment);
        this.gl.LinkProgram(handle);

        this.gl.GetProgram(handle, GLEnum.LinkStatus, out var status);
        if (status == 0)
            throw new GlException($"Program failed to link with error: {this.gl.GetProgramInfoLog(handle)}");

        this.gl.DetachShader(handle, vertex);
        this.gl.DetachShader(handle, fragment);
        this.gl.DeleteShader(vertex);
        this.gl.DeleteShader(fragment);
    }

    uint SendToGPU(ShaderType type, string src)
    {
        var handle = gl.CreateShader(type);
        gl.ShaderSource(handle, src);
        gl.CompileShader(handle);

        var infoLog = gl.GetShaderInfoLog(handle);
        if (!string.IsNullOrWhiteSpace(infoLog))
            throw new GlException($"Error compiling shader of type {type}, failed with error {infoLog}");

        return handle;
    }

    public void Use() => gl.UseProgram(handle);

    int GetUniformLocation(string name)
    {
        if (TryGetUniformLocation(name, out var location))
            return location;

        throw new GlException($"{name} uniform not found on shader.");
    }

    bool TryGetUniformLocation(string name, out int location)
    {
        location = gl.GetUniformLocation(handle, name);
        return location != -1;
    }

    internal void SetUniforms(IEnumerable<Uniform> uniforms)
    {
        foreach (var uniform in uniforms)
        {
            SetUniform(uniform);
        }
    }

    public unsafe void SetUniform<T>(string name, T uniform)
    {
        if (!TryGetUniformLocation(name, out var location))
            return;

        if (uniform is int intUni)
        {
            gl.Uniform1(location, intUni);
        }
        else if (uniform is float floatUni)
        {
            gl.Uniform1(location, floatUni);
        }
        else if (uniform is Vector2 vector2Uni)
        {
            gl.Uniform2(location, vector2Uni.X, vector2Uni.Y);
        }
        else if (uniform is Vector3 vector3Uni)
        {
            gl.Uniform3(location, vector3Uni.X, vector3Uni.Y, vector3Uni.Z);
        }
        else if (uniform is Matrix4x4 matrix4x4Uni)
        {
            var v = matrix4x4Uni;
            gl.UniformMatrix4(location, 1, false, (float*)&v);
        }
    }

    public unsafe void SetUniform(Uniform uniform)
    {
        if (!TryGetUniformLocation(uniform.name, out var location))
            return;

        if (uniform is Uniform<int> intUni)
        {
            SetUniform(uniform.name, intUni.value);
        }
        else if (uniform is Uniform<float> floatUni)
        {
            SetUniform(uniform.name, floatUni.value);
        }
        else if (uniform is Uniform<Vector2> vector2Uni)
        {
            SetUniform(uniform.name, vector2Uni.value);
        }
        else if (uniform is Uniform<Vector3> vector3Uni)
        {
            SetUniform(uniform.name, vector3Uni.value);
        }
        else if (uniform is Uniform<Matrix4x4> matrix4x4Uni)
        {
            SetUniform(uniform.name, matrix4x4Uni.value);
        }
    }

    public void Dispose() => gl.DeleteProgram(handle);
}
