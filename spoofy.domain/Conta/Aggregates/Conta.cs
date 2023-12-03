using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta.Aggregates
{
    public class Conta
    {
        public Guid Id { get; set; }
        [ForeignKey("Pessoa")]
        public Guid pessoaID { get; set; }
        public virtual Usuario usuario { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
    }
}

