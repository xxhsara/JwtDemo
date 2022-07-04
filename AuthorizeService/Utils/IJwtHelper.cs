using AuthorizeService.Dto;

namespace AuthorizeService.Utils
{
    public interface IJwtHelper
    {
        string GetToken(GetJwtTokenDto dto);
    }
}
