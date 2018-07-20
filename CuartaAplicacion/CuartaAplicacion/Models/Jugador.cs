using System;
using System.Collections.Generic;
using System.Text;

namespace PrimerAplicacion.Model
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Foto { get; set; }
        public string Pais { get; set; }
        public string Bandera { get; set; }

        override public string ToString()
        {
            return string.Format("{0} {1} nació en {2}.", Nombre, Apellido, Pais);
        }
    }
}