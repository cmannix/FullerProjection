using System;

namespace FullerProjection.Common
{
    public static class DoubleExtensions
    {
        public static bool IsEqualTo(this double x, double y) => Math.Abs(x - y) < Tolerance;

        public static bool IsNotEqualTo(this double x, double y) => !x.IsEqualTo(y);

        public static bool IsGreaterThan(this double x, double y) => x.IsNotEqualTo(y) && x > y;
        
        public static bool IsLessThan(this double x, double y) => x.IsNotEqualTo(y) && x < y;

        private const double Tolerance = 0.000_000_000_1;
    }
}