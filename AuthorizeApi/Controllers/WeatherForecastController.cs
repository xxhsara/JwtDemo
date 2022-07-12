using AuthorizeService.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Text;

namespace AuthorizeApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IJwtHelper _jwtHelper;

        public WeatherForecastController(IJwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpPost(Name = "GenerateToken")]
        public string GenerateToken(GetJwtTokenDto dto)
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