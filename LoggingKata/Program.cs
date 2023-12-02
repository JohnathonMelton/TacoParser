using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);
            if(lines.Length == 0)
            {
                logger.LogError("No file input");
            }
            
            if(lines.Length == 1)
            {
                logger.LogWarning("Only one file selected. Two required.");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            double distance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate();

                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for(int n =  0; n < locations.Length; n++)
                {
                    var locB = locations[n];
                    var corB = new GeoCoordinate();

                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    if(corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
            }

            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart.");

        }
    }
}
