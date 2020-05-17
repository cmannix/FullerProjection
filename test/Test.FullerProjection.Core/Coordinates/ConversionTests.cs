using System;
using Xunit;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;

namespace FullerProjection.Test
{
    public class ConversionTests
    {
        [Theory]
        [InlineData(90, 90, 1, 0, 1, 0)]
        [InlineData(30, 60, 10, 7.5, 4.3301270189221924, 5)]
        [InlineData(280, 88, 13.4, 2.32546810491142603, -13.1883849854850189, 0.46765325581351302)]
        public void Can_convert_spherical_to_cartesian(double phi, double theta, double r, double x, double y, double z)
        {
            var spherical = new Spherical(
                phi: Angle.From(Degrees.FromRaw(phi)),
                theta: Angle.From(Degrees.FromRaw(theta)),
                r: r);

            var cartesian = Conversion.Cartesian3D.From(spherical);

            var expected = new Cartesian3D(x,y,z);
            Assert.Equal(expected, cartesian);
        }

        [Theory]
        [InlineData(90, 90, 90, 0, 1)]
        [InlineData(-88, 290, 290, 178, 1)]
        [InlineData(-90, 370, 10, 180, 1)]
        public void Can_convert_geodesic_to_spherical(double lat, double lon, double phi, double theta, double r)
        {
            var geodesic = new Geodesic(
                latitude: Angle.From(Degrees.FromRaw(lat)), 
                longitude: Angle.From(Degrees.FromRaw(lon))
            );
            
            var result = Conversion.Spherical.From(geodesic);

            var expected = new Spherical(
                phi: Angle.From(Degrees.FromRaw(phi)),
                theta: Angle.From(Degrees.FromRaw(theta)),
                r: r);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 1, 0, 90, 90, 1)]
        [InlineData(7.5, 4.3301270189221924, 5, 30, 60, 10)]
        [InlineData(2.32546810491142603, -13.1883849854850189, 0.46765325581351302, 280, 88, 13.4)]
        public void Can_convert_cartesian_to_spherical(double x, double y, double z, double phi, double theta, double r)
        {
            var point = new Cartesian3D(x, y, z);
            
            var result = Conversion.Spherical.From(point);

            var expected = new Spherical(
                phi: Angle.From(Degrees.FromRaw(phi)),
                theta: Angle.From(Degrees.FromRaw(theta)),
                r: r);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(90, 0, 1, 90, 90)]
        [InlineData(290, 178, 1, -88, 290)]
        [InlineData(10, 179, 1, -89, 370)]
        public void Can_convert_spherical_to_geodesic(double phi, double theta, double r, double lat, double lon)
        {
            var point = new Spherical(
                phi: Angle.From(Degrees.FromRaw(phi)),
                theta: Angle.From(Degrees.FromRaw(theta)),
                r: r);
            
            var result = Conversion.Geodesic.From(point);

            var expected = new Geodesic(
                latitude: Angle.From(Degrees.FromRaw(lat)), 
                longitude: Angle.From(Degrees.FromRaw(lon))
            );
            Assert.Equal(expected, result);
        }
    }
}
