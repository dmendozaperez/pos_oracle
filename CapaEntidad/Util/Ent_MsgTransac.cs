using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Util
{
    public class Ent_MsgTransac
    {
        /// <summary>
        /// validacion de error de la ws de transacciones valor 0 es existosa y valor 1 es fallido
        /// </summary>
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}
