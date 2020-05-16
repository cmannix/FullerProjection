using FullerProjection.Geometry.Angles;
using System;
using FullerProjection.Geometry.Coordinates.Extensions;

namespace FullerProjection.Geometry.Coordinates
{
    public class FullerTransform2D
    {
        public FullerTransform2D(Angle rotationAngle, double transformX, double transformY)
        {
            RotationAngle = rotationAngle;
            XTransform = transformX;
            YTransform = transformY;
        }
        public Angle RotationAngle { get; }
        public double XTransform { get; }
        public double YTransform { get; }
    }
}
