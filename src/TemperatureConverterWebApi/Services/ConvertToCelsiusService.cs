using System;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Constants;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Services
{
    public class ConvertToCelsiusService : IConvertToCelsius
    {
        public decimal Convert(decimal fromValue, TemperatureUnit fromUnit)
        {
            var convertedValue = fromUnit switch
            {
                TemperatureUnit.Celsius => fromValue,
                TemperatureUnit.Fahrenheit => (fromValue - 32) * 5 / 9,
                TemperatureUnit.Kelvin => fromValue - 273.15M
            };

            return Math.Round(convertedValue, TemperaturePrecision.Precision);
        }
    }
}
