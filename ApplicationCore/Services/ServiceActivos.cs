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

        public Activos SaveTransaccion(Activos activos)
        {
            int diferenciaMeses;
            
            RepositoryActivos repository = new RepositoryActivos();

            //Averiguando los meses que han pasado desde la compra al dia de hoy cuando lo guardamos en el sistema
            diferenciaMeses = (DateTime.Now.Month - activos.FechaCompra.Month) + 12 * (DateTime.Now.Year - activos.FechaCompra.Year);
            //Sacando depreciacion por mes ==> Multiplicando depreciacion por los meses actuales para saber el valor actual 
            //Debo cambiar el valor en la base de datos a decimal!!!!

            //(activos.CostoColones / (activos.VidaUtil * 12) ===> Averiguamos cuanto se gasta por mes * la difernecia de meses
            // Le restamos el costo total a el valor actual ==> activos.CostoColones(necesito cambiar ValorActual a decimal en la DB)
            activos.ValorActual =  Convert.ToInt32(activos.CostoColones - ((activos.CostoColones/(activos.VidaUtil * 12) * diferenciaMeses)));
            
            //Repository save debe tener una transaccion para agregar el valor actual en la tabla depreciacion de una vez
            return repository.SaveTransaccion(activos);
        }
    }
}
