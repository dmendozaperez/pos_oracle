using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WS_Ecommerce.BataClub;

namespace BataClub_Correo
{
    public class ValidacionEmail
    {
        private  string convert_object_jason_validacion(string email)
        {
            string jason_query = "";
            try
            {
                if (email != null)
                    jason_query = "{\"Email\":" + "\"" + email + "\"}";

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return jason_query;
        }
        public  Boolean sendEmail_verificar(string email)
        {
            string url = "https://api.icommarketing.com/TransactionalEmail/VerifyEmail.Json/";
            string apiKey = "MTM3OS0zNTQ1LWJhdGFwZV91c3I1";

            string value_json = convert_object_jason_validacion(email);

            var request = HttpWebRequest.Create(url);
            var byteData = Encoding.UTF8.GetBytes(value_json);

            request.ContentType = "application/json; charset=UTF-8";
            request.Headers.Add("Authorization", apiKey);
            request.Method = "POST";
            Boolean valida = false;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

             
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                objeto_json oRootObject = new objeto_json();
                oRootObject = oJS.Deserialize<objeto_json>(responseString);

                oJS = new JavaScriptSerializer();
                Response_obj objeto_response_obj = new Response_obj();
                objeto_response_obj = oJS.Deserialize<Response_obj>(oRootObject.VerifyEmailJsonResult.Data.Response);
                oRootObject.VerifyEmailJsonResult.Data.Response_obj = objeto_response_obj;

                if (objeto_response_obj.reason.ToUpper().ToString() == "accepted_email".ToUpper().ToString())
                {
                    valida = true;
                }                
            }
            catch (WebException e)
            {
                valida = false;
            }
            return valida;
        }
    }
}
