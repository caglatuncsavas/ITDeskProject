using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITDeskServer.Abstractions;
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")] // attribute olarak ekledik. Bu attribute arka planda token'ın doğrulunu kontrol eediyor.Eğer token doğruysa buna izin veriyor.

public abstract class ApiController:ControllerBase
{
}
