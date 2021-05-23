using FluentAssertions;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemperatureConverterWebApi.Tests.Services
{
    [TestClass]
    public class ConvertToFahrenheitServiceTests
    {
        [DataTestMethod]
        [DataRow(0, TemperatureUnit.Celsius, 32)]
        [DataRow(0, TemperatureUnit.Fahrenheit, 0)]
        [DataRow(0, TemperatureUnit.Kelvin, -459.67)]
        [DataRow(2.23, TemperatureUnit.Celsius, 36.014)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit, 2.23)]
        [DataRow(2.23, TemperatureUnit.Kelvin, -455.656)]
        [DataRow(-2.23, TemperatureUnit.Celsius, 27.986)]
        [DataRow(-2.23, TemperatureUnit.Fahrenheit, -2.23)]
        [DataRow(-2.23, TemperatureUnit.Kelvin, -463.684)]
        public void ShouldConvertToFahrenheit(double fromValue, TemperatureUnit fromUnit, double expectResult)
        {
            // arrange
            var sut = new ConvertToFahrenheitService();
            
            // act
            var result = sut.Convert((decimal)fromValue, fromUnit);

            // assert
            result.Should().Be((decimal)expectResult);
        }
    }
}
