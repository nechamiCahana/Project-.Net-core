//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FinalProject.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class JwtController : ControllerBase
//    {
//            [HttpGet]
//            [Authorize]
//            public IActionResult Get()
//            {
//                var userId = HttpContext.Items["User"];
//                return Ok(new { message = "This is a protected route", userId });
//            }
//    }
//}
