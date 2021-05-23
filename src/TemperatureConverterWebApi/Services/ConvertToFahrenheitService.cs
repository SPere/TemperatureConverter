using System;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Constants;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Services
{
    public class ConvertToFahrenheitService : IConvertToFahrenheit
    {
        public decimal Convert(decimal fromValue, TemperatureUnit fromUnit)
        {
            var convertedValue = fromUnit switch
            {
                TemperatureUnit.Celsius => (fromValue * 9 / 5) + 32,
                TemperatureUnit.Fahrenheit => fromValue,
                TemperatureUnit.Kelvin => (fromValue - 273.15M) * 9 / 5 + 32
            };

            return Math.Round(convertedValue, TemperaturePrecision.Precision);
        }
    }
}
