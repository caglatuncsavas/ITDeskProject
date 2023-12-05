using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITDeskServer.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes ="Bearer")] // attribute olarak ekledik. Bu attribute arka planda token'ın doğrulunu kontrol eediyor.Eğer token doğruysa buna izin veriyor.
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {Message= "Api Çalışıyor!"});
    }
}
