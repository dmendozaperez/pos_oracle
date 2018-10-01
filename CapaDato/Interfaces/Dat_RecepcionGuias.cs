using CapaEntidad.Interfaces;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Interfaces
{
    public class Dat_RecepcionGuias
    {
        /// <summary>
        /// En este metodo envia por ws para actulizar la base de datos las guias cerradas
        /// </summary>       
        /// <returns></returns>
        public Ent_MsgTransac update_transaction_guias(Ent_Fvdespc guias,Ent_Scdddes g_cerrada)
        {
            Ent_MsgTransac msg_error =null;
            string sqlquery = "[USP_RECEPCION_GUIASTDA_ALMACEN]";
            try
            {
                msg_error = new Ent_MsgTransac();
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();

                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DESC_ALMAC", guias.DESC_ALMAC);
                            cmd.Parameters.AddWithValue("@DESC_GUDIS", guias.DESC_GUDIS);
                            cmd.Parameters.AddWithValue("@DESC_NDESP", guias.DESC_NDESP);
                            cmd.Parameters.AddWithValue("@DESC_TDES", guias.DESC_TDES);
                            cmd.Parameters.AddWithValue("@DESC_FECHA", guias.DESC_FECHA);
                            cmd.Parameters.AddWithValue("@DESC_FDESP", guias.DESC_FDESP);
                            cmd.Parameters.AddWithValue("@DESC_ESTAD", guias.DESC_ESTAD);
                            cmd.Parameters.AddWithValue("@DESC_TIPO", guias.DESC_TIPO);
                            cmd.Parameters.AddWithValue("@DESC_TORI", guias.DESC_TORI);
                            cmd.Parameters.AddWithValue("@DESC_FEMI", guias.DESC_FEMI);
                            cmd.Parameters.AddWithValue("@DESC_SEMI", guias.DESC_SEMI);
                            cmd.Parameters.AddWithValue("@DESC_FTRA", guias.DESC_FTRA);
                            cmd.Parameters.AddWithValue("@DESC_NUME", guias.DESC_NUME);
                            cmd.Parameters.AddWithValue("@DESC_CONCE", guias.DESC_CONCE);
                            cmd.Parameters.AddWithValue("@DESC_NMOVC", guias.DESC_NMOVC);
                            cmd.Parameters.AddWithValue("@DESC_EMPRE", guias.DESC_EMPRE);
                            cmd.Parameters.AddWithValue("@DESC_SECCI", guias.DESC_SECCI);
                            cmd.Parameters.AddWithValue("@DESC_CANAL", guias.DESC_CANAL);
                            cmd.Parameters.AddWithValue("@DESC_CADEN", guias.DESC_CADEN);
                            cmd.Parameters.AddWithValue("@DESC_FTX", guias.DESC_FTX);
                            cmd.Parameters.AddWithValue("@DESC_TXPOS", guias.DESC_TXPOS);
                            cmd.Parameters.AddWithValue("@DESC_UNCA", guias.DESC_UNCA);
                            cmd.Parameters.AddWithValue("@DESC_UNNC", guias.DESC_UNNC);
                            cmd.Parameters.AddWithValue("@DESC_CAJA", guias.DESC_CAJA);
                            cmd.Parameters.AddWithValue("@DESC_VACA", guias.DESC_VACA);
                            cmd.Parameters.AddWithValue("@DESC_VANC", guias.DESC_VANC);
                            cmd.Parameters.AddWithValue("@DESC_VCAJ", guias.DESC_VCAJ);
                            

                            /*PARAMETRO PARA LA TABLA SCDDDES*/
                            cmd.Parameters.AddWithValue("@DDES_TIPO", g_cerrada.DDES_TIPO);
                            cmd.Parameters.AddWithValue("@DDES_ALMAC", g_cerrada.DDES_ALMAC);
                            cmd.Parameters.AddWithValue("@DDES_GUIRE", g_cerrada.DDES_GUIRE);
                            cmd.Parameters.AddWithValue("@DDES_NDESP", g_cerrada.DDES_NDESP);
                            cmd.Parameters.AddWithValue("@DDES_MFDES", g_cerrada.DDES_MFDES);
                            cmd.Parameters.AddWithValue("@DDES_DESTI", g_cerrada.DDES_DESTI);
                            cmd.Parameters.AddWithValue("@DDES_N_INI", g_cerrada.DDES_N_INI);
                            cmd.Parameters.AddWithValue("@DDES_N_FIN", g_cerrada.DDES_N_FIN);
                            cmd.Parameters.AddWithValue("@DDES_CPAGO", g_cerrada.DDES_CPAGO);
                            cmd.Parameters.AddWithValue("@DDES_FEMBA", g_cerrada.DDES_FEMBA);
                            cmd.Parameters.AddWithValue("@DDES_FECHA", g_cerrada.DDES_FECHA);
                            cmd.Parameters.AddWithValue("@DDES_FDESP", g_cerrada.DDES_FDESP);
                            cmd.Parameters.AddWithValue("@DDES_ESTAD", g_cerrada.DDES_ESTAD);
                            cmd.Parameters.AddWithValue("@DDES_GGUIA", g_cerrada.DDES_GGUIA);
                            cmd.Parameters.AddWithValue("@DDES_CCOND", g_cerrada.DDES_CCOND);
                            cmd.Parameters.AddWithValue("@DDES_CALZ", g_cerrada.DDES_CALZ);
                            cmd.Parameters.AddWithValue("@DDES_NCALZ", g_cerrada.DDES_NCALZ);
                            cmd.Parameters.AddWithValue("@DDES_TOCAJ", g_cerrada.DDES_TOCAJ);
                            cmd.Parameters.AddWithValue("@DDES_IMPRE", g_cerrada.DDES_IMPRE);
                            cmd.Parameters.AddWithValue("@DDES_GVALO", g_cerrada.DDES_GVALO);
                            cmd.Parameters.AddWithValue("@DDES_SUBGR", g_cerrada.DDES_SUBGR);
                            cmd.Parameters.AddWithValue("@DDES_RUCTC", g_cerrada.DDES_RUCTC);
                            cmd.Parameters.AddWithValue("@DDES_TRANS", g_cerrada.DDES_TRANS);
                            cmd.Parameters.AddWithValue("@DDES_TRAN2", g_cerrada.DDES_TRAN2);
                            cmd.Parameters.AddWithValue("@DDES_OBSER", g_cerrada.DDES_OBSER);
                            cmd.Parameters.AddWithValue("@DDES_NOMTC", g_cerrada.DDES_NOMTC);
                            cmd.Parameters.AddWithValue("@DDES_NGUIA", g_cerrada.DDES_NGUIA);
                            cmd.Parameters.AddWithValue("@DDES_NRLIQ", g_cerrada.DDES_NRLIQ);
                            cmd.Parameters.AddWithValue("@DDES_LIMPR", g_cerrada.DDES_LIMPR);
                            cmd.Parameters.AddWithValue("@DDES_EMPRE", g_cerrada.DDES_EMPRE);
                            cmd.Parameters.AddWithValue("@DDES_CANAL", g_cerrada.DDES_CANAL);
                            cmd.Parameters.AddWithValue("@DDES_CADEN", g_cerrada.DDES_CADEN);
                            cmd.Parameters.AddWithValue("@DDES_SECCI", g_cerrada.DDES_SECCI);
                            cmd.Parameters.AddWithValue("@DDES_FTX", g_cerrada.DDES_FTX);
                            cmd.Parameters.AddWithValue("@DDES_FTXTD", g_cerrada.DDES_FTXTD);

                            /*detalle del temporal de guias*/
                            cmd.Parameters.AddWithValue("@TMP_DET", guias.DT_FVDESPD_TREGMEDIDA);

                            cmd.ExecuteNonQuery();

                            msg_error.codigo = "0";
                            msg_error.descripcion = "Transaction existosa";

                        }

                    }
                    catch (Exception exc)
                    {
                        msg_error.codigo = "1";
                        msg_error.descripcion = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                msg_error.codigo = "1";
                msg_error.descripcion = exc.Message;
            }
            return msg_error;
        }

       
    }
}
