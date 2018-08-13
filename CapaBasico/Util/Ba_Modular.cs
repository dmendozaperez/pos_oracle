using CapaDato.Basico;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBasico.Util
{
    public class Ba_Modular
    {
        public  string copiar_archivo_tienda_server(byte[] _archivo_zip, string _tienda_archivo)
        {
            string _ruta = "";
            string _error = "";
            Dat_Util get_path = null;
            try
            {
                get_path = new Dat_Util();
                _ruta = get_path.get_ruta_locationProcesa_dbf("VENTA");                
                
                if (_ruta.Length>0)
                {
                    string _archivo_ruta = _ruta + "\\" + _tienda_archivo;
                    File.WriteAllBytes(_archivo_ruta, _archivo_zip);
                }

            }
            catch (Exception exc)
            {
                _error = exc.Message;
                throw;
            }
            return _error;
        }
    }
}
