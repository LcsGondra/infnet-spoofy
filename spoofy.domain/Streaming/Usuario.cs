using spoofy.domain.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Streaming
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public List<Cartao> Cartoes { get; set; }
        public List<Assinatura> Assinaturas { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Favoritos> Favoritos { get; set; }

        public Usuario()
        {
            this.Playlists = new List<Playlist>();
            this.Assinaturas = new List<Assinatura>();
            this.Favoritos = new List<Favoritos>();
            this.Cartoes = new List<Cartao>();
        }

        public void Criar(string nome, string cpf, Plano plano, Cartao cartao)
        {
            this.Name = nome;
            this.CPF = cpf;

            this.AssinarPlano(plano, cartao);

            this.AdicionarCartao(cartao);

            this.CriarPlaylist();
        }

        private void CriarPlaylist(string nome = "Favoritas")
        {
            this.Playlists.Add(new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Publica = false,
                Usuario = this
            });
        }

        private void AdicionarCartao(Cartao cartao)
        {
            this.Cartoes.Add(cartao);
        }

        private void AssinarPlano(Plano plano, Cartao cartao)
        {

            //Debitar o valor do plano no cartão
            cartao.AutorizarCompra(plano.Valor, DateTime.Now, plano.Nome);

            //Caso tenha uma assinatura ativa, desativa ela
            if (this.Assinaturas.Count > 0 && this.Assinaturas.Any(x => x.Ativo))
            {
                var planoAtivo = this.Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }

            //Adiciona uma nova assinatura
            this.Assinaturas.Add(new Assinatura()
            {
                Ativo = true,
                DtAssinatura = DateTime.Now,
                Plano = plano,
                Id = Guid.NewGuid()
            });
        }
    }
}
