namespace AuthorizeApi
{
    public class DecodeJwt
    {
        public string Header { get; set; }
        public string Payload { get; set; }
        public string Sign { get; set; }
    }
}
