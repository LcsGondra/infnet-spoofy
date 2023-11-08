using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.domain.Conta
{
    public class Cartao
    {
        public Guid Id { get; set; }
        [ForeignKey("Conta")]
        public Guid ContaID { get; set; }
        public virtual Conta Conta { get; set; }
        public decimal LimiteAtual { get; set; }
        public decimal LimiteMax { get; set; }
        public string Numero { get; set; }
        public string Cvv { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/yyyy}")]
        public DateTime Validade { get; set; }
        public Boolean Ativo { get; set; }
        public List<Compra> compras { get; set; }

        private Boolean EstaAtivo(Compra compra)
        {
            if (!Ativo)
            {
                compra.Status = "RECUSADA";
                return false;
            }
            return true;
        }
        private Boolean DentroDoLimite(Compra compra, decimal valor)
        {
            if (LimiteAtual < valor)
            {
                compra.Status = "RECUSADA";
                return false;
            }
            return true;
        }
        public Compra AutorizarCompra(decimal valor, DateTime dataHora, string comerciante)
        {

            var compra = new Compra
            {
                Id = Guid.NewGuid(),
                Valor = valor,
                DataHora = dataHora,
                Comerciante = comerciante
            };
            //ta cartao ta ativo? dentro do limite? compra ta repitida?
            if (!EstaAtivo(compra)) return compra;
            if (!DentroDoLimite(compra, valor)) return compra;

            var ultimaCompra = compras[compras.Count() - 1];
            var penultimaCompra = compras[compras.Count() - 2];


            if ((dataHora.Subtract(ultimaCompra.DataHora) - ultimaCompra.DataHora.Subtract(penultimaCompra.DataHora)).TotalSeconds < 120)
            {
                compra.Status = "RECUSADA";
                return compra;
            }
            else if ((ultimaCompra.Valor == valor) && (ultimaCompra.Comerciante == comerciante) && (dataHora.Subtract(ultimaCompra.DataHora).TotalSeconds < 120))
            {
                compra.Status = "RECUSADA";
                return compra;
            }

            compra.Status = "APROVADA";
            LimiteAtual -= valor;
            return compra;
        }
    }
}