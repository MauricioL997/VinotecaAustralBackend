using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BodegaVinosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost ("login")] 
        public IActionResult Authenticate([FromBody] AuthRequestDto credentials)
        {
            // Intentar autenticar al usuario y generar un token
            var token = _userService.Authenticate(credentials);

            if (token == null)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas." });
            }

            // Retornar el token en caso de éxito
            return Ok(new
            {
                Token = token
            });
        }
    }
}
