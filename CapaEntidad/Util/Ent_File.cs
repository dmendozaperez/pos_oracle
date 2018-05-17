using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Util
{
    public class Ent_File
    {
        public string file_name { get; set; }
    }
    public class Ent_Lista_File
    {
        public Ent_File[] lista_file_name { get; set; }
    }
    public class Ent_File_Ruta
    {
        public string file_origen { get; set; }
        public string file_destino { get; set; }
    }
}
