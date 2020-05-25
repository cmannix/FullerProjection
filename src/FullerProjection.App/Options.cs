using CommandLine;
using FullerProjection.Core.Geometry.Angles;
using FullerProjection.Core.Geometry.Coordinates;

public interface IFileOptions
{
    [Option('f', "file", HelpText = "Path to input file", SetName = "file")]
    string? InputFilePath { get; set; }

    [Option('o', "output", HelpText = "Path to output file", SetName = "file")]
    string? OutputFilePath { get; set; }
}

public interface IConsoleOptions
{
    [Option("latitude", HelpText = "Latitude (degrees)", SetName = "console")]
    double? LatitudeDegrees { get; set; }

    [Option("longitude", HelpText = "Longitude (degrees)", SetName = "console")]
    double? LongitudeDegrees { get; set; }
}

public class Options : IFileOptions, IConsoleOptions
{
    public string? InputFilePath { get; set; }
    public string? OutputFilePath { get; set; }
    public double? LatitudeDegrees { get; set; }
    public double? LongitudeDegrees { get; set; }
    public Geodesic? InputAsGeodesic { get {
        return (LatitudeDegrees.HasValue && LongitudeDegrees.HasValue) ? 
        new Geodesic(Angle.From(Degrees.FromRaw(LatitudeDegrees.Value)), Angle.From(Degrees.FromRaw(LongitudeDegrees.Value)))
        : null;
    }
    }
}