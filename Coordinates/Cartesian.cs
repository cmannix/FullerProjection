using FullerProjection.Geometry;
using System;
using static System.Math;

namespace FullerProjection.Coordinates
{
    public class Cartesian
    {
        public Cartesian(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public static Cartesian FromSpherical(Spherical point)
        {
            var x = Sin(point.Theta.Radians) * Cos(point.Phi.Radians);
            var y = Sin(point.Theta.Radians) * Sin(point.Phi.Radians);
            var z = Cos(point.Theta.Radians);

            return new Cartesian(x, y, z);
        }

        public static Cartesian Rotate(Cartesian point, Axis axis, Angle angle)
        {
            var a = point.X;
            var b = point.Y;
            var c = point.Z;

            switch (axis)
            {
                case Axis.X:
                    return new Cartesian(point.X,
                    b * Cos(angle.Radians) + c * Sin(angle.Radians),
                    c * Cos(angle.Radians) - b * Sin(angle.Radians));
                case Axis.Y:
                    return new Cartesian(a * Cos(angle.Radians) - c * Sin(angle.Radians),
                    point.Y,
                    a * Sin(angle.Radians) + c * Cos(angle.Radians));
                case Axis.Z:
                    return new Cartesian(a * Cos(angle.Radians) + b * Sin(angle.Radians),
                    b * Cos(angle.Radians) - a * Sin(angle.Radians),
                    point.Z);
                default:
                    throw new ArgumentException("Axis must be X, Y, or Z");
            }
        }

        public static Cartesian operator -(Cartesian a, Cartesian b)
        {
            return new Cartesian(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }


        public double X { get; }
        public double Y { get; }
        public double Z { get; }
    }
}
