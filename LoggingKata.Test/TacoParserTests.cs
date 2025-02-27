using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("33.556383, -86.889051, Taco Bell Birmingha...", -86.889051)]
        [InlineData("33.911058, -84.82554, Taco Bell Dalla...", -84.82554)]

        //Add additional inline data. Refer to your CSV file.
        public void ShouldParseLongitude(string line, double expected)
        {

            var tacoParseLat = new TacoParser();
            
            var actual = tacoParseLat.Parse(line);
           
            Assert.Equal(expected, actual.Location.Longitude);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("33.556383, -86.889051, Taco Bell Birmingha...", 33.556383)]
        [InlineData("33.911058, -84.82554, Taco Bell Dalla...", 33.911058)]
        public void ShouldParseLatitude(string line, double expected)
        {
            var tacoParseLong = new TacoParser();

            var actual = tacoParseLong.Parse(line);

            Assert.Equal(expected, actual.Location.Latitude);
        }


    }
}
