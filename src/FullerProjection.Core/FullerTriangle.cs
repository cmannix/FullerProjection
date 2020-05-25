using FullerProjection.Core.Geometry.Coordinates;
using System;
using System.Linq;
using FullerProjection.Core.Geometry.Angles;
using static System.Math;

namespace FullerProjection.Core
{
    public class FullerTriangle
    {
        public static FullerTriangle ForPoint(Cartesian3D point)
        {
            var container = FullerIcosahedron.Faces
                .Select((f, i) => new { Index = i, Face = f })
                .OrderBy(o => (o.Face.Centroid - point).Magnitude())
                .First();

            var lcdIndex = GetLcdTriangleIndex(container.Face, point);
            var transform = GetFullerTransformation(container.Index, lcdIndex);
            return new FullerTriangle(container.Face, lcdIndex, transform);
        }

        public FullerIcosahedron.Face IcosahedronFace { get; }
        public int LcdIndex { get; }
        public Func<Cartesian2D, Cartesian2D> Transform { get; }

        private static int GetLcdTriangleIndex(FullerIcosahedron.Face face, Cartesian3D point)
        {
            var hDist1 = (point - face.A).Magnitude();
            var hDist2 = (point - face.B).Magnitude();
            var hDist3 = (point - face.C).Magnitude();

            if ((hDist1 <= hDist2) && (hDist2 <= hDist3)) { return 0; }
            if ((hDist1 <= hDist3) && (hDist3 <= hDist2)) { return 5; }
            if ((hDist2 <= hDist1) && (hDist1 <= hDist3)) { return 1; }
            if ((hDist2 <= hDist3) && (hDist3 <= hDist1)) { return 2; }
            if ((hDist3 <= hDist1) && (hDist1 <= hDist2)) { return 4; }
            if ((hDist3 <= hDist2) && (hDist2 <= hDist1)) { return 3; }
            else throw new Exception("Could not identify lowest common denominator triangle");
        }

        private static Func<Cartesian2D, Cartesian2D> GetFullerTransformation(int triangleIndex, int lcdTriangleIndex)
        {
            (Angle rotation, double xShift, double yShift) = triangleIndex switch
            {
                0 => (Angle.From(Degrees.FromRaw(240)), 2.0, 7.0 / (2.0 * Sqrt(3.0))),
                1 => (Angle.From(Degrees.FromRaw(300)), 2, 5.0 / (2.0 * Sqrt(3.0))),
                2 => (Angle.From(Degrees.FromRaw(0)), 2.5, 2.0 / Sqrt(3.0)),
                3 => (Angle.From(Degrees.FromRaw(60)), 3, 5.0 / (2.0 + Sqrt(3.0))),
                4 => (Angle.From(Degrees.FromRaw(180)), 2.5, 4.0 * Sqrt(3.0) / 3.0),
                5 => (Angle.From(Degrees.FromRaw(300)), 1.5, 4.0 * Sqrt(3.0) / 3.0),
                6 => (Angle.From(Degrees.FromRaw(300)), 1.0, 5.0 * Sqrt(2.0) / 3.0),
                7 => (Angle.From(Degrees.FromRaw(0)), 1.5, 2.0 / Sqrt(3.0)),
                8 when (lcdTriangleIndex > 2) => (Angle.From(Degrees.FromRaw(300)), 1.5, 1.0 / Sqrt(3.0)),
                8 => (Angle.From(Degrees.FromRaw(0)), 2, 1.0 / Sqrt(3.0)),
                9 => (Angle.From(Degrees.FromRaw(60)), 2.5, 1.0 / Sqrt(3.0)),
                10 => (Angle.From(Degrees.FromRaw(60)), 3.5, 1.0 / Sqrt(3.0)),
                11 => (Angle.From(Degrees.FromRaw(120)), 3.5, 2.0 / Sqrt(3.0)),
                12 => (Angle.From(Degrees.FromRaw(60)), 4.0, 5.0 / (2.0 * Sqrt(3.0))),
                13 => (Angle.From(Degrees.FromRaw(0)), 4.0, 7.0 / (2.0 * Sqrt(3.0))),
                14 => (Angle.From(Degrees.FromRaw(0)), 5.0, 7.0 / (2.0 * Sqrt(3.0))),
                15 when (lcdTriangleIndex < 4) => (Angle.From(Degrees.FromRaw(60)), 0.5, 1.0 / Sqrt(3.0)),
                15 => (Angle.From(Degrees.FromRaw(0)), 5.5, 2.0 / Sqrt(3.0)),
                16 => (Angle.From(Degrees.FromRaw(0)), 1.0, 1.0 / (2.0 * Sqrt(3.0))),
                17 => (Angle.From(Degrees.FromRaw(120)), 4.0, 1.0 / (2.0 * Sqrt(3.0))),
                18 => (Angle.From(Degrees.FromRaw(120)), 4.5, 5.0 / Sqrt(3.0)),
                19 => (Angle.From(Degrees.FromRaw(300)), 5.0, 5.0 / (2.0 * Sqrt(3.0))),
                _ => throw new ArgumentException(
                    message: $"Index ({triangleIndex}) of containing triangle was not recognized. Should be between 0 and 19.",
                    paramName: nameof(triangleIndex))
            };

            return p => p.Rotate(rotation).TransformX(xShift).TransformY(yShift);
        }
        private FullerTriangle(FullerIcosahedron.Face face, int lcdIndex, Func<Cartesian2D, Cartesian2D> transform)
        {
            IcosahedronFace = face;
            LcdIndex = lcdIndex;
            Transform = transform;
        }
    }
}
