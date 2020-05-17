using System;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;
using FullerProjection.Core.Projection;
using FullerProjection.Core.Common;
using static FullerProjection.Core.Projection.Icosahedron;
using static System.Math;

namespace FullerProjection.Core
{

    public class FullerProjectionService
    {

        public static Cartesian2D GetFullerPoint(Geodesic point) => GetCoordinatesOnFullerProjection(point);

        static Cartesian2D GetCoordinatesOnFullerProjection(Geodesic mapCoordinate)
        {
            var coordinate = Conversion.Cartesian3D.From(mapCoordinate);
            var containingTriangle = GetTriangleContainingPoint(coordinate);
            var vertexIndex = GetFaceVertexForTriangle(containingTriangle.Index);

            var sp = Conversion.Geodesic.From(GetCentreCoordinate(containingTriangle.Index));
            
            var vertexCoordinate = GetIcosahedronVertexPoint(vertexIndex)
                .RotateZ(sp.Longitude)
                .RotateY(sp.Latitude);

            var sp2 = Conversion.Geodesic.From(vertexCoordinate);
            var adjustedLongitude = sp2.Longitude - (Angle.From(Degrees.Ninety));

            coordinate = coordinate
                .RotateZ(sp.Longitude)
                .RotateY(sp.Latitude)
                .RotateZ(adjustedLongitude);

            /* exact transformation equations */

            var gz = Sqrt(1 - Pow(coordinate.X, 2) - Pow(coordinate.Y, 2));
            var gs = Sqrt(5 + 2 * Sqrt(5)) / (gz * Sqrt(15));

            var gxp = coordinate.X * gs;
            var gyp = coordinate.Y * gs;

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

            point = transform(point);

            return point;
        }

        private static Func<Cartesian2D, Cartesian2D> GetFullerTransform(Triangle containingTriangle)
        {
            (Angle rotation, double xShift, double yShift) = containingTriangle.Index switch
            {
                0 => (Angle.From(Degrees.FromRaw(240)), 2.0, 7.0 / (2.0 * Sqrt(3.0))),
                1 => (Angle.From(Degrees.FromRaw(300)), 2, 5.0 / (2.0 * Sqrt(3.0))),
                2 => (Angle.From(Degrees.FromRaw(0)), 2.5, 2.0 / Sqrt(3.0)),
                3 => (Angle.From(Degrees.FromRaw(60)), 3, 5.0 / (2.0 + Sqrt(3.0))),
                4 => (Angle.From(Degrees.FromRaw(180)), 2.5, 4.0 * Sqrt(3.0) / 3.0),
                5 => (Angle.From(Degrees.FromRaw(300)), 1.5, 4.0 * Sqrt(3.0) / 3.0),
                6 => (Angle.From(Degrees.FromRaw(300)), 1.0, 5.0 * Sqrt(2.0) / 3.0),
                7 => (Angle.From(Degrees.FromRaw(0)), 1.5, 2.0 / Sqrt(3.0)),
                8 when (containingTriangle.LcdIndex > 2) => (Angle.From(Degrees.FromRaw(300)), 1.5, 1.0 / Sqrt(3.0)),
                8 => (Angle.From(Degrees.FromRaw(0)), 2, 1.0 / Sqrt(3.0)),
                9 => (Angle.From(Degrees.FromRaw(60)), 2.5, 1.0 / Sqrt(3.0)),
                10 => (Angle.From(Degrees.FromRaw(60)), 3.5, 1.0 / Sqrt(3.0)),
                11 => (Angle.From(Degrees.FromRaw(120)), 3.5, 2.0 / Sqrt(3.0)),
                12 => (Angle.From(Degrees.FromRaw(60)), 4.0, 5.0 / (2.0 * Sqrt(3.0))),
                13 => (Angle.From(Degrees.FromRaw(0)), 4.0, 7.0 / (2.0 * Sqrt(3.0))),
                14 => (Angle.From(Degrees.FromRaw(0)), 5.0, 7.0 / (2.0 * Sqrt(3.0))),
                15 when (containingTriangle.LcdIndex < 4) => (Angle.From(Degrees.FromRaw(60)), 0.5, 1.0 / Sqrt(3.0)),
                15 => (Angle.From(Degrees.FromRaw(0)), 5.5, 2.0 / Sqrt(3.0)),
                16 => (Angle.From(Degrees.FromRaw(0)), 1.0, 1.0 / (2.0 * Sqrt(3.0))),
                17 => (Angle.From(Degrees.FromRaw(120)), 4.0, 1.0 / (2.0 * Sqrt(3.0))),
                18 => (Angle.From(Degrees.FromRaw(120)), 4.5, 5.0 / Sqrt(3.0)),
                19 => (Angle.From(Degrees.FromRaw(300)), 5.0, 5.0 / (2.0 * Sqrt(3.0))),
                _ => throw new ArgumentException(
                    message: $"Index ({containingTriangle.Index}) of containing triangle was not recognized. Should be between 1 and 20.", 
                    paramName: nameof(containingTriangle))
            };

            return p => p.Rotate(rotation).TransformX(xShift).TransformY(yShift);
        }

        private static readonly double GArc = 2.0 * Asin(Sqrt(5 - Sqrt(5)) / Sqrt(10));
        private static readonly double Gt = GArc / 2.0;
        private static readonly double GDve = Sqrt(3 + Sqrt(5)) / Sqrt(5 + Sqrt(5));
        private static readonly double GElevation = Sqrt(8) / Sqrt(5 + Sqrt(5));
    }
}
