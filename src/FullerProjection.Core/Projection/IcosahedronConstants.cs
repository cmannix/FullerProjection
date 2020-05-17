using FullerProjection.Core.Geometry.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullerProjection.Core.Projection
{
    public static class IcosahedronConstants
    {
        public static IEnumerable<int> TriangleIndices = Enumerable.Range(0, 20);
        public static Dictionary<int, Cartesian3D> CentreCoordinates = new Dictionary<int, Cartesian3D>();

        static IcosahedronConstants()
        {
            IcosahedronVertices = IcosahedronVerticesX.Zip(IcosahedronVerticesY, (x, y) => new { x, y }).Zip(IcosahedronVerticesZ, (xy, z) => new Cartesian3D(xy.x, xy.y, z)).ToArray();

            foreach (var index in TriangleIndices)
            {
                CentreCoordinates[index] = _GetCentreCoordinate(index);
            }

        }
        public static Dictionary<int, VertexIndex> CenterIndexToVertexIndicesMap = new Dictionary<int, VertexIndex>
        {
            {0, new VertexIndex(1, 2, 3)},
            {1, new VertexIndex(1, 3, 4)},
            {2, new VertexIndex(1, 4, 5)},
            {3, new VertexIndex(1, 5, 6)},
            {4, new VertexIndex(1, 2, 6)},
            {5, new VertexIndex(2, 3, 8)},
            {6, new VertexIndex(8, 3, 9)},
            {7, new VertexIndex(9, 3, 4)},
            {8, new VertexIndex(10, 9, 4)},
            {9, new VertexIndex(5, 10, 4)},
            {10, new VertexIndex(5, 11, 10)},
            {11, new VertexIndex(5, 6, 11)},
            {12, new VertexIndex(11, 6, 7)},
            {13, new VertexIndex(7, 6, 2)},
            {14, new VertexIndex(8, 7, 2)},
            {15, new VertexIndex(12, 9, 8)},
            {16, new VertexIndex(12, 9, 10)},
            {17, new VertexIndex(12, 11, 10)},
            {18, new VertexIndex(12, 11, 7)},
            {19, new VertexIndex(12, 8, 7)},
        };

        public static Dictionary<int, VertexIndex> HDistMap = new Dictionary<int, VertexIndex>
        {
            {0, new VertexIndex(1, 3, 2)},
            {1, new VertexIndex(1, 4, 3)},
            {2, new VertexIndex(1, 5, 4)},
            {3, new VertexIndex(1, 6, 5)},
            {4, new VertexIndex(1, 2, 6)},
            {5, new VertexIndex(2, 3, 8)},
            {6, new VertexIndex(3, 9, 8)},
            {7, new VertexIndex(3, 4, 9)},
            {8, new VertexIndex(4, 10, 9)},
            {9, new VertexIndex(4, 5, 10)},
            {10, new VertexIndex(5, 11, 10)},
            {11, new VertexIndex(5, 6, 11)},
            {12, new VertexIndex(6, 7, 11)},
            {13, new VertexIndex(2, 7, 6)},
            {14, new VertexIndex(2, 8, 7)},
            {15, new VertexIndex(8, 9, 12)},
            {16, new VertexIndex(9, 10, 12)},
            {17, new VertexIndex(10,11,12)},
            {18, new VertexIndex(11, 7, 12)},
            {19, new VertexIndex(8, 12, 7)},
        };
        public static Cartesian3D[] IcosahedronVertices;

        private static Cartesian3D _GetCentreCoordinate(int index)
        {
            var vertexIndices = CenterIndexToVertexIndicesMap[index];
            var hold_x = 0d;
            var hold_y = 0d;
            var hold_z = 0d;

            foreach (var idx in vertexIndices.Indices)
            {
                hold_x += IcosahedronVertices[idx].X;
                hold_y += IcosahedronVertices[idx].Y;
                hold_z += IcosahedronVertices[idx].Z;
            }

            hold_x /= vertexIndices.Indices.Count;
            hold_y /= vertexIndices.Indices.Count;
            hold_z /= vertexIndices.Indices.Count;

            var holdPoint = new Cartesian3D(hold_x, hold_y, hold_z);

            var magnitude = holdPoint.Magnitude();

            return new Cartesian3D(holdPoint.X / magnitude, holdPoint.Y / magnitude, holdPoint.Z / magnitude);
        }

        private static double[] IcosahedronVerticesX = new double[]
        {
            0.420152426708710003,
            0.995009439436241649 ,
            0.518836730327364437 ,
            -0.414682225320335218,
            -0.515455959944041808,
            0.355781402532944713 ,
            0.414682225320335218,
            0.515455959944041808,
            -0.355781402532944713,
            -0.995009439436241649,
            -0.518836730327364437,
            -0.420152426708710003
        };

        private static double[] IcosahedronVerticesY = new double[]
        {
            0.078145249402782959,
            -0.091347795276427931,
            0.835420380378235850,
            0.655962405434800777,
            -0.381716898287133011,
            -0.843580002466178147,
            -0.655962405434800777,
            0.381716898287133011,
            0.843580002466178147,
            0.091347795276427931,
            -0.835420380378235850,
            -0.078145249402782959
        };

        private static double[] IcosahedronVerticesZ = new double[]
        {
            0.904082550615019298,
            0.040147175877166645,
            0.181331837557262454,
            0.630675807891475371,
            0.767200992517747538,
            0.402234226602925571,
            -0.630675807891475371,
            -0.767200992517747538,
            -0.402234226602925571,
            -0.040147175877166645,
            -0.181331837557262454,
            -0.904082550615019298
        };

        public static Dictionary<int, int> TriangleIndexToFaceVertexMap = new Dictionary<int, int>
        {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 1 },
                { 6, 2 },
                { 7, 2 },
                { 8, 3 },
                { 9, 3 },
                { 10, 4 },
                { 11, 4 },
                { 12, 5 },
                { 13, 1 },
                { 14, 1 },
                { 15, 7 },
                { 16, 8 },
                { 17, 9 },
                { 18, 10 },
                { 19, 7 }
        };
    }
}
