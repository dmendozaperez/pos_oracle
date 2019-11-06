using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaOracleTda.CapaDato
{
    public class Dat_Ora_Data
    {
        public DataTable get_documento(string nro_doc)
        {
            DataTable dtdoc = null;
            string sqlquery = "select * from trn_trans where trans_typcode='RETAIL_SALE' and fiscal_number like '%" + nro_doc + "%' AND TRANS_STATCODE='COMPLETE'";
            try
            {
                object results = new object[1];
                Database db = new OracleDatabase(Ent_Acceso_BD.conn());
                //string bus = "0103001000036-1";

                DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlquery);

                dtdoc = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            }
            catch (Exception exc)
            {
                throw;
                dtdoc = null;
            }
            return dtdoc;
        }
        public DataTable get_poslog(string caja,string nro_ticket)
        {
            DataTable dtdoc = null;
            string sqlquery = "SELECT * FROM TRN_POSLOG_DATA where trans_seq=" + nro_ticket.ToString() + " and WKSTN_ID=" + caja + "";
            try
            {
                object results = new object[1];
                Database db = new OracleDatabase(Ent_Acceso_BD.conn());
                //string bus = "0103001000036-1";

                DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlquery);

                dtdoc = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            }
            catch (Exception exc)
            {

                dtdoc = null;
            }
            return dtdoc;
        }
    }
}
