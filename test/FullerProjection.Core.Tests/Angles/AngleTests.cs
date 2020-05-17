using System;
using Xunit;
using FullerProjection.Core.Geometry.Angles;

namespace FullerProjection.UnitTests.Core
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

        [Theory]
        [InlineData(1,2, false)]
        [InlineData(0,2, false)]
        [InlineData(1,1, true)]
        public void Can_compare_for_equality(double a, double b, bool expectedResult)
        {
            var angle1 = Angle.From(Degrees.FromRaw(a));
            var angle2 = Angle.From(Degrees.FromRaw(b));

            Assert.Equal(expectedResult, angle1 == angle2);
        }

        [Theory]
        [InlineData(1,2, false)]
        [InlineData(2,1, true)]
        [InlineData(1,1, false)]
        public void Can_compare_for_greater_than(double a, double b, bool expectedResult)
        {
            var angle1 = Angle.From(Degrees.FromRaw(a));
            var angle2 = Angle.From(Degrees.FromRaw(b));

            Assert.Equal(expectedResult, angle1 > angle2);
        }

        [Theory]
        [InlineData(1,2, true)]
        [InlineData(2,1, false)]
        [InlineData(1,1, false)]
        public void Can_compare_for_less_than(double a, double b, bool expectedResult)
        {
            var angle1 = Angle.From(Degrees.FromRaw(a));
            var angle2 = Angle.From(Degrees.FromRaw(b));

            Assert.Equal(expectedResult, angle1 < angle2);
        }

        [Theory]
        [InlineData(180,180, 0)]
        [InlineData(0,180, 0)]
        [InlineData(10,180, 10)]
        [InlineData(185,180, 5)]
        [InlineData(-75,180, -75)]
        [InlineData(-185,180, -5)]
        public void Can_preform_modulo(double value, double mod, double expectedResult)
        {
            var angleVal = Angle.From(Degrees.FromRaw(value));
            var angleMod = Angle.From(Degrees.FromRaw(mod));

            Assert.Equal(Angle.From(Degrees.FromRaw(expectedResult)), angleVal % angleMod);
        }

    }
}
