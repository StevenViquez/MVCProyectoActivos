using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAsegurador : IServiceAsegurador
    {

        public void DeleteAsegurador(int id)
        {
            IRepositoryAsegurador repository = new RepositoryAsegurador();
            repository.DeleteAsegurador(id);
        }

        public IEnumerable<Asegurador> GetAsegurador()
        {
            IRepositoryAsegurador repository = new RepositoryAsegurador();
            return repository.GetAsegurador();
        }

        public Asegurador GetAseguradorByID(int id)
        {
            RepositoryAsegurador repository = new RepositoryAsegurador();
            return repository.GetAseguradorByID(id);
        }

        public Asegurador Save(Asegurador asegurador)
        {
            RepositoryAsegurador repository = new RepositoryAsegurador();
            return repository.Save(asegurador);
        }

    }
}
