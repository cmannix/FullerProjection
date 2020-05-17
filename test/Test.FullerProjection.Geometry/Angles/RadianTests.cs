using System;
using Xunit;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Test
{
    public class RadianTests
    {
        [Theory]
        [InlineData(30, 30)]
        [InlineData(360, 360)]
        [InlineData(30.1, 30.1)]
        [InlineData(370, 370)]
        [InlineData(-10, -10)]
        public void Can_create_from_number(double n, double expected)
        {
            var degree = Radians.FromRaw(n);

            Assert.Equal(expected, degree.Value);
        }

        [Theory]
        [InlineData(30, 10, 40)]
        [InlineData(360, 360, 720)]
        [InlineData(30.1, 20.5, 50.6)]
        [InlineData(350, 20, 370)]
        public void Can_add(double a, double b, double result)
        {
            var degree1 = Radians.FromRaw(a);
            var degree2 = Radians.FromRaw(b);

            Assert.Equal(Radians.FromRaw(result), degree1 + degree2);
        }

        [Theory]
        [InlineData(30, 10, 20)]
        [InlineData(360, 360, 0)]
        [InlineData(30.1, 20.5, 9.6)]
        [InlineData(10, 20, -10)]
        public void Can_subtract(double a, double b, double result)
        {
            var degree1 = Radians.FromRaw(a);
            var degree2 = Radians.FromRaw(b);

            Assert.Equal(Radians.FromRaw(result), degree1 - degree2);
        }

        [Theory]
        [InlineData(1,2, false)]
        [InlineData(0,2, false)]
        [InlineData(1,1, true)]
        public void Can_compare_for_equality(double a, double b, bool expectedResult)
        {
            var degree1 = Radians.FromRaw(a);
            var degree2 = Radians.FromRaw(b);

            Assert.Equal(expectedResult, degree1 == degree2);
        }
    }
}
