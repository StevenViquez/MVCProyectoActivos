using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    interface IServiceMarca
    {
        IEnumerable<Marca> GetMarca();
        Marca GetMarcaByID(int id);
        void DeleteMarca(int id);
        Marca Save(Marca marca);
    }
}
