using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoActivo : IServiceTipoActivo
    {
        public void DeleteTipoActivo(int id)
        {
            IRepositoryTipoActivo repository = new RepositoryTipoActivo();
            repository.DeleteTipoActivo(id);
        }

        public IEnumerable<TipoActivo> GetTipoActivo()
        {
            IRepositoryTipoActivo repository = new RepositoryTipoActivo();
            return repository.GetTipoActivo();
        }

        public TipoActivo GetTipoActivoByID(int id)
        {
            RepositoryTipoActivo repository = new RepositoryTipoActivo();
            return repository.GetTipoActivoByID(id);
        }

        public TipoActivo Save(TipoActivo tipoActivo)
        {
            RepositoryTipoActivo repository = new RepositoryTipoActivo();
            return repository.Save(tipoActivo);
        }
    }
}
