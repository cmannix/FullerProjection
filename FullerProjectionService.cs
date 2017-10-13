using System;
using static System.Math;
using FullerProjection.Coordinates;
using FullerProjection.Geometry;
using FullerProjection.Projection;

namespace FullerProjection
{

    public class FullerProjectionService
    {

        public static FullerPoint GetFullerPoint(Geodesic point)
        {
            var spherical = Spherical.FromGeodesic(point);
            var cartesian = Cartesian.FromSpherical(spherical);

            var triangle = Icosahedron.GetTriangleContainingPoint(cartesian);

            return GetCoordinatesOnFullerProjection(triangle, cartesian);
        } 

        static FullerPoint GetCoordinatesOnFullerProjection(Triangle containingTriangle, Cartesian mapCoordinate)
        {
            var vertexIndex = Icosahedron.GetFaceVertexForTriangle(containingTriangle.Index);
            var vertexCoordinate = Icosahedron.GetIcosahedronVertexPoint(vertexIndex);

            var sp = Geodesic.FromCartesian(Icosahedron.GetCentreCoordinate(containingTriangle.Index));

            mapCoordinate = Cartesian.Rotate(mapCoordinate, Axis.Z, sp.Longitude);
            vertexCoordinate = Cartesian.Rotate(vertexCoordinate, Axis.Z, sp.Longitude);

            mapCoordinate = Cartesian.Rotate(mapCoordinate, Axis.Y, sp.Latitude);
            vertexCoordinate = Cartesian.Rotate(vertexCoordinate, Axis.Y, sp.Latitude);

            var sp2 = Geodesic.FromCartesian(vertexCoordinate);
            sp2.Longitude = sp2.Longitude.Subtract(Angle.FromDegrees(90.0));

            mapCoordinate = Cartesian.Rotate(mapCoordinate, Axis.Z, sp2.Longitude);

            /* exact transformation equations */

            var gz = Sqrt(1 - Pow(mapCoordinate.X, 2) - Pow(mapCoordinate.Y, 2));
            var gs = Sqrt(5 + 2 * Sqrt(5)) / (gz * Sqrt(15));
            
            var gxp = mapCoordinate.X * gs;
            var gyp = mapCoordinate.Y * gs;
            
            var ga1p = 2.0 * gyp / Sqrt(3.0) + (G_ELEVATION / 3.0);
            var ga2p = gxp - (gyp / Sqrt(3)) + (G_ELEVATION / 3.0);
            var ga3p = (G_ELEVATION / 3.0) - gxp - (gyp / Sqrt(3));
            
            var ga1 = G_T + Atan((ga1p - 0.5 * G_ELEVATION) / G_DVE);
            var ga2 = G_T + Atan((ga2p - 0.5 * G_ELEVATION) / G_DVE);
            var ga3 = G_T + Atan((ga3p - 0.5 * G_ELEVATION) / G_DVE);
            
            var gx = 0.5 * (ga2 - ga3);
            
            var gy = (1.0 / (2.0 * Sqrt(3))) * (2 * ga1 - ga2 - ga3);
            
            /* Re-scale so plane triangle edge length is 1. */

            var x = gx / G_ARC;
            var y = gy / G_ARC;

            var point = new FullerPoint(x: x, y: y);

            var transform = GetFullerTransform(containingTriangle);

            point = transform.Apply(point);

            return point;
        }

        private static FullerTransform2d GetFullerTransform(Triangle containingTriangle)
        {
            switch (containingTriangle.Index)
            {
                case 0:
                    return new FullerTransform2d(Angle.FromDegrees(240), i => i + 2, i => i + 7.0 / (2.0 * Sqrt(3.0)));
                case 1:
                    return new FullerTransform2d(Angle.FromDegrees(300), i => i + 2, i => i + 5.0 / (2.0 * Sqrt(3.0)));
                case 2:
                    return new FullerTransform2d(Angle.FromDegrees(0), i => i + 2.5, i => i + 2.0 / Sqrt(3.0));
                case 3:
                    return new FullerTransform2d(Angle.FromDegrees(60), i => i + 3, i => i + 5.0 / (2.0 + Sqrt(3.0)));
                case 4:
                    return new FullerTransform2d(Angle.FromDegrees(180), i => i + 2.5, i => i + 4.0 * Sqrt(3.0) / 3.0);
                case 5:
                    return new FullerTransform2d(Angle.FromDegrees(300), i => i + 1.5, i => i + 4.0 * Sqrt(3.0) / 3.0);
                case 6:
                    return new FullerTransform2d(Angle.FromDegrees(300), i => i + 1.0, i => i + 5.0 * Sqrt(2.0) / 3.0);
                case 7:
                    return new FullerTransform2d(Angle.FromDegrees(0), i => i + 1.5, i => i + 2.0 / Sqrt(3.0));
                case 8:
                    return (containingTriangle.LcdIndex > 2)
                        ? new FullerTransform2d(Angle.FromDegrees(300), i => i + 1.5, i => i + 1.0 / Sqrt(3.0))
                        : new FullerTransform2d(Angle.FromDegrees(0), i => i + 2, i => i + 1.0 / Sqrt(3.0));
                case 9:
                    return new FullerTransform2d(Angle.FromDegrees(60), i => i + 2.5, i => i + 1.0 / Sqrt(3.0));
                case 10:
                    return new FullerTransform2d(Angle.FromDegrees(60), i => i + 3.5, i => i + 1.0 / Sqrt(3.0));
                case 11:
                    return new FullerTransform2d(Angle.FromDegrees(120), i => i + 3.5, i => i + 2.0 / Sqrt(3.0));
                case 12:
                    return new FullerTransform2d(Angle.FromDegrees(60), i => i + 4.0, i => i + 5.0 / (2.0 *Sqrt(3.0)));
                case 13:
                    return new FullerTransform2d(Angle.FromDegrees(0), i => i + 4.0, i => i + 7.0 / (2.0 *Sqrt(3.0)));
                case 14:
                    return new FullerTransform2d(Angle.FromDegrees(0), i => i + 5.0, i => i + 7.0 / (2.0 *Sqrt(3.0)));
                case 15:
                    return (containingTriangle.LcdIndex < 4)
                    ? new FullerTransform2d(Angle.FromDegrees(60), i => i + 0.5, i => i + 1.0 / Sqrt(3.0))
                     : new FullerTransform2d(Angle.FromDegrees(0), i => i + 5.5, i => i + 2.0 / Sqrt(3.0));
                case 16:
                    return new FullerTransform2d(Angle.FromDegrees(0), i => i + 1.0, i => i + 1.0 / (2.0 * Sqrt(3.0)));
                case 17:
                    return new FullerTransform2d(Angle.FromDegrees(120), i => i + 4.0, i => i + 1.0 / (2.0 * Sqrt(3.0)));
                case 18:
                    return new FullerTransform2d(Angle.FromDegrees(120), i => i + 4.5, i => i + 5.0 / Sqrt(3.0));
                case 19:
                    return new FullerTransform2d(Angle.FromDegrees(300), i => i + 5.0, i => i + 5.0 / (2.0 * Sqrt(3.0)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(containingTriangle), $"Index ({containingTriangle.Index}) of containing triangle was not recognized. Should be between 1 and 20.");
            }
        }

        private static double G_ARC = 2.0 * Asin(Sqrt(5 - Sqrt(5)) / Sqrt(10));
        private static double G_T = G_ARC / 2.0;
        private static double G_DVE = Sqrt(3 + Sqrt(5)) / Sqrt(5 + Sqrt(5));
        private static double G_ELEVATION = Sqrt(8) / Sqrt(5 + Sqrt(5));

            

        
    }
}
