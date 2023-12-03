using spoofy.domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.repository.Streaming
{
    public class PlanoRepository
    {
        private static List<Plano> Plano;
        public PlanoRepository()
        {
            if (Plano == null)
            {
                Plano = new List<Plano>();
                Plano.Add(new Plano()
                {
                    Descricao = "Plano Basico",
                    Nome = "Plano Basico",
                    Valor = 19.90M,
                    Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
                });
            }
        }

        public Plano PlanoPorId(Guid id)
        {
            return Plano.FirstOrDefault(x => x.Id == id);
        }
    }
}
