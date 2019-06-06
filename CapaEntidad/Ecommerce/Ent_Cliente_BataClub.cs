using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Ecommerce
{
   
    public class Ent_Cliente_BataClub
    {        
        public string canal { get; set; }
        public string dni { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string apellidoPater { get; set; }
        public string apellidoMater { get; set; }
        public string genero { get; set; }
        public string correo { get; set; }
        public string fecNac { get; set; }
        public string telefono { get; set; }
        public string ubigeo { get; set; }
        public string tienda { get; set; }
        public Boolean registrado { get; set; }
        public Boolean miembro_bataclub { get; set; }       

        public Boolean existe_cliente { get; set; } 
        public string descripcion_error { get; set; }
    }
   
}
