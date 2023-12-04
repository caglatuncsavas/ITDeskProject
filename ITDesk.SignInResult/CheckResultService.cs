
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ITDesk.SignInResultNameSpace;
public class CheckResultService :ControllerBase
{
    public IActionResult PasswordResult(Microsoft.AspNetCore.Identity.SignInResult result, DateTimeOffset? LockoutEnd)
    {
        if (result.IsLockedOut)
        {
            TimeSpan? timeSpan = LockoutEnd - DateTime.UtcNow;
            if (timeSpan is not null)
                return BadRequest(new { Message = $"Kullanıcınız 3 kere yanlış şifre girişinden dolayı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika kitlendi!" });
        }

        if (result.IsNotAllowed)
        {
            return BadRequest(new { Message = "Mail adresiniz onaylı değil!" });
        }

        if (!result.Succeeded)
        {
            return BadRequest(new { Message = "Şifreniz yanlış!" });
        }
        return Ok();
    }
}
