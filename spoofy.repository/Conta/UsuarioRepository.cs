using spoofy.domain.Conta.Aggregates;
using spoofy.domain.Conta.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoofy.repository.Conta
{
    public class UsuarioRepository
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public Usuario ObterUsuario(Guid id)
        {
            return usuarios.FirstOrDefault(x => x.Id == id);
        }

        public void SalvarUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            UsuarioRepository.usuarios.Add(usuario);
        }

        public void UpdateUsuario(Guid id, String nome)
        {
            Usuario usuario = usuarios.FirstOrDefault(x => x.Id == id);
            usuario.Nome = nome;
        }
    }
}
