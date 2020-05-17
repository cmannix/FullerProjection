using System;
using FullerProjection.Geometry.Coordinates;
using static FullerProjection.Core.Projection.IcosahedronConstants;
using System.Linq;
using FullerProjection.Geometry.Coordinates.Extensions;

namespace FullerProjection.Core.Projection
{
    public static class Icosahedron
    {
        public static Triangle GetTriangleContainingPoint(Cartesian3D point)
        {
            var triangleIndex = GetClosestTriangleIndexForPoint(point);
            var lcdIndex = GetLcdTriangleIndex(triangleIndex, point);

            return new Triangle
            {
                Index = triangleIndex,
                LcdIndex = lcdIndex
            };
        }

        public static Cartesian3D GetCentreCoordinate(int index)
        {
            if (!CentreCoordinates.ContainsKey(index)) throw new ArgumentException("Triangle index not recognised");
            return CentreCoordinates[index];
        }

        public static Cartesian3D GetIcosahedronVertexPoint(int index)
        {
            return IcosahedronVertices[index];
        }

        public static int GetFaceVertexForTriangle(int triangleIndex)
        {
            if (!TriangleIndexToFaceVertexMap.ContainsKey(triangleIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(triangleIndex), "Triangle index provided was not found.");
            }

            return TriangleIndexToFaceVertexMap[triangleIndex];
        }

        private static int GetLcdTriangleIndex(int triangleIndex, Cartesian3D point)
        {
            var result = GetHdistsForIndexAtPoint(triangleIndex, point);

            var hDist1 = result.Item1;
            var hDist2 = result.Item2;
            var hDist3 = result.Item3;

            int h_lcd = 0;
            if ((hDist1 <= hDist2) && (hDist2 <= hDist3)) { h_lcd = 1; }
            if ((hDist1 <= hDist3) && (hDist3 <= hDist2)) { h_lcd = 6; }
            if ((hDist2 <= hDist1) && (hDist1 <= hDist3)) { h_lcd = 2; }
            if ((hDist2 <= hDist3) && (hDist3 <= hDist1)) { h_lcd = 3; }
            if ((hDist3 <= hDist1) && (hDist1 <= hDist2)) { h_lcd = 5; }
            if ((hDist3 <= hDist2) && (hDist2 <= hDist1)) { h_lcd = 4; }

            return h_lcd;
        }

        private static int GetClosestTriangleIndexForPoint(Cartesian3D point)
        {
            return TriangleIndices
                .Select(i =>
                {
                    var center = GetCentreCoordinate(i);
                    var diff = center - point;
                    return new { Index = i, Distance = diff.Magnitude() };
                })
                .OrderBy(x => x.Distance)
                .Select(x => x.Index)
                .First();
        }

        private static Tuple<double, double, double> GetHdistsForIndexAtPoint(int index, Cartesian3D point)
        {
            var triangleIndices = HDistMap[index];
            var h_dist1 = CalculateHdist(triangleIndices.I1, point);
            var h_dist2 = CalculateHdist(triangleIndices.I2, point);
            var h_dist3 = CalculateHdist(triangleIndices.I3, point);

            return new Tuple<double, double, double>(h_dist1, h_dist2, h_dist3);
        }

        private static double CalculateHdist(int vertexIndex, Cartesian3D point)
        {
            var vertexPoint = GetIcosahedronVertexPoint(vertexIndex);
            var dist = point - vertexPoint;

            return dist.Magnitude();
        }
    }

}