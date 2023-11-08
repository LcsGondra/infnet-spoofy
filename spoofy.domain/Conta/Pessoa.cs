using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }
        public string Telefone { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }

    }
}