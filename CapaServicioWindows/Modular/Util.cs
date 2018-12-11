using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Modular
{
    public class Util
    {
        public  List<BataTransac.Ent_PathDBF> get_location_dbf(ref string _error_ws)
        {
            List<BataTransac.Ent_PathDBF> list = null;
            Basico valida_ecu = null;
            try
            {

                valida_ecu = new Basico();

                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                var list_ws =bata_trans.ws_get_location_dbf(header_user);

                if (list_ws!=null)
                {
                    list = new List<BataTransac.Ent_PathDBF>();
                    foreach(var listar in list_ws)
                    {
                        BataTransac.Ent_PathDBF valor = new BataTransac.Ent_PathDBF();
                        valor.rutloc_namedbf = listar.rutloc_namedbf;

                        //if (!valida_ecu.valida_file_ecu())
                       // {
                         valor.rutloc_location = (!valida_ecu.valida_file_ecu()) ?listar.rutloc_location: listar.rutloc_location_ecu;
                        //}

                        

                        valor.rutloc_location_ecu = listar.rutloc_location_ecu;
                        list.Add(valor);
                    }
                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
                list = null;                
            }
            return list;
        }
        public void control_errores_transac(string cod_tipo, string error_des,ref string _error_ws)
        {
            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                bata_trans.ws_errores_transaction(header_user, cod_tipo, error_des);
            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;

            }
        }
        public string get_ruta_locationProcesa_dbf(string name)
        {
            string ruta = "";
            string sqlquery = "USP_GET_LOCATION_DBF";           
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@location_dbf", name);
                            SqlDataReader dr = cmd.ExecuteReader();



                            if (dr.HasRows)
                            {
                               
                                while (dr.Read())
                                {
                                    ruta = dr["RUTLOC_LOCATION"].ToString();                                   
                                }
                            }


                        }
                    }
                    catch (Exception)
                    {
                       
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

               
            }
            catch (Exception)
            {
                throw;
            }
            return ruta;
        }

        public DataSet get_ds_venta(string _ruta,ref string error)
        {
            DataSet ds_venta = null;
            DataTable dt_FFACTC = null;
            DataTable dt_FFACTD = null;
            DataTable dt_FNOTAA = null;
            string sqlquery_FFACTC = "";
            string sqlquery_FFACTD = "";
            string sqlquery_FNOTAA = "";
            try
            {
                sqlquery_FFACTC = "SELECT [FC_NINT], [FC_NNOT],[FC_CODI],[FC_SUNA],[FC_SFAC],[FC_NFAC],[FC_FFAC],[FC_NORD],[FC_CREF],[FC_SREF]," +
                        "[FC_NREF],[FC_PVTA],[FC_CSUC],[FC_GVTA],[FC_ZONA],[FC_CLIE],[FC_NCLI],[FC_NOMB],[FC_APEP],[FC_APEM],[FC_DCLI]," +
                        "[FC_CUBI],[FC_RUC],[FC_VUSE],[FC_VEND],[FC_IPRE],[FC_TINT],[FC_PINT],[FC_LCSG],[FC_NCON],[FC_DCON],[FC_LCON]," +
                        "[FC_LRUC],[FC_AGEN],[FC_MONE],[FC_TASA],[FC_FPAG],[FC_NLET],[FC_QTOT],[FC_PREF],[FC_DREF],[FC_BRUT],[FC_VIMP1]," +
                        "[FC_VIMP2],[FC_VDCT1],[FC_VDCT4],[FC_PDC2],[FC_PDC3], [FC_VDC23],[FC_VVTA], [FC_VIMP3],[FC_PIMP4],[FC_VIMP4]," +
                        "[FC_TOTAL],[FC_ESTA],[FC_TDOC],[FC_CUSE],[FC_MUSE],[FC_FCRE],[FC_FMOD],[FC_HORA],[FC_AUTO],[FC_FTX],[FC_ESTC]," +
                        "[FC_SEXO],[FC_MPUB],[FC_EDAD],[FC_REGV] FROM FFACTC";

                sqlquery_FFACTD = "SELECT [FD_NINT],[FD_TIPO],[FD_ARTI],[FD_REGL],[FD_COLO],[FD_ITEM],[FD_ICMB],[FD_QFAC],[FD_LPRE],[FD_CALM],[FD_PREF],[FD_DREF],[FD_PREC],[FD_BRUT]," +
                                 "[FD_PIMP1], [FD_VIMP1], [FD_SUBT1], [FD_PIMP2], [FD_VIMP2], [FD_SUBT2], [FD_PDCT1], [FD_VDCT1], [FD_SUBT3], [FD_VDCT4], [FD_VDC23]," +
                                 "[FD_VVTA], [FD_PIMP3], [FD_VIMP3], [FD_PIMP4], [FD_VIMP4], [FD_TOTAL], [FD_CUSE],[FD_MUSE],[FD_FCRE],[FD_FMOD]," +
                                 "[FD_AUTO],[FD_DRE2], [FD_ASOC] FROM FFACTD";

                sqlquery_FNOTAA = "select [NA_NOTA], [NA_ITEM], [NA_MONE], [NA_TPAG], [NA_TASA], [NA_CREF], [NA_SREF]," +
                                 "[NA_NREF], [NA_VREF], [NA_VPAG], [NA_ESTA], [NA_CIER], [NA_FCRE], [NA_FMOD] " +
                                 "from FNOTAA";

                /*facctc*/
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_oledb(_ruta)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_FFACTC, cn))
                    {
                        cmd.CommandTimeout = 0;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            dt_FFACTC = new DataTable();
                            da.Fill(dt_FFACTC);
                        }
                    }
                }
                /**/
                /*facctd*/
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_oledb(_ruta)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_FFACTD, cn))
                    {
                        cmd.CommandTimeout = 0;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            dt_FFACTD = new DataTable();
                            da.Fill(dt_FFACTD);
                        }
                    }
                }


                /**/
                /*fnotaa*/
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_oledb(_ruta)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_FNOTAA, cn))
                    {
                        cmd.CommandTimeout = 0;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            dt_FNOTAA = new DataTable();
                            da.Fill(dt_FNOTAA);
                        }
                    }
                }

                if (dt_FFACTC!=null && dt_FFACTD!=null && dt_FNOTAA!=null)
                {
                    ds_venta = new DataSet();
                    ds_venta.Tables.Add(dt_FFACTC);
                    ds_venta.Tables.Add(dt_FFACTD);
                    ds_venta.Tables.Add(dt_FNOTAA);
                }               
                /**/
            }
            catch (Exception exc)
            {
                error = exc.Message;
                ds_venta = null;
            }
            return ds_venta;
        }
    }
}
