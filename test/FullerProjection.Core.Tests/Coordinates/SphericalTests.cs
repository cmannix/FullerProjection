using System;
using Xunit;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;

namespace FullerProjection.UnitTests.Core
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
            var phi = Angle.From(Degrees.FromRaw(rawPhi));
            var theta = Angle.From(Degrees.FromRaw(rawTheta));
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
            var phi = Angle.From(Degrees.FromRaw(rawPhi));
            var theta = Angle.From(Degrees.Ninety);
            var point = new Spherical(phi, theta);

            Assert.Equal(Degrees.FromRaw(expected), point.Phi.Degrees);
        }

        [Theory]
        [InlineData(-1.23, 178.77)]
        [InlineData(181.23, 1.23)]
        [InlineData(365.23, 5.23)]
        [InlineData(-365.23, 174.77)]
        public void Theta_outside_of_0_to_180_range_wraps(double rawTheta, double expected)
        {
            var phi = Angle.From(Degrees.Ninety);
            var theta = Angle.From(Degrees.FromRaw(rawTheta));
            var point = new Spherical(phi, theta);

            Assert.Equal(Degrees.FromRaw(expected), point.Theta.Degrees);
        }

        [Fact]
        public void Negative_r_throws()
        {
            var phi = Angle.From(Degrees.Ninety);
            var theta = Angle.From(Degrees.Ninety);
            
            Assert.Throws<ArgumentException>(() => new Spherical(phi, theta, -1));
        }

        [Fact]
        public void Default_r_value_is_one()
        {
            var phi = Angle.From(Degrees.Ninety);
            var theta = Angle.From(Degrees.Ninety);

            var point = new Spherical(phi, theta);
            
            Assert.Equal(1, point.R);
        }
    }
}
