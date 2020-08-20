using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDepreciacion : IServiceDepreciacion
    {
        public IEnumerable<Depreciacion> GetDepreciacionByActivo(int activo)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.GetDepreciacionByActivo(activo);
        }

        public void DeleteDepreciacion(int idActivo)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            repository.DeleteDepreciacion(idActivo);
        }

        public Depreciacion SaveTransaccion(int activo, DateTime date)
        {
            int diferenciaMeses;
            IEnumerable<Depreciacion> lista = null;

            RepositoryDepreciacion repository = new RepositoryDepreciacion();
            RepositoryActivos repositoryActivo = new RepositoryActivos();
            Activos oActivo = repositoryActivo.GetActivoByID(activo);
            Depreciacion oDepreciacion = new Depreciacion();
            int valorDepreciacion = 0;

            ////Averiguando los meses que han pasado desde la compra al dia de hoy cuando lo guardamos en el sistema
            ///Recibimos date como parametro
            lista = repository.GetDepreciacionByActivo(activo).ToList();

            foreach (var item in lista)
            {
                    if (((item.Fecha.Month) == (date.Month)) && ((item.Fecha.Year) == (date.Year))) // si el mes es diferente
                    {
                            //return item
                            return item;
                    }
            }


            //Calculando la diferencia de meses
            diferenciaMeses = (date.Month - oActivo.FechaCompra.Month) + 12 * (date.Year - oActivo.FechaCompra.Year);


            ////Calculando depreciacion por mes ==> Multiplicando depreciacion por los meses actuales para saber el valor actual 
            ////Debo cambiar el valor en la base de datos a decimal!!!!
            ////(activos.CostoColones / (activos.VidaUtil * 12) ===> Averiguamos cuanto se gasta por mes * la difernecia de meses
            //// Le restamos el costo total a el valor actual ==> activos.CostoColones(necesito cambiar ValorActual a decimal en la DB)
            valorDepreciacion = Convert.ToInt32(oActivo.CostoColones - ((oActivo.CostoColones / (oActivo.VidaUtil * 12) * diferenciaMeses)));


            oDepreciacion.Valor = valorDepreciacion.ToString();
            oDepreciacion.Activo = oActivo.ActivoID;
            oDepreciacion.Fecha = date;

            return oDepreciacion = repository.SaveTransaccion(oDepreciacion);


        }


        public IEnumerable<Depreciacion> GetDepreciacionByID(int id)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();

            return repository.GetDepreciacionByID(id);
        }

    }
}
