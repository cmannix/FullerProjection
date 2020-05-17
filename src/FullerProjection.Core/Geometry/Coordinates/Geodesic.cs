using FullerProjection.Core.Geometry.Angles;
using System;

namespace FullerProjection.Core.Geometry.Coordinates
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
            var value = Angle.From(Degrees.FromRaw(candidateValue.Degrees.Value % 360));
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

        public static bool operator ==(Geodesic value1, Geodesic value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Geodesic value1, Geodesic value2) => !(value1 == value2);

        public bool Equals(Geodesic? other) => other is object && this.Latitude == other.Latitude && this.Longitude == other.Longitude;

        public override bool Equals(System.Object? obj) => obj is Geodesic s && this.Equals(s);

        public override int GetHashCode() => this.Latitude.GetHashCode() + this.Longitude.GetHashCode();

        public override string ToString() => $"Latitude: {Latitude}, Longitude: {Longitude}";
    }
}
