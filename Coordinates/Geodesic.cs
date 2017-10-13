using FullerProjection.Geometry;
using static System.Math;

namespace FullerProjection.Coordinates
{
    public class Geodesic
    {
        public Geodesic(Angle latitude, Angle longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public static Geodesic FromCartesian(Cartesian point)
        {
            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            var latitude = Angle.FromRadians(Acos(z));
            Angle longitude = Angle.FromDegrees(0);

            if (x == 0.0 && y > 0.0) { longitude = Angle.FromDegrees(90.0); }
            if (x == 0.0 && y < 0.0) { longitude = Angle.FromDegrees(270.0); }
            if (x > 0.0 && y == 0.0) { longitude = Angle.FromDegrees(0.0); }
            if (x < 0.0 && y == 0.0) { longitude = Angle.FromDegrees(180.0); }
            if (x != 0.0 && y != 0.0)
            {
                var a = Angle.FromDegrees(0);
                if (x > 0.0 && y > 0.0) { a = Angle.FromDegrees(0.0); }
                if (x < 0.0 && y > 0.0) { a = Angle.FromDegrees(180.0); }
                if (x < 0.0 && y < 0.0) { a = Angle.FromDegrees(180.0); }
                if (x > 0.0 && y < 0.0) { a = Angle.FromDegrees(360.0); }
                longitude = Angle.FromRadians(Atan(y / x) + a.Radians);
            }

            return new Geodesic(latitude, longitude);
        }
        public Angle Latitude { get; set; }
        public Angle Longitude
        {
            get => this._longitude;
            set
            {
                if (value.Degrees > 360.0) value -= Angle.FromDegrees(360);
                if (value.Degrees < 0.0) value += Angle.FromDegrees(360);
                this._longitude = value;
            }
        }

        private Angle _longitude;
    }
}
