using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Xoficce
{
    public class StockAct
    {
        public DataTable get_stock_xoffice(string cod_tda)
        {
            string sqlquery = "select rtl_loc_id,item_id,unitcount from INV_STOCK_LEDGER_ACCT where " + 
                                "organization_id = 2000 and update_date >= to_date('19-09-2018', 'DD-MM-YYYY') and rtl_loc_id = 50140; ";
            DataTable dt = null;
            try
            {
                object results = new object[1];
                Database db = new OracleDatabase(CapaEntidad.Util.Ent_Conexion.conn_oracle);
                DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlquery, results);
                dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

            }
            catch (Exception exc)
            {
                dt = null;
            }
            return dt;
        }
    }
}
