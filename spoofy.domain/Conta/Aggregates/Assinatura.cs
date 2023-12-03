using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta.Aggregates
{
    public class Assinatura
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime DtAssinatura { get; set; }
        public Plano Plano { get; set; }
    }
}
