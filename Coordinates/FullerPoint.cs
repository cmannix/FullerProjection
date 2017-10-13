using FullerProjection.Coordinates.Interfaces;

namespace FullerProjection.Coordinates
{
    public class FullerPoint : IFullerPoint
    {
        public FullerPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get;  }
        public double Y { get;  }
    }
}
