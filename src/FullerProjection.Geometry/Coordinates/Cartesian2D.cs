using System;
using System.Diagnostics;
using static System.Math;
using FullerProjection.Common;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Geometry.Coordinates
{
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public class Cartesian2D : ICoordinate, IEquatable<Cartesian2D>
    {
        public Cartesian2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; }
        public double Y { get; }

        public Cartesian2D Rotate(Angle angle) => new Cartesian2D(
                x: X * Cos(angle.Radians.Value) - Y * Sin(angle.Radians.Value),
                y: X * Sin(angle.Radians.Value) + Y * Cos(angle.Radians.Value));

        public Cartesian2D TransformX(double value) => new Cartesian2D(
            x: X + value,
            y: Y
        );

        public Cartesian2D TransformY(double value) => new Cartesian2D(
            x: X,
            y: Y + value
        );

        public static bool operator ==(Cartesian2D value1, Cartesian2D value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Cartesian2D value1, Cartesian2D value2) => !(value1 == value2);

        public bool Equals(Cartesian2D? other) => other is object && this.X.IsEqualTo(other.X) && this.Y.IsEqualTo(other.Y);

        public override bool Equals(System.Object? obj) => obj is Cartesian2D d && this.Equals(d);

        public override int GetHashCode() => this.X.GetHashCode() + this.Y.GetHashCode();

        public override string ToString() => $"X: {X}, Y: {Y}";
    }
}
