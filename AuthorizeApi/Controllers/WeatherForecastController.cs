using AuthorizeService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IJwtHelper _jwtHelper;

        public WeatherForecastController(IJwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpGet(Name = "GetToken")]
        public string GetToken(GetJwtTokenDto dto)
        {
            var token = String.Empty;
            if (dto.UserName == "admin" && dto.Password == "123456")
            {
                token = _jwtHelper.GetToken(dto);
            }
            return token;
        }
    }
}