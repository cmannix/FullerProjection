using System;
using Xunit;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;

namespace FullerProjection.Test
{
    public class Cartesian2DTests
    {
        [Fact]
        public void Can_create()
        {
            var (x, y) = (1, 2);
            var point = new Cartesian2D(x, y);

            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, -1, 0)]
        [InlineData(-1, 0.5, -0.5)]
        [InlineData(-1, 0, -1)]
        public void Can_transform_x(double initialX, double transform, double expectedX)
        {
            var point = new Cartesian2D(initialX, 1);

            var result = point.TransformX(transform);

            var expectedPoint = new Cartesian2D(expectedX, 1);
            Assert.Equal(expectedPoint, result);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, -1, 0)]
        [InlineData(-1, 0.5, -0.5)]
        [InlineData(-1, 0, -1)]
        public void Can_transform_y(double initialY, double transform, double expectedY)
        {
            var point = new Cartesian2D(1, initialY);

            var result = point.TransformY(transform);

            var expectedPoint = new Cartesian2D(1, expectedY);
            Assert.Equal(expectedPoint, result);
        }

        [Theory]
        [InlineData(1, 1, 90, -1, 1)]
        [InlineData(1, 1, 180, -1, -1)]
        [InlineData(1, 1, 270, 1, -1)]
        [InlineData(1, 1, 360, 1, 1)]
        public void Can_rotate_about_origin(double initialX, double initialY, double rotationAngleDegrees, double expectedX, double expectedY)
        {
            var point = new Cartesian2D(initialX, initialY);
            var rotationAngle = Angle.From(Degrees.FromRaw(rotationAngleDegrees));

            var result = point.Rotate(Angle.From(Degrees.FromRaw(rotationAngleDegrees)));

            Assert.Equal(new Cartesian2D(expectedX, expectedY), result);
        }
    }
}
