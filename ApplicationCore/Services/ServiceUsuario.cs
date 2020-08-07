using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public void DeleteUsuario(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            repository.DeleteUsuario(id);
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario();
        }

        public Usuario GetUsuarioByID(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioByID(id);
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.Save(usuario);
        }

        public Usuario GetUsuario(string id, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            // Se encripta el valor que viene y se compara con el valor encriptado en al BD.
            //string crytpPasswd = Cryptography.EncrypthAES(password);

            return repository.GetUsuario(id, password);

        }
    }
}
