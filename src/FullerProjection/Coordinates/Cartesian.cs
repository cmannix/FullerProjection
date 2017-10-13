using FullerProjection.Geometry;
using System;
using FullerProjection.Coordinates.Interfaces;
using static System.Math;

namespace FullerProjection.Coordinates
{
    public class Cartesian : ICartesianPoint
    {
        public Cartesian(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
    }
}
