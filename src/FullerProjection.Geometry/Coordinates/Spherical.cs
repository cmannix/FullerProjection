using System;
using System.Diagnostics;
using FullerProjection.Geometry.Angles;

namespace FullerProjection.Geometry.Coordinates
{
    [DebuggerDisplay("Phi: {Phi}, Theta: {Theta}, R: {R}")]
    public class Spherical : ICoordinate
    {
        public Spherical(Angle phi, Angle theta, double r = 1)
        {
            this.Phi = EnsurePhi(phi);
            this.Theta = EnsureTheta(theta);
            this.R = EnsureR(r);
        }

        public Angle Phi { get; }
        public Angle Theta { get; }
        public double R { get; }

        private Angle EnsurePhi(Angle candidateValue)
        {
            var value = Angle.FromDegrees(new Degrees(candidateValue.Degrees.Value % 360));
            if (value.Degrees.Value < 0) value += Angle.FromDegrees(Degrees.ThreeSixty);

            return value;
        }

        private Angle EnsureTheta(Angle candidateValue)
        {
            var value = Angle.FromDegrees(new Degrees(candidateValue.Degrees.Value % 180));
            if (value.Degrees.Value < 0) value += Angle.FromDegrees(Degrees.OneEighty);

            return value;
        }

        private double EnsureR(double candidateValue)
        {
            if (candidateValue < 0) throw new ArgumentException($"r must be positive");
            return candidateValue;
        }
        public override string ToString() => $"Phi: {Phi.Degrees} degrees, Theta: {Theta.Degrees} degrees, R: {R}";
    }
}
