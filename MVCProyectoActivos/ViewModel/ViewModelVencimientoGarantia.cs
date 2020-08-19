using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProyectoActivos.ViewModel
{
    public class ViewModelVencimientoGarantia
    {
        [DataType(DataType.Date)]
        public System.DateTime FechaGarantiaRangoInicial { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime FechaGarantiaRangoFinal{ get; set; }

        public int ActivoID { get; set; }
    }
}