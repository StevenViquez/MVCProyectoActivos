using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryDepreciacion
    {
        IEnumerable<Depreciacion> GetDepreciacionByActivo(int activo);

        void DeleteDepreciacion(int idActivo);

        IEnumerable<Depreciacion> GetDepreciacionByID(int id);
        Depreciacion SaveTransaccion(Depreciacion depreciacion);
    }
}
