using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceWSPrueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wsconsulta_des.validateLogin la = new wsconsulta_des.validateLogin();
            la.Username = "BataPeru";
            la.Password = "Bata2018**.";

            wsconsulta_des.Sunat_Reniec_PESoapClient c = new wsconsulta_des.Sunat_Reniec_PESoapClient();
            var r = c.ws_persona_reniec(la, "41149125");

            //wsconsulta.Cons_ClienteSoapClient c = new wsconsulta.Cons_ClienteSoapClient();
            //DataTable r = c.ws_persona_reniec("41149120");

            string num = "";

         

        }
    }
}
