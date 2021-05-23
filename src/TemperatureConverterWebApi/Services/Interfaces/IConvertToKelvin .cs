using TemperatureConverterWebApi.Enums;

namespace TemperatureConverterWebApi.Services.Interfaces
{
    public interface IConvertToKelvin
    {
        decimal Convert(decimal fromValue, TemperatureUnit fromUnit);
    }
}
