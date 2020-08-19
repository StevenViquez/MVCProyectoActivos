using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceDepreciacion
    {
        IEnumerable<Depreciacion> GetDepreciacionByActivo(int activo);

        void DeleteDepreciacion(int idActivo);

        Depreciacion SaveTransaccion(int activo, DateTime date);

        IEnumerable<Depreciacion> GetDepreciacionByID(int id);
    }
}
