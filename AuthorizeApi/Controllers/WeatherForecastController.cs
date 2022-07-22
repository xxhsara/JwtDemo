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

        [HttpPost]
        public string GenerateToken(GetJwtTokenDto dto)
        {
            var token = String.Empty;
            if (dto.UserName == "admin" && dto.Password == "123456")
            {
                token = _jwtHelper.GetToken(dto);
            }
            return token;
        }

        [HttpGet]
        public DecodeJwt GetDecodeJwtStr(string jwtToken)
        {
            var header = jwtToken.Split('.')[0];
            var payload = jwtToken.Split('.')[1];
            var sign = jwtToken.Split('.')[2];
            var  decodeHeader=JwtDecode.GetJwtDecodeStr(header);
            var  decodePayload=JwtDecode.GetJwtDecodeStr(payload);
            var  decodeSign=JwtDecode.GetJwtDecodeStr(sign);
            return new DecodeJwt
            {
                Header=decodeHeader,
                Payload=decodePayload,
                Sign=decodeSign
            };
        }

        [HttpPost]
        public string EncodeJwtStr(DecodeJwt decodeJwt)
        {
            var token = JwtEncode.GetJwtEncodeStr(decodeJwt);
            return token;
        }
    }
}