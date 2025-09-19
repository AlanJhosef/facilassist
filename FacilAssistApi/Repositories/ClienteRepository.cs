using Dapper;
using FacilAssistApi.Command;
using FacilAssistApi.Dto;
using FacilAssistApi.Enums;
using FacilAssistApi.Ports;
using Microsoft.Data.SqlClient;

namespace FacilAssistApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Criar(ClienteCommand command)
        {
            var sql = "exec InserirCliente @Nome,@CPF,@DataNascimento,@Sexo,@UsuarioCriacao";

            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(sql, new
            {
                command.Nome,
                command.CPF,
                command.DataNascimento,
                command.Sexo,
                command.UsuarioCriacao
            });
        }
        public async Task Alterar(ClienteCommand command)
        {
            var sql = "exec AlterarCliente @Id,@Nome,@CPF,@DataNascimento,@Sexo,@SituacaoId,@Usuario";

            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(sql, new
            {
                command.Id,
                command.Nome,
                command.CPF,
                command.DataNascimento,
                command.Sexo,
                command.SituacaoId,
                Usuario = command.UsuarioCriacao
            });
        }
        public async Task Excluir(int id)
        {

            var sql = "exec ExcluirCliente @Id";

            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(sql, new
            {
                id
            });
        }

        public async Task<IEnumerable<ClienteDto>> ObterClientes(int Id)
        {
            var sql = "exec ListarCliente @Id";

            using var connection = new SqlConnection(_connectionString);

            var retorno = await connection.QueryAsync<ClienteDto>(sql, new
            {
                Id
            });

            return retorno;
        }
        public async Task AprovarReprovarCliente(int id, ESituacaoCliente status, string usuario)
        {
            var sql = "exec AprovarReprovarCliente @Id, @SituacaoId, @usuario";

            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(sql, new
            {
                id,
                SituacaoId = (int)status,
                usuario
            });
        }
    }
}
