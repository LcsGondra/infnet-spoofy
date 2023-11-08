using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta
{
    public class Compra
    {
        public Guid Id { get; set; }
        [ForeignKey("Cartao")]
        public Guid CartaoID { get; set; }
        public virtual Cartao Cartao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public string Comerciante { get; set; }
        public string Status { get; set; }
    }
}
