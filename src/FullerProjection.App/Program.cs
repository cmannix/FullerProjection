using System;
using System.IO;
using System.Linq;
using static FullerProjection.Core.FullerProjection;
using FullerProjection.Core.Geometry.Coordinates;
using FullerProjection.Core.Geometry.Angles;

namespace ProjectionApp
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var mode = GetMode();
            if (mode is ProcessingMode.Console) ProcessConsole();
            if (mode is ProcessingMode.File) ProcessFile();

            Console.WriteLine("Any key to quit");
            Console.ReadKey();

            ProcessingMode GetMode()
            {
                while (true)
                {
                    Console.WriteLine("Console (1) or File (2) input: ");
                    if (int.TryParse(Console.ReadLine(), out var choice))
                    {
                        if (choice == 1) return ProcessingMode.Console;
                        if (choice == 2) return ProcessingMode.File;
                    }
                }

            }
        }

        private static void ProcessConsole()
        {
            var input = GetPoint();
            var result = GetFullerPoint(input);
            WriteResult(result);

            Geodesic GetPoint()
            {
                Console.WriteLine("Enter coordinate: ");
                var latitude = GetDegreesInput("Latitude");
                var longitude = GetDegreesInput("Longitude");

                return new Geodesic(latitude, longitude);
                Angle GetDegreesInput(string name)
                {
                    Console.WriteLine($"{name}: ");
                    if (double.TryParse(Console.ReadLine(), out var value))
                    {
                        return Angle.From(Degrees.FromRaw(value));
                    }
                    throw new ArgumentException("Could not parse input as degree value");
                }
            }

            void WriteResult(Cartesian2D result) => Console.WriteLine(result);
        }

        private static void ProcessFile()
        {
            const string InputPath = @"/Users/chris.mannix/src/personal/TestProm/test.csv";
            const string OutputPath = @"/Users/chris.mannix/src/personal/TestProm/test_dymax.csv";

            var lines = File.ReadAllLines(InputPath);

            var results = lines
                .Select(ParseLine)
                .Select(GetFullerPoint)
                .Select(r => $"{r.X}, {r.Y}");

            File.WriteAllLines(OutputPath, results);

            Geodesic ParseLine(string line)
            {
                var elements = line.Split(',');

                return new Geodesic(Angle.From(Degrees.FromRaw(double.Parse(elements[1]))), Angle.From(Degrees.FromRaw(double.Parse(elements[0]))));
            }
        }

        internal enum ProcessingMode
        {
            Console,
            File
        }
    }
}

