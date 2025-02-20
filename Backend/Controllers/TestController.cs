using Microsoft.AspNetCore.Mvc;

[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Salut din Backend!" });
    }
}
