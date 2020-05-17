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
        private Angle(IAngleUnit angle)
        {
            switch (angle) {
                case Radians r:
                    this.Radians = r;
                    this.Degrees = ToDegrees(r);
                    break;
                case Degrees d:
                    this.Degrees = d;
                    this.Radians = ToRadians(d);
                    break;
                default:
                    throw new ArgumentException(
                        message: "nameof(angle) is not a recognized angle representation",
                        paramName: nameof(angle));
            }
        }
        public Degrees Degrees { get; }

        public Radians Radians { get; }

        private const double TransformFactor = PI / 180d;
        private static Radians ToRadians(Degrees degrees)
        {
            return new Radians(TransformFactor * degrees.Value);
        }
        private static Degrees ToDegrees(Radians radians)
        {
            return new Degrees(radians.Value / TransformFactor);
        }

        public static Angle FromDegrees(Degrees degrees)
        {
            return new Angle(degrees);
        }

        public static Angle FromRadians(Radians radians)
        {
            return new Angle(radians);
        }

        public static Angle operator +(Angle a1, Angle a2) => FromDegrees(a1.Degrees + a2.Degrees);
        public static Angle operator -(Angle a1, Angle a2) => FromDegrees(a1.Degrees - a2.Degrees);
        public static bool operator ==(Angle value1, Angle value2) 
        { 
            if (value1 is null || value2 is null) {
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
