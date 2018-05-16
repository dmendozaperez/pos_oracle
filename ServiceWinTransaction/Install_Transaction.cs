using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace ServiceWinTransaction
{
    [RunInstaller(true)]
    public partial class Install_Transaction : System.Configuration.Install.Installer
    {
        public Install_Transaction()
        {
            InitializeComponent();
        }
    }
}
