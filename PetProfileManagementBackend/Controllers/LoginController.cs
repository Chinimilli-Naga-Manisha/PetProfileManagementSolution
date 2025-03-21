using Microsoft.AspNetCore.Mvc;

namespace PetProfileManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private static readonly string StaticEmail = "admin@example.com";
        private static readonly string StaticPassword = "password123";

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Email == StaticEmail && request.Password == StaticPassword)
            {
                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized(new { Message = "Invalid email or password" });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
