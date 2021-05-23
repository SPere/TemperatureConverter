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
    public class ConvertTemperatureServiceTests
    {
        [DataTestMethod]
        [DataRow(2.23, TemperatureUnit.Fahrenheit)]
        [DataRow(2.23, TemperatureUnit.Kelvin)]
        public void ShouldCallConvertToCelsius_WhenToUnitIsCelsius(double fromValue, TemperatureUnit fromUnit)
        {
            // arrange
            var mockToCelsiusService = new Mock<IConvertToCelsius>();
            var mockToFahrenheitService = new Mock<IConvertToFahrenheit>();
            var mockToKelvinService = new Mock<IConvertToKelvin>();
            var logger = new Mock<ILogger<ConvertTemperatureService>>();

            const decimal returnValue = 4.4M;
            mockToCelsiusService.Setup(x => x.Convert(It.IsAny<decimal>(), It.IsAny<TemperatureUnit>())).Returns(returnValue);

            var sut = new ConvertTemperatureService(
                mockToCelsiusService.Object, 
                mockToFahrenheitService.Object, 
                mockToKelvinService.Object,
                logger.Object);
            
            // act
            var result = sut.Convert((decimal)fromValue, fromUnit, TemperatureUnit.Celsius);

            // assert
            mockToCelsiusService.Verify(s => s.Convert((decimal)fromValue, fromUnit), Times.Once);
            result.Should().Be(returnValue);
        }

        [DataTestMethod]
        [DataRow(2.23, TemperatureUnit.Celsius)]
        [DataRow(2.23, TemperatureUnit.Kelvin)]
        public void ShouldCallConvertToFahrenheit_WhenToUnitIsFahrenheit(double fromValue, TemperatureUnit fromUnit)
        {
            // arrange
            var mockToCelsiusService = new Mock<IConvertToCelsius>();
            var mockToFahrenheitService = new Mock<IConvertToFahrenheit>();
            var mockToKelvinService = new Mock<IConvertToKelvin>();
            var logger = new Mock<ILogger<ConvertTemperatureService>>();

            const decimal returnValue = 4.4M;
            mockToFahrenheitService.Setup(x => x.Convert(It.IsAny<decimal>(), It.IsAny<TemperatureUnit>())).Returns(returnValue);

            var sut = new ConvertTemperatureService(
                mockToCelsiusService.Object,
                mockToFahrenheitService.Object,
                mockToKelvinService.Object,
                logger.Object);

            // act
            var result = sut.Convert((decimal)fromValue, fromUnit, TemperatureUnit.Fahrenheit);

            // assert
            mockToFahrenheitService.Verify(s => s.Convert((decimal)fromValue, fromUnit), Times.Once);
            result.Should().Be(returnValue);
        }

        [DataTestMethod]
        [DataRow(2.23, TemperatureUnit.Celsius)]
        [DataRow(2.23, TemperatureUnit.Fahrenheit)]
        public void ShouldCallConvertToKelvin_WhenToUnitIsKelvin(double fromValue, TemperatureUnit fromUnit)
        {
            // arrange
            var mockToCelsiusService = new Mock<IConvertToCelsius>();
            var mockToFahrenheitService = new Mock<IConvertToFahrenheit>();
            var mockToKelvinService = new Mock<IConvertToKelvin>();
            var logger = new Mock<ILogger<ConvertTemperatureService>>();

            const decimal returnValue = 4.4M;
            mockToKelvinService.Setup(x => x.Convert(It.IsAny<decimal>(), It.IsAny<TemperatureUnit>())).Returns(returnValue);
            
            var sut = new ConvertTemperatureService(
                mockToCelsiusService.Object,
                mockToFahrenheitService.Object,
                mockToKelvinService.Object,
                logger.Object);

            // act
            var result = sut.Convert((decimal)fromValue, fromUnit, TemperatureUnit.Kelvin);

            // assert
            mockToKelvinService.Verify(s => s.Convert((decimal)fromValue, fromUnit), Times.Once);
            result.Should().Be(returnValue);
        }

    }
}
