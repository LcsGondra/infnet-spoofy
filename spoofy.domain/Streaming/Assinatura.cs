using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Streaming
{
    public class Assinatura
    {
        public bool Ativo { get; internal set; }
        public DateTime DtAssinatura { get; internal set; }
        public Plano Plano { get; internal set; }
        public Guid Id { get; internal set; }
    }
}
