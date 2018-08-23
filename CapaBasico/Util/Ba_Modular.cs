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
                        if (!Directory.Exists(@item.rutloc_location)) Directory.CreateDirectory(@item.rutloc_location);
                        /*si es venta de procesos SQL SERVER*/
                        if (item.rutloc_namedbf.Substring(0,1)=="S")
                        {
                            
                            _archivo_ruta = item.rutloc_location + "\\" + _tienda_archivo;
                            File.WriteAllBytes(_archivo_ruta, _archivo_zip);
                        }
                        else
                        {
                            /*en este caso vamos a generar casilla*/
                            string folder_td = "TD" + _tienda_archivo.Substring(_tienda_archivo.Length - 3, 3);

                            /*creamos folder para las tiendas*/

                            string ruta_td = @item.rutloc_location + "/" + folder_td;

                            if (!Directory.Exists(@ruta_td)) Directory.CreateDirectory(@ruta_td);

                            string _WX = "WX"  /*envio de paquete de tiendas*/;
                            string _TX = "TX"; /*envio haci tiendas cen*/

                            string folder_wx = item.rutloc_location + "/" + folder_td + "/" + _WX;
                            string folder_tx = item.rutloc_location + "/" + folder_td + "/" + _TX;

                            if (!Directory.Exists(@folder_wx)) Directory.CreateDirectory(@folder_wx);
                            if (!Directory.Exists(@folder_tx)) Directory.CreateDirectory(@folder_tx);

                            if (Directory.Exists(@folder_wx))
                            {
                                _archivo_ruta = folder_wx + "/" + _tienda_archivo;
                                File.WriteAllBytes(_archivo_ruta, _archivo_zip);
                            }


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
