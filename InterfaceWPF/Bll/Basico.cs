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
        public String ruta_temp_DBF = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//tmpDBF";

        //private string ftp_ruta_destino = "/opt/webxst/BCL/autodeploy/data/org2000";
        private string ftp_ruta_orce = "/app/webce/BPE/CE/batch_processing/auto_fileset";
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

        public Boolean sendftp_file_orce()
        {
            Boolean valida = false;
            try
            {

                // return false;

                string[] _archivos_xml = Directory.GetFiles(@ruta_temp_interface, "*.XML");

                for (Int32 a = 0; a < _archivos_xml.Length; ++a)
                {
                    string _path_archivo_xml = _archivos_xml[a].ToString();
                    string _nombrearchivo_xml = Path.GetFileNameWithoutExtension(@_path_archivo_xml);
                    string _extension_archivo = Path.GetExtension(@_path_archivo_xml);
                    string _file_path_destino = ftp_ruta_orce + "/" + _nombrearchivo_xml + _extension_archivo;
                    Boolean valida_subida = subida_server_ftp_orce(@_path_archivo_xml, _file_path_destino);
                    if (valida_subida)
                    {
                        if (File.Exists(@_path_archivo_xml)) File.Delete(@_path_archivo_xml);
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
                    //Protocol = Protocol.Sftp,
                    //HostName = "172.24.20.183",//Ent_Conexion.ftp_server,// "172.24.28.216",
                    //UserName = "retailc",//Ent_Conexion.ftp_user,// "webposintg",
                    //Password = "1wiAwNRa", //Ent_Conexion.ftp_password,// "SubJFpHEN27y",
                    //PortNumber =Ent_Conexion.ftp_puerto,// 22,
                    //GiveUpSecurityAndAcceptAnySshHostKey = true,
                    //SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx..."
                    
                    Protocol = Protocol.Sftp,
                    HostName = Ent_Conexion.ftp_server,// "172.24.28.216",
                    UserName = Ent_Conexion.ftp_user,// "webposintg",
                    Password = Ent_Conexion.ftp_password,// "SubJFpHEN27y",
                    PortNumber = Ent_Conexion.ftp_puerto,// 22,
                    GiveUpSecurityAndAcceptAnySshHostKey = true,
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

                    /*destino ORCE*/
                    //transferResult =
                    //    session.PutFiles(file_origen, "/tmp/", false, transferOptions);

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

        private Boolean subida_server_ftp_orce(string file_origen, string file_destino)
        {
            Boolean valida_envio = false;
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = Ent_Conexion.ftp_orce_server,// "172.24.28.216",
                    UserName = Ent_Conexion.ftp_orce_user,// "webposintg",
                    Password = Ent_Conexion.ftp_orce_password,// "SubJFpHEN27y",
                    PortNumber = Ent_Conexion.ftp_orce_puerto,// 22,
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
            catch (Exception exc)
            {
                valida_envio = false;
            }
            return valida_envio;
        }
        #endregion
    }
}
