using AuthorizeApi;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizeService.Utils
{
    public class JwtHelper : IJwtHelper
    {

        private readonly JwtTokenOptions _jwtTokenOptions;

        public JwtHelper(IOptions<JwtTokenOptions> jwtTokenOptions)
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
        }

        public string GetToken(GetJwtTokenDto dto)
        {
            string securityKey = _jwtTokenOptions.SecurityKey;
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name,dto.UserName),
                new Claim(ClaimTypes.Role,"admin")
            };
            var token = new JwtSecurityToken(
                issuer:_jwtTokenOptions.Issuer,
                audience:_jwtTokenOptions.Audience,
                claims:claims,
                expires:System.DateTime.Now.AddDays(1),
                signingCredentials:signingCredentials
                );

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
    }
}
