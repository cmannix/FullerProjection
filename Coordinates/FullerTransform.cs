using FullerProjection.Coordinates;
using FullerProjection.Geometry;
using System;

namespace FullerProjection.Coordinates
{
    public class FullerTransform2d
    {
        public FullerTransform2d(Angle rotationAngle, Func<double, double> xTransform, Func<double, double> yTransform)
        {
            RotationAngle = rotationAngle;
            XTransform = xTransform;
            YTransform = yTransform;
        }
        Angle RotationAngle { get; }
        Func<double, double> XTransform { get; }
        Func<double, double> YTransform { get; }

        public FullerPoint Apply(FullerPoint point)
        {
            var rotatedPoint = FullerPoint.Rotate(point, RotationAngle);
            var newX = XTransform(rotatedPoint.X);
            var newY = YTransform(rotatedPoint.Y);
            return new FullerPoint(newX, newY);
        }
    }
}
