using CapaDato.Basico;
using CapaEntidad.Util;
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
            List<Ent_PathDBF> path_server = null;
            try
            {

                get_path = new Dat_Util();
                path_server = get_path.get_ruta_locationProcesa_dbf("T");      
                
                if (path_server!=null)
                {
                    foreach(var item in path_server)
                    {
                        string _archivo_ruta = "";
                        /*si es venta de procesos*/
                        if (item.rutloc_namedbf.Substring(0,1)=="V")
                        {
                            _archivo_ruta = item.rutloc_location + "\\" + _tienda_archivo;
                            File.WriteAllBytes(_archivo_ruta, _archivo_zip);
                        }
                        else
                        {
                            /*en este caso vamos a generar casilla*/
                            //string folder_td="TD" + _tienda_archivo.
                            //_archivo_ruta = item.rutloc_location;
                            /**/
                        }
                    }
                }          
                
                //if (_ruta.Length>0)
                //{
                //    string _archivo_ruta = _ruta + "\\" + _tienda_archivo;
                //    File.WriteAllBytes(_archivo_ruta, _archivo_zip);
                //}

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
