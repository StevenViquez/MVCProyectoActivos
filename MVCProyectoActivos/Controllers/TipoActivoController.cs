using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCProyectoActivos.Controllers
{
    public class TipoActivoController : Controller
    {
        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
        // public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}
        //[CustomAuthorize((int)Roles.Administrador)]
        // GET: Bodega
        public ActionResult Index()
        {
            try
            {

                // Forzar un error 
                // int c = 0;
                // int x = c / 0;
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
        {
            IEnumerable<TipoActivo> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                lista = _ServiceTipoActivo.GetTipoActivo();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(TipoActivo tipoActivo)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                    _ServiceTipoActivo.Save(tipoActivo);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", tipoActivo);
                }

                // redirigir
                return RedirectToAction("List");
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
        }


        // GET: Bodega/Details/5      
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
            TipoActivo tipoActivo = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                tipoActivo = _ServiceTipoActivo.GetTipoActivoByID(id.Value);

                return View(tipoActivo);
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
        }

        // GET: Bodega/Edit/5
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
            TipoActivo tipoActivo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                tipoActivo = _ServiceTipoActivo.GetTipoActivoByID(id.Value);
                // Response.StatusCode = 500;
                return View(tipoActivo);
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
        }


        // GET: Bodega/Create
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            return View();
        }


        // GET: TipoActivo/Delete/5
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int? id)
        {
            IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceTipoActivo.DeleteTipoActivo(id.Value);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();

                    IServiceTipoActivo _ServiceTipoActivo1 = new ServiceTipoActivo();
                    ViewBag.ListaTipoActivo = _ServiceTipoActivo1.GetTipoActivo();
                    return View("List");
                }

                // redirigir
                return RedirectToAction("List");
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult DeleteConfirmed(int? id)
        {
            ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceTipoActivo.DeleteTipoActivo(id.Value);

                return RedirectToAction("List");
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
        }
    }
}