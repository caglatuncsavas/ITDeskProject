using ITDeskServer.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITDeskServer.Controllers;

public class ValuesController : ApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {Message= "Api Çalışıyor!"});
    }
}
