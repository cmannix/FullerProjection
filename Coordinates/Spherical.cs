using FullerProjection.Geometry;

namespace FullerProjection.Coordinates
{
    public class Spherical
    {
        public Spherical(Angle phi, Angle theta)
        {
            this.Phi = phi;
            this.Theta = theta;
        }

        public static Spherical FromGeodesic(Geodesic point)
        {
            var theta = Angle.FromDegrees(90.0) - point.Latitude;

            var phi = point.Longitude;

            return new Spherical(phi, theta);
        }

        public Angle Phi { get; }
        public Angle Theta { get; }
        public double R { get; } = 1;
    }
}
