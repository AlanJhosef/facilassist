using FacilAssistApi.Command;
using FacilAssistApi.Dto;
using FacilAssistApi.Ports;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FacilAssistApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration config, IAuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;
        }

        public async Task<LoginDto> Validar(LoginCommand login)
        {
            var result = new LoginDto();

            var usuarioValido = await _authRepository.ValidarUsuario(login);

            if (usuarioValido)
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 4. Criar o token
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30), //  30 minutos
                    signingCredentials: creds
                );

                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new Exception("Usuario e/ou senha inválidos");
            }

            return result;
        }
    }
}
