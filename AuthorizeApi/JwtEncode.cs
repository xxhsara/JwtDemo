using AuthorizeApi.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizeApi
{

    /// <summary>
    /// jwt加密
    /// </summary>
    public class JwtEncode
    {
        public static string GetJwtEncodeStr(DecodeJwt jwtDto)
        {
            var header=Convert.ToBase64String(Encoding.UTF8.GetBytes(jwtDto.Header));
            var payload= Convert.ToBase64String(Encoding.UTF8.GetBytes(jwtDto.Payload));

            var sign= RSAHelper.RSAEncrypt($"{header}.{payload}");

            return $"{header}.{payload}.{sign}";

        }
    }
}
