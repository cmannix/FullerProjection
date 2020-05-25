using FullerProjection.Core.Geometry.Coordinates;
using System;
using System.Collections.Generic;

namespace FullerProjection.Core
{
    public static class FullerIcosahedron
    {
        static FullerIcosahedron()
        {
            // Initialise in constructor to ensure vertices are available.
            Faces = Array.AsReadOnly(new Face[]
        {
            new Face(IcosahedronVertices[0], IcosahedronVertices[1], IcosahedronVertices[2]),
            new Face(IcosahedronVertices[0], IcosahedronVertices[2], IcosahedronVertices[3]),
            new Face(IcosahedronVertices[0], IcosahedronVertices[3], IcosahedronVertices[4]),
            new Face(IcosahedronVertices[0], IcosahedronVertices[4], IcosahedronVertices[5]),
            new Face(IcosahedronVertices[0], IcosahedronVertices[1], IcosahedronVertices[5]),
            new Face(IcosahedronVertices[1], IcosahedronVertices[2], IcosahedronVertices[7]),
            new Face(IcosahedronVertices[7], IcosahedronVertices[2], IcosahedronVertices[8]),
            new Face(IcosahedronVertices[8], IcosahedronVertices[2], IcosahedronVertices[3]),
            new Face(IcosahedronVertices[9], IcosahedronVertices[8], IcosahedronVertices[3]),
            new Face(IcosahedronVertices[4], IcosahedronVertices[9], IcosahedronVertices[3]),
            new Face(IcosahedronVertices[4], IcosahedronVertices[10], IcosahedronVertices[9]),
            new Face(IcosahedronVertices[4], IcosahedronVertices[5], IcosahedronVertices[10]),
            new Face(IcosahedronVertices[10], IcosahedronVertices[5], IcosahedronVertices[6]),
            new Face(IcosahedronVertices[6], IcosahedronVertices[5], IcosahedronVertices[1]),
            new Face(IcosahedronVertices[5], IcosahedronVertices[6], IcosahedronVertices[1]),
            new Face(IcosahedronVertices[11], IcosahedronVertices[8], IcosahedronVertices[7]),
            new Face(IcosahedronVertices[11], IcosahedronVertices[8], IcosahedronVertices[9]),
            new Face(IcosahedronVertices[11], IcosahedronVertices[10], IcosahedronVertices[9]),
            new Face(IcosahedronVertices[11], IcosahedronVertices[10], IcosahedronVertices[6]),
            new Face(IcosahedronVertices[11], IcosahedronVertices[7], IcosahedronVertices[6]),
        });
        }

        public const int FaceCount = 20;
        public static IReadOnlyList<Face> Faces { get; }

        /// <summary> Cartesian coordinates for the 12 vertices of the Icosahedron </summary>
        private static readonly Cartesian3D[] IcosahedronVertices = new Cartesian3D[]
        {
            new Cartesian3D(x: 0.42015242670871, y: 0.07814524940278296, z: 0.9040825506150193),
            new Cartesian3D(x: 0.9950094394362416, y: -0.09134779527642793, z: 0.040147175877166645),
            new Cartesian3D(x: 0.5188367303273644, y: 0.8354203803782358, z: 0.18133183755726245),
            new Cartesian3D(x: -0.4146822253203352, y: 0.6559624054348008, z: 0.6306758078914754),
            new Cartesian3D(x: -0.5154559599440418, y: -0.381716898287133, z: 0.7672009925177475),
            new Cartesian3D(x: 0.3557814025329447, y: -0.8435800024661781, z: 0.40223422660292557),
            new Cartesian3D(x: 0.4146822253203352, y: -0.6559624054348008, z: -0.6306758078914754),
            new Cartesian3D(x: 0.5154559599440418, y: 0.381716898287133, z: -0.7672009925177475),
            new Cartesian3D(x: -0.3557814025329447, y: 0.8435800024661781, z: -0.40223422660292557),
            new Cartesian3D(x: -0.9950094394362416, y: 0.09134779527642793, z: -0.040147175877166645),
            new Cartesian3D(x: -0.5188367303273644, y: -0.8354203803782358, z: -0.18133183755726245),
            new Cartesian3D(x: -0.42015242670871, y: -0.07814524940278296, z: -0.9040825506150193)

        };

        public class Face
        {
            public Face(Cartesian3D a, Cartesian3D b, Cartesian3D c)
            {
                A = a;
                B = b;
                C = c;
                var unscaledCentroid = (A + B + C).Divide(3);
                Centroid = unscaledCentroid.Divide(unscaledCentroid.Magnitude());
            }
            public Cartesian3D Centroid { get; }
            public Cartesian3D A { get; }
            public Cartesian3D B { get; }
            public Cartesian3D C { get; }
        }
    }
}
