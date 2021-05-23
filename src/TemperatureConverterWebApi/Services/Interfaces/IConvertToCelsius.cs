using TemperatureConverterWebApi.Enums;

namespace TemperatureConverterWebApi.Services.Interfaces
{
    public interface IConvertToCelsius
    {
        decimal Convert(decimal fromValue, TemperatureUnit fromUnit);
    }
}
