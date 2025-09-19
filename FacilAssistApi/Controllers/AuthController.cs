using FacilAssistApi.Command;
using FacilAssistApi.Ports;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FacilAssistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration config, IAuthService auth)
        {
            _config = config;
            _authService = auth;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand login, [FromServices] IValidator<LoginCommand> validator)
        {

            try
            {
                
                var validationResult = await validator.ValidateAsync(login);

                if (!validationResult.IsValid)
                {   
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var dados = await _authService.Validar(login);

                return Ok(dados);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }

            
        }
    }
}
