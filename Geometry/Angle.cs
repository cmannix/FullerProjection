using System;
using System.Collections.Generic;
using System.Text;

namespace FullerProjection.Geometry
{
    public class Angle
    {
        private Angle(double angle, Unit unit)
        {
            switch (unit)
            {
                case Unit.Degrees:
                    this.Degrees = angle;
                    this.Radians = ToRadians(angle);
                    break;
                case Unit.Radians:
                    this.Radians = angle;
                    this.Degrees = ToDegrees(angle);
                    break;
                default:
                    throw new ArgumentException("Base unit is not recognized");
            }
        }

        
        public double Degrees { get; }

        public double Radians { get; }

        private const double TransformFactor = Math.PI / 180d;
        private static double ToRadians(double degrees)
        {
            return (TransformFactor * degrees);
        }
        private static double ToDegrees(double radians)
        {
            return radians / TransformFactor;
        }

        public static Angle FromDegrees(double angleInDegrees)
        {
            return new Angle(angleInDegrees, Unit.Degrees);
        }

        public static Angle FromRadians(double angleInRadians)
        {
            return new Angle(angleInRadians, Unit.Radians);
        }

        public Angle Subtract(Angle angle)
        {
            return FromDegrees(this.Degrees - angle.Degrees);
        }

        public static Angle operator +(Angle a1, Angle a2) => FromDegrees(a1.Degrees + a2.Degrees);
        public static Angle operator -(Angle a1, Angle a2) => FromDegrees(a1.Degrees - a2.Degrees);
    }
}
