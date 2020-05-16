using System;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Geometry.Coordinates.Extensions
{
    public static class FullerPointExtensions
    {
        public static Cartesian2D ApplyTransform(this Cartesian2D point, FullerTransform2D transform)
        {
            var rotatedPoint = point.Rotate(transform.RotationAngle);
            var newX = transform.XTransform(rotatedPoint.X);
            var newY = transform.YTransform(rotatedPoint.Y);
            return new Cartesian2D(newX, newY);
        }
    }
}