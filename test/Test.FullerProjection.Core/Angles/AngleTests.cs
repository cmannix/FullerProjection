using System;
using Xunit;
using FullerProjection.Core.Geometry.Angles;

namespace FullerProjection.Test
{
    public class AngleTests
    {
        [Fact]
        public void Can_create_from_degrees()
        {
            var degree = Degrees.FromRaw(180);

            var angle = Angle.From(degree);

            Assert.Equal(angle.Degrees, degree);
        }

        [Fact]
        public void Can_create_from_radians()
        {
            var radians = Radians.FromRaw(Math.PI);

            var angle = Angle.From(radians);

            Assert.Equal(radians, angle.Radians);
        }

        [Fact]
        public void Correctly_converts_degrees_from_radians()
        {
            var radians = Radians.FromRaw(Math.PI);

            var angle = Angle.From(radians);

            Assert.Equal(Degrees.FromRaw(180), angle.Degrees);
        }
        
        [Fact]
        public void Correctly_converts_radians_from_degrees()
        {
            var degrees = Degrees.FromRaw(180);

            var angle = Angle.From(degrees);

            Assert.Equal(Radians.FromRaw(Math.PI), angle.Radians);
        }

    }
}