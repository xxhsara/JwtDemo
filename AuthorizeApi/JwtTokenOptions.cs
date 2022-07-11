namespace AuthorizeApi
{
    public class JwtTokenOptions
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
