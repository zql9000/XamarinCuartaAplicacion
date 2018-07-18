using System;
using System.Collections.Generic;
using System.Text;

namespace CuartaAplicacion.Models
{
    public class DatosDelTiempo
    {
        public long id { get; set; }
        public string name { get; set; }
        public DatosDelTiempoMain main { get; set; }
    }

    public class DatosDelTiempoMain
    {
        public decimal temp { get; set; }
        public decimal temp_min { get; set; }
        public decimal temp_max { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
    }
}
