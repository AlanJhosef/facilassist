using FacilAssistApi.Command;
using FacilAssistApi.Enums;

namespace FacilAssistApi.Ports
{
    public interface IClienteRepository
    {
        Task Criar(ClienteCommand command);
        Task Alterar(ClienteCommand command);
        Task Excluir(int id);
        Task<IEnumerable<ClienteDto>> ObterClientes(int Id);
        Task AprovarReprovarCliente(int id, ESituacaoCliente status, string usuario);
    }
}
