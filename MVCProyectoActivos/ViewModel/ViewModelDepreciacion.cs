using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProyectoActivos.ViewModel
{
    public class ViewModelDepreciacion
    {
        [DataType(DataType.Date)]
        public System.DateTime FechaDepreciacion { get; set; }

        public int ActivoID { get; set; }
    }
}