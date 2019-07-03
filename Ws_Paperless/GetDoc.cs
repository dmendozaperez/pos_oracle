using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Ws_Paperless
{
    public class GetDoc
    {
    private CookieContainer myCookie;
    public void fe()
    {
            try
            {
                String myurl_onpe = "http://200.121.128.110:8080/axis2/services/Online/OnlineRecovery?ruc=20101951872&login=admin_ws&clave=abc123&tipoDoc=3&folio=B221-142192&tipoRetorno=1";// string.Format("http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI={0}", numDni);
                HttpWebRequest myWebRequest_onpe = (HttpWebRequest)WebRequest.Create(myurl_onpe);
                myWebRequest_onpe.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";//esto creo que lo puse por gusto :/
                myWebRequest_onpe.CookieContainer = myCookie;
                myWebRequest_onpe.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest_onpe.Proxy = null;

                HttpWebResponse myHttpWebResponse_onpe = (HttpWebResponse)myWebRequest_onpe.GetResponse();

                Stream myStream_onpe = myHttpWebResponse_onpe.GetResponseStream();

                StreamReader myStreamReader_onpe = new StreamReader(myStream_onpe);

                string _WebSource_onpe = HttpUtility.HtmlDecode(myStreamReader_onpe.ReadToEnd());
            }
            catch (Exception exc)
            {

                throw;
            }
        

    }
    }
}
