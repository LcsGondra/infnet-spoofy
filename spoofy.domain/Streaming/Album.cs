using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Streaming
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Banda Banda { get; set; }
    }
}
