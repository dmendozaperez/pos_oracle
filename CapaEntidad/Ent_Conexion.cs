using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Ent_Conexion
    {

        #region<CONEXION FTP>
        public static string ftp_server { get { return "172.24.12.176"; } }
        public static string ftp_user { get { return "retailc"; } }
        public static string ftp_password { get { return "1wiAwNRa"; } }
        public static Int32 ftp_puerto { get { return 22; } }
        #endregion

        #region<CONEXION DE BASE DE DATOS>
        public static string conexion
        {           
            get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
        }
        #endregion
    }
}
