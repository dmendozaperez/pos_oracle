using CapaEntidad;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace InterfaceWPF.Bll
{
    public class Basico
    {
        public String ruta_temp_interface = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//tmpinterface";
        private string ftp_ruta_destino = "/tmp";
        //private string ftp_ruta_destino = "/app/webxst/BCL/autodeploy/data/";
        #region<ENVIO POR FTP ARCHIVOS MNT>
        public Boolean sendftp_file_mnt()
        {
            Boolean valida = false;
            try
            {

               // return false;

                string[] _archivos_mnt = Directory.GetFiles(@ruta_temp_interface, "*.MNT");

                for (Int32 a = 0; a < _archivos_mnt.Length; ++a)
                {
                    string _path_archivo_mnt = _archivos_mnt[a].ToString();
                    string _nombrearchivo_mnt = Path.GetFileNameWithoutExtension(@_path_archivo_mnt);
                    string _extension_archivo = Path.GetExtension(@_path_archivo_mnt);
                    string _file_path_destino = ftp_ruta_destino + "/" + _nombrearchivo_mnt + _extension_archivo;
                    Boolean valida_subida= subida_server_ftp(@_path_archivo_mnt,_file_path_destino);
                    if (valida_subida)
                    {
                        if (File.Exists(@_path_archivo_mnt)) File.Delete(@_path_archivo_mnt);
                    }
                   
                }
                valida = true;
            }
            catch (Exception)
            {
                valida = false;                
            }
            return valida;
        }
        private Boolean subida_server_ftp(string file_origen,string file_destino)
        {
            Boolean valida_envio = false;
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = Ent_Conexion.ftp_server,// "172.24.28.216",
                    UserName = Ent_Conexion.ftp_user,// "webposintg",
                    Password = Ent_Conexion.ftp_password,// "SubJFpHEN27y",
                    PortNumber =Ent_Conexion.ftp_puerto,// 22,
                    GiveUpSecurityAndAcceptAnySshHostKey = true,
                    //SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx..."
                };

                using (Session session = new Session())
                {                 
                    session.Open(sessionOptions);                 
                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.FilePermissions = null; // This is default
                    transferOptions.PreserveTimestamp = false;
                    transferOptions.TransferMode = TransferMode.Binary;
                    TransferOperationResult transferResult;

                    transferResult =
                        session.PutFiles(file_origen, file_destino, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();
                    //string subido = "SUBIDO_FTP";


                    valida_envio = true;
                   // valida_envio = false;
                }


            }
            catch(Exception exc) 
            {
                valida_envio = false;
            }
            return valida_envio;
        }
        #endregion
    }
}
