
namespace FullerProjection.Geometry.Coordinates.Extensions
{
    public static class SphericalExtensions
    {
        public static Cartesian3D ToCartesian(this Spherical point) => Conversion.CartestianFrom(point);
    }
}