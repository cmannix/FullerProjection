using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using static System.Math;
using static FullerProjection.Core.Geometry.Angles.AngleMath;

namespace FullerProjection.Core.Geometry.Angles
{
    [DebuggerDisplay("Degrees: {Degrees}, Radians: {Radians}")]
    public class Angle : IEquatable<Angle>, IComparable<Angle>
    {
        private const double TransformFactor = PI / 180d;
        private Angle(Degrees d)
        {
            this.Degrees = d;
        }
        public Degrees Degrees { get; }
        public Radians Radians => Radians.FromRaw(this.Degrees.Value * TransformFactor);
        public static Angle From(Degrees d) => new Angle(d);
        public static Angle From(Radians r) => new Angle(Degrees.FromRaw(r.Value / TransformFactor));
        public static Angle operator +(Angle a1, Angle a2) => new Angle(a1.Degrees + a2.Degrees);
        public static Angle operator -(Angle a1, Angle a2) => new Angle(a1.Degrees - a2.Degrees);
        public static Angle operator %(Angle a, Angle b) => new Angle(a.Degrees % b.Degrees);
        public int CompareTo(Angle other) => this.Degrees.CompareTo(other.Degrees);
        public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
        public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
        public static bool operator ==(Angle value1, Angle value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Angle value1, Angle value2) => !(value1 == value2);

        public bool Equals(Angle? other) => other is object && this.Degrees == other.Degrees;

        public override bool Equals(System.Object? obj) => obj is Degrees d && this.Equals(d);

        public override int GetHashCode() => this.Degrees.GetHashCode();

        public override string ToString() => $"{Degrees}, {Radians}";
    }
}
