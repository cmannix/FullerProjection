using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;

namespace FullerProjection.Coordinates.Extensions
{
    public static class GeodesicExtensions
    {
        public static IGeodesicPoint WithLongitude(this IGeodesicPoint point, Angle longitude) => new Geodesic(
            latitude: point.Latitude,
            longitude: longitude);
        
        public static ISphericalPoint ToSpherical(this IGeodesicPoint point) => Conversion.SphericalFrom(point);
    }
}