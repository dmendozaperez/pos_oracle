﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Tesseract;

namespace Ws_ConsultReniecSunat.Bll
{
    public class PersonaSunat
    {

        public enum Resul
        {
            Ok = 0,
            NoResul = 1,
            ErrorCapcha = 2,
            Error = 3,
        }
        private Resul state;
        
        private string _Nombres;
        private string _ApePaterno;
        private string _ApeMaterno;
        private string _direccion;
        private string _telefono;
        private string _estado;
        private CookieContainer myCookie;

        public Image GetCapcha { get { return ReadCapcha(); } }
        public string Nombres { get { return _Nombres; } }
        public string ApePaterno { get { return _ApePaterno; } }
        public string ApeMaterno { get { return _ApeMaterno; } }
        public string direccion { get { return _direccion; } }
        public string telefono { get { return _telefono; } }
        public string estado { get { return _estado; } }

        public Resul GetResul { get { return state; } }
        TesseractEngine engine;
        public PersonaSunat(Boolean valida_cli=false)
        {
            try
            {
                myCookie = null;
                myCookie = new CookieContainer();
                if (valida_cli) return;
               
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ReadCapcha();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Boolean ValidarCertificado(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        //Aqui obtenemos el captcha
        private Image ReadCapcha()
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidarCertificado);
                //Esta es la direccion que les pase en el grupo de facebook para obtener el captcha
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image&magic=2");
                myWebRequest.CookieContainer = myCookie;
                myWebRequest.Proxy = null;
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myImgStream = myWebResponse.GetResponseStream();
                return Image.FromStream(myImgStream);
                //Modificación 1 ... Esta fue la primera modificación ... cree un mapa de bits que utilizaré como
                //parámetro para en fin ... mejor se los muestro xd
                Bitmap bm = new Bitmap(Image.FromStream(myImgStream));
                //quitamos el color a nuestro mapa de bits 
                qutarColor(bm);
                //Procesamos la imagen (separación de carácteres, alineación etc)
                //Y se devuelve la imagen lista para ser procesada por el OCR
                return (Image)PreProcessImage(bm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //En este metodo es el que utiliza el tesseract ... se obtiene la imagen del captcha terminada
        // y devuelve el texto obtenido ...
        public string UseTesseract(string _ruta = "")
        {

            string text = String.Empty;
            try
            {
                //Recordemos que el metodo ( si ya obviaré las tildes ) ... 
                // el metodo ReadCapcha devuelve la imagen ya procesada ...
                using (Bitmap bm = new Bitmap(ReadCapcha()))
                {

                    //Instanciamos el TesseractEngine declarado arriba !
                    //engine = new TesseractEngine(@".\tessdata", "eng", EngineMode.Default);                    
                    engine = new TesseractEngine(@_ruta, "eng", EngineMode.Default);
                    engine.DefaultPageSegMode = PageSegMode.SingleBlock;
                    Tesseract.Page p = engine.Process(bm);
                    text = p.GetText().Trim().ToUpper().Replace(" ", "");
                    //  Console.WriteLine("Text recognized: " + text);
                }
            }
            catch
            {

            }
            //Retornamos luego del trabajo del OCR el texto obtenido 
            return text;
        }
        //En este metodo se procesa la imagen, se separan los caracteres de manera individual
        private static Bitmap PreProcessImage(Bitmap memStream)
        {
            Bitmap bm = memStream;
            // Flatten Image to Black and White
            qutarColor(bm);

            // We have a know 6 charcter captcha
            List<Rectangle> charcters = new List<Rectangle>();
            List<int> blackin_x = new List<int>();

            int x_max = bm.Width - 1;
            int y_max = bm.Height - 1;

            // Here we are going to scan through the columns to determine if there in any black in them (charcter)
            for (int temp_x = 0; temp_x <= x_max; temp_x++)
            {
                for (int temp_y = 0; temp_y <= y_max; temp_y++)
                {
                    if (bm.GetPixel(temp_x, temp_y).Name != "ffffffff")
                    {
                        blackin_x.Add(temp_x);
                        break;
                    }
                }
            }

            // Building inital rectangles with X Boundaries
            // This is where we are using our previous results to build the horiztonal boundaries of our charcters
            int temp_start = blackin_x[0];
            for (int temp_x = 0; temp_x < blackin_x.Count - 1; temp_x++)
            {
                if (temp_x == blackin_x.Count - 2) // handles the last iteration
                {
                    Rectangle r = new Rectangle();
                    r.X = temp_start;
                    r.Width = blackin_x[temp_x] - r.X + 2;

                    charcters.Add(r);
                }
                if (blackin_x[temp_x] - blackin_x[temp_x + 1] == -1)
                {
                    continue;
                }
                else
                {
                    Rectangle r = new Rectangle();
                    r.X = temp_start;
                    r.Width = blackin_x[temp_x] - r.X + 1;
                    temp_start = blackin_x[temp_x + 1];
                    charcters.Add(r);
                }

            }

            // Finish out by getting y boundaries
            for (int i = 0; i < charcters.Count; i++)
            {
                Rectangle r = charcters[i];

                for (int temp_y = 0; temp_y < y_max; temp_y++)
                {
                    if (r.Y == 0)
                    {
                        if (!IsRowWhite(bm, temp_y, r.X, r.X + r.Width - 1))
                            r.Y = temp_y;
                    }
                    else if (r.Height == 0)
                    {
                        if (IsRowWhite(bm, temp_y, r.X, r.X + r.Width - 1))
                            r.Height = temp_y - r.Y + 1;
                    }
                    else
                        break;

                }

                charcters[i] = r; // have to do this as rectangle is struct

            }

            int totalWidth = 1 + charcters.Sum(o => o.Width) + (charcters.Count * 2); // we need padding
            int totalHeight = charcters.Max(o => o.Height) + 2; // padding here too 
            int current_x = 1; // start off the left edge 1px

            Bitmap bmp = new Bitmap(totalWidth, totalHeight);
            Graphics g = Graphics.FromImage(bmp);

            // the following four lines are added to help image quality
            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // take our four charcters and move them into a new bitmap 
            foreach (Rectangle r in charcters)
            {
                g.DrawImage(bm, current_x, 1, r, GraphicsUnit.Pixel);
                current_x += r.Width + 2;
            }

            //  bmp.Save(@"C:\postprocess.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return bmp;
        }

        /// <summary>
        /// Determines whether the specified row in the bitmap contains white.
        /// </summary>
        /// <param name="bm">The Image.</param>
        /// <param name="temp_y">The temp_y.</param>
        /// <param name="x">The x.</param>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        private static bool IsRowWhite(Bitmap bm, int temp_y, int x, int width)
        {
            for (int i = x; i < width; i++)
            {
                if (bm.GetPixel(i, temp_y).Name != "ffffffff")
                    return false;
            }
            return true;
        }
        // Aqui quitamos el color ... lo dejamos en blanco y negro (El captcha)
        public static void qutarColor(Bitmap bm)
        {
            for (int x = 0; x < bm.Width; x++)
                for (int y = 0; y < bm.Height; y++)
                {
                    Color pix = bm.GetPixel(x, y);
                    //Aqui puedes jugar con los valores del brillo yo he probado poco pero tu puedes cambiarlo
                    if (pix.GetBrightness() > 0.870f)
                    {
                        bm.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bm.SetPixel(x, y, Color.Black);
                    }
                }
        }
        private static async Task<string> buscar_ruc_new(string ruc)
        {
            try
            {
                return await ConsultaSunatRucMbb.obtenerInformacion(ruc);
            }
            catch (Exception)
            {
                throw;

            }

        }
        public void GetInfo(string numDni, string ImgCapcha)
        {
            string xRazSoc = ""; string xEst = ""; string xCon = ""; string xDir = ""; string xAg = "";
            try
            {

                #region<API DE SUNAT CONSULTA DE RUC>
                //string myUrl_API = String.Format("https://api.sunat.cloud/ruc/{0}",
                //                       numDni);

                //string myUrl_API = "https://dniruc.apisperu.com/api/v1/ruc/" + numDni.ToString() + "?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImRhdmlkX21lbmRvemFwQGhvdG1haWwuY29tIn0.MkWKjhAArrvYhkjDzXcsZC_eaIs_vCzzVzL3AyVXSZE";

                //string myUrl_API = "https://dni.optimizeperu.com/api/company/" + numDni.ToString() + "?format=json";
                //string response_str = "";
                //HttpWebRequest request = null;
                //string myUrl_API = "https://dni.optimizeperu.com/api/company/" + numDni.ToString() + "?format=json";
                ////System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //request = (HttpWebRequest)WebRequest.Create(myUrl_API);
                ////request.Headers["Authorization"] = "Bearer X-Trx-Api-Key";
                //request.ContentType = "application/json";
                ////request.Headers.Add("X-Trx-Api-Key", apiKey);

                ////HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)request.GetResponse();

                //var response = (HttpWebResponse)request.GetResponse();
                //response_str = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //HttpWebRequest myWebRequest_API = (HttpWebRequest)WebRequest.Create(myUrl_API);
                //myWebRequest_API.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                //myWebRequest_API.CookieContainer = myCookie;
                //myWebRequest_API.Credentials = CredentialCache.DefaultCredentials;
                //myWebRequest_API.Proxy = null;
                //HttpWebResponse myHttpWebResponse_API = (HttpWebResponse)myWebRequest_API.GetResponse();
                //Stream myStream_API = myHttpWebResponse_API.GetResponseStream();
                //Encoding encode_API = System.Text.Encoding.GetEncoding("utf-8");
                //StreamReader myStreamReader_API = new StreamReader(myStream_API, encode_API);

                //var jason = myStreamReader_API.ReadToEnd();
                //DataSunat_Jason ent_sunat=new DataSunat_Jason();
                //ent_sunat.ruc = numDni;
                //ent_sunat.n
                //ent_sunat = JsonConvert.DeserializeObject<DataSunat_Jason>(jason);

                //if (ent_sunat != null)
                //{
                //    _Nombres = ""; //(ent_sunat.razon_social== "******")?ent_sunat.nombre_comercial: ent_sunat.razon_social;
                //    _direccion = ""; //ent_sunat.direccion;
                //    _telefono = "";// ent_sunat.telefono;
                //    //_estado = (Left(ent_sunat.contribuyente_condicion, 1) == "H") ? "A" : "I";
                //    _estado = "A"; //(Left(ent_sunat.condicion, 1) == "H") ? "A" : "I";
                //    return;
                //}


                //Leemos los datos
                //string xDat_API = HttpUtility.HtmlDecode(myStreamReader_API.ReadToEnd());



                #endregion

                //A este link le pasamos los datos , RUC y valor del captcha
                //string myUrl = String.Format("http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc={0}&codigo={1}",
                //                        numDni, ImgCapcha);

                //string myUrl = String.Format("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS03Alias?accion=consPorRuc&nroRuc={0}&codigo={1}",
                //                       numDni, ImgCapcha);

                //HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
                //myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                //myWebRequest.CookieContainer = myCookie;
                //myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                //myWebRequest.Proxy = null;
                //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                //Stream myStream = myHttpWebResponse.GetResponseStream();
                //Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                //StreamReader myStreamReader = new StreamReader(myStream, encode);
                //Leemos los datos

                #region<region del consulta entidad sql>
                DataEntidad obj=Basico.buscar_ruc_entidad(numDni);

                if (obj!=null)
                {
                    _estado = "A";
                    _Nombres = obj.razon_social;
                }
                else
                {
                    _estado = "A";
                    _Nombres = "-";
                }

                #endregion

                    //if obj==

                    //string xDat = buscar_ruc_new(numDni).Result;
                    //string[] tabla;
                    //tabla = Regex.Split(xDat, "|");

                    ////xDat = xDat.Replace("\r\n", "");

                    //string[] filas1 = xDat.Split(new string[1]
                    //         {
                    //" <div class=\"list-group-item\">"
                    //         }, StringSplitOptions.None);


                    //string nombre_comercial = filas1[3].ToString().Replace("\r\n","");
                    //nombre_comercial = nombre_comercial.Replace("\t","").TrimEnd().TrimStart();
                    //nombre_comercial = nombre_comercial.Replace("    ", "");
                    //nombre_comercial = nombre_comercial.Replace("</div> </div>", "");
                    //nombre_comercial = nombre_comercial.Replace("<div class=\"row\">", "");
                    //nombre_comercial = nombre_comercial.Replace("<div class=\"col-sm-5\"> <h4 class=\"list-group-item-heading\">", "");
                    //nombre_comercial = nombre_comercial.Replace("Nombre Comercial:", "");
                    //nombre_comercial = nombre_comercial.Replace("</h4> </div> <div class=\"col-sm-7\">", "");
                    //nombre_comercial = nombre_comercial.Replace("<p class=\"list-group-item-text\">","");
                    //nombre_comercial = nombre_comercial.Replace("</p> </div>", "").Trim().TrimStart().TrimEnd();
                    //nombre_comercial = nombre_comercial.Replace("</div>  </div>", "").Trim().TrimStart().TrimEnd();

                    //Boolean valida_nom = nombre_comercial.Contains("Tipo de Documento: DNI");

                    //if (nombre_comercial== "-" || valida_nom)
                    //{
                    //    nombre_comercial = "";
                    //    nombre_comercial = filas1[1].ToString().Replace("\r\n", "");
                    //    nombre_comercial = nombre_comercial.Replace("\t", "").TrimEnd().TrimStart();
                    //    nombre_comercial = nombre_comercial.Replace("    ", "");
                    //    nombre_comercial = nombre_comercial.Replace("</div> </div>", "");
                    //    nombre_comercial = nombre_comercial.Replace("<div class=\"row\">", "");
                    //    nombre_comercial = nombre_comercial.Replace("<div class=\"col-sm-5\"> <h4 class=\"list-group-item-heading\">", "");
                    //    nombre_comercial = nombre_comercial.Replace("Nombre Comercial:", "");
                    //    nombre_comercial = nombre_comercial.Replace("</h4> </div> <div class=\"col-sm-7\">", "");
                    //    nombre_comercial = nombre_comercial.Replace("<p class=\"list-group-item-text\">", "");
                    //    nombre_comercial = nombre_comercial.Replace("</p> </div>", "").Trim().TrimStart().TrimEnd();
                    //    nombre_comercial = nombre_comercial.Replace("</div>  </div>", "").Trim().TrimStart().TrimEnd();
                    //    nombre_comercial = nombre_comercial.Replace("N&uacute;mero de RUC: <h4 class=\"list-group-item-heading\">", "");
                    //    nombre_comercial = nombre_comercial.Replace("</h4> </div>","").Trim().TrimStart().TrimEnd();
                    //    nombre_comercial = nombre_comercial.Substring(13, nombre_comercial.Length - 13);
                    //    nombre_comercial = nombre_comercial.Trim();
                    //}

                    ////string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

                    //var estado_sunat_I=xDat.Contains("NO HABIDO");

                    //if (estado_sunat_I)
                    //{
                    //    _estado = "I";
                    //}
                    //else
                    //{  
                    //    var estado_sunat_A = xDat.Contains("HABIDO");

                    //    if (estado_sunat_A)
                    //    {
                    //        _estado = "A";
                    //    }
                    //}

                    //if (xDat.Length <= 635)
                    //{
                    //    return;
                    //}



                    //string[] tabla;
                    //xDat = xDat.Replace("     ", " ");
                    //xDat = xDat.Replace("    ", " ");
                    //xDat = xDat.Replace("   ", " ");
                    //xDat = xDat.Replace("  ", " ");
                    //xDat = xDat.Replace("( ", "(");
                    //xDat = xDat.Replace(" )", ")");
                    //xDat = xDat.Replace("class", "");
                    //xDat = xDat.Replace("colspan=1", "");
                    //xDat = xDat.Replace("colspan=2", "");
                    //xDat = xDat.Replace("colspan=3", "");
                    //xDat = xDat.Replace("bgn", "");
                    //xDat = xDat.Replace("bg", "");
                    //xDat = xDat.Replace("=", "");
                    //xDat = xDat.Replace("\"", "");
                    //xDat = xDat.Replace("<td  >", "<td>");
                    //xDat = xDat.Replace("</td>", "");
                    //xDat = xDat.Replace("-->\r\n<!--", "");
                    //xDat = xDat.Replace("\r\n", "");
                    //xDat = xDat.Replace("</tr>", "");
                    //xDat = xDat.Replace("<tr>", "");
                    //xDat = xDat.Replace("      <td >", "<td>");
                    //xDat = xDat.Replace("<td   >", "<td>");
                    //xDat = xDat.Replace("<td width27%  >", "<td>");
                    //xDat = xDat.Replace("\t", "");
                    //xDat = xDat.Replace("     <td >", "");
                    //xDat = xDat.Replace("<!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207", "");
                    //xDat = xDat.Replace("--> <!--", "");

                    //string[] filas = xDat.Split(new string[1]
                    //          {
                    //"list-group-item\""
                    //          }, StringSplitOptions.None);

                    //Lo convertimos a tabla o mejor dicho a un arreglo de string como se ve declarado arriba
                    //tabla = Regex.Split(xDat, "<td class");
                    //tabla = Regex.Split(xDat, "<td>");
                    //if (tabla.Length != 1 && tabla.Length != 5)
                    //{
                    //    for (int i = 0; i < tabla.Length; i++)
                    //    {
                    //        switch (tabla[i])
                    //        {
                    //            case "Número Ruc.":
                    //                //_Info.RazonSocial = _resul[i + 2].Substring(14);
                    //                break;
                    //            case "Antiguo Ruc.":
                    //                //_Info.AntiguoRuc = _resul[i + 5];
                    //                break;
                    //            case "Estado.":
                    //                //_Info.Estado = _resul[i + 2];
                    //                break;
                    //            case "Agente Retención IGV.":
                    //                //_Info.EsAgenteRetencion = _resul[i + 3];
                    //                break;
                    //            case "Tipo de Documento:  ":
                    //                _Nombres = tabla[i + 1].ToString().Trim();
                    //                //_Nombres=_Nombres.Replace("�", "Ñ");
                    //                break;
                    //            case "Nombre Comercial:  ":
                    //                _Nombres = tabla[i + 1].ToString().Trim();
                    //                //_Nombres=_Nombres.Replace("�", "Ñ");
                    //                break;
                    //            case "Dirección del Domicilio Fiscal: ":
                    //                _direccion = tabla[i + 1].ToString().Trim();
                    //                //_direccion=_direccion.Replace("�", "Ñ");
                    //                break;
                    //            case "Teléfono(s):  ":
                    //                _telefono = tabla[i + 1].ToString().Trim();
                    //                break;
                    //            case "Dependencia.":
                    //                //_Info.Dependencia = _resul[i + 3];
                    //                break;
                    //            case "Tipo.":
                    //                //_Info.Tipo = _resul[i + 3];
                    //                break;
                    //        }
                    //    }
                    //_Nombres = nombre_comercial;

                    //if (_Nombres.Length == 1)
                    //{
                    //    _Nombres = tabla[1].ToString().Trim();
                    //    _Nombres = _Nombres.Substring(_Nombres.IndexOf('-') + 1, _Nombres.Length - (_Nombres.IndexOf('-') + 1));
                    //    //_Nombres = Nombres.Replace("�", "Ñ");
                    //    _Nombres = _Nombres.Trim();
                    //}

                    ////VERIFICAR SI ES QUE ES PERSONA NATURAL
                    //if (Left(numDni, 2) == "10")
                    //{
                    //    _Nombres = tabla[1].ToString().Trim();
                    //    _Nombres = _Nombres.Substring(_Nombres.IndexOf('-') + 1, _Nombres.Length - (_Nombres.IndexOf('-') + 1));
                    //    //_Nombres = Nombres.Replace("�", "Ñ");
                    //    _Nombres = _Nombres.Trim();
                    //}
                    //**************************************** 

                //}
                //else
                //{
                //    _Nombres = "Error!";
                //}

               
            }
            catch (Exception ex)
            {
                _Nombres = "Error!";
                // throw ex;
            }
        }
        public string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

    }

    public class SunatValida
    {
        public string Estado { get; set; }
        public string Descripcion { get; set; }
    }
    public class DataSunat
    {
        public SunatValida Valida_Sunat { get; set; }
        public string Ruc { get; set; }
        public string Razon_Social { get; set; }
        public string Direccion { get; set; }                
        public string Telefono { get; set; }
        public string Estado { get; set; }
        
    }
    public class DataSunat_Jason
    {
        public string ciiu { get; set; }
        public string fecha_actividad { get; set; }
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string telefono { get; set; }
        public string contribuyente_condicion { get; set; }
        public string nombre_comercial { get; set; }
        public string contribuyente_tipo { get; set; }
        public string contribuyente_estado { get; set; }
        public string echa_inscripcion { get; set; }
        public string condicion { get; set; }

        public string direccion { get; set; }
        public string sistema_emision { get; set; }
        public string actividad_exterior { get; set; }
        public string sistema_contabilidad { get; set; }
        public string emision_electronica { get; set; }
        public string fecha_inscripcion_ple { get; set; }
    }

    public class DataReniec_Jason
    {
        public string dni { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string codVerifica { get; set; }
    }
    public class DataEntidad
    {
        public string ruc { get; set; }
        public string razon_social { get; set; }
    }

}