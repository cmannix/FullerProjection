using System;
using static System.Math;
using FullerProjection.Geometry.Coordinates;
using FullerProjection.Geometry.Coordinates.Extensions;
using FullerProjection.Geometry.Angles;
using FullerProjection.Core.Projection;
using FullerProjection.Common;
using static FullerProjection.Core.Projection.Icosahedron;

namespace FullerProjection.Core
{

    public class FullerProjectionService
    {

        public static Cartesian2D GetFullerPoint(Geodesic point)
        {
            Console.WriteLine($"Geodesic point: {point}");
            var spherical = Conversion.SphericalFrom(point);
            Console.WriteLine($"Spherical point: {spherical}");
            var cartesian = Conversion.CartesianFrom(point);
            Console.WriteLine($"Cartesian point: {cartesian}");

            var triangle = GetTriangleContainingPoint(cartesian);

            return GetCoordinatesOnFullerProjection(triangle, cartesian);
        } 

        static Cartesian2D GetCoordinatesOnFullerProjection(Triangle containingTriangle, Cartesian3D mapCoordinate)
        {
            var vertexIndex = GetFaceVertexForTriangle(containingTriangle.Index);
            var vertexCoordinate = GetIcosahedronVertexPoint(vertexIndex);

            var sp = Conversion.GeodesicFrom(GetCentreCoordinate(containingTriangle.Index));

            mapCoordinate = mapCoordinate.RotateZ(sp.Longitude);
            vertexCoordinate = vertexCoordinate.RotateZ(sp.Longitude);

            mapCoordinate = mapCoordinate.RotateY(sp.Latitude);
            vertexCoordinate = vertexCoordinate.RotateY(sp.Latitude);

            var sp2 = Conversion.GeodesicFrom(vertexCoordinate);
            var adjustedLongitude = sp2.Longitude - (Angle.FromDegrees(Degrees.Ninety));

            mapCoordinate = mapCoordinate.RotateZ(adjustedLongitude);

            /* exact transformation equations */

            var gz = Sqrt(1 - Pow(mapCoordinate.X, 2) - Pow(mapCoordinate.Y, 2));
            var gs = Sqrt(5 + 2 * Sqrt(5)) / (gz * Sqrt(15));
            
            var gxp = mapCoordinate.X * gs;
            var gyp = mapCoordinate.Y * gs;
            
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

            var point = new Cartesian2D(x: x, y: y);

            var transform = GetFullerTransform(containingTriangle);

            point = point.ApplyTransform(transform);

            return point;
        }

        private static FullerTransform2D GetFullerTransform(Triangle containingTriangle)
        {
            switch (containingTriangle.Index)
            {
                case 0:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(240)), 2, 7.0 / (2.0 * Sqrt(3.0)));
                case 1:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(300)), 2, 5.0 / (2.0 * Sqrt(3.0)));
                case 2:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 2.5, 2.0 / Sqrt(3.0));
                case 3:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(60)), 3, 5.0 / (2.0 + Sqrt(3.0)));
                case 4:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(180)), 2.5, 4.0 * Sqrt(3.0) / 3.0);
                case 5:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(300)), 1.5, 4.0 * Sqrt(3.0) / 3.0);
                case 6:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(300)), 1.0, 5.0 * Sqrt(2.0) / 3.0);
                case 7:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 1.5, 2.0 / Sqrt(3.0));
                case 8:
                    return (containingTriangle.LcdIndex > 2)
                        ? new FullerTransform2D(Angle.FromDegrees(new Degrees(300)), 1.5, 1.0 / Sqrt(3.0))
                        : new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 2, 1.0 / Sqrt(3.0));
                case 9:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(60)), 2.5, 1.0 / Sqrt(3.0));
                case 10:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(60)), 3.5, 1.0 / Sqrt(3.0));
                case 11:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(120)), 3.5, 2.0 / Sqrt(3.0));
                case 12:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(60)), 4.0, 5.0 / (2.0 *Sqrt(3.0)));
                case 13:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 4.0, 7.0 / (2.0 *Sqrt(3.0)));
                case 14:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 5.0, 7.0 / (2.0 *Sqrt(3.0)));
                case 15:
                    return (containingTriangle.LcdIndex < 4)
                    ? new FullerTransform2D(Angle.FromDegrees(new Degrees(60)), 0.5, 1.0 / Sqrt(3.0))
                     : new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 5.5, 2.0 / Sqrt(3.0));
                case 16:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(0)), 1.0, 1.0 / (2.0 * Sqrt(3.0)));
                case 17:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(120)), 4.0, 1.0 / (2.0 * Sqrt(3.0)));
                case 18:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(120)), 4.5, 5.0 / Sqrt(3.0));
                case 19:
                    return new FullerTransform2D(Angle.FromDegrees(new Degrees(300)), 5.0, 5.0 / (2.0 * Sqrt(3.0)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(containingTriangle), $"Index ({containingTriangle.Index}) of containing triangle was not recognized. Should be between 1 and 20.");
            }
        }

        private static readonly double GArc = 2.0 * Asin(Sqrt(5 - Sqrt(5)) / Sqrt(10));
        private static readonly double Gt = GArc / 2.0;
        private static readonly double GDve = Sqrt(3 + Sqrt(5)) / Sqrt(5 + Sqrt(5));
        private static readonly double GElevation = Sqrt(8) / Sqrt(5 + Sqrt(5));

            

        
    }
}
