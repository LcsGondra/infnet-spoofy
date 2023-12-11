using spoofy.domain.Conta.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta.Aggregates
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public CPF CPF { get; set; }
        public List<Cartao> Cartoes { get; set; }
        public List<Assinatura> Assinaturas { get; set; }
        public List<Playlist> Playlists { get; set; }

        public Usuario()
        {
            Playlists = new List<Playlist>();
            Cartoes = new List<Cartao>();
            Assinaturas = new List<Assinatura>();
        }

        public void Criar(string nome, string cpf, Plano plano, Cartao cartao, Playlist playlist)
        {
            Nome = nome;
            CPF = new CPF(cpf);

            AssinarPlano(plano, cartao);

            AdicionarCartao(cartao);

            CriarPlaylist(playlist);
        }



        private void AdicionarCartao(Cartao cartao)
        {
            cartao.Id = Guid.NewGuid();
            Cartoes.Add(cartao);
        }

        public void AssinarPlano(Plano plano, Cartao cartao)
        {

            //Debitar o valor do plano no cartão
            cartao.AutorizarCompra(plano.Valor, DateTime.Now, plano.Nome);

            //Caso tenha uma assinatura ativa, desativa ela
            if (Assinaturas.Count > 0 && Assinaturas.Any(x => x.Ativo))
            {
                var planoAtivo = Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }

            //Adiciona uma nova assinatura
            Assinaturas.Add(new Assinatura()
            {
                Ativo = true,
                DtAssinatura = DateTime.Now,
                Plano = plano,
                Id = Guid.NewGuid()
            });
        }
        public void CriarPlaylist(Playlist playlist)
        {
            Playlists.Add(new Playlist()
            { 
                Id = playlist.Id,
                Nome = playlist.Nome,
                Publica = playlist.Publica,
                Usuario = this,
                Musicas = playlist.Musicas,
            });
        }
        public void Favoritar(Musica musica)
        {
            Playlists.FirstOrDefault(x => x.Nome == "Favoritas").Musicas.Add(musica);
        }

        public void AddtoPlaylist(Musica musica, Guid id)
        {
            Playlists.FirstOrDefault(x => x.Id == id).Musicas.Add(musica);
        }
        public void MudarNome(String nome)
        {
            Nome = nome;
        }

        
    }
}
