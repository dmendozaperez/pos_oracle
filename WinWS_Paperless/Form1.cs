using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinWS_Paperless
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnpaperless_Click(object sender, EventArgs e)
        {
            Ws_Paperless.GetDoc f = new Ws_Paperless.GetDoc();
            f.fe();
        }
    }
}
