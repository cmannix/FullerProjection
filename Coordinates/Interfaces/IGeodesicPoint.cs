using FullerProjection.Geometry;

namespace FullerProjection.Coordinates.Interfaces
{
    public interface IGeodesicPoint
    {
        Angle Latitude { get; }
        Angle Longitude { get; }
    }
}