using FluentAssertions;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemperatureConverterWebApi.Tests.Services
{
    [TestClass]
    public class ConvertToKelvinServiceTests
    {
        [DataTestMethod]
        [DataRow(0, TemperatureUnit.Celsius, 273.15)]
        [DataRow(0, TemperatureUnit.Fahrenheit, 255.37222)]
        [DataRow(0, TemperatureUnit.Kelvin, 0)]
        [DataRow(2.23, TemperatureUnit.Celsius, 275.38)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit, 256.61111)]
        [DataRow(2.23, TemperatureUnit.Kelvin, 2.23)]
        [DataRow(-2.23, TemperatureUnit.Celsius, 270.92)]
        [DataRow(-2.23, TemperatureUnit.Fahrenheit, 254.13333)]
        [DataRow(-2.23, TemperatureUnit.Kelvin, -2.23)]
        public void ShouldConvertToKelvin(double fromValue, TemperatureUnit fromUnit, double expectResult)
        {
            // arrange
            var sut = new ConvertToKelvinService();
            
            // act
            var result = sut.Convert((decimal)fromValue, fromUnit);

            // assert
            result.Should().Be((decimal)expectResult);
        }
    }
}
