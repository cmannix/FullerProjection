using System;
using static System.Math;
using FullerProjection.Common;

namespace FullerProjection.Geometry.Angles
{
    public class Radians : IAngleUnit, IEquatable<Radians>
    {
        public static Radians Pi = Radians.FromRaw(Math.PI);
        public static Radians TwoPi = Radians.FromRaw(2 * Math.PI);
        public static Radians HalfPi = Radians.FromRaw(Math.PI / 2);

        public double Value { get; }
        private Radians(double value)
        {
            Value = value;
        }
        public static Radians FromRaw(double value) => new Radians(value);

        public static Radians operator +(Radians a, Radians b) => Radians.FromRaw(a.Value + b.Value);
        public static Radians operator -(Radians a, Radians b) => Radians.FromRaw(a.Value - b.Value);
        public static bool operator ==(Radians value1, Radians value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Radians value1, Radians value2) => !(value1 == value2);

        public bool Equals(Radians? other) => other is object && this.Value.IsEqualTo(other.Value);

        public override bool Equals(System.Object? obj) => obj is Radians d && this.Equals(d);

        public override int GetHashCode() => this.Value.GetHashCode();
    }
}