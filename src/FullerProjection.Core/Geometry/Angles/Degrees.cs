using System;
using FullerProjection.Core.Common;

namespace FullerProjection.Core.Geometry.Angles
{
    public class Degrees : IAngleUnit, IEquatable<Degrees>
    {
        public static Degrees Zero = FromRaw(0);
        public static Degrees Ninety = FromRaw(90);
        public static Degrees OneEighty = FromRaw(180);
        public static Degrees TwoSeventy = FromRaw(270);
        public static Degrees ThreeSixty = FromRaw(360);
        public static Degrees MinusNinety = FromRaw(-90);

        public double Value { get; }
        private Degrees(double value)
        {
            Value = value;
        }
        public static Degrees FromRaw(double value) => new Degrees(value);
        public static Degrees operator +(Degrees a, Degrees b) => FromRaw(a.Value + b.Value);
        public static Degrees operator -(Degrees a, Degrees b) => FromRaw(a.Value - b.Value);
        public static bool operator ==(Degrees value1, Degrees value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Degrees value1, Degrees value2) => !(value1 == value2);

        public bool Equals(Degrees? other) => other is object && this.Value.IsEqualTo(other.Value);

        public override bool Equals(System.Object? obj) => obj is Degrees d && this.Equals(d);

        public override int GetHashCode() => this.Value.GetHashCode();

        public override string ToString() => $"{Value} degrees";
    }
}