using System;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;
using FullerProjection.Core;
using static System.Math;
using static FullerProjection.Core.DymaxionConstants;

namespace FullerProjection.Core
{

    public class FullerProjection
    {

        public static Cartesian2D GetFullerPoint(Geodesic point) => GetCoordinatesOnFullerProjection(Conversion.Cartesian3D.From(point));

        public static Cartesian2D GetCoordinatesOnFullerProjection(Cartesian3D point)
        {
            var containingTriangle = FullerTriangle.ForPoint(point);
            var triangleCentre = Conversion.Spherical.From(containingTriangle.IcosahedronFace.Centroid);

            var vertexPoint = containingTriangle.IcosahedronFace.A
                .RotateZ(triangleCentre.Phi)
                .RotateY(triangleCentre.Theta);

            point = point
                .RotateZ(triangleCentre.Phi)
                .RotateY(triangleCentre.Theta);

            var sphericalVertexPoint = Conversion.Spherical.From(vertexPoint);
            var adjustedLongitude = sphericalVertexPoint.Phi - (Angle.From(Degrees.Ninety));

            point = point.RotateZ(adjustedLongitude);

            var dymaxionPoint = ToDymaxionPoint(point);

            return containingTriangle.Transform(dymaxionPoint);
        }

        private static Cartesian2D ToDymaxionPoint(Cartesian3D point) {
            // Here be dragons            
            var gz = Sqrt(1 - Pow(point.X, 2) - Pow(point.Y, 2));
            var gs = Sqrt(5 + 2 * Sqrt(5)) / (gz * Sqrt(15));

            var gxp = point.X * gs;
            var gyp = point.Y * gs;

            var ga1p = 2.0 * gyp / Sqrt(3.0) + (GElevation / 3.0);
            var ga2p = gxp - (gyp / Sqrt(3)) + (GElevation / 3.0);
            var ga3p = (GElevation / 3.0) - gxp - (gyp / Sqrt(3));

            var ga1 = Gt + Atan((ga1p - 0.5 * GElevation) / GDve);
            var ga2 = Gt + Atan((ga2p - 0.5 * GElevation) / GDve);
            var ga3 = Gt + Atan((ga3p - 0.5 * GElevation) / GDve);

            var gx = 0.5 * (ga2 - ga3);

            var gy = (1.0 / (2.0 * Sqrt(3))) * (2 * ga1 - ga2 - ga3);

            /* Re-scale so plane triangle edge length is 1. */
            var x = gx / GArc;
            var y = gy / GArc;

            return new Cartesian2D(x: x, y: y);
        }

    }

    public static class DymaxionConstants
    {
        public static readonly double GArc = 2.0 * Asin(Sqrt(5 - Sqrt(5)) / Sqrt(10));
        public static readonly double Gt = GArc / 2.0;
        public static readonly double GDve = Sqrt(3 + Sqrt(5)) / Sqrt(5 + Sqrt(5));
        public static readonly double GElevation = Sqrt(8) / Sqrt(5 + Sqrt(5));
    }
}
