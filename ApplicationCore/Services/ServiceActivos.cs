using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceActivos : IServiceActivos
    {
        public void DeleteActivo(int id)
        {
            IRepositoryActivos repository = new RepositoryActivos();
            repository.DeleteActivo(id);
        }

        public IEnumerable<Activos> GetActivos()
        {
            IRepositoryActivos repository = new RepositoryActivos();
            return repository.GetActivos();
        }

        public Activos GetActivoByID(int id)
        {
            RepositoryActivos repository = new RepositoryActivos();
            return repository.GetActivoByID(id);
        }

        public Activos Save(Activos activos)
        {
            RepositoryActivos repository = new RepositoryActivos();
            return repository.Save(activos);
        }
    }
}
