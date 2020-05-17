using System;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;
using FullerProjection.Core.Common;
using static FullerProjection.Core.Geometry.Angles.AngleMath;
using static System.Math;

namespace FullerProjection.Core.Geometry.Coordinates
{
    public static class Conversion
    {
        public static class Cartesian3D
        {
            public static Coordinates.Cartesian3D From(Coordinates.Spherical point)
            {
                var x = point.R * Sin(point.Theta) * Cos(point.Phi);
                var y = point.R * Sin(point.Theta) * Sin(point.Phi);
                var z = point.R * Cos(point.Theta);

                return new Coordinates.Cartesian3D(
                    x: x,
                    y: y,
                    z: z);
            }

            public static Coordinates.Cartesian3D From(Coordinates.Geodesic point) => From(Spherical.From(point));
        }

        public static class Spherical
        {
            public static Coordinates.Spherical From(Coordinates.Geodesic point)
            {
                var theta = Angle.From(Degrees.Ninety) - point.Latitude;

                var phi = point.Longitude;

                return new Coordinates.Spherical(
                    phi: phi,
                    theta: theta);
            }

            public static Coordinates.Spherical From(Coordinates.Cartesian3D point)
            {
                var r = Sqrt(Pow(point.X, 2) + Pow(point.Y, 2) + Pow(point.Z, 2));
                var phi = AngleMath.Atan2(point.Y, point.X);
                var theta = AngleMath.Atan2(Sqrt(Pow(point.X, 2) + Pow(point.Y, 2)), point.Z);

                return new Coordinates.Spherical(
                    phi: phi,
                    theta: theta,
                    r: r);
            }
        }

        public static class Geodesic
        {
            public static Coordinates.Geodesic From(Coordinates.Cartesian3D point) => Geodesic.From(Spherical.From(point));
            public static Coordinates.Geodesic From(Coordinates.Spherical point) => new Coordinates.Geodesic(latitude: Angle.From(Degrees.Ninety) - point.Theta, longitude: point.Phi);
        }
    }
}