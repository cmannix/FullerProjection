using System;

namespace FullerProjection.Geometry.Angles
{
    public static class AngleMath
    {
        public static double Sin(Angle angle) => Math.Sin(angle.Radians.Value);
        public static double Cos(Angle angle) => Math.Cos(angle.Radians.Value);
        public static double Tan(Angle angle) => Math.Tan(angle.Radians.Value);
    }
}