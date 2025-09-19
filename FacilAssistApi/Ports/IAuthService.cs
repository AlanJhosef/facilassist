using FacilAssistApi.Command;
using FacilAssistApi.Dto;

namespace FacilAssistApi.Ports
{
    public interface IAuthService
    {
        Task<LoginDto> Validar(LoginCommand login);
    }
}
