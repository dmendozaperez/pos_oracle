using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicioWindows_x64.Conexion
{
    public class ConexionOrce
    {
        public static string ORG
        {           
            get { return ConfigurationManager.ConnectionStrings["ORCE_ORG"].ConnectionString; }
        }
        public static string USER
        {         
            get { return ConfigurationManager.ConnectionStrings["ORCE_USER"].ConnectionString; }
        }
        public static string PASS
        {
            get { return ConfigurationManager.ConnectionStrings["ORCE_PASS"].ConnectionString; }
        }
        public static string HEADER
        {
            get { return "Authorization"; }
        }
        public static string VALUE
        {
            get { return "Org-User " + Convert.ToBase64String(Encoding.UTF8.GetBytes(ORG+ ":" + USER + ":" + PASS)); }
        }
    }
}
