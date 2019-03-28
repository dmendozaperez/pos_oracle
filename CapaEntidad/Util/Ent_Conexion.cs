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

        #region<ENTORNO XCENTER>

        #region<CONEXION DESARROLLO>
        //public static string ftp_server { get { return "172.24.12.176"; } }
        //public static string ftp_user { get { return "retailc"; } }
        //public static string ftp_password { get { return "1wiAwNRa"; } }
        //public static Int32 ftp_puerto { get { return 22; } }
        #endregion

        #region<CONEXION QA>
       
        public static string ftp_server = "";//{ get { return "172.24.20.182222222222"; } }
        public static string ftp_user = "";//{ get { return "retailc"; } }
        public static string ftp_password = "";//{ get { return "1wiAwNRa"; } }
        public static Int32 ftp_puerto = 0;//{ get { return 22; } }

        public static string ftp_orce_server = "";//{ get { return "172.24.20.18222222"; }}
        public static string ftp_orce_user = "";//{ get { return "retailc"; } }
        public static string ftp_orce_password = "";//{ get { return "1wiAwNRa"; } }
        public static Int32 ftp_orce_puerto = 0;//{ get { return 22; } }
        #endregion

        #region<CONEXION PRODUCCION>
        //public static string ftp_server { get { return "172.16.24.216"; } }
        //public static string ftp_user { get { return "webposbpe"; } }
        //public static string ftp_password { get { return "JU737CbDmJvu"; } }
        //public static Int32 ftp_puerto { get { return 22; } }

        #endregion
        #endregion
        #region<ENTORNO CE>
        #region<CONEXION PRODUCCION>
        //public static string ftp_server { get { return "172.16.24.220"; } }
        //public static string ftp_user { get { return "webposbpe"; } }
        //public static string ftp_password { get { return "JU737CbDmJvu"; } }
        //public static Int32 ftp_puerto { get { return 22; } }

        #endregion
        #endregion

        #region<CONEXION DE BASE DE DATOS>

        public static string conexion { get; set; }
        //public static string conexion_posperu { get; set; }
        public static string conexion_posperu_DES { get; set; }
        public static string conexion_posperu_QA { get; set; }

        //public static string conexion_posperu { get; set; }
        //public static string conexion
        //{
        //    //get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
        //    get { return "Server=POSPERUBD.BGR.PE;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        //}
        #endregion

        #region<CONEXION DE BASE DE DATOS NUBE POS PERU>
        public static string conexion_posperu
        {
            get { return "Server=posperubd.bgr.pe;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }

        public static string conexion_DBF_POS
        {
            //get { return "Provider=VFPOLEDB.1;Data Source=" + _path_default + ";Exclusive=No"; }
            get { return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=XXXX;Extended Properties=dBASE IV;"; }
        }
        //public static string conexion_posperu_DES
        //{
        //    get { return "Server=posperu.bgr.pe;Database=BDPOS_DES;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        //}
        //public static string conexion_posperu_QA
        //{
        //    get { return "Server=posperu.bgr.pe;Database=BDPOS_QA;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        //}
        public static string conexion_208
        {
            get { return "Server=www.bgr.pe;Database=BdTienda;User ID=dmendoza;Password=Bata2013;Trusted_Connection=False;"; }
        }

        #endregion

        #region<CONEXIONES ECUADOR>
        public static string conexion_posecuador { get; set; }
        public static string conexion_posecuador_QA { get; set; }

        //public static string conexion_posecuador_QA
        //{
        //    get { return "Server=posperu.bgr.pe;Database=BDPOS_ECU_QA;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        //}
        //public static string conexion_posecuador
        //{
        //    get { return "Server=posperu.bgr.pe;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        //}
        #endregion
        #endregion
        #region<CONEXION ORACLE>
        //private static string _server_oracle = "172.24.16.62";
        private static string _user_oracle = "dtv";
        private static string _pass_oracle = "bMWUTfYV5rC";
        public static string _database { get { return "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.24.16.62)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XCENTER)))"; } }

        public static string conn_oracle = "USER ID=" + _user_oracle +
            ";PASSWORD=" + _pass_oracle + ";DATA SOURCE=" + _database;
        #endregion
    }
}
