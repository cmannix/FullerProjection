using System;
using FullerProjection.Coordinates.Extensions;
using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;
using static System.Math;
namespace FullerProjection.Coordinates
{
    public static class Conversion
    {
        public static ICartesianPoint CartestianFrom(ISphericalPoint point)
        {
            var x = Sin(point.Theta.Radians) * Cos(point.Phi.Radians);
            var y = Sin(point.Theta.Radians) * Sin(point.Phi.Radians);
            var z = Cos(point.Theta.Radians);

            return new Cartesian(
                x: x, 
                y: y, 
                z: z);
        }
        
        public static IGeodesicPoint GeodesicFrom(ICartesianPoint point)
        {
            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            var latitude = Angle.FromRadians(Acos(z));
            var longitude = Angle.FromDegrees(0);

            if (x.Is(0) && y.IsGreaterThan(0)) { longitude = Angle.FromDegrees(90.0); }
            if (x.Is(0) && y.IsLessThan(0)) { longitude = Angle.FromDegrees(270.0); }
            if (x.Is(0) && y.Is(0)) { longitude = Angle.FromDegrees(0.0); }
            if (x.IsLessThan(0) && y.Is(0)) { longitude = Angle.FromDegrees(180.0); }
            if (x.IsNot(0) && y.IsNot(0))
            {
                var a = Angle.FromDegrees(0);
                if (x.IsGreaterThan(0) && y.IsGreaterThan(0)) { a = Angle.FromDegrees(0.0); }
                if (x.IsLessThan(0) && y.IsGreaterThan(0)) { a = Angle.FromDegrees(180.0); }
                if (x.IsLessThan(0) && y.IsLessThan(0)) { a = Angle.FromDegrees(180.0); }
                if (x.IsGreaterThan(0) && y.IsLessThan(0)) { a = Angle.FromDegrees(360.0); }
                longitude = Angle.FromRadians(Atan(y / x) + a.Radians);
            }

            return new Geodesic(
                latitude: latitude, 
                longitude: longitude);
        }
        
        public static ISphericalPoint SphericalFrom(IGeodesicPoint point) 
        {
            var theta = Angle.FromDegrees(90.0) - point.Latitude;

            var phi = point.Longitude;

            return new Spherical(
                phi: phi, 
                theta: theta);
        }

        
    }

    
}