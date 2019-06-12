using CapaDato.Basico;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CapaBasico.Util
{
    public class Ba_DownloadFile
    {
        /// <summary>
        /// metodo para copiar los archivos 
        /// </summary>
        /// <param name="bytes archivos"></param>
        /// <param name="nombre del archivo"></param>
        /// <param name="tipo de archivo"></param>
        public string download_files(Byte[] file,string name,string tipofilecod, string file_creacion, string file_update)
        {
            //Boolean valida = false;
            Dat_File path_dest = null;
            string error = "";
            try
            {
                /*verificar las archivos destinos*/
                path_dest = new Dat_File();
                Ent_File_Ruta rutadestino = path_dest.get_ruta_file(tipofilecod);

                if (rutadestino!=null)
                {
                    /*envia el archivo hacia la carpeta destino */
                    Boolean valida = set_envio_file(file, name, rutadestino.file_destino);
                    /*si es que copa en el server entonces vamos a insertar en la bd*/
                    if (valida)
                    {
                        Dat_File upd_file_bd = new Dat_File();
                        upd_file_bd.insert_update_fileBD(tipofilecod, name,file_creacion,file_update);
                    }
                }


            }
            catch (Exception exc)
            {
                error = exc.Message;
                //valida = false;                
            }
            return error;
            //return valida;
        }
        /// <summary>
        /// sube los archivos envia false si hay error
        /// </summary>
        /// <param name="en bytes"></param>
        /// <param name="nombre del archivo"></param>
        /// <param name="destino donde se va copiar"></param>
        /// <returns></returns>
        private Boolean set_envio_file(Byte[] file, string name,string destino)
        {
            Boolean valida = false;
            try
            {
                if (!Directory.Exists(@destino)) Directory.CreateDirectory(@destino);

                string destino_file = destino + "\\" + name;

                if (File.Exists(@destino_file)) File.Delete(@destino_file);

                /*copia los archivos al destino*/
                File.WriteAllBytes(destino_file, file);
                valida = true;

            }
            catch (Exception exc)
            {
                valida = false;
                throw exc;               
            }
            return valida;
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        #region GuardarImagen 
        //Gabriela Flores 25-03-19
        public string genera_img_barra(byte[] img_bytes, string name_barra, ref string ubi_archivo)
        {
            string devuelve = "";
            try
            {                              
                string ls_rutfileweb = @"Barra/";
                string ls_srvrut = HttpContext.Current.Server.MapPath("~");
                string ls_ruta_fisica_file = "";
                string ls_filename = name_barra;             

                ls_ruta_fisica_file = Path.GetFullPath(Path.Combine(ls_srvrut, ls_rutfileweb));

                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString().PadLeft(2,'0');
                               
                string sub_carpeta = year + month + "/";
                string path_file_img = Path.GetFullPath(Path.Combine(ls_ruta_fisica_file)) + sub_carpeta + name_barra + ".png";

               

                ls_ruta_fisica_file = Path.GetFullPath(Path.Combine(ls_ruta_fisica_file)) + sub_carpeta;
                if (!(Directory.Exists(ls_ruta_fisica_file)))
                {
                    Directory.CreateDirectory(ls_ruta_fisica_file);
                }

                File.WriteAllBytes(path_file_img, img_bytes);
                ubi_archivo = get_url_barra_default() + "/"   + sub_carpeta + name_barra + ".png";

                Boolean update_url = update_url_barra(name_barra, ubi_archivo);                

                devuelve = (update_url) ? "Ok":"ERROR" ;



            }
            catch (Exception ex)
            {                
                throw ex;
            }

            return devuelve ;
        }
        private Boolean update_url_barra(string barra,string url_barra)
        {
            string sqlquery = "USP_BATACLUB_UPDATE_URL_BARRA";
            Boolean valida = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BARRA", barra);
                            cmd.Parameters.AddWithValue("@URL_BARRA", url_barra);
                            cmd.ExecuteNonQuery();
                            valida = true;
                        }
                    }
                    catch (Exception exc)
                    {

                        valida = false;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                valida = false;
            }
            return valida;
        }
        private string get_url_barra_default()
        {
            string sqlquery = "USP_BATACLUB_GET_URI";
            string uri = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                while(dr.Read())
                                {
                                    uri = dr["URL_BARRA"].ToString();
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {

                        throw exc;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return uri;
        }

        #endregion
    }
}
