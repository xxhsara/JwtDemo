using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeService.Dto;
using AuthorizeService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthorizeService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
      
        private readonly ILogger<HomeController> _logger;
        private readonly  IJwtHelper _jwtHelper;

        public HomeController(ILogger<HomeController> logger,IJwtHelper jwtHelper)
        {
            _logger = logger;
            _jwtHelper = jwtHelper;
        }

       
        [HttpPost]
        public string Login(GetJwtTokenDto dto)
        {
            var token = String.Empty;
            if (dto.UserName == "admin" && dto.Password == "123456")
            {
                token=_jwtHelper.GetToken(dto);
            }
            return token;
        }
    }
}
