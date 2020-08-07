using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMarca : IServiceMarca
    {
        public void DeleteMarca(int id)
        {
            IRepositoryMarca repository = new RepositoryMarca();
            repository.DeleteMarca(id);
        }

        public IEnumerable<Marca> GetMarca()
        {
            IRepositoryMarca repository = new RepositoryMarca();
            return repository.GetMarca();
        }

        public Marca GetMarcaByID(int id)
        {
            RepositoryMarca repository = new RepositoryMarca();
            return repository.GetMarcaByID(id);
        }

        public Marca Save(Marca marca)
        {
            RepositoryMarca repository = new RepositoryMarca();
            return repository.Save(marca);
        }
    }
}
