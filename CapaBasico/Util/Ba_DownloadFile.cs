using CapaDato.Basico;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void download_files(Byte[] file,string name,string tipofilecod)
        {
            //Boolean valida = false;
            Dat_File path_dest = null;
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
                        upd_file_bd.insert_update_fileBD(tipofilecod, name);
                    }
                }


            }
            catch (Exception)
            {
                //valida = false;                
            }
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
                /*copia los archivos al destino*/
                File.WriteAllBytes(destino_file, file);
                valida = true;

            }
            catch (Exception)
            {
                valida = false;                
            }
            return valida;
        }
    }
}
