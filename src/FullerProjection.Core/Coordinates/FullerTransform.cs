using FullerProjection.Geometry.Angles;
using System;
using FullerProjection.Geometry.Coordinates.Extensions;

namespace FullerProjection.Geometry.Coordinates
{
    public class FullerTransform2D
    {
        public FullerTransform2D(Angle rotationAngle, Func<double, double> xTransform, Func<double, double> yTransform)
        {
            RotationAngle = rotationAngle;
            XTransform = xTransform;
            YTransform = yTransform;
        }

        public Angle RotationAngle { get; }
        public Func<double, double> XTransform { get; }
        public Func<double, double> YTransform { get; }
    }
}
