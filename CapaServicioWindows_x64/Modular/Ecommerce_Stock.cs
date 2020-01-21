using CapaServicioWindows_x64.CapaDato;
using CapaServicioWindows_x64.Conexion;
using CapaServicioWindows_x64.Entidad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicioWindows_x64.Modular
{
    public class Ecommerce_Stock
    {
        public  Boolean valida_time;
        public static Int32 intervalo_min;
        public static DateTime activa_fecha_ini;
        public static DateTime activa_fecha_fin;
        public static string ejecuciontime = "";
        public  void envio_stock(ref string enviando_stk)
        {
            try
            {
                

                /*si la variable es false entonces verifica el intervalo*/
                if (!valida_time)
                {
                    valida_time = true;
                    intervalo_min = timer_intervalo_min();
                    activa_fecha_ini = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    activa_fecha_fin = activa_fecha_ini.AddMinutes(intervalo_min);
                    ejecuciontime = "NO EJECUTANDO";
                }
                else
                {
                    DateTime fecha_hora_actual = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                    if (fecha_hora_actual >= activa_fecha_fin)
                    {
                        string error = "";
                        ejecutar_genera_file_ecommerce_auto(ref error);
                        ejecuciontime = "EJECUTANDO PROCESO";
                        enviando_stk = "Enviando Stock E-Commerce";
                        valida_time = false;
                    }


                }
            }
            catch(Exception exc)
            {


            }
        }
        public  Int32 timer_intervalo_min()
        {
            Int32 timer_min = 0;
            BataTransac.ValidateAcceso header_user = null;

            BataTransac.Bata_TransactionSoapClient batatran = null;
            try
            {
                header_user = new BataTransac.ValidateAcceso();
                header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
                header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

                batatran = new BataTransac.Bata_TransactionSoapClient();
                var intervalo = batatran.ws_get_time_servicetrans(header_user, "02");
                if (intervalo != null)
                {
                    timer_min = intervalo.cser_min;
                }

            }
            catch
            {
                timer_min = 0;
            }
            return timer_min;
        }
        public void ejecutar_genera_file_ecommerce_auto(ref string error)
        {
            StreamWriter tw1 = null;
            Ent_Ecommerce_Ruta rut_ec = null;
            Dat_Ecommerce_Stock dat_stk = null;
            string nom_file_ecommerce = "STOCK_ECOMMERCE";
            try
            {
                dat_stk = new Dat_Ecommerce_Stock();
                rut_ec = dat_stk.get_ruta_ec();

                if (rut_ec!=null)
                {
                    if (!Directory.Exists(@rut_ec.ruta_ec)) Directory.CreateDirectory(@rut_ec.ruta_ec);
                    genera_interface_ecommerce(rut_ec.ruta_ec, nom_file_ecommerce);
                }
               
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
        }

        public void genera_interface_ecommerce(string _ruta_ec,string _nom_file)
        {
            Dat_Ecommerce_Stock dat_stk = null;
            StringBuilder str = null;
            string str_cadena = "";
            try
            {
              
                dat_stk = new Dat_Ecommerce_Stock();
                List<Ent_Ecommerce_Stock> stk = dat_stk.lista_stock(); 

                if (stk!=null)
                {                   
                    if (stk.Count>0)
                    {
                        str = new StringBuilder();
                        Int32 s = 0;
                        foreach(Ent_Ecommerce_Stock dat in stk)
                        {
                            str.Append(dat.STK_EC.ToString());

                            if (s< stk.Count - 1)
                            {
                                str.Append("\r\n");
                            }
                            s += 1;
                        }
                        str_cadena = str.ToString();

                        string in_stk_ec = _ruta_ec + "\\" + _nom_file + ".txt";

                        if (File.Exists(@in_stk_ec)) File.Delete(@in_stk_ec);
                        File.WriteAllText(@in_stk_ec, str_cadena);

                    }
                }

              
            }
            catch (Exception exc)
            {

                
            }
        }

    }
}
