using System;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Geometry.Coordinates.Extensions
{
    public static class CartesianExtensions
    {
        public static Geodesic ToGeodesic(this Cartesian3D point) => Conversion.GeodesicFrom(point);
    }
}