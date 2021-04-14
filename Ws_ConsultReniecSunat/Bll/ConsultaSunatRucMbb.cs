using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;
using System.Web;

namespace Ws_ConsultReniecSunat.Bll
{
    public class ConsultaSunatRucMbb
    {
        public static async Task<string> obtenerInformacion(string rucs)
        {
            string rpta = "";
            if (!string.IsNullOrEmpty(rucs))
            {
                List<string> lstRucs = new List<string>();
                string[] rs = rucs.Split(',');
                int n = rs.Length;
                string ruc = "";
                for (int i = 0; i < n; ++i)
                {
                    if (!string.IsNullOrWhiteSpace(rs[i]))
                    {
                        ruc = await ConsultaSunatRucMbb.obtenerRuc(rs[i]);
                        if (!string.IsNullOrEmpty(ruc))
                            lstRucs.Add(ruc);
                        else
                            lstRucs.Add(rs[i] + "|Error al obtener información");
                    }
                }
                rpta = string.Join("¯", lstRucs.ToArray());
            }
            return rpta;
        }

        private static async Task<string> obtenerRuc2(string ruc, string token)
        {
            string rpta = "";
            CookieContainer cookies = new CookieContainer();
            using (HttpClient client = new HttpClient((HttpMessageHandler)new HttpClientHandler()
            {
                CookieContainer = cookies,
                UseCookies = true,
                AllowAutoRedirect = true
            }))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
                    client.DefaultRequestHeaders.Add("accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Host", "e-consultaruc.sunat.gob.pe");
                    ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)((se, cert, chain, sslerror) => true);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("accion", "consPorRuc"),
            new KeyValuePair<string, string>("nroRuc", ruc),
            new KeyValuePair<string, string>("contexto", "ti-it"),
            new KeyValuePair<string, string>("modo", "1"),
            new KeyValuePair<string, string>("numRnd", token)
          };
                    FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)pairs);
                    string url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + ruc + "&contexto=ti-it&modo=1&numRnd=" + token;
                    HttpResponseMessage result = await client.GetAsync(new Uri(url));
                    if (result.IsSuccessStatusCode)
                    {
                        List<string> DataRuc = new List<string>();
                        string html = await result.Content.ReadAsStringAsync();
                        string[] filas = html.Split(new string[1]
                        {
              "list-group-item\""
                        }, StringSplitOptions.None);
                        int n = filas.Length;
                        int posClave = -1;
                        int posValor = -1;
                        int posMayor = -1;
                        int posMenor = -1;
                        for (int index1 = 0; index1 < n; ++index1)
                        {
                            string fila = filas[index1];
                            posClave = fila.IndexOf("list-group-item-heading");
                            if (posClave > -1)
                            {
                                posMayor = fila.IndexOf(">", posClave);
                                if (posMayor > -1)
                                {
                                    posMenor = fila.IndexOf("<", posMayor);
                                    string clave = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                                    posClave = index1 > 1 ? fila.IndexOf("list-group-item-text", posMenor) : fila.IndexOf("list-group-item-heading", posMenor);
                                    string valor;
                                    if (posClave > -1)
                                    {
                                        posMayor = fila.IndexOf(">", posClave);
                                        posMenor = fila.IndexOf("<", posMayor);
                                        valor = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                                        DataRuc.Add(clave + "|" + valor);
                                        posClave = fila.IndexOf("list-group-item-heading", posMenor);
                                        if (posClave > -1)
                                        {
                                            posMayor = fila.IndexOf(">", posClave);
                                            if (posMayor > -1)
                                            {
                                                posMenor = fila.IndexOf("<", posMayor);
                                                clave = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                                                posClave = fila.IndexOf("list-group-item-text", posMenor);
                                                posMayor = fila.IndexOf(">", posClave);
                                                posMenor = fila.IndexOf("<", posMayor);
                                                valor = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                                                DataRuc.Add(clave + "|" + valor);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        posClave = fila.IndexOf("tblResultado", posMenor);
                                        if (posClave > -1)
                                        {
                                            fila.Substring(posClave, fila.Length - posClave);
                                            List<string> stringList = new List<string>();
                                            string[] strArray = html.Split(new string[1]
                                            {
                        "<tr>"
                                            }, StringSplitOptions.None);
                                            int length = strArray.Length;
                                            for (int index2 = 0; index2 < length; ++index2)
                                            {
                                                int num1 = strArray[index2].IndexOf("<td>");
                                                if (num1 > -1)
                                                {
                                                    int num2 = strArray[index2].IndexOf("<", num1 + 1);
                                                    stringList.Add(strArray[index2].Substring(num1 + 4, num2 - (num1 + 4) - 1));
                                                }
                                            }
                                            valor = string.Join(",", (IEnumerable<string>)stringList);
                                            DataRuc.Add(clave + "|" + valor);
                                        }
                                    }
                                }
                            }
                        }
                        rpta = string.Join("¬", (IEnumerable<string>)DataRuc);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return rpta;
        }

        private static async Task<string> obtenerRuc(string ruc)
        {
            string rpta = "";
            CookieContainer cookies = new CookieContainer();
            using (HttpClient client = new HttpClient((HttpMessageHandler)new HttpClientHandler()
            {
                CookieContainer = cookies,
                UseCookies = true,
                AllowAutoRedirect = true
            }))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
                    client.DefaultRequestHeaders.Add("accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Host", "e-consultaruc.sunat.gob.pe");
                    ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)((se, cert, chain, sslerror) => true);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    string url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=random";
                    HttpResponseMessage result = await client.GetAsync(new Uri(url));
                    if (result.IsSuccessStatusCode)
                    {
                        string token = await result.Content.ReadAsStringAsync();
                        List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>()
            {
              new KeyValuePair<string, string>("accion", "consPorRuc"),
              new KeyValuePair<string, string>("nroRuc", ruc),
              new KeyValuePair<string, string>("contexto", "ti-it"),
              new KeyValuePair<string, string>("modo", "1"),
              new KeyValuePair<string, string>("numRnd", token)
            };
                        FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)pairs);
                        url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias";
                        result = await client.PostAsync(new Uri(url), (HttpContent)content);
                        if (result.IsSuccessStatusCode)
                        {
                            List<string> DataRuc = new List<string>();
                            string html = await result.Content.ReadAsStringAsync();
                            rpta = html;
                //            string[] filas = html.Split(new string[1]
                //            {
                //"list-group-item\""
                //            }, StringSplitOptions.None);


                            //int n = filas.Length;
                            //int posClave = -1;
                            //int posValor = -1;
                            //int posMayor = -1;
                            //int posMenor = -1;
                            //  for (int index1 = 0; index1 < n; ++index1)
                            //  {
                            //      string fila = filas[index1];
                            //      posClave = fila.IndexOf("list-group-item-heading");
                            //      if (posClave > -1)
                            //      {
                            //          posMayor = fila.IndexOf(">", posClave);
                            //          if (posMayor > -1)
                            //          {
                            //              posMenor = fila.IndexOf("<", posMayor);
                            //              string clave = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                            //              posClave = index1 > 1 ? fila.IndexOf("list-group-item-text", posMenor) : fila.IndexOf("list-group-item-heading", posMenor);
                            //              string valor;
                            //              if (posClave > -1)
                            //              {
                            //                  posMayor = fila.IndexOf(">", posClave);
                            //                  posMenor = fila.IndexOf("<", posMayor);
                            //                  valor = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                            //                  DataRuc.Add(clave + "|" + valor);
                            //                  posClave = fila.IndexOf("list-group-item-heading", posMenor);
                            //                  if (posClave > -1)
                            //                  {
                            //                      posMayor = fila.IndexOf(">", posClave);
                            //                      if (posMayor > -1)
                            //                      {
                            //                          posMenor = fila.IndexOf("<", posMayor);
                            //                          clave = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                            //                          posClave = fila.IndexOf("list-group-item-text", posMenor);
                            //                          posMayor = fila.IndexOf(">", posClave);
                            //                          posMenor = fila.IndexOf("<", posMayor);
                            //                          valor = fila.Substring(posMayor + 1, posMenor - posMayor - 1);
                            //                          DataRuc.Add(clave + "|" + valor);
                            //                      }
                            //                  }
                            //              }
                            //              else
                            //              {
                            //                  posClave = fila.IndexOf("tblResultado", posMenor);
                            //                  if (posClave > -1)
                            //                  {
                            //                      string str = fila.Substring(posClave, fila.Length - posClave);
                            //                      List<string> stringList = new List<string>();
                            //                      string[] strArray = str.Split(new string[1]
                            //                      {
                            //"<tr>"
                            //                      }, StringSplitOptions.None);
                            //                      int length = strArray.Length;
                            //                      for (int index2 = 0; index2 < length; ++index2)
                            //                      {
                            //                          int num1 = strArray[index2].IndexOf("<td>");
                            //                          if (num1 > -1)
                            //                          {
                            //                              int num2 = strArray[index2].IndexOf("<", num1 + 1);
                            //                              stringList.Add(strArray[index2].Substring(num1 + 4, num2 - (num1 + 4)));
                            //                          }
                            //                      }
                            //                      valor = string.Join(",", (IEnumerable<string>)stringList);
                            //                      DataRuc.Add(clave + "|" + valor);
                            //                  }
                            //              }
                            //          }
                            //      }
                            //  }
                            //rpta = string.Join("¬", (IEnumerable<string>)DataRuc);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return rpta;
        }
    }
}