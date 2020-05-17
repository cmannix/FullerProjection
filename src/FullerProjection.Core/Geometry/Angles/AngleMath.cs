using System;

namespace FullerProjection.Core.Geometry.Angles
{
    public static class AngleMath
    {
        public static double Sin(Angle angle) => Math.Sin(angle.Radians.Value);
        public static double Cos(Angle angle) => Math.Cos(angle.Radians.Value);
        public static double Tan(Angle angle) => Math.Tan(angle.Radians.Value);
        public static Angle Atan(double value) => Angle.From(Radians.FromRaw(Math.Atan(value)));
        public static Angle Atan2(double y, double x) => Angle.From(Radians.FromRaw(Math.Atan2(y, x)));
        public static Angle Acos(double value) => Angle.From(Radians.FromRaw(Math.Acos(value)));
    }
}