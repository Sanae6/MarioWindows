using System;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace MarioWindows.Structs;

public abstract class Mario : IDisposable {
    protected static int MarioCount;
    public readonly uint Id;
    public unsafe MarioGeo* Geometry = (MarioGeo*) Marshal.AllocHGlobal(Marshal.SizeOf<MarioGeo>());
    public unsafe MarioInputs* Inputs = (MarioInputs*) Marshal.AllocHGlobal(Marshal.SizeOf<MarioInputs>());
    public unsafe MarioState* State = (MarioState*) Marshal.AllocHGlobal(Marshal.SizeOf<MarioState>());
    private bool VeryFirstRender = true;

    protected Mario(Vector3i start) {
        Id = Sm64.sm64_mario_create((short) start.X, (short) start.Y, (short) start.Z);
        Console.WriteLine($"Created new mario {Id}");
        MarioCount++;
        unsafe {
            Sm64.RtlZeroMemory((IntPtr) Geometry, (UIntPtr) Marshal.SizeOf<MarioGeo>());
            Sm64.RtlZeroMemory((IntPtr) Inputs, (UIntPtr) Marshal.SizeOf<MarioInputs>());
            Sm64.RtlZeroMemory((IntPtr) State, (UIntPtr) Marshal.SizeOf<MarioState>());
        }
    }

    protected virtual void FirstRender() {}

    public virtual void Render(Matrix4 view, Matrix4 projection) {
        if (VeryFirstRender) {
            FirstRender();
            VeryFirstRender = false;
        }
    }

    public virtual unsafe void Update() {
        Sm64.sm64_mario_tick(Id, Inputs, State, Geometry);
    }

    public virtual void Dispose() {
        Sm64.sm64_mario_delete(Id);
        MarioCount--;
    }
}