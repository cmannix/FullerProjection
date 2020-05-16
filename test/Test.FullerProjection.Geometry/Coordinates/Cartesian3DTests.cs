using System;
using Xunit;
using FullerProjection.Geometry.Coordinates;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Test
{
    public class Cartesian3DTests
    {
        [Fact]
        public void Can_create()
        {
            var (x, y, z) = (1, 2, 3);
            var point = new Cartesian3D(x, y, z);

            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
            Assert.Equal(z, point.Z);
        }

        [Fact]
        public void Can_add()
        {
            var point1 = new Cartesian3D(1, 2, 3);
            var point2 = new Cartesian3D(4, 5, 6);

            var result = point1 + point2;

            Assert.Equal(new Cartesian3D(5, 7, 9), result);
        }

        [Fact]
        public void Can_subtract()
        {
            var point1 = new Cartesian3D(1, 2, 3);
            var point2 = new Cartesian3D(4, 5, 6);

            var result = point2 - point1;

            Assert.Equal(new Cartesian3D(3, 3, 3), result);
        }

        [Theory]
        [InlineData(1, 1, 1, 1.7320508075688772)]
        [InlineData(2, 2, 2, 3.4641016151377544)]
        public void Can_calculate_magnitude(double x, double y, double z, double expected)
        {
            var point = new Cartesian3D(x, y, z);

            var result = point.Magnitude();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 1, 1, 360, 1, 1, 1)]
        [InlineData(1, 1, 1, 90, 1, -1, 1)]
        [InlineData(1, 1, 1, 180, 1, -1, -1)]
        [InlineData(1, 1, 1, 270, 1, 1, -1)]
        public void Can_rotate_around_x(double initialX, double initialY, double initialZ, double rotationAngleDegrees, double expectedX, double expectedY, double expectedZ)
        {
            var point = new Cartesian3D(initialX, initialY, initialZ);

            var result = point.RotateX(Angle.FromDegrees(new Degrees(rotationAngleDegrees)));

            Assert.Equal(new Cartesian3D(expectedX, expectedY, expectedZ), result);
        }

        [Theory]
        [InlineData(1, 1, 1, 360, 1, 1, 1)]
        [InlineData(1, 1, 1, 90, 1, 1, -1)]
        [InlineData(1, 1, 1, 180, -1, 1, -1)]
        [InlineData(1, 1, 1, 270, -1, 1, 1)]
        public void Can_rotate_around_y(double initialX, double initialY, double initialZ, double rotationAngleDegrees, double expectedX, double expectedY, double expectedZ)
        {
            var point = new Cartesian3D(initialX, initialY, initialZ);

            var result = point.RotateY(Angle.FromDegrees(new Degrees(rotationAngleDegrees)));

            Assert.Equal(new Cartesian3D(expectedX, expectedY, expectedZ), result);
        }

        [Theory]
        [InlineData(1, 1, 1, 360, 1, 1, 1)]
        [InlineData(1, 1, 1, 90, -1, 1, 1)]
        [InlineData(1, 1, 1, 180, -1, -1, 1)]
        [InlineData(1, 1, 1, 270, 1, -1, 1)]
        public void Can_rotate_around_z(double initialX, double initialY, double initialZ, double rotationAngleDegrees, double expectedX, double expectedY, double expectedZ)
        {
            var point = new Cartesian3D(initialX, initialY, initialZ);

            var result = point.RotateZ(Angle.FromDegrees(new Degrees(rotationAngleDegrees)));

            Assert.Equal(new Cartesian3D(expectedX, expectedY, expectedZ), result);
        }

    }
}
