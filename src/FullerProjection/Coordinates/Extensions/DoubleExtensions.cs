using System;

namespace FullerProjection.Coordinates.Extensions
{
    public static class DoubleExtensions
    {
        public static bool Is(this double x, double y) => Math.Abs(x - y) < Tolerance;

        public static bool IsNot(this double x, double y) => !x.Is(y);

        public static bool IsGreaterThan(this double x, double y) => x.IsNot(y) && x > y;
        
        public static bool IsLessThan(this double x, double y) => x.IsNot(y) && x < y;

        private const double Tolerance = 0.000_000_000_1;
    }
}