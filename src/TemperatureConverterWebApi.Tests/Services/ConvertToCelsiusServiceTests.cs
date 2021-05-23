using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Tests.Services
{
    [TestClass]
    public class ConvertToCelsiusServiceTests
    {
        [DataTestMethod]
        [DataRow(0, TemperatureUnit.Celsius, 0)]
        [DataRow(0, TemperatureUnit.Fahrenheit, -17.77778)]
        [DataRow(0, TemperatureUnit.Kelvin, -273.15)]
        [DataRow(2.23, TemperatureUnit.Celsius, 2.23)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit, -16.53889)]
        [DataRow(2.23, TemperatureUnit.Kelvin, -270.92)]
        [DataRow(-2.23, TemperatureUnit.Celsius, -2.23)]
        [DataRow(-2.23, TemperatureUnit.Fahrenheit, -19.01667)]
        [DataRow(-2.23, TemperatureUnit.Kelvin, -275.38)]
        public void ShouldConvertToCelsius(double fromValue, TemperatureUnit fromUnit, double expectResult)
        {
            // arrange
            var sut = new ConvertToCelsiusService();
            
            // act
            var result = sut.Convert((decimal)fromValue, fromUnit);

            // assert
            result.Should().Be((decimal)expectResult);
        }
    }
}
