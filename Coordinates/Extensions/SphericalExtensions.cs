using FullerProjection.Coordinates.Interfaces;

namespace FullerProjection.Coordinates.Extensions
{
    public static class SphericalExtensions
    {
        public static ICartesianPoint ToCartesian(this ISphericalPoint point) => Conversion.CartestianFrom(point);
    }
}