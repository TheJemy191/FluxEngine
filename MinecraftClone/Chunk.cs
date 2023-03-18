using System.Numerics;
using Flux.Rendering;
using Silk.NET.OpenGL;
using Shader = Flux.Rendering.Shader;

namespace MinecraftClone
{
    readonly struct Chunk
    {
        public const int VertexSize = 8;
        readonly GL Gl;

        readonly VertexArrayObject<float, uint> VAO;
        readonly BufferObject<float> VBO;
        readonly BufferObject<uint> EBO;
        readonly Shader shader;

        readonly uint indexesCount;
        readonly uint verticesCount;

        public Chunk(GL gl, in Shader shader, float[] vertexs, uint[] indices)
        {
            Gl = gl;
            this.shader = shader;

            indexesCount = (uint)indices.Length;
            verticesCount = (uint)vertexs.Length;
            EBO = new BufferObject<uint>(Gl, indices, BufferTargetARB.ElementArrayBuffer);
            VBO = new BufferObject<float>(Gl, vertexs, BufferTargetARB.ArrayBuffer);
            VAO = new VertexArrayObject<float, uint>(Gl, VBO, EBO);

            VAO.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, VertexSize, 0);
            VAO.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, VertexSize, 3);
            VAO.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, VertexSize, 6);
        }

        internal unsafe void Draw(Matrix4x4 view, Matrix4x4 projection, Matrix4x4 modelMatrix, Vector3 lightDir)
        {
            VAO.Bind();

            shader.Use();

            // Set texture here
            //texture.Bind(TextureUnit.Texture0);
            //shader.SetUniform(uniformName, 0);

            shader.SetUniform("uModel", modelMatrix);
            shader.SetUniform("uView", view);
            shader.SetUniform("uProjection", projection);

            shader.SetUniform("lightDirection", lightDir);
            Gl.DrawElements(PrimitiveType.Triangles, indexesCount, DrawElementsType.UnsignedInt, null);
            //Gl.DrawArrays(PrimitiveType.Triangles, 0, verticesCount);
        }

        public void Dispose()
        {
            VAO.Dispose();
            VBO.Dispose();
            EBO.Dispose();
        }
    }
}