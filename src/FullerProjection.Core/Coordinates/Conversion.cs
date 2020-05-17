using System;
using FullerProjection.Geometry.Coordinates.Extensions;
using FullerProjection.Geometry.Coordinates;
using FullerProjection.Geometry.Angles;
using FullerProjection.Common;
using static FullerProjection.Geometry.Angles.AngleMath;

namespace FullerProjection.Geometry.Coordinates
{
    public static class Conversion
    {
        public static Cartesian3D CartesianFrom(Spherical point)
        {
            var x = Sin(point.Theta) * Cos(point.Phi);
            var y = Sin(point.Theta) * Sin(point.Phi);
            var z = Cos(point.Theta);

            return new Cartesian3D(
                x: x, 
                y: y, 
                z: z);
        }
        
        public static Geodesic GeodesicFrom(Cartesian3D point)
        {
            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            var latitude = Angle.FromRadians(new Radians(System.Math.Acos(z)));
            var longitude = Angle.FromDegrees(Degrees.Zero);

            if (x.IsEqualTo(0) && y.IsGreaterThan(0)) { longitude = Angle.FromDegrees(Degrees.Ninety); }
            if (x.IsEqualTo(0) && y.IsLessThan(0)) { longitude = Angle.FromDegrees(Degrees.TwoSeventy); }
            if (x.IsEqualTo(0) && y.IsEqualTo(0)) { longitude = Angle.FromDegrees(Degrees.Zero); }
            if (x.IsLessThan(0) && y.IsEqualTo(0)) { longitude = Angle.FromDegrees(Degrees.OneEighty); }
            if (x.IsNotEqualTo(0) && y.IsNotEqualTo(0))
            {
                var a = Angle.FromDegrees(Degrees.Zero);
                if (x.IsGreaterThan(0) && y.IsGreaterThan(0)) { a = Angle.FromDegrees(Degrees.Zero); }
                if (x.IsLessThan(0) && y.IsGreaterThan(0)) { a = Angle.FromDegrees(Degrees.OneEighty); }
                if (x.IsLessThan(0) && y.IsLessThan(0)) { a = Angle.FromDegrees(Degrees.OneEighty); }
                if (x.IsGreaterThan(0) && y.IsLessThan(0)) { a = Angle.FromDegrees(Degrees.ThreeSixty); }
                longitude = Angle.FromRadians(new Radians(System.Math.Atan(y / x) + a.Radians.Value));
            }

            return new Geodesic(
                latitude: latitude, 
                longitude: longitude);
        }
        
        public static Spherical SphericalFrom(Geodesic point) 
        {
            var theta = Angle.FromDegrees(Degrees.Ninety) - point.Latitude;

            var phi = point.Longitude;

            return new Spherical(
                phi: phi, 
                theta: theta);
        }

        public static Cartesian3D CartesianFrom(Geodesic point) 
        {
            return Conversion.CartesianFrom(SphericalFrom(point));
        }
    }

    
}