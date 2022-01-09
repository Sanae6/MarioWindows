using System;
using System.Runtime.InteropServices;
using MarioWindows.Structs;

namespace MarioWindows;

public static unsafe class Sm64 {
    [DllImport("kernel32.dll")]
    public static extern void RtlZeroMemory(IntPtr dst, UIntPtr length);

    [DllImport("sm64")]
    public static extern void sm64_global_init(byte[] rom, byte[] texture, void* debugVarFunc = null);
    [DllImport("sm64")]
    public static extern void sm64_global_terminate();

    [DllImport("sm64")]
    public static extern void sm64_static_surfaces_load(Surface* surfaceArray, uint numSurfaces);

    [DllImport("sm64")]
    public static extern int sm64_mario_create(short x, short y, short z);
    [DllImport("sm64")]
    public static extern AnimInfo* sm64_get_anim_info(int marioId, short* rot); // short rot[3]
    [DllImport("sm64")]
    public static extern void sm64_mario_animTick(int marioId, uint stateFlags, AnimInfo* info, MarioGeo* outBuffers, short* rot); // short rot[3]
    [DllImport("sm64")]
    public static extern void sm64_mario_tick(int marioId, MarioInputs* inputs, MarioState* outState, MarioGeo* outBuffers);
    [DllImport("sm64")]
    public static extern void sm64_mario_delete(int marioId);
    [DllImport("sm64")]
    public static extern void sm64_mario_teleport(int marioId, float x, float y, float z);
    [DllImport("sm64")]
    public static extern void sm64_mario_apply_damage(int marioId, uint damage, uint interactionSubtype, float xSrc, float ySrc, float zSrc);
    [DllImport("sm64")]
    public static extern void sm64_mario_set_state(int marioId, uint capType);
    [DllImport("sm64")]
    public static extern void sm64_mario_set_water_level(int marioId, int yLevel);

    [DllImport("sm64")]
    public static extern uint sm64_surface_object_create(SurfaceObject* surfaceObject);
    [DllImport("sm64")]
    public static extern void sm64_surface_object_move(uint objectId, ObjectTransform* transform);
    [DllImport("sm64")]
    public static extern void sm64_surface_object_delete(uint objectId);
}