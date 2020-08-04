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
    public class AseguradorController : Controller
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
            IEnumerable<Asegurador> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
                lista = _ServiceAsegurador.GetAsegurador();
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
        public ActionResult Save(Asegurador asegurador)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
                    _ServiceAsegurador.Save(asegurador);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", asegurador);
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
            ServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
            Asegurador asegurador = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                asegurador = _ServiceAsegurador.GetAseguradorByID(id.Value);

                return View(asegurador);
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
            IServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
            Asegurador asegurador = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                asegurador = _ServiceAsegurador.GetAseguradorByID(id.Value);
                // Response.StatusCode = 500;
                return View(asegurador);
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


        // GET: Asegurador/Delete/5
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int? id)
        {
            IServiceAsegurador _ServiceProducto = new ServiceAsegurador();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceProducto.DeleteAsegurador(id.Value);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();

                    IServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
                    ViewBag.ListaAsegurador = _ServiceAsegurador.GetAsegurador();
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
            ServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceAsegurador.DeleteAsegurador(id.Value);

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