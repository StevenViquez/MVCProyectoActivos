using ApplicationCore.Services;
using Infrastructure.Models;
using MVCProyectoActivos.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCProyectoActivos.Controllers
{
    public class MarcaController : Controller
    {
        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
         public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}

        // GET: Bodega
        [CustomAuthorize((int)Roles.Administrador)]
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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceMarca _ServiceMarca = new ServiceMarca();
                lista = _ServiceMarca.GetMarca();
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Marca marca)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceMarca _ServiceMarca = new ServiceMarca();
                    _ServiceMarca.Save(marca);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", marca);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            ServiceMarca _ServiceMarca = new ServiceMarca();
            Marca marca = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                marca = _ServiceMarca.GetMarcaByID(id.Value);

                return View(marca);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceMarca _ServiceMarca = new ServiceMarca();
            Marca marca = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                marca = _ServiceMarca.GetMarcaByID(id.Value);
                // Response.StatusCode = 500;
                return View(marca);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            return View();
        }


        // GET: Marca/Delete/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int? id)
        {
            IServiceMarca _ServiceMarca = new ServiceMarca();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceMarca.DeleteMarca(id.Value);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();

                    IServiceMarca _ServiceMarca1 = new ServiceMarca();
                    ViewBag.ListaMarca = _ServiceMarca1.GetMarca();
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult DeleteConfirmed(int? id)
        {
            ServiceMarca _ServiceMarca = new ServiceMarca();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceMarca.DeleteMarca(id.Value);

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