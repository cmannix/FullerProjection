using System;
using Xunit;
using FullerProjection.Geometry.Angles;
using static FullerProjection.Geometry.Angles.AngleMath;

namespace FullerProjection.Test
{
    public class AngleMathTests
    {
        [Fact]
        public void Can_calculate_sin_of_angle()
        {
            var angle = Angle.FromDegrees(new Degrees(90));

            var result = Sin(angle);

            var expected = 1;
            Assert.Equal(expected, result, DoubleComparisonPrecision);
        }

        [Fact]
        public void Can_calculate_cos_of_angle()
        {
            var angle = Angle.FromDegrees(new Degrees(90));

            var result = Cos(angle);

            var expected = 0;
            Assert.Equal(expected, result, DoubleComparisonPrecision);
        }

        [Fact]
        public void Can_calculate_tan_of_angle()
        {
            var angle = Angle.FromDegrees(new Degrees(30));

            var result = Tan(angle);

            var expected = 0.5773502691996256;
            Assert.Equal(expected, result, DoubleComparisonPrecision);
        }

        private const int DoubleComparisonPrecision = 10;
    }
}