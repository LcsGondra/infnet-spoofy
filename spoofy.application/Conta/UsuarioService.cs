using spoofy.application.Conta.Dto;
using spoofy.application.Conta;
using spoofy.application.Conta.Dto;
using spoofy.core;
using spoofy.domain.Conta.Aggregates;
using spoofy.repository.Conta;
using spoofy.repository.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace spoofy.application.Conta
{
    public class UsuarioService
    {
        private PlanoRepository planoRepository =  new PlanoRepository();
        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        private BandaRepository bandaRepository = new BandaRepository();
        public async Task<UsuarioDto> CriarConta(UsuarioDto conta)
        {
            Plano plano = await this.planoRepository.PlanoPorId(conta.PlanoId);

            if (plano == null)
            {
                new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Plano nao encontrado",
                    ErrorName = nameof(CriarConta)
                }).ValidateAndThrow();
            }
            Playlist playlist = new Playlist();
            playlist.Nome = "Favoritas";
            playlist.Publica = false;
            playlist.Id = Guid.NewGuid();
            Cartao cartao = new Cartao();
            cartao.Ativo = conta.Cartao.Ativo;
            cartao.Numero = conta.Cartao.Numero;
            cartao.LimiteAtual = conta.Cartao.LimiteAtual;

            //criar usuario
            Usuario usuario = new Usuario();
            usuario.Criar(conta.Nome, conta.CPF, plano, cartao, playlist);

            //gravar usuario na base
            this.usuarioRepository.SalvarUsuario(usuario);
            conta.Id = usuario.Id;
            
            //retornar conta gerada
            return conta;
        }

        public UsuarioDto ObterUsuario(Guid id)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
                return null;

            UsuarioDto result = new UsuarioDto()
            {
                Id = usuario.Id,
                Cartao = new CartaoDto()
                {
                    Id = usuario.Cartoes.FirstOrDefault().Id,
                    Ativo = usuario.Cartoes.FirstOrDefault().Ativo,
                    LimiteAtual = usuario.Cartoes.FirstOrDefault().LimiteAtual,
                    Numero = "xxxx-xxxx-xxxx-xxxx"
                },
                CPF = usuario.CPF.NumeroFormatado(),
                Nome = usuario.Nome,
                Playlists = new List<PlaylistDto>(),
                Assinaturas = new List<AssinaturaDto>(),
                PlanoId = usuario.Assinaturas.FirstOrDefault(x => x.Ativo == true).Plano.Id
            };

            foreach (var item in usuario.Playlists)
            {
                var playList = new PlaylistDto()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Publica = item.Publica,
                    Musicas = new List<MusicaDto>()
                };

                foreach (var musicas in item.Musicas)
                {
                    playList.Musicas.Add(new MusicaDto()
                    {
                        Duracao = musicas.Duracao,
                        Id = musicas.Id,
                        Nome = musicas.Nome
                    });
                }

                result.Playlists.Add(playList);
            }

            foreach (var item in usuario.Assinaturas)
            {
                var assinatura = new AssinaturaDto()
                {
                    Id = item.Id,
                    Ativo = item.Ativo,
                    DtAssinatura = item.DtAssinatura,
                    Plano = new PlanoDto()
                    {
                        Id = item.Plano.Id,
                        Nome = item.Plano.Nome,
                        Descricao = item.Plano.Descricao,
                        Valor = item.Plano.Valor
                    }
                };
                result.Assinaturas.Add(assinatura);
            }
            return result;
        }

        public async Task FavoritarMusica(Guid id, Guid idMusica)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Nao encontrei usuario",
                    ErrorName = nameof(FavoritarMusica)
                });
            }
            
            var musica = await bandaRepository.ObterMusica(idMusica);

            if (musica == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Nao encontrei musica",
                    ErrorName = nameof(FavoritarMusica)
                });
            }

            usuario.Favoritar(musica);

            usuarioRepository.Update(usuario);

        }
        public async Task AdicionarMusica(Guid id, Guid idMusica, Guid idPlaylist)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Nao encontrei usuario",
                    ErrorName = nameof(AdicionarMusica)
                });
            }

            var musica = await bandaRepository.ObterMusica(idMusica);

            if (musica == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Nao encontrei musica",
                    ErrorName = nameof(AdicionarMusica)
                });
            }

            usuario.AddtoPlaylist(musica, idPlaylist);

            usuarioRepository.Update(usuario);

        }
        public async Task NovaPlaylist(Guid id, PlaylistDto dto)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Não encontrei o usuário",
                    ErrorName = nameof(NovaPlaylist)
                });
            }

            var playList = new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Publica = dto.Publica,
                Musicas = new List<Musica>()
            };

            foreach (var musicas in dto.Musicas)
            {
                playList.Musicas.Add(new Musica()
                {
                    Duracao = musicas.Duracao,
                    Id = musicas.Id,
                    Nome = musicas.Nome
                });
            }
        
            usuario.CriarPlaylist(playList);

            usuarioRepository.Update(usuario);
        }

        public void UpdateNomeUsuario(Guid id, NomeDto dto)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Não encontrei o usuário",
                    ErrorName = nameof(FavoritarMusica)
                });
            }

            usuario.MudarNome(dto.Nome);
            usuarioRepository.Update(usuario);
        }

        public async Task NovaAssinatura(Guid id, CompraDto dto)
        {
            var usuario = usuarioRepository.ObterUsuario(id);

            if (usuario == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Não encontrei o usuário",
                    ErrorName = nameof(NovaAssinatura)
                });
            }
            var plano = await planoRepository.PlanoPorId(dto.IdPlano);

            if (plano == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Não encontrei o plano",
                    ErrorName = nameof(NovaAssinatura)
                });
            }

            var cartao = usuario.Cartoes.FirstOrDefault(c => c.Id == dto.IdCartao);
            if (cartao == null)
            {
                throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Não encontrei o cartao",
                    ErrorName = nameof(NovaAssinatura)
                });
            }

            usuario.AssinarPlano(plano, cartao);
            usuarioRepository.Update(usuario);
        }
    }
}
