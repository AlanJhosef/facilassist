using FacilAssist.Front.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacilAssist.Front.Models
{
    public class ClienteCommand
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Sexo { get; set; }
        public int? SituacaoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string UsuarioAlteracao { get; set; }

        public void Criar()
        {
            UsuarioAlteracao = "api";
            UsuarioCriacao = "api";
            DataCriacao = DateTime.Now;
            DataAlteracao = DateTime.Now;
            CPF = FuncoesString.RemoverFormatacao(CPF);
        }
        public void Alterar()
        {
            UsuarioAlteracao = "api";
            DataAlteracao = DateTime.Now;
        }
    }
}