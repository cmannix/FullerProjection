using System;
using static System.Math;
using FullerProjection.Coordinates;
using FullerProjection.Coordinates.Extensions;
using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;
using FullerProjection.Projection;
using static FullerProjection.Projection.Icosahedron;

namespace FullerProjection
{

    public class FullerProjectionService
    {

        public static IFullerPoint GetFullerPoint(Geodesic point)
        {
            var spherical = point.ToSpherical();
            var cartesian = spherical.ToCartesian();

            var triangle = GetTriangleContainingPoint(cartesian);

            return GetCoordinatesOnFullerProjection(triangle, cartesian);
        } 

        static IFullerPoint GetCoordinatesOnFullerProjection(Triangle containingTriangle, ICartesianPoint mapCoordinate)
        {
            var vertexIndex = GetFaceVertexForTriangle(containingTriangle.Index);
            var vertexCoordinate = GetIcosahedronVertexPoint(vertexIndex);

            var sp = GetCentreCoordinate(containingTriangle.Index).ToGeodesic();

            mapCoordinate = mapCoordinate.Rotate(Axis.Z, sp.Longitude);
            vertexCoordinate = vertexCoordinate.Rotate(Axis.Z, sp.Longitude);

            mapCoordinate = mapCoordinate.Rotate(Axis.Y, sp.Latitude);
            vertexCoordinate = vertexCoordinate.Rotate(Axis.Y, sp.Latitude);

            var sp2 = vertexCoordinate.ToGeodesic();
            var adjustedLongitude = sp2.Longitude.Subtract(Angle.FromDegrees(90.0));
            sp2 = sp2.WithLongitude(adjustedLongitude);

            mapCoordinate = mapCoordinate.Rotate(Axis.Z, sp2.Longitude);

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

            IFullerPoint point = new FullerPoint(x: x, y: y);

            var transform = GetFullerTransform(containingTriangle);

            point = point.ApplyTransform(transform);

            return point;
        }

        private static FullerTransform2D GetFullerTransform(Triangle containingTriangle)
        {
            switch (containingTriangle.Index)
            {
                case 0:
                    return new FullerTransform2D(Angle.FromDegrees(240), i => i + 2, i => i + 7.0 / (2.0 * Sqrt(3.0)));
                case 1:
                    return new FullerTransform2D(Angle.FromDegrees(300), i => i + 2, i => i + 5.0 / (2.0 * Sqrt(3.0)));
                case 2:
                    return new FullerTransform2D(Angle.FromDegrees(0), i => i + 2.5, i => i + 2.0 / Sqrt(3.0));
                case 3:
                    return new FullerTransform2D(Angle.FromDegrees(60), i => i + 3, i => i + 5.0 / (2.0 + Sqrt(3.0)));
                case 4:
                    return new FullerTransform2D(Angle.FromDegrees(180), i => i + 2.5, i => i + 4.0 * Sqrt(3.0) / 3.0);
                case 5:
                    return new FullerTransform2D(Angle.FromDegrees(300), i => i + 1.5, i => i + 4.0 * Sqrt(3.0) / 3.0);
                case 6:
                    return new FullerTransform2D(Angle.FromDegrees(300), i => i + 1.0, i => i + 5.0 * Sqrt(2.0) / 3.0);
                case 7:
                    return new FullerTransform2D(Angle.FromDegrees(0), i => i + 1.5, i => i + 2.0 / Sqrt(3.0));
                case 8:
                    return (containingTriangle.LcdIndex > 2)
                        ? new FullerTransform2D(Angle.FromDegrees(300), i => i + 1.5, i => i + 1.0 / Sqrt(3.0))
                        : new FullerTransform2D(Angle.FromDegrees(0), i => i + 2, i => i + 1.0 / Sqrt(3.0));
                case 9:
                    return new FullerTransform2D(Angle.FromDegrees(60), i => i + 2.5, i => i + 1.0 / Sqrt(3.0));
                case 10:
                    return new FullerTransform2D(Angle.FromDegrees(60), i => i + 3.5, i => i + 1.0 / Sqrt(3.0));
                case 11:
                    return new FullerTransform2D(Angle.FromDegrees(120), i => i + 3.5, i => i + 2.0 / Sqrt(3.0));
                case 12:
                    return new FullerTransform2D(Angle.FromDegrees(60), i => i + 4.0, i => i + 5.0 / (2.0 *Sqrt(3.0)));
                case 13:
                    return new FullerTransform2D(Angle.FromDegrees(0), i => i + 4.0, i => i + 7.0 / (2.0 *Sqrt(3.0)));
                case 14:
                    return new FullerTransform2D(Angle.FromDegrees(0), i => i + 5.0, i => i + 7.0 / (2.0 *Sqrt(3.0)));
                case 15:
                    return (containingTriangle.LcdIndex < 4)
                    ? new FullerTransform2D(Angle.FromDegrees(60), i => i + 0.5, i => i + 1.0 / Sqrt(3.0))
                     : new FullerTransform2D(Angle.FromDegrees(0), i => i + 5.5, i => i + 2.0 / Sqrt(3.0));
                case 16:
                    return new FullerTransform2D(Angle.FromDegrees(0), i => i + 1.0, i => i + 1.0 / (2.0 * Sqrt(3.0)));
                case 17:
                    return new FullerTransform2D(Angle.FromDegrees(120), i => i + 4.0, i => i + 1.0 / (2.0 * Sqrt(3.0)));
                case 18:
                    return new FullerTransform2D(Angle.FromDegrees(120), i => i + 4.5, i => i + 5.0 / Sqrt(3.0));
                case 19:
                    return new FullerTransform2D(Angle.FromDegrees(300), i => i + 5.0, i => i + 5.0 / (2.0 * Sqrt(3.0)));
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
