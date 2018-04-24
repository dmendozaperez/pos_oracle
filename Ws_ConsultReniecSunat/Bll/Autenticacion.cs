using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace Ws_ConsultReniecSunat.Bll
{
    public class Autenticacion : SoapHeader
    {
        private string sUserPass;
        private string sUserName;
        public string user_password
        {
            get
            {
                return sUserPass;
            }
            set
            {
                sUserPass = value;
            }
        }
        public string user_name
        {
            get
            {
                return sUserName;
            }
            set
            {
                sUserName = value;
            }
        }
    }
}