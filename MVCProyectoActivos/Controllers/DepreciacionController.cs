using ApplicationCore.Services;
using Infrastructure.Models;
using MVCProyectoActivos.Security;
using MVCProyectoActivos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCProyectoActivos.Controllers
{
    public class DepreciacionController : Controller
    {

        public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3 }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: Depreciacion
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult ListActivos()
        {
            IEnumerable<Activos> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceActivos _ServiceActivos = new ServiceActivos();
                lista = _ServiceActivos.GetActivos();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }




        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult ListDepreciacion(int id)
        {

            IEnumerable<Depreciacion> lista = null;


            //ViewBag para luego usarlo en el jquery script al cambiar la fecha
            TempData["IdActivo"] = id.ToString();
            TempData.Keep();

            try
            {
                Log.Info("Visita");


                IServiceDepreciacion _ServiceDepreciacion = new ServiceDepreciacion();

                //Seleccionamos toda la depreciacion del Activo y la incluimos en la lista depreciacion del Activo
                lista = _ServiceDepreciacion.GetDepreciacionByActivo(id);


            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return PartialView("_ListDepreciacion", lista);
        }


        //[HttpPost]

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult DepreciacionByDate(int ActivoID, DateTime FechaDepreciacion)
        {

            IEnumerable<Depreciacion> lista = null;
            Depreciacion oDepreciacion = new Depreciacion();

            try
            {
                Log.Info("Visita");


                IServiceDepreciacion _ServiceDepreciacion = new ServiceDepreciacion();

                oDepreciacion = _ServiceDepreciacion.SaveTransaccion(ActivoID, FechaDepreciacion);

                if(oDepreciacion != null) {
                    //Looking for "Depreciacion" we just added in order to show it in the partial view
                    lista = _ServiceDepreciacion.GetDepreciacionByID(oDepreciacion.DepreciacionID);
                }

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return PartialView("_ListDepreciacion", lista);
        }



        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult FilterDepreciacionView(int idActivo)
        {

            ViewModelDepreciacion oViewModelDepreciacion = new ViewModelDepreciacion();

            try
            {
                Log.Info("Visita");


                IServiceActivos _ServiceActivo = new ServiceActivos();

                oViewModelDepreciacion.ActivoID = idActivo;

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View("FilterDepreciacionView", oViewModelDepreciacion);
        }






    }
}