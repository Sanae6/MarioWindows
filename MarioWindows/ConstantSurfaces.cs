using MarioWindows.Structs;
using OpenTK.Mathematics;

namespace MarioWindows;

public static class ConstantSurfaces {
    public static readonly Surface[] Vertices = {
        new Surface(
            0,
            0,
            1,
            new[] {
                new Vector3i(1000, 10, 1000),
                new Vector3i(1000, 10, -1000),
                new Vector3i(-1000, 10, 1000)
            }
        ),
        new Surface(
            0,
            0,
            1,
            new[] {
                new Vector3i(-1000, 10, -1000),
                new Vector3i(1000, 10, -1000),
                new Vector3i(-1000, 10, 1000),
            }
        )
    };
}