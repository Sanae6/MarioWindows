using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace MarioWindows.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MarioGeo {
    public const int MaxVertexCount = 3 * 1024;
    public readonly float* Position = (float*) Marshal.AllocHGlobal(Marshal.SizeOf<Vector3>() * MaxVertexCount);
    public readonly float* Normal = (float*) Marshal.AllocHGlobal(Marshal.SizeOf<Vector3>() * MaxVertexCount);
    public readonly float* Color = (float*) Marshal.AllocHGlobal(Marshal.SizeOf<Vector3>() * MaxVertexCount);
    public readonly float* Uv = (float*) Marshal.AllocHGlobal(Marshal.SizeOf<Vector2>() * MaxVertexCount);
    public readonly ushort TrisUsed;
}
