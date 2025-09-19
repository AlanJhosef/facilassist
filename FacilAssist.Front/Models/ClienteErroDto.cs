using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacilAssist.Front.Models
{
    public class ClienteErroDto
    {
        public ClienteErroDto()
        {            
            Nome = new List<string>();
            Cpf = new List<string>();
            DataNascimento = new List<string>();
            Sexo = new List<string>();
        }        
        public List<string> Nome { get; set; }
        public List<string> Cpf { get; set; }
        public List<string> DataNascimento { get; set; }        
        public List<string> Sexo { get; set; }
        public string message { get; set; }
    }
}