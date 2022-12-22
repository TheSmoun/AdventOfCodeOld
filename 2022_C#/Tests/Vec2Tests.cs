using Advent_of_Code_2022.Lib;

namespace Tests;

public class Vec2Tests
{
    public static IEnumerable<object[]> RotationData => new List<object[]>
    {
        new object[] { DirRight, true, DirDown },
        new object[] { DirDown, true, DirLeft },
        new object[] { DirLeft, true, DirUp },
        new object[] { DirUp, true, DirRight },
        new object[] { DirRight, false, DirUp },
        new object[] { DirDown, false, DirRight },
        new object[] { DirLeft, false, DirDown },
        new object[] { DirUp, false, DirLeft }
    };

    [Theory, MemberData(nameof(RotationData))]
    public void Rotation(Vec2<int> vec, bool clockwise, Vec2<int> expectedResult)
    {
        var newVec = vec.Rotate(clockwise);
        Assert.Equal(expectedResult.X, newVec.X);
        Assert.Equal(expectedResult.Y, newVec.Y);
    }
}
