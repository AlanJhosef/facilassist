using FacilAssistApi.Command;

namespace FacilAssistApi.Ports
{
    public interface IAuthRepository
    {
        Task<bool> ValidarUsuario(LoginCommand command);
    }
}
