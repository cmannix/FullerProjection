using FullerProjection.Core.Common;
using FullerProjection.Core.Geometry.Angles;
using System;
using System.Diagnostics;
using static System.Math;
using static FullerProjection.Core.Geometry.Angles.AngleMath;

namespace FullerProjection.Core.Geometry.Coordinates
{
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}")]
    public class Cartesian3D : ICoordinate, IEquatable<Cartesian3D>
    {
        public Cartesian3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Double Magnitude() => Sqrt(Pow(X, 2) + Pow(Y, 2) + Pow(Z, 2));
        public Cartesian3D Divide(double value) => new Cartesian3D(x: X / value, y: Y / value, z: Z / value );
        public Cartesian3D RotateX(Angle angle) => new Cartesian3D(
                        x: X,
                        y: Y * Cos(angle) - Z * Sin(angle),
                        z: Z * Cos(angle) + Y * Sin(angle));

        public Cartesian3D RotateY(Angle angle) => new Cartesian3D(
                        x: X * Cos(angle) + Z * Sin(angle),
                        y: Y,
                        z: Z * Cos(angle) - X * Sin(angle));

        public Cartesian3D RotateZ(Angle angle) => new Cartesian3D(
                        x: X * Cos(angle) - Y * Sin(angle),
                        y: Y * Cos(angle) + X * Sin(angle),
                        z: Z);

        public static Cartesian3D operator +(Cartesian3D a, Cartesian3D b) => new Cartesian3D(x: a.X + b.X, y: a.Y + b.Y, z: a.Z + b.Z);
        public static Cartesian3D operator -(Cartesian3D a, Cartesian3D b) => new Cartesian3D(x: a.X - b.X, y: a.Y - b.Y, z: a.Z - b.Z);
        public static bool operator ==(Cartesian3D value1, Cartesian3D value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Cartesian3D value1, Cartesian3D value2) => !(value1 == value2);

        public bool Equals(Cartesian3D? other) => other is object && this.X.IsEqualTo(other.X) && this.Y.IsEqualTo(other.Y) && this.Z.IsEqualTo(other.Z);

        public override bool Equals(System.Object? obj) => obj is Cartesian3D d && this.Equals(d);

        public override int GetHashCode() => this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();

        public override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}";
    }
}
