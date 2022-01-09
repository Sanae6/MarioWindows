using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace MarioWindows.Structs;

public struct Surface {
    public short Type;
    public short Force;
    public ushort Terrain;
    public unsafe Vector3i* Vertices;

    public Surface(short type, short force, ushort terrain, Vector3i[] vertices) {
        Type = type;
        Force = force;
        Terrain = terrain;
        unsafe {
            Vertices = (Vector3i*) Marshal.AllocHGlobal(Marshal.SizeOf<Vector3i>() * vertices.Length);
            for (int i = 0; i < vertices.Length; i++) Marshal.StructureToPtr(vertices[i], (IntPtr) (Vertices + i), false);
        }
    }

    public static unsafe Surface* CreateSurfaceList(Surface[] surfaces) {
        Surface* surfaceList = (Surface*) Marshal.AllocHGlobal(Marshal.SizeOf<Surface>() * surfaces.Length);
        for (int i = 0; i < surfaces.Length; i++) Marshal.StructureToPtr(surfaces[i], (IntPtr) (surfaceList + i), false);

        return surfaceList;
    }
}
