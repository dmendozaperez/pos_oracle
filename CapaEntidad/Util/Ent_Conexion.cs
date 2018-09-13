using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Util
{
    public class Ent_Conexion
    {

        #region<CONEXION FTP>

        #region<CONEXION DESARROLLO>
        //public static string ftp_server { get { return "172.24.12.176"; } }
        //public static string ftp_user { get { return "retailc"; } }
        //public static string ftp_password { get { return "1wiAwNRa"; } }
        //public static Int32 ftp_puerto { get { return 22; } }
        #endregion

        #region<CONEXION QA>
        //public static string ftp_server { get { return "172.24.20.182"; } }
        //public static string ftp_user { get { return "retailc"; } }
        //public static string ftp_password { get { return "1wiAwNRa"; } }
        //public static Int32 ftp_puerto { get { return 22; } }
        #endregion

        #region<CONEXION PRODUCCION>
        public static string ftp_server { get { return "172.16.24.216"; } }
        public static string ftp_user { get { return "webposbpe"; } }
        public static string ftp_password { get { return "JU737CbDmJvu"; } }
        public static Int32 ftp_puerto { get { return 22; } }

        #endregion
        #endregion

        #region<CONEXION DE BASE DE DATOS>
        public static string conexion
        {           
            //get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            get { return "Server=posperu.bgr.pe;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }
        #endregion

        #region<CONEXION DE BASE DE DATOS NUBE POS PERU>
        public static string conexion_posperu
        {
            get { return "Server=posperu.bgr.pe;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }

        public static string conexion_DBF_POS
        {
            //get { return "Provider=VFPOLEDB.1;Data Source=" + _path_default + ";Exclusive=No"; }
            get { return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=XXXX;Extended Properties=dBASE IV;"; }
        }
        public static string conexion_posperu_DES
        {
            get { return "Server=posperu.bgr.pe;Database=BDPOS_DES;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }
        public static string conexion_posperu_QA
        {
            get { return "Server=posperu.bgr.pe;Database=BDPOS_QA;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }
        public static string conexion_208
        {
            get { return "Server=www.bgr.pe;Database=BdTienda;User ID=dmendoza;Password=Bata2013;Trusted_Connection=False;"; }
        }
        #endregion

    }
}
