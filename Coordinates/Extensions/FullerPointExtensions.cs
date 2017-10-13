using System;
using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;

namespace FullerProjection.Coordinates.Extensions
{
    public static class FullerPointExtensions
    {
        public static IFullerPoint Rotate(this IFullerPoint point, Angle angle)
        {
            return new FullerPoint(
                x: point.X * Math.Cos(angle.Radians) - point.Y * Math.Sin(angle.Radians),
                y: point.X * Math.Sin(angle.Radians) + point.Y * Math.Cos(angle.Radians));
        }

        public static IFullerPoint ApplyTransform(this IFullerPoint point, FullerTransform2D transform)
        {
            var rotatedPoint = point.Rotate(transform.RotationAngle);
            var newX = transform.XTransform(rotatedPoint.X);
            var newY = transform.YTransform(rotatedPoint.Y);
            return new FullerPoint(newX, newY);
        }
    }
}