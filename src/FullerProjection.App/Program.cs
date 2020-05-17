using System;
using System.IO;
using System.Linq;
using FullerProjection.Core;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;

namespace ProjectionApp
{
    static class Program
    {
        private static void Main(string[] args)
        {
            // Console.WriteLine("Console (1) or File (2) input: ");
            // var valid = int.TryParse(Console.ReadLine(), out var choice);
            // if (!valid) return;

            // if (choice == 1)
            // {
            //     ProcessConsole();    
            // }
            // else if (choice == 2)
            // {
            //     ProcessFile(InputPath);    
            // }
            
            // Console.WriteLine("Any key to quit");
            // Console.ReadKey();

            ProcessConsole();
        }

        private static void ProcessConsole()
        {
            // Console.WriteLine("Enter a longitude and latitude coordinate: ");
            // Console.WriteLine("Longitude: ");
            // var longitude = double.Parse(Console.ReadLine());

            // Console.WriteLine("Latitude: ");
            // var latitude = double.Parse(Console.ReadLine());
            // Console.WriteLine($"Longitude = {longitude}, Latitude = {latitude}");
            var longitude = Angle.From(Degrees.FromRaw(10));
            var latitude = Angle.From(Degrees.FromRaw(10));

            var point = new Geodesic(latitude, longitude);
            var fullerPoint = FullerProjectionService.GetFullerPoint(point);
            Console.WriteLine($"X = {fullerPoint.X}, Y = {fullerPoint.Y}");
        }

        private static void ProcessFile(string path)
        {
            var lines = File.ReadAllLines(path);

            var results = lines
                .Select(ParseLine)
                .Select(FullerProjectionService.GetFullerPoint)
                .Select(r => $"{r.X}, {r.Y}");

            File.WriteAllLines(OutputPath, results);
        }

        private static Geodesic ParseLine(string line)
        {
            var elements = line.Split(',');

            return new Geodesic(Angle.From(Degrees.FromRaw(double.Parse(elements[1]))), Angle.From(Degrees.FromRaw(double.Parse(elements[0]))));
        }

        private const string InputPath = @"";
        private const string OutputPath = @"";
    }

    

}

