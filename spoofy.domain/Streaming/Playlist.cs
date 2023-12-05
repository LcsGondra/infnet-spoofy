using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using spoofy.domain.Conta.Aggregates;

namespace spoofy.domain.Streaming
{
    public class Playlist
    {
        public Guid Id { get;  set; }
        public String Nome { get;  set; }
        public Boolean Publica { get;  set; }
        public Usuario Usuario { get;  set; }
        public List<Musica> Musicas { get; set; }

        public Playlist()
        {
            this.Musicas = new List<Musica>();
        }

    }
}
