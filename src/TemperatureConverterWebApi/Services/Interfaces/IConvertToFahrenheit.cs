using TemperatureConverterWebApi.Enums;

namespace TemperatureConverterWebApi.Services.Interfaces
{
    public interface IConvertToFahrenheit
    {
        decimal Convert(decimal fromValue, TemperatureUnit fromUnit);
    }
}
