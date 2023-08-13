using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "OrderService.FullAccess")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Orders()
        {
            return Ok(true);
        }
    }
}
