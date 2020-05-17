using FullerProjection.Geometry.Angles;
using System;

namespace FullerProjection.Geometry.Coordinates
{
    public class Geodesic : ICoordinate
    {
        public Geodesic(Angle latitude, Angle longitude)
        {
            this.Latitude = EnsureLatitude(latitude);
            this.Longitude = EnsureLongitude(longitude);
        }

        public Angle Latitude { get; }

        public Angle Longitude { get; }

        private Angle EnsureLongitude(Angle candidateValue)
        {
            var value = Angle.From(new Degrees(candidateValue.Degrees.Value % 360));
            if (value.Degrees.Value < 0) value += Angle.From(Degrees.ThreeSixty);

            return value;
        }

        private Angle EnsureLatitude(Angle candidateValue)
        {
            if (candidateValue.Degrees.Value < LatitudeLowerBound.Degrees.Value || candidateValue.Degrees.Value > LatitudeUpperBound.Degrees.Value)
            {
                throw new ArgumentException("Longitude must be between -90 and 90 degrees");
            }
            return candidateValue;
        }

        private static Angle LatitudeLowerBound = Angle.From(Degrees.MinusNinety);
        private static Angle LatitudeUpperBound = Angle.From(Degrees.Ninety);

        public override string ToString() => $"Latitude: {Latitude.Degrees} degrees, Longitude: {Longitude.Degrees} degrees";
    }
}
