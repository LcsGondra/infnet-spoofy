using spoofy.domain.Conta.ValueObject;
using spoofy.domain.Streaming;
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
        public List<Favoritos> Favoritos { get; set; }

        public Usuario()
        {
            Playlists = new List<Playlist>();
            Assinaturas = new List<Assinatura>();
            Favoritos = new List<Favoritos>();
            Cartoes = new List<Cartao>();
            Assinaturas = new List<Assinatura>();
        }

        public void Criar(string nome, string cpf, Plano plano, Cartao cartao)
        {
            Nome = nome;
            CPF = new CPF(cpf);

            AssinarPlano(plano, cartao);

            AdicionarCartao(cartao);

            CriarPlaylist();
        }

        private void CriarPlaylist(string nome = "Favoritas")
        {
            Playlists.Add(new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Publica = false,
                Usuario = this
            });
        }

        private void AdicionarCartao(Cartao cartao)
        {
            Cartoes.Add(cartao);
        }

        private void AssinarPlano(Plano plano, Cartao cartao)
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
    }
}
