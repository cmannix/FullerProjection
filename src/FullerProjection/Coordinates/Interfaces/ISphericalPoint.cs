using FullerProjection.Geometry;

namespace FullerProjection.Coordinates.Interfaces
{
    public interface ISphericalPoint
    {
         Angle Phi { get; }
         Angle Theta { get; }
         double R { get; }
    }
}