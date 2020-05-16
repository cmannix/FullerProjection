using System;
using Xunit;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Test
{
    public class AngleTests
    {
        [Fact]
        public void Can_create_from_degrees()
        {
            var degree = new Degrees(180);

            var angle = Angle.FromDegrees(degree);

            Assert.Equal(angle.Degrees, degree);
        }

        [Fact]
        public void Can_create_from_radians()
        {
            var radians = new Radians(Math.PI);

            var angle = Angle.FromRadians(radians);

            Assert.Equal(radians, angle.Radians);
        }

        [Fact]
        public void Correctly_converts_degrees_from_radians()
        {
            var radians = new Radians(Math.PI);

            var angle = Angle.FromRadians(radians);

            Assert.Equal(new Degrees(180), angle.Degrees);
        }
        
        [Fact]
        public void Correctly_converts_radians_from_degrees()
        {
            var degrees = new Degrees(180);

            var angle = Angle.FromDegrees(degrees);

            Assert.Equal(new Radians(Math.PI), angle.Radians);
        }

    }
}
