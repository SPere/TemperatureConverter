using Microsoft.Extensions.Logging;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Services
{
    public class ConvertTemperatureService : IConvertTemperature
    {
        public IConvertToCelsius ConvertToCelsiusService { get; set;  }
        public IConvertToFahrenheit ConvertToFahrenheitService { get; set; }
        public IConvertToKelvin ConvertToKelvin { get; set; }
        public ILogger<ConvertTemperatureService> Logger { get; }

        public ConvertTemperatureService(
            IConvertToCelsius convertToCelsiusService,
            IConvertToFahrenheit convertToFahrenheitService,
            IConvertToKelvin convertToKelvin,
            ILogger<ConvertTemperatureService> logger)
        {
            ConvertToCelsiusService = convertToCelsiusService;
            ConvertToFahrenheitService = convertToFahrenheitService;
            ConvertToKelvin = convertToKelvin;
            Logger = logger;
        }

        public decimal Convert(decimal fromValue, TemperatureUnit fromUnit, TemperatureUnit toUnit)
        {
            Logger.LogWarning($"Convert; FromValue:{fromValue}, fromUnit:{fromUnit}, toUnit:{toUnit}");

            return toUnit switch
            {
                TemperatureUnit.Celsius => ConvertToCelsiusService.Convert(fromValue, fromUnit),
                TemperatureUnit.Fahrenheit => ConvertToFahrenheitService.Convert(fromValue, fromUnit),
                TemperatureUnit.Kelvin => ConvertToKelvin.Convert(fromValue, fromUnit),
            };
        }
    }
}
