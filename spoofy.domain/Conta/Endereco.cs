using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta
{
    public class Endereco
    {
        public int Id { get; set; }
        [ForeignKey("Pessoa")]
        public int pessoaID { get; set; }
        public virtual Pessoa pessoa { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}
