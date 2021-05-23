using TemperatureConverterWebApi.Enums;

namespace TemperatureConverterWebApi.Services.Interfaces
{
    public interface IConvertTemperature
    {
        decimal Convert(decimal fromValue, TemperatureUnit fromUnit, TemperatureUnit toUnit);
    }
}
