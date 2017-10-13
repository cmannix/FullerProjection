using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;

namespace FullerProjection.Coordinates
{
    public class Spherical : ISphericalPoint
    {
        public Spherical(Angle phi, Angle theta)
        {
            this.Phi = phi;
            this.Theta = theta;
        }

        public Angle Phi { get; }
        public Angle Theta { get; }
        public double R { get; } = 1;
    }
}
