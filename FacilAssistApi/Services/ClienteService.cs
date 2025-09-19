using FacilAssistApi.Command;
using FacilAssistApi.Enums;
using FacilAssistApi.Ports;

namespace FacilAssistApi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task Criar(ClienteCommand command) => await _clienteRepository.Criar(command);
        public async Task Alterar(ClienteCommand command)
        {
            command.Alterar();

            await _clienteRepository.Alterar(command);
        }
        public async Task Excluir(int id) => await _clienteRepository.Excluir(id);
        public async Task<IEnumerable<ClienteDto>> ObterClientes()
        {
            var clientes = await _clienteRepository.ObterClientes(0);

            foreach(var c in clientes)
            {
                c.DescricaoSexo = ObterDescricaoSexo(c.Sexo);
            }

            return clientes;
        }
        public async Task<ClienteDto> ObterDetalhesCliente(int id)
        {
            var clientes = await _clienteRepository.ObterClientes(id);

            foreach (var c in clientes)
            {                
                c.DescricaoSexo = ObterDescricaoSexo(c.Sexo);
            }

            return clientes.FirstOrDefault();
        }

        private string ObterDescricaoSituacao(int? situacaoId)
        {
            switch (situacaoId)
            {
                case (int)ESituacaoCliente.EmAnalise:
                    return "Em Análise";
                case (int)ESituacaoCliente.Aprovado:
                    return "Aprovado";
                case (int)ESituacaoCliente.Reprovado:
                    return "Reprovado";
                default:
                    return string.Empty;
            }
        }

        private string ObterDescricaoSexo(string sexo)
        {
            switch (sexo)
            {
                case "M":
                    return "Masculino";
                case "F":
                    return "Feminino";
                default:
                    return "Não informado";
            }
        }

        public async Task AprovarReprovarCliente(int id, ESituacaoCliente status)
        {
            var clientes = await _clienteRepository.ObterClientes(id);
            var cliente = clientes.FirstOrDefault();

            if (cliente.SituacaoId != (int)ESituacaoCliente.EmAnalise)
            {
                throw new Exception("Só é possível aprovar/reprovar clientes em analise");
            }             

            await _clienteRepository.AprovarReprovarCliente(id, status, "api");
        }
    }
}
