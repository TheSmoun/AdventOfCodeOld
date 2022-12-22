using Advent_of_Code_2022.Days;
using Advent_of_Code_2022.Lib;

namespace Tests;

public class CubeTests
{
    public static IEnumerable<object[]> CubeTestData => new List<object[]>
    {
        // Face 1
        new object[] { new Vec2<int>(50, 20), DirRight, 1, new Vec2<int>(1, 20), DirRight, 5 },
        new object[] { new Vec2<int>(20, 50), DirDown, 1, new Vec2<int>(20, 1), DirDown, 4 },
        new object[] { new Vec2<int>(1, 20), DirLeft, 1, new Vec2<int>(1, 31), DirRight, 2 },
        new object[] { new Vec2<int>(20, 1), DirUp, 1, new Vec2<int>(1, 20), DirRight, 3 },
        
        // Face 2
        new object[] { new Vec2<int>(50, 20), DirRight, 2, new Vec2<int>(1, 20), DirRight, 6 },
        new object[] { new Vec2<int>(20, 50), DirDown, 2, new Vec2<int>(20, 1), DirDown, 3 },
        new object[] { new Vec2<int>(1, 20), DirLeft, 2, new Vec2<int>(1, 31), DirRight, 1 },
        new object[] { new Vec2<int>(20, 1), DirUp, 2, new Vec2<int>(1, 20), DirRight, 4 },
        
        // Face 3
        new object[] { new Vec2<int>(50, 20), DirRight, 3, new Vec2<int>(20, 50), DirUp, 6 },
        new object[] { new Vec2<int>(20, 50), DirDown, 3, new Vec2<int>(20, 1), DirDown, 5 },
        new object[] { new Vec2<int>(1, 20), DirLeft, 3, new Vec2<int>(20, 1), DirDown, 1 },
        new object[] { new Vec2<int>(20, 1), DirUp, 3, new Vec2<int>(20, 50), DirUp, 2 },
        
        // Face 4
        new object[] { new Vec2<int>(50, 20), DirRight, 4, new Vec2<int>(20, 50), DirUp, 5 },
        new object[] { new Vec2<int>(20, 50), DirDown, 4, new Vec2<int>(20, 1), DirDown, 6 },
        new object[] { new Vec2<int>(1, 20), DirLeft, 4, new Vec2<int>(20, 1), DirDown, 2 },
        new object[] { new Vec2<int>(20, 1), DirUp, 4, new Vec2<int>(20, 50), DirUp, 1 },
        
        // Face 5
        new object[] { new Vec2<int>(50, 20), DirRight, 5, new Vec2<int>(50, 31), DirLeft, 6 },
        new object[] { new Vec2<int>(20, 50), DirDown, 5, new Vec2<int>(50, 20), DirLeft, 4 },
        new object[] { new Vec2<int>(1, 20), DirLeft, 5, new Vec2<int>(50, 20), DirLeft, 1 },
        new object[] { new Vec2<int>(20, 1), DirUp, 5, new Vec2<int>(20, 50), DirUp, 3 },
        
        // Face 6
        new object[] { new Vec2<int>(50, 20), DirRight, 6, new Vec2<int>(50, 31), DirLeft, 5 },
        new object[] { new Vec2<int>(20, 50), DirDown, 6, new Vec2<int>(50, 20), DirLeft, 3 },
        new object[] { new Vec2<int>(1, 20), DirLeft, 6, new Vec2<int>(50, 20), DirLeft, 2 },
        new object[] { new Vec2<int>(20, 1), DirUp, 6, new Vec2<int>(20, 50), DirUp, 4 }
    };
    
    [Theory, MemberData(nameof(CubeTestData))]
    public void CubeTest(Vec2<int> pos, Vec2<int> dir, int face, Vec2<int> expectedPos, Vec2<int> expectedDir, int expectedFace)
    {
        var (newFace, newPosition, _, newDirection) = new Day22.Cube().Faces[face].Move(pos, dir);
        
        Assert.Equal(expectedFace, newFace.Id);
        Assert.Equal(expectedPos.X, newPosition.X);
        Assert.Equal(expectedPos.Y, newPosition.Y);
        Assert.Equal(expectedDir.X, newDirection.X);
        Assert.Equal(expectedDir.Y, newDirection.Y);
    }
}
