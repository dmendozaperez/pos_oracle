﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppBata_WS_Interfaces.BataEC {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://bata.ecommerce.pe/", ConfigurationName="BataEC.BataEcommerceSoap")]
    public interface BataEcommerceSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el mensaje ws_get_stk_tdaRequest tiene encabezados.
        [System.ServiceModel.OperationContractAttribute(Action="http://bata.ecommerce.pe/ws_get_stk_tda", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaResponse ws_get_stk_tda(AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bata.ecommerce.pe/")]
    public partial class ValidateAcceso : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string usernameField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
                this.RaisePropertyChanged("Username");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bata.ecommerce.pe/")]
    public partial class Ent_Stock_Tienda : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cod_tdaField;
        
        private string des_tdaField;
        
        private string ubigeo_tdaField;
        
        private string direccion_tdaField;
        
        private string cod_artField;
        
        private string tallaField;
        
        private int cantidadField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string cod_tda {
            get {
                return this.cod_tdaField;
            }
            set {
                this.cod_tdaField = value;
                this.RaisePropertyChanged("cod_tda");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string des_tda {
            get {
                return this.des_tdaField;
            }
            set {
                this.des_tdaField = value;
                this.RaisePropertyChanged("des_tda");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ubigeo_tda {
            get {
                return this.ubigeo_tdaField;
            }
            set {
                this.ubigeo_tdaField = value;
                this.RaisePropertyChanged("ubigeo_tda");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string direccion_tda {
            get {
                return this.direccion_tdaField;
            }
            set {
                this.direccion_tdaField = value;
                this.RaisePropertyChanged("direccion_tda");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string cod_art {
            get {
                return this.cod_artField;
            }
            set {
                this.cod_artField = value;
                this.RaisePropertyChanged("cod_art");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string talla {
            get {
                return this.tallaField;
            }
            set {
                this.tallaField = value;
                this.RaisePropertyChanged("talla");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public int cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
                this.RaisePropertyChanged("cantidad");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bata.ecommerce.pe/")]
    public partial class Ent_Stock_Tienda_Acceso : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string estadoField;
        
        private string descripcionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
                this.RaisePropertyChanged("estado");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
                this.RaisePropertyChanged("descripcion");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bata.ecommerce.pe/")]
    public partial class Ent_Stock_Lista : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Ent_Stock_Tienda_Acceso validaField;
        
        private Ent_Stock_Tienda[] lista_stk_tdaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public Ent_Stock_Tienda_Acceso valida {
            get {
                return this.validaField;
            }
            set {
                this.validaField = value;
                this.RaisePropertyChanged("valida");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        public Ent_Stock_Tienda[] lista_stk_tda {
            get {
                return this.lista_stk_tdaField;
            }
            set {
                this.lista_stk_tdaField = value;
                this.RaisePropertyChanged("lista_stk_tda");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ws_get_stk_tda", WrapperNamespace="http://bata.ecommerce.pe/", IsWrapped=true)]
    public partial class ws_get_stk_tdaRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://bata.ecommerce.pe/")]
        public AppBata_WS_Interfaces.BataEC.ValidateAcceso ValidateAcceso;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://bata.ecommerce.pe/", Order=0)]
        public string cod_art;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://bata.ecommerce.pe/", Order=1)]
        public string talla;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://bata.ecommerce.pe/", Order=2)]
        public string cod_ubigeo;
        
        public ws_get_stk_tdaRequest() {
        }
        
        public ws_get_stk_tdaRequest(AppBata_WS_Interfaces.BataEC.ValidateAcceso ValidateAcceso, string cod_art, string talla, string cod_ubigeo) {
            this.ValidateAcceso = ValidateAcceso;
            this.cod_art = cod_art;
            this.talla = talla;
            this.cod_ubigeo = cod_ubigeo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ws_get_stk_tdaResponse", WrapperNamespace="http://bata.ecommerce.pe/", IsWrapped=true)]
    public partial class ws_get_stk_tdaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://bata.ecommerce.pe/", Order=0)]
        public AppBata_WS_Interfaces.BataEC.Ent_Stock_Lista ws_get_stk_tdaResult;
        
        public ws_get_stk_tdaResponse() {
        }
        
        public ws_get_stk_tdaResponse(AppBata_WS_Interfaces.BataEC.Ent_Stock_Lista ws_get_stk_tdaResult) {
            this.ws_get_stk_tdaResult = ws_get_stk_tdaResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BataEcommerceSoapChannel : AppBata_WS_Interfaces.BataEC.BataEcommerceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BataEcommerceSoapClient : System.ServiceModel.ClientBase<AppBata_WS_Interfaces.BataEC.BataEcommerceSoap>, AppBata_WS_Interfaces.BataEC.BataEcommerceSoap {
        
        public BataEcommerceSoapClient() {
        }
        
        public BataEcommerceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BataEcommerceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BataEcommerceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BataEcommerceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaResponse AppBata_WS_Interfaces.BataEC.BataEcommerceSoap.ws_get_stk_tda(AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaRequest request) {
            return base.Channel.ws_get_stk_tda(request);
        }
        
        public AppBata_WS_Interfaces.BataEC.Ent_Stock_Lista ws_get_stk_tda(AppBata_WS_Interfaces.BataEC.ValidateAcceso ValidateAcceso, string cod_art, string talla, string cod_ubigeo) {
            AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaRequest inValue = new AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaRequest();
            inValue.ValidateAcceso = ValidateAcceso;
            inValue.cod_art = cod_art;
            inValue.talla = talla;
            inValue.cod_ubigeo = cod_ubigeo;
            AppBata_WS_Interfaces.BataEC.ws_get_stk_tdaResponse retVal = ((AppBata_WS_Interfaces.BataEC.BataEcommerceSoap)(this)).ws_get_stk_tda(inValue);
            return retVal.ws_get_stk_tdaResult;
        }
    }
}
