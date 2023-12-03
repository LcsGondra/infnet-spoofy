using spoofy.domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.application.Service
{
    public class CartaoService
    {
        public List<Cartao> CartaoDb { get; set; }
        public void CriarCartao(Guid contaId, decimal limiteAtual, decimal limiteMax, string numero, string cvv, DateTime validade)
        {
            var cartao = new Cartao
            {
                Id = new Guid(),
                ContaID = contaId,
                LimiteAtual = limiteAtual,
                LimiteMax = limiteMax,
                Numero = numero,
                Cvv = cvv,
                Validade = validade,
                Ativo = false
            };

            CartaoDb.Add(cartao);
        }
    }
}
