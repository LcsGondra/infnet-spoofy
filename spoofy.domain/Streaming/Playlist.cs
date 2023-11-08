using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Streaming
{
    public class Playlist
    {
        public Guid Id { get; internal set; }
        public object Nome { get; internal set; }
        public bool Publica { get; internal set; }
        public Usuario Usuario { get; internal set; }
        public List<Musica> Musicas { get; set; }

        public Playlist()
        {
            this.Musicas = new List<Musica>();
        }

    }
}
