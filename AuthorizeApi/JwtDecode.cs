using System.Text;

namespace AuthorizeApi
{
    public class JwtDecode
    {
        public static string GetJwtDecodeStr(string jwtToken)
        {
            jwtToken=jwtToken.Replace('-','+').Replace('_','/');
            switch(jwtToken.Length % 4)
            {
                case 2:
                    jwtToken += "==";
                    break;
                case 3:
                    jwtToken += "=";
                    break;
            }
            var bytes = Convert.FromBase64String(jwtToken);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
