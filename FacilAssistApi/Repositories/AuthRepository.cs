using Dapper;
using FacilAssistApi.Command;
using FacilAssistApi.Dto;
using FacilAssistApi.Ports;
using Microsoft.Data.SqlClient;

namespace FacilAssistApi.Repositories
{
    public class AuthRepository  : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> ValidarUsuario(LoginCommand command)
        {
            var sql = "exec ListarUsuario @UsuarioId,@Senha";
            using var connection = new SqlConnection(_connectionString);

            // O método .QueryAsync do Dapper executa a query e mapeia os resultados para uma lista de objetos Cliente.
            var clientes = await connection.QueryAsync<UsuarioDto>(sql, new { UsuarioId = command.Username, Senha = command.Password });
            return clientes.Count() > 0;
        }
    }
}
