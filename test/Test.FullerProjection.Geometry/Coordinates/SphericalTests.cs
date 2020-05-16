using System;
using Xunit;
using FullerProjection.Geometry.Coordinates;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Test
{
    public class SphericalTests
    {
        [Theory]
        [InlineData(30, 30, 1)]
        [InlineData(30, 30, 0)]
        [InlineData(359, 179, 100)]
        [InlineData(0, 0, 1)]
        public void Can_create(double rawPhi, double rawTheta, double r)
        {
            var phi = Angle.FromDegrees(new Degrees(rawPhi));
            var theta = Angle.FromDegrees(new Degrees(rawTheta));
            var point = new Spherical(phi, theta, r);

            Assert.Equal(phi, point.Phi);
            Assert.Equal(theta, point.Theta);
            Assert.Equal(r, point.R);
        }

        [Theory]
        [InlineData(-1.23, 358.77)]
        [InlineData(361.23, 1.23)]
        [InlineData(725.23, 5.23)]
        [InlineData(-725.23, 354.77)]
        public void Phi_outside_of_0_to_360_range_wraps(double rawPhi, double expected)
        {
            var phi = Angle.FromDegrees(new Degrees(rawPhi));
            var theta = Angle.FromDegrees(Degrees.Ninety);
            var point = new Spherical(phi, theta);

            Assert.Equal(new Degrees(expected), point.Phi.Degrees);
        }

        [Theory]
        [InlineData(-1.23, 178.77)]
        [InlineData(181.23, 1.23)]
        [InlineData(365.23, 5.23)]
        [InlineData(-365.23, 174.77)]
        public void Theta_outside_of_0_to_180_range_wraps(double rawTheta, double expected)
        {
            var phi = Angle.FromDegrees(Degrees.Ninety);
            var theta = Angle.FromDegrees(new Degrees(rawTheta));
            var point = new Spherical(phi, theta);

            Assert.Equal(new Degrees(expected), point.Theta.Degrees);
        }

        [Fact]
        public void Negative_r_throws()
        {
            var phi = Angle.FromDegrees(Degrees.Ninety);
            var theta = Angle.FromDegrees(Degrees.Ninety);
            
            Assert.Throws<ArgumentException>(() => new Spherical(phi, theta, -1));
        }

        [Fact]
        public void Default_r_value_is_one()
        {
            var phi = Angle.FromDegrees(Degrees.Ninety);
            var theta = Angle.FromDegrees(Degrees.Ninety);

            var point = new Spherical(phi, theta);
            
            Assert.Equal(1, point.R);
        }
    }
}
