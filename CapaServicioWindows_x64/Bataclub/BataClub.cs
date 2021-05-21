using CapaServicioWindows_x64.Conexion;
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
        #endregion
    }
}
