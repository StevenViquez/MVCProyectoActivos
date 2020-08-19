using ApplicationCore.Services;
using Infrastructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MVCProyectoActivos.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCProyectoActivos.Controllers
{
    public class ReporteController : Controller
    {
        public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3 }
        // GET: Reporte
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult ActivosCatalogo()
        {
            IEnumerable<Activos> lista = null;
            try
            {

                IServiceActivos _ServiceActivos = new ServiceActivos();
                lista = _ServiceActivos.GetActivos();
                return View(lista);
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

        /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult CreatePdfActivosCatalogo()
        {
            IEnumerable<Activos> lista = null;
            try
            {
                // Extraer informacion
                IServiceActivos _ServiceActivos = new ServiceActivos();
                lista = _ServiceActivos.GetActivos();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Catálogo de Productos")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 6 columnas 
                Table table = new Table(6, true);


                // los Encabezados
                table.AddHeaderCell("Activo");
                table.AddHeaderCell("Descripción");
                table.AddHeaderCell("Costo en Colones");
                table.AddHeaderCell("Fecha de compra");
                table.AddHeaderCell("Condición de Activo");
                table.AddHeaderCell("Imagen");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.ActivoID.ToString()));
                    table.AddCell(new Paragraph(item.Descripcion.ToString()));
                    table.AddCell(new Paragraph(item.CostoColones.ToString()));
                    table.AddCell(new Paragraph(item.FechaCompra.ToString()));
                    table.AddCell(new Paragraph(item.CondicionActivo.ToString()));

                    // Convierte la imagen que viene en Bytes en imagen para PDF
                    Image image = new Image(ImageDataFactory.Create(item.FotoActivo));
                    // Tamaño de la imagen
                    image = image.SetHeight(75).SetWidth(75);
                    table.AddCell(image);
                }
                doc.Add(table);

                // Calculo del monto total
                //decimal montoTotal = lista.ToList().Sum(k => k.Cantidad * k.Precio);
                // Agrega  el monto total
                // doc.Add(new Paragraph("\n\rMonto total " + montoTotal.ToString("C", CultureInfo.CreateSpecificCulture("cr-CR"))));


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte");

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




    }
}