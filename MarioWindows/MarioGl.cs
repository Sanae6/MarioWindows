using System;
using System.Runtime.InteropServices;
using MarioWindows.Structs;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MarioWindows;

public class MarioGl : Mario {
    private int vao;
    private int posBuffer;
    private int normalBuffer;
    private int colorBuffer;
    private int uvBuffer;
    private short[] indices = new short[MarioGeo.MaxVertexCount];
    private int vertexCount;
    public MarioGl(Vector3i start) : base(start) { }
    public Vector3 Position {
        get {
            unsafe {
                return State->Position;
            }
        }
        set {
            unsafe {
                State->Position = value;
            }
        }
    }

    protected override void FirstRender() {
        for (short i = 0; i < indices.Length; i++) indices[i] = i;
        vertexCount = MarioGeo.MaxVertexCount;

        vao = GL.GenVertexArray();
        GL.BindVertexArray(vao);
        
        unsafe void CreateBuffer<T>(uint location, out int buffer, float* data) {
            buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) Marshal.SizeOf<T>(), (IntPtr) data, BufferUsageHint.DynamicDraw);
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, Marshal.SizeOf<T>() / Marshal.SizeOf<float>(), VertexAttribPointerType.Float, false, Marshal.SizeOf<float>(), IntPtr.Zero);
        }

        unsafe {
            CreateBuffer<Vector3>(0, out posBuffer, Geometry->Position);
            CreateBuffer<Vector3>(1, out normalBuffer, Geometry->Normal);
            CreateBuffer<Vector3>(2, out colorBuffer, Geometry->Color);
            CreateBuffer<Vector2>(3, out uvBuffer, Geometry->Uv);
        }
    }

    public override unsafe void Render(Matrix4 view, Matrix4 projection) {
        base.Render(view, projection);

        vertexCount = 3 * Geometry->TrisUsed;

        void SetBufferData<T>(int buffer, float* data) {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf<T>() * MarioGeo.MaxVertexCount, (IntPtr) data, BufferUsageHint.DynamicDraw);
        }
        
        SetBufferData<Vector3>(posBuffer, Geometry->Position);
        SetBufferData<Vector3>(normalBuffer, Geometry->Normal);
        SetBufferData<Vector3>(colorBuffer, Geometry->Color);
        SetBufferData<Vector3>(uvBuffer, Geometry->Uv);
        
        GL.UseProgram(Entrypoint.MarioProgram);
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, Entrypoint.MarioTexture);
        GL.BindVertexArray(vao);
        GL.UniformMatrix4(GL.GetUniformLocation(Entrypoint.MarioProgram, "view"), false, ref view);
        GL.UniformMatrix4(GL.GetUniformLocation(Entrypoint.MarioProgram, "projection"), false, ref projection);
        GL.Uniform1(GL.GetUniformLocation(Entrypoint.MarioProgram, "marioTex"), 0);
        GL.DrawElements(PrimitiveType.Triangles, vertexCount, DrawElementsType.UnsignedShort, indices);
    }

    public override void Dispose() {
        base.Dispose();
        if (MarioCount == 0) {
            // clear out gl shit
        }
    }
}