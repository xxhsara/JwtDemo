using AuthorizeApi;
using AuthorizeApi.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizeService.Utils
{
    public class RSAJwtHelper : IJwtHelper
    {

        private readonly JwtTokenOptions _jwtTokenOptions;

        public RSAJwtHelper(IOptions<JwtTokenOptions> jwtTokenOptions)
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
        }

        public string GetToken(GetJwtTokenDto dto)
        {
            string keyDir = Directory.GetCurrentDirectory();
            if(RSAHelper.TryGetKeyParameters(keyDir,true,out RSAParameters rSAParameters)==false)
            {
                RSAHelper.GenerateAndSaveKey(keyDir);
                RSAHelper.TryGetKeyParameters(keyDir, true,out rSAParameters);
            }

            string securityKey = _jwtTokenOptions.SecurityKey;
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            SigningCredentials signingCredentials = new SigningCredentials(new RsaSecurityKey(rSAParameters), SecurityAlgorithms.RsaSha256Signature);
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
