using System;
using FullerProjection.Coordinates;
using static System.Math;
using static FullerProjection.Projection.IcosahedronConstants;
using System.Linq;

namespace FullerProjection.Projection
{
    public class Icosahedron
    {        
        public static Triangle GetTriangleContainingPoint(Cartesian point)
        {
            var triangleIndex = GetClosestTriangleIndexForPoint(point);
            var lcdIndex = GetLcdTriangleIndex(triangleIndex, point);

            return new Triangle
            {
                Index = triangleIndex,
                LcdIndex = lcdIndex
            };
        }

        public static Cartesian GetCentreCoordinate(int index)
        {
            if (!CentreCoordinates.ContainsKey(index)) throw new ArgumentException("Triangle index not recognised");
            return CentreCoordinates[index];
        }

        public static Cartesian GetIcosahedronVertexPoint(int index)
        {
            return new Cartesian(
                x: IcosahedronVertices[Axis.X][index],
                y: IcosahedronVertices[Axis.Y][index],
                z: IcosahedronVertices[Axis.Z][index]
            );
        }

        public static int GetFaceVertexForTriangle(int triangleIndex)
        {
            if (!TriangleIndexToFaceVertexMap.ContainsKey(triangleIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(triangleIndex), "Triangle index provided was not found.");
            }

            return TriangleIndexToFaceVertexMap[triangleIndex];
        }

        private static int GetLcdTriangleIndex(int triangleIndex, Cartesian point)
        {
            var result = GetHdistsForIndexAtPoint(triangleIndex, point);

            var h_dist1 = result.Item1;
            var h_dist2 = result.Item2;
            var h_dist3 = result.Item3;

            int h_lcd = 0;
            if ((h_dist1 <= h_dist2) && (h_dist2 <= h_dist3)) { h_lcd = 1; }
            if ((h_dist1 <= h_dist3) && (h_dist3 <= h_dist2)) { h_lcd = 6; }
            if ((h_dist2 <= h_dist1) && (h_dist1 <= h_dist3)) { h_lcd = 2; }
            if ((h_dist2 <= h_dist3) && (h_dist3 <= h_dist1)) { h_lcd = 3; }
            if ((h_dist3 <= h_dist1) && (h_dist1 <= h_dist2)) { h_lcd = 5; }
            if ((h_dist3 <= h_dist2) && (h_dist2 <= h_dist1)) { h_lcd = 4; }

            return h_lcd;
        }

        private static int GetClosestTriangleIndexForPoint(Cartesian point)
        {
            return TriangleIndices
                .Select(i =>
                {
                    var center = GetCentreCoordinate(i);
                    var diff = center - point;
                    return new { Index = i, Distance = Magnitude(diff.X, diff.Y, diff.Z) };
                })
                .OrderBy(x => x.Distance)
                .Select(x => x.Index)
                .First();
        }

        private static Tuple<double, double, double> GetHdistsForIndexAtPoint(int index, Cartesian point)
        {
            var triangleIndices = HDistMap[index];
            var h_dist1 = CalculateHdist(triangleIndices.I1, point);
            var h_dist2 = CalculateHdist(triangleIndices.I2, point);
            var h_dist3 = CalculateHdist(triangleIndices.I3, point);

            return new Tuple<double, double, double>(h_dist1, h_dist2, h_dist3);
        }

        private static double Magnitude(double x, double y, double z)
        {
            return Sqrt(Pow(x, 2) + Pow(y, 2) + Pow(z, 2));
        }

        private static double CalculateHdist(int vertexIndex, Cartesian point)
        {
            var h11 = point.X - IcosahedronVertices[Axis.X][vertexIndex];
            var h12 = point.Y - IcosahedronVertices[Axis.Y][vertexIndex];
            var h13 = point.Z - IcosahedronVertices[Axis.Z][vertexIndex];
            return Magnitude(h11, h12, h13);
        }



        
    }

}