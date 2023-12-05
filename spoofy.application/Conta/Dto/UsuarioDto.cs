using spoofy.application.Streaming.Dto;
using spoofy.domain.Conta;
using spoofy.domain.Conta.Aggregates;
using spoofy.domain.Streaming;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.application.Conta.Dto
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String CPF { get; set; }
        [Required]
        public Guid PlanoId { get; set; }
        public CartaoDto Cartao { get; set; }
        public List<PlaylistDto> Playlists { get; set; }
    }

    public class CartaoDto
    {
        [Required]
        public String Numero { get; set; }
        [Required]
        public Decimal LimiteAtual { get; set; }
        [Required]
        public Boolean Ativo { get; set; }
    }

    public class PlaylistDto
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public Boolean Publica { get; set; }
        public List<MusicaDto> Musicas { get; set; }
    }
}
