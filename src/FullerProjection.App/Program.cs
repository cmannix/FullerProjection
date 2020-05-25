using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static FullerProjection.Core.FullerProjection;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;
using CommandLine;

namespace FullerProjection.App
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);

            result.WithParsed(o =>
            {
                if (o.InputAsGeodesic is object)
                {
                    var fullerPoint = GetFullerPoint(o.InputAsGeodesic);
                    Console.WriteLine("Projected point on Dymaxion projection:");
                    Console.WriteLine($"\t{fullerPoint}");
                }
                else if (o.InputFilePath is object && o.OutputFilePath is object)
                {
                    var inputPath = Path.GetFullPath(o.InputFilePath);
                    var outputPath = Path.GetFullPath(o.OutputFilePath);

                    ProcessFiles(inputPath, outputPath);
                }
                else {
                    Console.WriteLine("Must provide either Longitude/Latitude OR an input/ouput file");
                }
            });
        }

        private static void ProcessFiles(string inputPath, string outputPath)
        {
            var sw = Stopwatch.StartNew();
            var results = File.ReadAllLines(inputPath)
            .Select(ParseLine)
            .Select(GetFullerPoint)
            .Select(r => $"{r.X}, {r.Y}")
            .ToList();

            Console.WriteLine($"Read {results.Count} lines from '{inputPath} in {sw.Elapsed.TotalSeconds:N2} seconds'");

            File.WriteAllLines(outputPath, results);

            Console.WriteLine($"Successfully wrote to '{outputPath}'");

            Geodesic ParseLine(string line)
            {
                var elements = line.Split(',');

                return new Geodesic(Angle.From(Degrees.FromRaw(double.Parse(elements[1]))), Angle.From(Degrees.FromRaw(double.Parse(elements[0]))));
            }
        }
    }
}

