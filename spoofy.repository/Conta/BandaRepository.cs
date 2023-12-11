using spoofy.domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace spoofy.repository.Conta
{
    public class BandaRepository
    {
        private HttpClient HttpClient { get; set; }

        public BandaRepository()
        {
            this.HttpClient = new HttpClient();
        }

        public async Task<Musica> ObterMusica(Guid id)
        {
            var result = await HttpClient.GetAsync($"http://localhost:5115/api/banda/musica/{id}");

            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Musica>(content);

        }

    }
}
