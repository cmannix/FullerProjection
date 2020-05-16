using FullerProjection.Geometry.Angles;

namespace FullerProjection.Geometry.Coordinates.Extensions
{
    public static class GeodesicExtensions
    {
        public static Geodesic WithLongitude(this Geodesic point, Angle longitude) => new Geodesic(
            latitude: point.Latitude,
            longitude: longitude);
        
        public static Spherical ToSpherical(this Geodesic point) => Conversion.SphericalFrom(point);
        public static Cartesian3D ToCartesian(this Geodesic point) => Conversion.CartesianFrom(point);
    }
}