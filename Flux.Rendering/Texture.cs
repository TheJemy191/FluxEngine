using Silk.NET.OpenGL;

namespace Flux.Rendering;

public readonly struct Texture
{
    readonly uint handle;
    readonly GL gl;

    public unsafe Texture(GL gl)
    {
        this.gl = gl;

        handle = this.gl.GenTexture();
    }

    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        gl.ActiveTexture(textureSlot);
        gl.BindTexture(TextureTarget.Texture2D, handle);
    }

    public void Delete() => gl.DeleteTexture(handle);
}