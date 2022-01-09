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
    public static extern uint sm64_mario_create(short x, short y, short z);
    [DllImport("sm64")]
    public static extern AnimInfo* sm64_get_anim_info(uint marioId, short* rot); // short rot[3]
    [DllImport("sm64")]
    public static extern void sm64_mario_animTick(uint marioId, uint stateFlags, AnimInfo* info, MarioGeo* outBuffers, short* rot); // short rot[3]
    [DllImport("sm64")]
    public static extern void sm64_mario_tick(uint marioId, MarioInputs* inputs, MarioState* outState, MarioGeo* outBuffers);
    [DllImport("sm64")]
    public static extern void sm64_mario_delete(uint marioId);
    [DllImport("sm64")]
    public static extern void sm64_mario_teleport(uint marioId, float x, float y, float z);
    [DllImport("sm64")]
    public static extern void sm64_mario_apply_damage(uint marioId, uint damage, uint interactionSubtype, float xSrc, float ySrc, float zSrc);
    [DllImport("sm64")]
    public static extern void sm64_mario_set_state(uint marioId, uint capType);
    [DllImport("sm64")]
    public static extern void sm64_mario_set_water_level(uint marioId, int yLevel);

    [DllImport("sm64")]
    public static extern uint sm64_surface_object_create(SurfaceObject* surfaceObject);
    [DllImport("sm64")]
    public static extern void sm64_surface_object_move(uint objectId, ObjectTransform* transform);
    [DllImport("sm64")]
    public static extern void sm64_surface_object_delete(uint objectId);
}