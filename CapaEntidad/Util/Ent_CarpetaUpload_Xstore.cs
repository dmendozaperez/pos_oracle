using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Util
{
    public class Ent_CarpetaUpload_Xstore
    {
        public string pais { get; set; }
        public string entorno { get; set; }
        public string opcion { get; set; }
        public string rut_upload { get; set; }
        public string ftp_server { get; set; }
        public string ftp_user { get; set; }
        public string ftp_pass { get; set; }
        public Int32 ftp_port { get; set; }
        public string ftp_folder { get; set; }
        public string ftp_send { get; set; }
        public string bata_sftp_server { get; set; }
        public string bata_sftp_user { get; set; }
        public string bata_sftp_passwrod { get; set; }        
        public int bata_sftp_port { get; set; }
        public string bata_sftp_folder { get; set; }
    }
}
