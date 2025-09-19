using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacilAssist.Front.Models
{
    public class Cliente
    {
        public int? Id { get; set; }
        public int? SituacaoId { get; set; }
        public string Situacao { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Sexo { get; set; }        
        public string DescricaoSexo { get; set; }        
    }
}