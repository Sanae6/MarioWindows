using System.Runtime.InteropServices;

namespace MarioWindows.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MarioInputs {
    public float CamLookX, CamLookZ;
    public float StickX, StickY;
    public byte ButtonA, ButtonB, ButtonZ;
}
