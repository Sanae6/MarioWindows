using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace MarioWindows.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MarioState {
    public Vector3 Position;
    public Vector3 Velocity;
    public float FaceAngle;
    public short Health;
    public uint Flags;
    public uint Action;
}