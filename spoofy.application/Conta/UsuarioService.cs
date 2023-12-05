using spoofy.application.Conta.Dto;
using spoofy.application.Streaming;
using spoofy.application.Streaming.Dto;
using spoofy.core;
using spoofy.domain.Conta.Aggregates;
using spoofy.repository.Conta;
using spoofy.repository.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.application.Conta
{
    public class UsuarioService
    {
        private PlanoRepository planoRepository =  new PlanoRepository();
        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        private BandaService bandaService = new BandaService();
        public UsuarioDto CriarConta(UsuarioDto conta)
        {
            //todo: verificar e pegar plano
            Plano plano = this.planoRepository.PlanoPorId(conta.PlanoId);

            if (plano == null)
            {
                new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Plano nao encontrado",
                    ErrorName = nameof(CriarConta)
                }).ValidateAndThrow();
            }

            Cartao cartao = new Cartao();
            cartao.Ativo = conta.Cartao.Ativo;
            cartao.Numero = conta.Cartao.Numero;
            cartao.LimiteAtual = conta.Cartao.LimiteAtual;

            //criar usuario
            Usuario usuario = new Usuario();
            usuario.Criar(conta.Nome, conta.CPF, plano, cartao);

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
                    Ativo = usuario.Cartoes.FirstOrDefault().Ativo,
                    LimiteAtual = usuario.Cartoes.FirstOrDefault().LimiteAtual,
                    Numero = "xxxx-xxxx-xxxx-xxxx"
                },
                CPF = usuario.CPF.NumeroFormatado(),
                Nome = usuario.Nome,
                Playlists = new List<PlaylistDto>(),
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
            return result;
        }

        public void FavoritarMusica(Guid id, Guid idMusica)
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
            
            var musica = bandaService.ObterMusica(idMusica);

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

        //public UsuarioDto UpdateUsuario(Guid id)
        //{
        //    var usuario = this.usuarioRepository.ObterUsuario(id);

        //    if (usuario == null)
        //        return null;

        //    UsuarioDto result = new UsuarioDto()
        //    {
        //        Id = usuario.Id,
        //        Cartao = new CartaoDto()
        //        {
        //            Ativo = usuario.Cartoes.FirstOrDefault().Ativo,
        //            LimiteAtual = usuario.Cartoes.FirstOrDefault().LimiteAtual,
        //            Numero = "xxxx-xxxx-xxxx-xxxx"
        //        },
        //        CPF = usuario.CPF.NumeroFormatado(),
        //        Nome = usuario.Nome,
        //    };

        //    usuarioRepository.SalvarUsuario(usuario);
        //    return result;
        //}
    }
}
