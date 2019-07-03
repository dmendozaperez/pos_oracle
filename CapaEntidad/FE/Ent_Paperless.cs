using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.FE
{
    public class Ent_Paperless_Envio
    {
        public string ruc { get; set; }//RUC de la empresa
        public string login { get; set; }//Login del usuario
        public string password { get; set; }//Password del usuario
        public string tipodoc { get; set; }//Tipo de documento Electrónicos según la SUNAT
                                            //01 = Factura
                                            //03 = Boleta
                                            //07 = Nota de Crédito
                                            //08 = Nota de Débito
                                            //09 = Guía de Remisión Remitente
                                            //720 = Comprobante de Retención
                                            //40 = Comprobante de Percepción        public string folio { get; set; }// F001-1
        public string tipoRetorno { get; set; }//
        //0 = ID asignado
        //1 = URL del XML
        //2 = URL del PDF
        //3 = Estado en la SUNAT
        //4 = Folio Asignado(Serie-Correlativo)
        //5 = Bytes del PDF en Base64
        //6 = PDF417(Cadena de texto a imprimir en el PDF 417)
        //7 = HASH(Cadena de texto)


    }
    public class Ent_Paperless_Return
    {
        public string codigo { get; set; }
        public string respuesta { get; set; }
    }
}
