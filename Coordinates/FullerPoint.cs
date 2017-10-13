using FullerProjection.Geometry;
using static System.Math;

namespace FullerProjection.Coordinates
{
    public class FullerPoint
    {
        public FullerPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get;  }
        public double Y { get;  }

        public static FullerPoint Rotate(FullerPoint point, Angle angle)
        {
            return new FullerPoint(
                x: point.X * Cos(angle.Radians) - point.Y * Sin(angle.Radians),
                y: point.X * Sin(angle.Radians) + point.Y * Cos(angle.Radians));
        }
    }
}
