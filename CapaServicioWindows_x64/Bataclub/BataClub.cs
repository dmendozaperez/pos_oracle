﻿using CapaServicioWindows_x64.Conexion;
using CapaServicioWindows_x64.Entidad;
using ServiceInspector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;

namespace CapaServicioWindows_x64.Bataclub
{
    public class BataClub
    {
        public string genera_miembro_bataclub()
        {
            string error = "";
            string sqlquery = "USP_BATACLUB_GENERA_MIEMBRO_CUPON";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery,cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception exc)
                    {
                        error = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;                
            }
            return error;
        }
        public string genera_envio_correo_bataclub()
        {
            string error = "";
            string sqlquery = "[USP_BATACLUB_ENVIO_CORREO]";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception exc)
                    {
                        error = exc.Message;

                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

        public string genera_update_orce_cupones()
        {
            string error = "";
            string sqlquery = "[USP_ORCE_UPDATE_CUPONES]";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception exc)
                    {

                        error = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

        public string genera_procesos_compartir()
        {
            string sqlquery = "USP_BATA_PROCESOS_COMPARTIR";
            string valida = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        valida = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {

                valida = exc.Message;
            }
            return valida;
        }

        #region <ACTUALIZACION AL ORCE BATACLUB,TARJETA DE FIDELIZACION>
        #region<ACTUALIZACION DE CLIENTES>
        private List<BataClub_Cliente_Orce> listar_cliente()
        {
            List<BataClub_Cliente_Orce> listar = null;
            string sqlquery = "USP_BATACLUB_ORCE_CLIENTES";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BataClub_Cliente_Orce>();
                            listar = (from DataRow fila in dt.Rows
                                      select new BataClub_Cliente_Orce()
                                      {
                                          dni=fila["dni"].ToString(),
                                          nombres = fila["nombres"].ToString(),
                                          apellidos = fila["apellidos"].ToString(),
                                          genero = fila["genero"].ToString(),
                                          correo = fila["correo"].ToString(),
                                          fec_nac = (fila["fec_nac"]==System.DBNull.Value)?(DateTime?)null:Convert.ToDateTime(fila["fec_nac"]),
                                          telefono = fila["telefono"].ToString(),
                                          ubigeo = fila["ubigeo"].ToString(),
                                          cod_tda = fila["cod_tda"].ToString(),
                                          miem_bataclub =Convert.ToBoolean(fila["miem_bataclub"]),
                                      }).ToList();
                        }
                    }
                }
            }
            catch(Exception exc) 
            {
                throw exc;
                listar = new List<BataClub_Cliente_Orce>();                
            }
            return listar;
        }

        private void update_cliente_orce(string dni)
        {
            string sqlquery = "USP_BATACLUB_ORCE_ENVIO_ACT";            
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DNI", dni);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {

                
            }
        }
        
        public string creacion_actualizacion_clientes_orce()
        {
            string valida = "";
            
            try
            {
                List<BataClub_Cliente_Orce> lista_cliente = listar_cliente();

                foreach(BataClub_Cliente_Orce cl in lista_cliente)
                {
                    string error = "";
                    if (actualizar_cliente_orce(cl,ref error)) update_cliente_orce(cl.dni);

                    if (error.Length > 0) valida += error;
                }
             
            }
            catch (Exception exc)
            {
                valida= valida + "=>" +  exc.Message;                
            }
            return valida;
        }

        private Boolean actualizar_cliente_orce(BataClub_Cliente_Orce cliente,ref String error)
        {
            Boolean valida = false;
            OrceCliente.CustomerServicesApiClient ws_clientes = null;
            try
            {
                ws_clientes = new OrceCliente.CustomerServicesApiClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MessageBehavior messageBehavior = new MessageBehavior("Authorization", ConexionOrce.VALUE);

                ws_clientes.Endpoint.EndpointBehaviors.Add(messageBehavior);

                OrceCliente.CustomerType CustomerType = new OrceCliente.CustomerType();
                OrceCliente.EntityInformationType EntityInformation = new OrceCliente.EntityInformationType();
                OrceCliente.IndividualType Individual = new OrceCliente.IndividualType();
                OrceCliente.CustomerNameTypeEntry[] Name = new OrceCliente.CustomerNameTypeEntry[2];

                Name[0] = new OrceCliente.CustomerNameTypeEntry();
                Name[0].Location = OrceCliente.NameLocationType.First;
                Name[0].LocationSpecified = true;
                Name[0].Value = cliente.nombres;

                Name[1] = new OrceCliente.CustomerNameTypeEntry();
                Name[1].Location = OrceCliente.NameLocationType.Last;
                Name[1].LocationSpecified = true;
                Name[1].Value = cliente.apellidos;

                OrceCliente.ContactPreferenceType[] PersonalPreferences = new OrceCliente.ContactPreferenceType[1];
                OrceCliente.AlternateKeyType[] AlternateKey = new OrceCliente.AlternateKeyType[1];
                OrceCliente.CustomAttributeType[] CustomAttribute = new OrceCliente.CustomAttributeType[1];

                OrceCliente.ContactInformationType ContactInformation = new OrceCliente.ContactInformationType();
                OrceCliente.AddressType[] Address = new OrceCliente.AddressType[1];
                OrceCliente.EMailType[] EMail = new OrceCliente.EMailType[1];
                OrceCliente.TelephoneType[] Telephone = new OrceCliente.TelephoneType[1];

                OrceCliente.PersonalSummaryType PersonalSummary = new OrceCliente.PersonalSummaryType();

                OrceCliente.AffiliationType[] Affiliation = new OrceCliente.AffiliationType[1];

                Affiliation[0] = new OrceCliente.AffiliationType();
                Affiliation[0].RetailStoreID = cliente.cod_tda;
                Affiliation[0].Action = "Add";

                Address[0] = new OrceCliente.AddressType();
                Address[0].PrimaryFlag = true;
                Address[0].TypeCode = "HOME";
                Address[0].ValidFlag = true;
                Address[0].Country = "PE";
                Address[0].County = "PE";
                Address[0].PostalCode = cliente.ubigeo;

                EMail[0] = new OrceCliente.EMailType();
                EMail[0].PrimaryFlag = true;
                EMail[0].TypeCode = "HOME";
                EMail[0].ContactPreferenceCode = "Y";
                EMail[0].FormatPreferenceCode = "HTML";
                EMail[0].EMailAddress = cliente.correo;

                Telephone[0] = new OrceCliente.TelephoneType();
                Telephone[0].PrimaryFlag = true;
                Telephone[0].TypeCode = "MOBILE";
                Telephone[0].PhoneNumber = cliente.telefono;

                if (cliente.genero.Trim().Length>0) PersonalSummary.GenderType = cliente.genero;
                if (cliente.fec_nac!=null)PersonalSummary.BirthDate =Convert.ToDateTime(cliente.fec_nac);

                ContactInformation.Address = Address;
                ContactInformation.EMail = EMail;
                ContactInformation.Telephone = Telephone;


                Individual.Name = Name;
                Individual.ContactInformation = ContactInformation;

                if (cliente.fec_nac != null) Individual.PersonalSummary = PersonalSummary;

                EntityInformation.Individual = Individual;

                PersonalPreferences[0] = new OrceCliente.ContactPreferenceType();
                PersonalPreferences[0].ContactType = OrceCliente.ContactPreferenceContactType.Mail;
                PersonalPreferences[0].Permission = true;

                AlternateKey[0] = new OrceCliente.AlternateKeyType();
                AlternateKey[0].TypeCode = "DNI";
                AlternateKey[0].AlternateID = cliente.dni;

                CustomAttribute[0] = new OrceCliente.CustomAttributeType();
                CustomAttribute[0].name = "BATA_CLUB_ACEPTADO";

           
                string[] AttributeValue = new string[1];
                AttributeValue[0] =(cliente.miem_bataclub)? "TRUE":"FALSE";
                CustomAttribute[0].AttributeValue = AttributeValue;

           
                CustomerType.OrgName = ConexionOrce.ORG;

                CustomerType.EntityInformation = EntityInformation;
                CustomerType.PersonalPreferences = PersonalPreferences;
                CustomerType.AlternateKey = AlternateKey;
                CustomerType.CustomAttribute = CustomAttribute;

                CustomerType.Affiliation = Affiliation;
                var str = ws_clientes.addOrUpdateCustomer(CustomerType, ConexionOrce.USER);
                valida = true;
            }
            catch(Exception exc)
            {
                error = exc.Message;
                valida = false;
            }
            return valida;
        }
        #endregion

        #region<ACTUALIZACION DE TARJETAS DE FIDELIZACION>
        private Boolean Verifica_existe_cliente(BataClub_Cliente_Orce cliente, ref String error,ref string tarjeta_asignada)
        {
            Boolean valida = false;
            OrceCliente.CustomerServicesApiClient ws_clientes = null;
            try
            {
                ws_clientes = new OrceCliente.CustomerServicesApiClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MessageBehavior messageBehavior = new MessageBehavior("Authorization", ConexionOrce.VALUE);

                ws_clientes.Endpoint.EndpointBehaviors.Add(messageBehavior);

                OrceCliente.AlternateKeyLookupType[] AlternateKeyLookupType = new OrceCliente.AlternateKeyLookupType[1];
                AlternateKeyLookupType[0] = new OrceCliente.AlternateKeyLookupType();
                AlternateKeyLookupType[0].TypeCode = "DNI";
                AlternateKeyLookupType[0].AlternateID = cliente.dni;
                OrceCliente.CustomerResponseType respuesta = ws_clientes.retrieveCustomer("", AlternateKeyLookupType, "", ConexionOrce.USER);

               
                //OrceCliente.InstrumentReturnType[] InstrumentReturnType= respuesta.CustomerCards;

                //InstrumentReturnType.Where(b => b.Description.ToUpper().Contains("BATACLUB")).ToList();


                // awardAccountId=respuesta.AlternateKey
                tarjeta_asignada = (respuesta.CustomerCards.Length==0)?"": respuesta.CustomerCards[0].CardNumber;

                var BC= respuesta.CustomAttribute.Where(a => a.name == "BATA_CLUB_ACEPTADO").ToList();

                if (BC.Count>0)
                {                    
                   valida = (BC[0].AttributeValue[0] == "TRUE") ? true : false;
                }
                
            }
            catch (Exception exc)
            {
                error = exc.Message;
                valida = false;
            }
            return valida;
        }
        private Boolean Verifica_existe_card(BataClub_Cliente_Orce cliente, ref String error)
        {
            Boolean valida = false;
            //OrceCliente.CustomerServicesApiClient ws_clientes = null;
            OrceLoyalty.LoyaltyAccountServicesApiClient ws_loyalty = null;

            try
            {
                ws_loyalty = new OrceLoyalty.LoyaltyAccountServicesApiClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MessageBehavior messageBehavior = new MessageBehavior("Authorization", ConexionOrce.VALUE);

                ws_loyalty.Endpoint.EndpointBehaviors.Add(messageBehavior);

                OrceLoyalty.LoyaltyAwardRuleType[] respuesta= ws_loyalty.getLoyaltyAwardRules(cliente.num_tarjeta, 1, ConexionOrce.USER);
                valida = true;               
            }
            catch (Exception exc)
            {
                error = "";// exc.Message;
                valida = false;
            }
            return valida;
        }
        private void update_cliente_tarjeta_orce(Boolean existe_cliente,Boolean existe_card,string dni,string num_tarjeta,string num_tarjeta_orce)
        {
            string sqlquery = "USP_BATACLUB_ORCE_ADD_CARD_CLIENTE";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DNI_EXIST_BC_ORCE", existe_cliente);
                            cmd.Parameters.AddWithValue("@CARD_EXIST_BC_ORCE", existe_card);
                            cmd.Parameters.AddWithValue("@DNI", dni);
                            cmd.Parameters.AddWithValue("@CARD", num_tarjeta);
                            cmd.Parameters.AddWithValue("@CARD_ORCE", num_tarjeta_orce);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {

                    }
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {


            }
        }
        public string asociar_tarjeta_cliente()
        {
            string valida = "";

            try
            {
                List<BataClub_Cliente_Orce> lista_cliente_tarjeta = listar_cliente_asociar();

                foreach (BataClub_Cliente_Orce cl in lista_cliente_tarjeta)
                {
                    string error = "";
                    string tarjeta_asignada = "";
                  
                    if (!Verifica_existe_cliente(cl, ref error,ref tarjeta_asignada))
                    {
                        update_cliente_tarjeta_orce(false, false, cl.dni, cl.num_tarjeta, "");
                    }
                    else
                    {
                        if (Verifica_existe_card(cl, ref error))
                        {
                            update_cliente_tarjeta_orce(true, true, cl.dni, cl.num_tarjeta, tarjeta_asignada);
                        }
                        else
                        {
                            if (tarjeta_asignada.Length > 0)
                            {
                                update_cliente_tarjeta_orce(true, false, cl.dni, cl.num_tarjeta, tarjeta_asignada);

                            }
                            else
                            {
                                if (asociar_cliente_tarjeta_orce(cl, ref error) && tarjeta_asignada.Length == 0)
                                {
                                    update_cliente_tarjeta_orce(true, false, cl.dni, cl.num_tarjeta, tarjeta_asignada);
                                }
                            }

                         
                               
                        }
                            

                    }

                    if (error.Length > 0) valida += error;
                }

            }
            catch (Exception exc)
            {
                valida = valida + "=>" + exc.Message;
            }
            return valida;
        }
        public List<BataClub_Cliente_Orce> listar_cliente_asociar()
        {
            List<BataClub_Cliente_Orce> listar = null;
            string sqlquery = "USP_BATACLUB_ORCE_ASOCIAR_TARJETA_CLIENTE";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BataClub_Cliente_Orce>();
                            listar = (from DataRow fila in dt.Rows
                                      select new BataClub_Cliente_Orce()
                                      {
                                          dni = fila["dni"].ToString(),
                                          num_tarjeta=fila["cupon"].ToString(),  
                                      }).ToList();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return listar;
        }
        private Boolean asociar_cliente_tarjeta_orce(BataClub_Cliente_Orce cliente, ref String error)
        {
            Boolean valida = false;
            OrceCliente.CustomerServicesApiClient ws_clientes = null;
            try
            {
                ws_clientes = new OrceCliente.CustomerServicesApiClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MessageBehavior messageBehavior = new MessageBehavior("Authorization", ConexionOrce.VALUE);

                ws_clientes.Endpoint.EndpointBehaviors.Add(messageBehavior);

                OrceCliente.AlternateKeyLookupType[] AlternateKeyLookupType = new OrceCliente.AlternateKeyLookupType[1];
                AlternateKeyLookupType[0] = new OrceCliente.AlternateKeyLookupType();
                AlternateKeyLookupType[0].TypeCode = "DNI";
                AlternateKeyLookupType[0].AlternateID = cliente.dni;
                ws_clientes.associateCardToCustomer("", AlternateKeyLookupType, cliente.num_tarjeta, null, "CreateAwards", ConexionOrce.USER);
                valida = true;
            }
            catch 
            {
                valida = false;
                
            }
            return valida;
        }
        #endregion

        #region<REGALO DE TARJETA PARA LOS CLIENTES BATACLUB DE BIENVENIDA>

        private void update_cliente_punto_orce_bienvenido(BataClub_Cliente_Orce cliente)
        {
            string sqlquery = "USP_BATACLUB_ORCE_UPD_BC_BIENVENIDO_ADD_PUNTO";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DNI", cliente.dni);
                            cmd.Parameters.AddWithValue("@MONTO", cliente.monto_punto);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {

                    }
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {


            }
        }

        private List<BataClub_Cliente_Orce> listar_cliente_puntos_bataclub_bienvenida()
        {
            List<BataClub_Cliente_Orce> listar = null;
            string sqlquery = "USP_BATACLUB_ORCE_GET_REGALO_ADD_PUNTOS";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BataClub_Cliente_Orce>();
                            listar = (from DataRow fila in dt.Rows
                                      select new BataClub_Cliente_Orce()
                                      {
                                          id = fila["id"].ToString(),
                                          documento = fila["documento"].ToString(),
                                          dni = fila["dni"].ToString(),
                                          total = Convert.ToDecimal(fila["total"]),
                                          monto_punto = Convert.ToDecimal(fila["monto_punto"]),
                                          num_tarjeta = fila["num_tarjeta"].ToString(),
                                          fecha_transac = Convert.ToDateTime(fila["fecha_transac"]),
                                          cod_tda= fila["COD_TDA"].ToString(),
                                      }).ToList();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
                listar = new List<BataClub_Cliente_Orce>();
            }
            return listar;
        }

        public string agregar_puntos_clientes_bataclub_bienvenida_orce()
        {
            string valida = "";

            try
            {
                List<BataClub_Cliente_Orce> lista_cliente = listar_cliente_puntos_bataclub_bienvenida();

                foreach (BataClub_Cliente_Orce cl in lista_cliente)
                {
                    string error = "";
                    if (add_puntos_cliente_ecommerce_orce(cl, ref error)) update_cliente_punto_orce_bienvenido(cl);

                    if (error.Length > 0) valida += error;
                }

            }
            catch (Exception exc)
            {
                valida = valida + "=>" + exc.Message;
            }
            return valida;
        }

        #endregion

        #region<GENERACION DE PREMIOS SEGUN VENTA, SOLO ECOMMERCE>
        #endregion
        public string agregar_puntos_clientes_ecommerce_orce()
        {
            string valida = "";

            try
            {
                List<BataClub_Cliente_Orce> lista_cliente = listar_cliente_puntos_ecommerce();

                foreach (BataClub_Cliente_Orce cl in lista_cliente)
                {
                    string error = "";
                    if (add_puntos_cliente_ecommerce_orce(cl, ref error)) update_cliente_punto_orce(cl);                    

                    if (error.Length > 0) valida += error;
                }

            }
            catch (Exception exc)
            {
                valida = valida + "=>" + exc.Message;
            }
            return valida;
        }
        private Boolean add_puntos_cliente_ecommerce_orce(BataClub_Cliente_Orce cliente, ref String error)
        {
            Boolean valida = false;
            OrceAward.AwardAccountServicesApiClient ws_award = null;
            OrceCliente.CustomerServicesApiClient ws_clientes = null;
            try
            {
                ws_clientes = new OrceCliente.CustomerServicesApiClient();
                ws_award = new OrceAward.AwardAccountServicesApiClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MessageBehavior messageBehavior = new MessageBehavior("Authorization", ConexionOrce.VALUE);

                ws_award.Endpoint.EndpointBehaviors.Add(messageBehavior);
                ws_clientes.Endpoint.EndpointBehaviors.Add(messageBehavior);

                OrceAward.awardTransactionRequestInfo awardTransactionRequestInfo = new OrceAward.awardTransactionRequestInfo();

                awardTransactionRequestInfo.storeId =(cliente.cod_tda==null)? "EC": cliente.cod_tda;
                awardTransactionRequestInfo.sequenceNumber = cliente.documento;
                awardTransactionRequestInfo.userId = "9999";
                awardTransactionRequestInfo.businessDate = cliente.fecha_transac;
                awardTransactionRequestInfo.registerId = "1";
                

                OrceAward.awardInstrumentData awardInstrumentData = new OrceAward.awardInstrumentData();
                string awardAccountId = "";
                #region <BUSCAR CLIENTE>
                OrceCliente.AlternateKeyLookupType[] AlternateKeyLookupType = new OrceCliente.AlternateKeyLookupType[1];
                AlternateKeyLookupType[0] = new OrceCliente.AlternateKeyLookupType();
                AlternateKeyLookupType[0].TypeCode = "DNI";
                AlternateKeyLookupType[0].AlternateID = cliente.dni;// cliente.dni;
                OrceCliente.CustomerResponseType respuesta = ws_clientes.retrieveCustomer("", AlternateKeyLookupType, "", ConexionOrce.USER);

                OrceCliente.InstrumentReturnType[] InstrumentReturnType = respuesta.CustomerCards;

                //var s= InstrumentReturnType.Where(b=>b.AccountId.
                var AccountIdType = InstrumentReturnType.Where(b => b.CardNumber == cliente.num_tarjeta).ToList(); 
               
                if (AccountIdType.Count>0)
                {
                    var AccountId = AccountIdType[0].AccountId.Where(b => b.Type == "Award").ToList();
                    awardAccountId = AccountId[0].Value.ToString(); 

                }               
                    /**/
               #endregion
                   ///* string awardAccountId */= "56617";/*DE LA WEB SERVICE retrieveCustomer ,  <AccountId Type="Award">56617</AccountId>*/
                awardInstrumentData.cardNumber = cliente.num_tarjeta;
                awardInstrumentData.authenticationData = "";
                awardInstrumentData.cardSwiped = false;

                DateTime fec_caducidad = DateTime.Today.AddDays(365);

                OrceAward.AwardResponseType AwardResponseType = ws_award.issueCoupon(awardAccountId, awardTransactionRequestInfo, awardInstrumentData, "PEN", cliente.monto_punto, fec_caducidad, "EC", null, null, OrceAward.IssueAwardCouponType.EAward, 0, ConexionOrce.USER);               

                valida = true;
            }
            catch (Exception exc)
            {
                error = exc.Message;
                valida = false;
            }
            return valida;
        }
        private List<BataClub_Cliente_Orce> listar_cliente_puntos_ecommerce()
        {
            List<BataClub_Cliente_Orce> listar = null;
            string sqlquery = "USP_BATACLUB_ORCE_GET_EC_ADD_PUNTOS";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BataClub_Cliente_Orce>();
                            listar = (from DataRow fila in dt.Rows
                                      select new BataClub_Cliente_Orce()
                                      {
                                          id = fila["id"].ToString(),
                                          documento = fila["documento"].ToString(),
                                          dni = fila["dni"].ToString(),
                                          total =Convert.ToDecimal(fila["total"]),
                                          monto_punto = Convert.ToDecimal(fila["monto_punto"]),
                                          num_tarjeta= fila["num_tarjeta"].ToString(),
                                          fecha_transac= Convert.ToDateTime(fila["fecha_transac"]),
                                      }).ToList();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
                listar = new List<BataClub_Cliente_Orce>();
            }
            return listar;
        }
        private void update_cliente_punto_orce(BataClub_Cliente_Orce cliente)
        {
            string sqlquery = "USP_BATACLUB_ORCE_UPD_EC_ADD_PUNTO";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@doc_tra_id", cliente.id);
                            cmd.Parameters.AddWithValue("@monto_punto", cliente.monto_punto);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {

                    }
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {


            }
        }
        #endregion
    }
}
