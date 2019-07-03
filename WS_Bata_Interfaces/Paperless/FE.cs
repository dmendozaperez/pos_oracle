using CapaEntidad.FE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WS_Bata_Interfaces.Paperless
{
    public  class FE
    {
        public Ent_Paperless_Return get_docuento(Ent_Paperless_Envio env)
        {
            Ent_Paperless_Return obj = null;
            FEBata.OnlinePortTypeClient get_FE = null;
            string consulta = "";
            try
            {
                obj = new Ent_Paperless_Return();
                get_FE = new FEBata.OnlinePortTypeClient();
                /*SI LA GENERACION ES EXITOSA ENTONCES EXTRAEMOS EL PDF URL*/
                consulta = get_FE.OnlineRecovery(env.ruc,env.login, env.password,Convert.ToInt32(env.tipodoc),env.folio,Convert.ToInt32(env.tipoRetorno));
                consulta = consulta.Replace("&", "amp;");
                var docpdf = XDocument.Parse(consulta);
                var resultpdf = from factura in docpdf.Descendants("Respuesta")
                                select new Ent_Paperless_Return
                                {
                                    codigo = factura.Element("Codigo").Value,
                                    respuesta = factura.Element("Mensaje").Value.Replace("amp;", "&"),
                                };

                foreach (var itempdf in resultpdf)
                {
                    obj.codigo = itempdf.codigo;
                    obj.respuesta = itempdf.respuesta;                    
                }
            }
            catch(Exception exc) 
            {
                obj = new Ent_Paperless_Return();
                obj.codigo = "-x";
                obj.respuesta = exc.Message;

            }
            return obj;
        }
    }
}