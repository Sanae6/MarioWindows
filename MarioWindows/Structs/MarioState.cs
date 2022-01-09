using OpenTK.Mathematics;

namespace MarioWindows.Structs;

public struct MarioState {
    public Vector3 Position;
    public Vector3 Velocity;
    public float FaceAngle;
    public short Health;
    public uint Flags;
    public uint Action;
}