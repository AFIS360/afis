﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AFIS360WebApp.GetPersonServiceRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonDetail", Namespace="http://localhost/AFIS360Webservice")]
    [System.SerializableAttribute()]
    public partial class PersonDetail : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PersonIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MiddleNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrefixField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SuffixField;
        
        private System.Nullable<System.DateTime> DateOfBirthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateOfBirthTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StreetAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostalCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProfessionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FatherNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CellNbrField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkPhoneNbrField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HomePhoneNbrField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PassportPhotoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string PersonId {
            get {
                return this.PersonIdField;
            }
            set {
                if ((object.ReferenceEquals(this.PersonIdField, value) != true)) {
                    this.PersonIdField = value;
                    this.RaisePropertyChanged("PersonId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string MiddleName {
            get {
                return this.MiddleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MiddleNameField, value) != true)) {
                    this.MiddleNameField = value;
                    this.RaisePropertyChanged("MiddleName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Prefix {
            get {
                return this.PrefixField;
            }
            set {
                if ((object.ReferenceEquals(this.PrefixField, value) != true)) {
                    this.PrefixField = value;
                    this.RaisePropertyChanged("Prefix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Suffix {
            get {
                return this.SuffixField;
            }
            set {
                if ((object.ReferenceEquals(this.SuffixField, value) != true)) {
                    this.SuffixField = value;
                    this.RaisePropertyChanged("Suffix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public System.Nullable<System.DateTime> DateOfBirth {
            get {
                return this.DateOfBirthField;
            }
            set {
                if ((this.DateOfBirthField.Equals(value) != true)) {
                    this.DateOfBirthField = value;
                    this.RaisePropertyChanged("DateOfBirth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string DateOfBirthText {
            get {
                return this.DateOfBirthTextField;
            }
            set {
                if ((object.ReferenceEquals(this.DateOfBirthTextField, value) != true)) {
                    this.DateOfBirthTextField = value;
                    this.RaisePropertyChanged("DateOfBirthText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string StreetAddress {
            get {
                return this.StreetAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.StreetAddressField, value) != true)) {
                    this.StreetAddressField = value;
                    this.RaisePropertyChanged("StreetAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string PostalCode {
            get {
                return this.PostalCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostalCodeField, value) != true)) {
                    this.PostalCodeField = value;
                    this.RaisePropertyChanged("PostalCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string Profession {
            get {
                return this.ProfessionField;
            }
            set {
                if ((object.ReferenceEquals(this.ProfessionField, value) != true)) {
                    this.ProfessionField = value;
                    this.RaisePropertyChanged("Profession");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string FatherName {
            get {
                return this.FatherNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FatherNameField, value) != true)) {
                    this.FatherNameField = value;
                    this.RaisePropertyChanged("FatherName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=15)]
        public string CellNbr {
            get {
                return this.CellNbrField;
            }
            set {
                if ((object.ReferenceEquals(this.CellNbrField, value) != true)) {
                    this.CellNbrField = value;
                    this.RaisePropertyChanged("CellNbr");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=16)]
        public string WorkPhoneNbr {
            get {
                return this.WorkPhoneNbrField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkPhoneNbrField, value) != true)) {
                    this.WorkPhoneNbrField = value;
                    this.RaisePropertyChanged("WorkPhoneNbr");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=17)]
        public string HomePhoneNbr {
            get {
                return this.HomePhoneNbrField;
            }
            set {
                if ((object.ReferenceEquals(this.HomePhoneNbrField, value) != true)) {
                    this.HomePhoneNbrField = value;
                    this.RaisePropertyChanged("HomePhoneNbr");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=18)]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=19)]
        public string PassportPhoto {
            get {
                return this.PassportPhotoField;
            }
            set {
                if ((object.ReferenceEquals(this.PassportPhotoField, value) != true)) {
                    this.PassportPhotoField = value;
                    this.RaisePropertyChanged("PassportPhoto");
                }
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://localhost/AFIS360Webservice", ConfigurationName="GetPersonServiceRef.GetPersonSoap")]
    public interface GetPersonSoap {
        
        // CODEGEN: Generating message contract since element name personId from namespace http://localhost/AFIS360Webservice is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost/AFIS360Webservice/getPerson", ReplyAction="*")]
        AFIS360WebApp.GetPersonServiceRef.getPersonResponse getPerson(AFIS360WebApp.GetPersonServiceRef.getPersonRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost/AFIS360Webservice/getPerson", ReplyAction="*")]
        System.Threading.Tasks.Task<AFIS360WebApp.GetPersonServiceRef.getPersonResponse> getPersonAsync(AFIS360WebApp.GetPersonServiceRef.getPersonRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getPersonRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getPerson", Namespace="http://localhost/AFIS360Webservice", Order=0)]
        public AFIS360WebApp.GetPersonServiceRef.getPersonRequestBody Body;
        
        public getPersonRequest() {
        }
        
        public getPersonRequest(AFIS360WebApp.GetPersonServiceRef.getPersonRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class getPersonRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string personId;
        
        public getPersonRequestBody() {
        }
        
        public getPersonRequestBody(string personId) {
            this.personId = personId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getPersonResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getPersonResponse", Namespace="http://localhost/AFIS360Webservice", Order=0)]
        public AFIS360WebApp.GetPersonServiceRef.getPersonResponseBody Body;
        
        public getPersonResponse() {
        }
        
        public getPersonResponse(AFIS360WebApp.GetPersonServiceRef.getPersonResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class getPersonResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public AFIS360WebApp.GetPersonServiceRef.PersonDetail getPersonResult;
        
        public getPersonResponseBody() {
        }
        
        public getPersonResponseBody(AFIS360WebApp.GetPersonServiceRef.PersonDetail getPersonResult) {
            this.getPersonResult = getPersonResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface GetPersonSoapChannel : AFIS360WebApp.GetPersonServiceRef.GetPersonSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetPersonSoapClient : System.ServiceModel.ClientBase<AFIS360WebApp.GetPersonServiceRef.GetPersonSoap>, AFIS360WebApp.GetPersonServiceRef.GetPersonSoap {
        
        public GetPersonSoapClient() {
        }
        
        public GetPersonSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GetPersonSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GetPersonSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GetPersonSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AFIS360WebApp.GetPersonServiceRef.getPersonResponse AFIS360WebApp.GetPersonServiceRef.GetPersonSoap.getPerson(AFIS360WebApp.GetPersonServiceRef.getPersonRequest request) {
            return base.Channel.getPerson(request);
        }
        
        public AFIS360WebApp.GetPersonServiceRef.PersonDetail getPerson(string personId) {
            AFIS360WebApp.GetPersonServiceRef.getPersonRequest inValue = new AFIS360WebApp.GetPersonServiceRef.getPersonRequest();
            inValue.Body = new AFIS360WebApp.GetPersonServiceRef.getPersonRequestBody();
            inValue.Body.personId = personId;
            AFIS360WebApp.GetPersonServiceRef.getPersonResponse retVal = ((AFIS360WebApp.GetPersonServiceRef.GetPersonSoap)(this)).getPerson(inValue);
            return retVal.Body.getPersonResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AFIS360WebApp.GetPersonServiceRef.getPersonResponse> AFIS360WebApp.GetPersonServiceRef.GetPersonSoap.getPersonAsync(AFIS360WebApp.GetPersonServiceRef.getPersonRequest request) {
            return base.Channel.getPersonAsync(request);
        }
        
        public System.Threading.Tasks.Task<AFIS360WebApp.GetPersonServiceRef.getPersonResponse> getPersonAsync(string personId) {
            AFIS360WebApp.GetPersonServiceRef.getPersonRequest inValue = new AFIS360WebApp.GetPersonServiceRef.getPersonRequest();
            inValue.Body = new AFIS360WebApp.GetPersonServiceRef.getPersonRequestBody();
            inValue.Body.personId = personId;
            return ((AFIS360WebApp.GetPersonServiceRef.GetPersonSoap)(this)).getPersonAsync(inValue);
        }
    }
}
