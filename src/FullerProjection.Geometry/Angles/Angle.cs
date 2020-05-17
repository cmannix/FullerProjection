using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using static System.Math;
using static FullerProjection.Geometry.Angles.AngleMath;

namespace FullerProjection.Geometry.Angles
{
    [DebuggerDisplay("Degrees: {Degrees}, Radians: {Radians}")]
    public class Angle : IEquatable<Angle>
    {
        private const double TransformFactor = PI / 180d;
        private Angle(Degrees d)
        {
            this.Degrees = d;
            this.Radians = Radians.FromRaw(d.Value * TransformFactor);
        }

        private Angle(Radians r)
        {
            this.Radians = r;
            this.Degrees = Degrees.FromRaw(r.Value / TransformFactor);
        }
        public Degrees Degrees { get; }

        public Radians Radians { get; }

        public static Angle From(IAngleUnit angle) => angle switch
        {
            Radians r => new Angle(r),
            Degrees d => new Angle(d),
            _ => throw new ArgumentException(
                    message: "nameof(angle) is not a recognized angle representation",
                    paramName: nameof(angle))
        };
        public static Angle operator +(Angle a1, Angle a2) => new Angle(a1.Degrees + a2.Degrees);
        public static Angle operator -(Angle a1, Angle a2) => new Angle(a1.Degrees - a2.Degrees);
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
    }
}
