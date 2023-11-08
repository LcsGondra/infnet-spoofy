using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Streaming
{
    public class Plano
    {
        public string Nome { get; internal set; }
        public decimal Valor { get; internal set; }
        public string Descricao { get; internal set; }
    }
}
