using System;
using System.Diagnostics;
using FullerProjection.Core.Geometry.Angles;
using FullerProjection.Core.Common;

namespace FullerProjection.Core.Geometry.Coordinates
{
    [DebuggerDisplay("Phi: {Phi}, Theta: {Theta}, R: {R}")]
    public class Spherical : ICoordinate, IEquatable<Spherical>
    {
        /// <summary>
        /// Spherical (polar) coordinates - Phi, Theta and R.
        /// <para>
        ///   <para>
        ///   Phi: Azimuthal angle (Longitude)
        ///   </para>
        ///   <para>
        ///   Theta: Polar angle (Latitude)
        ///   </para>
        /// </para>
        /// </summary>
        public Spherical(Angle phi, Angle theta, double r = 1)
        {
            this.Phi = EnsurePhi(phi);
            this.Theta = EnsureTheta(theta);
            this.R = EnsureR(r);
        }

        /// <value>The azimuthal angle in the xy-plane from the x axis. 0 <= phi <= 2pi</value>
        public Angle Phi { get; }

        /// <value>The polar angle from the z axis. 0 <= theta <= pi. (Latitude)</value>
        public Angle Theta { get; }

        /// <value>The distance (radius) from the origin to the point</value>
        public double R { get; }

        private Angle EnsurePhi(Angle candidateValue)
        {
            var value = Angle.From(Degrees.FromRaw(candidateValue.Degrees.Value % 360));
            if (value.Degrees.Value.IsLessThan(0)) value += Angle.From(Degrees.ThreeSixty);

            return value;
        }

        private Angle EnsureTheta(Angle candidateValue)
        {
            var value = Angle.From(Degrees.FromRaw(candidateValue.Degrees.Value % 180));
            if (value.Degrees.Value.IsLessThan(0)) value += Angle.From(Degrees.OneEighty);

            return value;
        }

        private double EnsureR(double candidateValue)
        {
            if (candidateValue < 0) throw new ArgumentException($"r must be positive");
            return candidateValue;
        }

        public static bool operator ==(Spherical value1, Spherical value2)
        {
            if (value1 is null || value2 is null)
            {
                return System.Object.Equals(value1, value2);
            }

            return value1.Equals(value2);
        }
        public static bool operator !=(Spherical value1, Spherical value2) => !(value1 == value2);

        public bool Equals(Spherical? other) => other is object && this.Phi == other.Phi && this.Theta == other.Theta && this.R == other.R;

        public override bool Equals(System.Object? obj) => obj is Spherical s && this.Equals(s);

        public override int GetHashCode() => this.Phi.GetHashCode() + this.Theta.GetHashCode() + this.R.GetHashCode();

        public override string ToString() => $"Phi: {Phi}, Theta: {Theta}, R: {R}";
    }
}
