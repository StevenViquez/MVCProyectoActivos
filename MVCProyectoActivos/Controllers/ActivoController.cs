using ApplicationCore.DTOS;
using ApplicationCore.Services;
using Infrastructure.Models;
using MVCProyectoActivos.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCProyectoActivos.Controllers
{
    public class ActivoController : Controller
    {
        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
        public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}
        [CustomAuthorize((int)Roles.Administrador)]
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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
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

        [HttpPost]  
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Activos activos, HttpPostedFileBase ImageFile)
        {
            string errores = "";
            MemoryStream target = new MemoryStream();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if ((activos.FotoActivo == null) && (activos.FotoFactura == null))
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        activos.FotoActivo = target.ToArray();
                        activos.FotoFactura = target.ToArray();
                        ModelState.Remove("FotoActivo");
                        ModelState.Remove("FotoFactura");
                    }

                }

                // Es valido
                if (ModelState.IsValid)
                {
                    IServiceActivos _ServiceActivos = new ServiceActivos();
                    _ServiceActivos.SaveTransaccion(activos);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    IServiceMarca _ServiceMarca = new ServiceMarca();
                    ViewBag.ListaMarca = _ServiceMarca.GetMarca();

                    IServiceVendedor _ServiceVendedor = new ServiceVendedor();
                    ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();

                    IServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
                    ViewBag.ListaAsegurador = _ServiceAsegurador.GetAsegurador();

                    IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                    ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();

                    return View("Create", activos);
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
            ServiceActivos _ServiceActivos = new ServiceActivos();
            Activos activos = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                activos = _ServiceActivos.GetActivoByID(id.Value);

                return View(activos);
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
            IServiceActivos _ServiceActivos = new ServiceActivos();
            Activos activos = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                activos = _ServiceActivos.GetActivoByID(id.Value);
                IServiceMarca _ServiceMarca = new ServiceMarca();
                ViewBag.ListaMarca = _ServiceMarca.GetMarca();

                IServiceVendedor _ServiceVendedor = new ServiceVendedor();
                ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();

                IServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
                ViewBag.ListaAsegurador = _ServiceAsegurador.GetAsegurador();

                IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();
                // Response.StatusCode = 500;
                return View(activos);
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
            IServiceMarca _ServiceMarca = new ServiceMarca();
            ViewBag.ListaMarca = _ServiceMarca.GetMarca();

            IServiceVendedor _ServiceVendedor = new ServiceVendedor();
            ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();

            IServiceAsegurador _ServiceAsegurador = new ServiceAsegurador();
            ViewBag.ListaAsegurador = _ServiceAsegurador.GetAsegurador();

            IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
            ViewBag.ListaTipoActivo = _ServiceTipoActivo.GetTipoActivo();

            //IServiceValorActual _ServiceValorActual = new ServiceValorActual();
            //ViewBag.ListaValorActual = _ServiceValorActual.GetValorActual();

            return View();
        }


        // GET: Activos/Delete/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int? id)
        {
            IServiceActivos _ServiceActivos = new ServiceActivos();
            IServiceDepreciacion _ServiceDepreciacion = new ServiceDepreciacion();

            try
            {

                // Es valido
                if (id != null)
                {
                    Activos oActivo = _ServiceActivos.GetActivoByID(id.Value);
                    ////Converting IEnumerable to ICollection
                    //oActivo.Depreciacion = _ServiceDepreciacion.GetDepreciacionByActivo(id.Value).ToList();
                    _ServiceDepreciacion.DeleteDepreciacion(id.Value);
                    _ServiceActivos.DeleteActivo(oActivo);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();

                    IServiceActivos _ServiceActivos1 = new ServiceActivos();
                    ViewBag.ListaActivos = _ServiceActivos1.GetActivos();
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
            ServiceActivos _ServiceActivos = new ServiceActivos();

            try
            {

                if (id == null)
                {
                    return View();
                }

                //_ServiceActivos.DeleteActivo(id.Value);

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
        public ActionResult Dolar(DateTime? fechaDeCompra, decimal? costoColones)
        {
            //Of course you want to authorize the call
            ServiceBCCR oServiceBCCR = new ServiceBCCR();
            decimal dolar = Convert.ToDecimal(costoColones / oServiceBCCR.GetDolar(Convert.ToDateTime(fechaDeCompra)));
            
            if (fechaDeCompra != null)
            {
                return Json(new { Status = "success", Dolar = Math.Round(dolar,2) });
            }
            else
            {
                return Json(new { Status = "error Debe ingresar la fecha de compra" });
            }
        }

    }
}