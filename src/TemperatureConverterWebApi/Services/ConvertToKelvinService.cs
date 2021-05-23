using System;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Constants;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Services
{
    public class ConvertToKelvinService : IConvertToKelvin
    {
        public decimal Convert(decimal fromValue, TemperatureUnit fromUnit)
        {
            var convertedValue = fromUnit switch
            {
                TemperatureUnit.Celsius => fromValue + 273.15M,
                TemperatureUnit.Fahrenheit => (fromValue - 32) * 5 / 9 + 273.15M,
                TemperatureUnit.Kelvin => fromValue
            };

            return Math.Round(convertedValue, TemperaturePrecision.Precision);
        }
    }
}
