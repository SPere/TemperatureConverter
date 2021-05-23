using Moq;
using FluentAssertions;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Tests.Controllers
{
    [TestClass]
    public class TemperatureConverterControllerTests
    {
        [DataTestMethod]
        [DataRow(2.23, TemperatureUnit.Celsius, TemperatureUnit.Celsius)]
        [DataRow(2.23, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit)]
        [DataRow(2.23, TemperatureUnit.Celsius, TemperatureUnit.Kelvin)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit, TemperatureUnit.Fahrenheit)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit, TemperatureUnit.Kelvin)]
        [DataRow(2.23, TemperatureUnit.Kelvin, TemperatureUnit.Celsius)]
        [DataRow(2.23, TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit)]
        [DataRow(2.23, TemperatureUnit.Kelvin, TemperatureUnit.Kelvin)]
        public void ShouldCallTemperatureConverter_WhenRequiredParametersAreProvided(double fromValue, TemperatureUnit fromUnit, TemperatureUnit toUnit)
        {
            // arrange
            var mockConvertTemperatureService = new Mock<IConvertTemperature>();
            const decimal returnValue = 4.4M;
            mockConvertTemperatureService
                .Setup(s => s.Convert(It.IsAny<decimal>(), It.IsAny<TemperatureUnit>(), It.IsAny<TemperatureUnit>()))
                .Returns(returnValue);
            var sut = new TemperatureConverterController(mockConvertTemperatureService.Object);

            // act
            var result = sut.Get((decimal)fromValue, fromUnit, toUnit);

            // assert
            mockConvertTemperatureService.Verify(s => s.Convert((decimal)fromValue, fromUnit, toUnit));
            result.Should().Be(returnValue);
        }
    }
}
