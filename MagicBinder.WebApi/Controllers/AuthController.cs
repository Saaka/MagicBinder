using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("google")]
    public async Task<ActionResult> AuthorizeWithGoogle()
    {
        throw new NotImplementedException();
    }
}