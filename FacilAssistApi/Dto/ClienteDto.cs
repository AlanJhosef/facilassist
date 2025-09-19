using FacilAssistApi.Enums;

namespace FacilAssistApi.Command
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Sexo { get; set; }
        public int? SituacaoId { get; set; }        
        public DateTime DataCriacao { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string Situacao { get; set; }
        public string DescricaoSexo { get; set; }

    }
}
