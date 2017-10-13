using System;
using System.IO;
using System.Linq;
using FullerProjection;
using FullerProjection.Coordinates;
using FullerProjection.Geometry;

namespace ProjectionApp
{
    static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Console (1) or File (2) input: ");
            var valid = int.TryParse(Console.ReadLine(), out var choice);
            if (!valid) return;

            if (choice == 1)
            {
                ProcessConsole();    
            }
            else if (choice == 2)
            {
                ProcessFile(InputPath);    
            }
            
            Console.WriteLine("Any key to quit");
            Console.ReadKey();
        }

        private static void ProcessConsole()
        {
            Console.WriteLine("Enter a longitude and latitude coordinate: ");
            Console.WriteLine("Longitude: ");
            var longitude = double.Parse(Console.ReadLine());

            Console.WriteLine("Latitude: ");
            var latitude = double.Parse(Console.ReadLine());
            Console.WriteLine($"Longitude = {longitude}, Latitude = {latitude}");

            var point = new Geodesic(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude));
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

            return new Geodesic(Angle.FromDegrees(double.Parse(elements[1])), Angle.FromDegrees(double.Parse(elements[0])));
        }

        private const string InputPath = @"";
        private const string OutputPath = @"";
    }

    

}

