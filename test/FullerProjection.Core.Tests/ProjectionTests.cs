using System;
using Xunit;
using static FullerProjection.Core.FullerProjection;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;

namespace FullerProjection.UnitTests.Core
{
    public class ProjectionTests
    {
        [Theory]
        [InlineData(1.9197353297395328, 1.7650430126220475, 1.6731232934057263, 2.7402556686364163)]
        [InlineData(1.9239508708615556, 1.7682793474832617, 1.673212149772711, 2.7402718623985605)]
        [InlineData(1.9240604247081798, 1.7683482975038836, 1.6732142703958879, 2.740272422904502)]
        [InlineData(3.955068897917265, 1.9194547340412926, 1.6987314546364436, 2.7610661040272078)]
        [InlineData(3.971420332610222, 1.9168984875155828, 1.6988916028046115, 2.7612689072668677)]
        [InlineData(3.9549913830747796, 1.919345313526879, 1.6987291768839385, 2.7610662796050955)]
        [InlineData(1.4190731865452855, 2.285594029005955, 1.6738161796979933, 2.730013663510176)]
        [InlineData(1.4097791907369905, 2.281032339745036, 1.673653071701723, 2.729954453332398)]
        [InlineData(1.3926558517240109, 2.2836921421006275, 1.6734892499452931, 2.729743837619109)]
        public void Calculates_dymaxion_points_from_geodesic_correctly(double longitude, double latitude, double expectedX, double expectedY)
        {
            var point = new Geodesic(Angle.From(Degrees.FromRaw(latitude)), Angle.From(Degrees.FromRaw(longitude)));

            var result = GetFullerPoint(point);

            Assert.Equal(new Cartesian2D(expectedX, expectedY), result);
        }
    }
}
