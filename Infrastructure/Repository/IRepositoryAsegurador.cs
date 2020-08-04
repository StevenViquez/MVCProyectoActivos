using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryAsegurador
    {
        IEnumerable<Asegurador> GetAsegurador();
        Asegurador GetAseguradorByID(int id);
        void DeleteAsegurador(int id);
        Asegurador Save(Asegurador asegurador);
    }
}
