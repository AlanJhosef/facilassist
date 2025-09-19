using FacilAssistApi.Command;
using FacilAssistApi.Enums;

namespace FacilAssistApi.Ports
{
    public interface IClienteService
    {
        Task Criar(ClienteCommand command);
        Task Alterar(ClienteCommand command);
        Task Excluir(int id);
        Task<IEnumerable<ClienteDto>> ObterClientes();
        Task<ClienteDto> ObterDetalhesCliente(int id);
        Task AprovarReprovarCliente(int id, ESituacaoCliente status);
    }
}
