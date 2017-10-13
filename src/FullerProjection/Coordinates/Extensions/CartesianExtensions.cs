using System;
using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;

namespace FullerProjection.Coordinates.Extensions
{
    public static class CartesianExtensions
    {
        public static ICartesianPoint Add(this ICartesianPoint a, ICartesianPoint b) => new Cartesian(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        
        public static ICartesianPoint Subtract(this ICartesianPoint a, ICartesianPoint b) => new Cartesian(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        
        public static ICartesianPoint Rotate(this ICartesianPoint point, Axis axis, Angle angle)
        {
            var a = point.X;
            var b = point.Y;
            var c = point.Z;

            switch (axis)
            {
                case Axis.X:
                    return new Cartesian(
                        x: point.X,
                        y: b * Math.Cos(angle.Radians) + c * Math.Sin(angle.Radians),
                        z: c * Math.Cos(angle.Radians) - b * Math.Sin(angle.Radians));
                case Axis.Y:
                    return new Cartesian(
                        x: a * Math.Cos(angle.Radians) - c * Math.Sin(angle.Radians),
                        y: point.Y,
                        z: a * Math.Sin(angle.Radians) + c * Math.Cos(angle.Radians));
                case Axis.Z:
                    return new Cartesian(
                        x: a * Math.Cos(angle.Radians) + b * Math.Sin(angle.Radians),
                        y: b * Math.Cos(angle.Radians) - a * Math.Sin(angle.Radians),
                        z: point.Z);
                default:
                    throw new ArgumentException("Axis must be X, Y, or Z");
            }
        }

        public static IGeodesicPoint ToGeodesic(this ICartesianPoint point) => Conversion.GeodesicFrom(point);
    }
}