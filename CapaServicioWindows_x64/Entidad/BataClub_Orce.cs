using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows_x64.Entidad
{
   public  class BataClub_Orce
    {
    }
    public class BataClub_Cliente_Orce
    {
        public string dni { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string genero { get; set; }
        public string correo { get; set; }
        public DateTime? fec_nac { get; set; }
        public string telefono { get; set; }
        public string ubigeo { get; set; }
        public string cod_tda { get; set; }
        public Boolean miem_bataclub { get; set; }
        public string num_tarjeta { get; set; }
        /*variables para sumar puntos*/
        public string id { get; set; }
        public string documento { get; set; }
        public decimal total { get; set; }
        public decimal monto_punto { get; set; }
        public DateTime fecha_transac { get; set; }
    }
}
