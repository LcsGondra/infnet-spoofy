using spoofy.domain.Conta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.application.Conta.Dto
{
    public class CriarContaDTO
    {
        public int Id { get; set; }
        [ForeignKey("Pessoa")]
        public Guid pessoaID { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
    }

    public class Criar
}
