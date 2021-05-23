using Microsoft.AspNetCore.Mvc;
using TemperatureConverterWebApi.Enums;
using TemperatureConverterWebApi.Services.Interfaces;

namespace TemperatureConverterWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureConverterController : ControllerBase
    {
        private readonly IConvertTemperature _convertTemperatureService;

        public TemperatureConverterController(IConvertTemperature convertTemperatureService)
        {
            _convertTemperatureService = convertTemperatureService;
        }

        [HttpGet]
        [Route("{fromValue}/{fromUnit}/{toUnit}")]
        public decimal Get(decimal fromValue, TemperatureUnit fromUnit, TemperatureUnit toUnit)
        {
            return _convertTemperatureService.Convert(fromValue, fromUnit, toUnit);
        }
    }
}
