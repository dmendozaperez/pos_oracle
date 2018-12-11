using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

//namespace SourceDBF

    public class DBF
    {
    [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillRowCustomTable", TableDefinition = "ID int,Name nvarchar(255)")]

    public static IEnumerable GetFmc(string ruta)

    {
        List<Fmc_Data> entity = new List<Fmc_Data>();

        string sqlcon= "Provider=VFPOLEDB;Data Source=" + ruta + ";Exclusive=No";

        string sqlquery = "select * from FMC";

        using (OleDbConnection cn = new OleDbConnection(sqlcon))
        {
            using (OleDbCommand cmd = new OleDbCommand(sqlquery, cn))
            {
                cmd.CommandTimeout = 0;
                //cmd.Parameters.Add("DATE", OleDbType.Date).Value = fecha_despacho;
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach(DataRow fila in dt.Rows)
                    {
                        entity.Add(new Fmc_Data { v_nfor =fila["v_nfor"].ToString() });
                    }
                }
            }
        }
        //            entity.Add(new myEntity { ID = 1000, Name = "Jignesh Trivedi" });

        //entity.Add(new myEntity { ID = 1001, Name = "Tejas Trivedi" });

        //entity.Add(new myEntity { ID = 1002, Name = "Rakesh Trivedi" });

        //entity.Add(new myEntity { ID = 1003, Name = "S G Trivedi" });

        return entity;

    }
    public static void FillRowCustomTable(object resultObj, out SqlString v_nfor)

    {

        Fmc_Data myResult = (Fmc_Data)resultObj;




        v_nfor = myResult.v_nfor;

    }

}
    public class Fmc_Data

    {

        public string v_nfor { get; set; }

        
    }

