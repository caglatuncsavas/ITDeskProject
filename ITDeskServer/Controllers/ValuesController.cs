using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITDeskServer.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes ="Bearer")] // attribute olarak ekledik. Bu attribute ile bu controller'a gelen tüm isteklerde kullanıcı girişi olup olmadığı kontrol edilecek.
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {Message= "Api Çalışıyor!"});
    }
}
