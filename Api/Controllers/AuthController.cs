using Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.IServices;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult<UserDto> Register([FromBody] RegisterDto registerDto)
        {
            var user = _securityService.RegisterUser(registerDto.Name, registerDto.Password);
            if (user == null)
            {
                return Problem("Error occurred during registration");
            }

            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name
            };
        }
        
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<UserDto> Login([FromBody] LoginDto loginDto)
        {
            var user = _securityService.Login(loginDto.Name, loginDto.Password);
            if (user == null)
            {
                return Problem("Error occurred during login");
            }

            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name
            };
        }
        
    }
}