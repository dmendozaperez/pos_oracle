using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaOracleTda
{
    public class Ent_Ora_Conexion
    {
        public string tienda { get; set; }
        public string descripcion { get; set; }
        public string server_ora { get; set; }
        public string user_ora { get; set; }
        public string pas_ora { get; set; }
        public Int32 port_ora { get; set; }
        public string sid_ora { get; set; }
    }
    public class Ent_Acceso_BD
    {
        #region<PROPIEDADES ESTATICAS PARA LA CONEXION ORACLE>
        public static string user { get; set;  }
        public static string password { get; set; }
        public static string server { get; set; }
        public static Int32 port { get; set; }
        public static string sid { get; set; }

        public static string database()
        {
            string con = "";
            try
            {
                con= "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + server + ")(PORT=" +  port.ToString() + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + sid +")))";
            }
            catch 
            {
                                
            }
            return con;
        }
        #region<CADENA DE CONEXION STATICA ORACLE>  
        public static string conn()
        { 
           return "USER ID=" + user +
             ";PASSWORD=" + password + ";DATA SOURCE=" + database();
        }
        #endregion

        //public static string database { get { return "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=200.1.1.40)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ADCO)))"; } }
        #endregion
    }
}
