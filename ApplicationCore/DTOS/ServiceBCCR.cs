using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOS
{
	public class ServiceBCCR
	{
		private readonly string TOKEN = "QSLIAVZTM1";
		private readonly string NOMBRE = "Steven Viquez";
		private readonly string CORREO = "sviquez48@gmail.com";

		public decimal GetDolar(DateTime pFechaInicial)
		{

			DataSet dataset = null;
			string fecha_inicial, fecha_final;
			string tipoCompraoVenta;
			decimal precioDolarActual = 0; 

			// Se convierten las fechas a string en el formato solicitado
			fecha_inicial = pFechaInicial.ToString("dd/MM/yyyy");
			fecha_final = fecha_inicial;

			// se compara si es compra o venta 318 o 317
			tipoCompraoVenta = "317";

			// Protocolo de comunicaciones
			System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

			// Se instancia al Servicio Web
			BCCR.wsindicadoreseconomicosSoapClient client =
				new BCCR.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");

			// Se invoca.
			dataset = client.ObtenerIndicadoresEconomicos(tipoCompraoVenta, fecha_inicial,
						  fecha_final, NOMBRE, "N", CORREO, TOKEN);

			DataTable table = dataset.Tables[0];

			foreach (DataRow row in table.Rows)
			{
				// Validar el error. No es la forma correcta pero bueno.
				if (row[0].ToString().Contains("error"))
				{
					throw new Exception(row[0].ToString());
				}

				precioDolarActual = Convert.ToDecimal(row[2].ToString());
			}

			return precioDolarActual;
		}

	}
}
