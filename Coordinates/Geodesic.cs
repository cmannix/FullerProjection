using FullerProjection.Coordinates.Interfaces;
using FullerProjection.Geometry;
using static System.Math;

namespace FullerProjection.Coordinates
{
    public class Geodesic : IGeodesicPoint
    {
        public Geodesic(Angle latitude, Angle longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public Angle Latitude { get; }
        public Angle Longitude
        {
            get => this._longitude;
            private set
            {
                if (value.Degrees > 360.0) value -= Angle.FromDegrees(360);
                if (value.Degrees < 0.0) value += Angle.FromDegrees(360);
                this._longitude = value;
            }
        }

        private Angle _longitude;
    }
}
