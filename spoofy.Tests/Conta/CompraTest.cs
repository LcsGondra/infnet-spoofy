using spoofy.domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace spoofy.Tests.Conta
{
    public class AutorizarCompra
    {
        [Fact]
        public void Inativo()
        {
            var cartao = new Cartao
            {
                LimiteAtual = 2000.0M,
                LimiteMax = 2000.0M,
                Ativo = false,
                Compras = new List<Compra>()
            };
            Assert.Equal("RECUSADA", cartao.AutorizarCompra(500, DateTime.Now, "lojas americanas").Status);
        }
    }
}

