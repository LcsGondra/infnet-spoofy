using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.application.Conta.Dto
{
    public class CompraDto
    {
        public Guid Id { get; set; }
        public Guid IdPlano { get; set; }

        public Guid IdCartao { get; set; }
    }
}

