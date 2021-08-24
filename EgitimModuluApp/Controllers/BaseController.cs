using Entities.Dtos;
using Entities.Dtos.AccountDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgitimModuluApp.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        // doğrulanmış kullanıcıyı gönderir, giriş yapmamış ise null döner.
        public AccountDto Account => (AccountDto)HttpContext.Items["Account"];
    }
}
