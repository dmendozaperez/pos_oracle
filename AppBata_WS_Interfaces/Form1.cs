using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaServicioWindows.Modular;
namespace AppBata_WS_Interfaces
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnhel_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();

            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

            var dd = batatran.HelloWorld(header_user);
        }

        private void btn_ws_update_transaction_guias_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

            BataTransac.Ent_Fvdespc ent = new BataTransac.Ent_Fvdespc();
            ent.DESC_ALMAC = "ok";

            BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();
            
            //batatran.ws_update_transaction_guias(header_user,ent);
        }

        private void btnenvio_Click(object sender, EventArgs e)
        {

        }

        private void btn_servicewin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Basico cc = new Basico();
            cc.eje_envio_guias();
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;
        }
    }
}
