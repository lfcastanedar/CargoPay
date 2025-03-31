using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected int UserId
    {
        get
        {
            var userId = HttpContext.Items["UserId"]?.ToString() ?? "";
            return Convert.ToInt32(userId);
        }
    }
}