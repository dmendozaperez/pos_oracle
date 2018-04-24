using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ws_ConsultReniecSunat.Bll
{
    public class Basico
    {
        public static Boolean VerificarPermisos(Autenticacion value)
        {
            Boolean _valida_permiso = false;
            if (value == null)
            {
                _valida_permiso = false;
            }
            else
            {
                /*user=Bata*/
                /*pass=123*/

                if (value.user_name=="Bata" && value.user_password=="123")
                {
                    _valida_permiso = true;
                }
                else
                {
                    _valida_permiso = false;
                }

                //_valida_permiso = VALIDA_USUARIO_WS(value.user_name, value.user_password);
            }
            return _valida_permiso;
        }
    }
}