using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceActivos
    {
        IEnumerable<Activos> GetActivos();
        Activos GetActivoByID(int id);
        void DeleteActivo(int id);
        //Activos Save(Activos activo);

        Activos SaveTransaccion(Activos activo);
    }
}
