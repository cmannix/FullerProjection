using System;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Geometry.Coordinates.Extensions
{
    public static class FullerPointExtensions
    {
        public static Cartesian2D ApplyTransform(this Cartesian2D point, FullerTransform2D transform)
        {
            return point.Rotate(transform.RotationAngle).TransformX(transform.XTransform).TransformY(transform.YTransform);
        }
    }
}