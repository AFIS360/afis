﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AFIS360WebApp.PersonCriminalRecordServiceRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CriminalRecord", Namespace="http://localhost/AFIS360Webservice")]
    [System.SerializableAttribute()]
    public partial class CriminalRecord : AFIS360WebApp.PersonCriminalRecordServiceRef.DataObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PersonIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CrimeDetailField;
        
        private System.DateTime CrimeDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CrimeLocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CourtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatuteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CourtAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CaseIdField;
        
        private System.DateTime SentencedDateField;
        
        private System.DateTime ReleaseDateField;
        
        private System.DateTime ArrestDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArrestAgencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        private System.DateTime ParoleDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CriminalAlertLevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CriminalAlertMsgField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RefDocLocationField;
        
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
        public string CrimeDetail {
            get {
                return this.CrimeDetailField;
            }
            set {
                if ((object.ReferenceEquals(this.CrimeDetailField, value) != true)) {
                    this.CrimeDetailField = value;
                    this.RaisePropertyChanged("CrimeDetail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public System.DateTime CrimeDate {
            get {
                return this.CrimeDateField;
            }
            set {
                if ((this.CrimeDateField.Equals(value) != true)) {
                    this.CrimeDateField = value;
                    this.RaisePropertyChanged("CrimeDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string CrimeLocation {
            get {
                return this.CrimeLocationField;
            }
            set {
                if ((object.ReferenceEquals(this.CrimeLocationField, value) != true)) {
                    this.CrimeLocationField = value;
                    this.RaisePropertyChanged("CrimeLocation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Court {
            get {
                return this.CourtField;
            }
            set {
                if ((object.ReferenceEquals(this.CourtField, value) != true)) {
                    this.CourtField = value;
                    this.RaisePropertyChanged("Court");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Statute {
            get {
                return this.StatuteField;
            }
            set {
                if ((object.ReferenceEquals(this.StatuteField, value) != true)) {
                    this.StatuteField = value;
                    this.RaisePropertyChanged("Statute");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string CourtAddress {
            get {
                return this.CourtAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.CourtAddressField, value) != true)) {
                    this.CourtAddressField = value;
                    this.RaisePropertyChanged("CourtAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string CaseId {
            get {
                return this.CaseIdField;
            }
            set {
                if ((object.ReferenceEquals(this.CaseIdField, value) != true)) {
                    this.CaseIdField = value;
                    this.RaisePropertyChanged("CaseId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=8)]
        public System.DateTime SentencedDate {
            get {
                return this.SentencedDateField;
            }
            set {
                if ((this.SentencedDateField.Equals(value) != true)) {
                    this.SentencedDateField = value;
                    this.RaisePropertyChanged("SentencedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=9)]
        public System.DateTime ReleaseDate {
            get {
                return this.ReleaseDateField;
            }
            set {
                if ((this.ReleaseDateField.Equals(value) != true)) {
                    this.ReleaseDateField = value;
                    this.RaisePropertyChanged("ReleaseDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=10)]
        public System.DateTime ArrestDate {
            get {
                return this.ArrestDateField;
            }
            set {
                if ((this.ArrestDateField.Equals(value) != true)) {
                    this.ArrestDateField = value;
                    this.RaisePropertyChanged("ArrestDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string ArrestAgency {
            get {
                return this.ArrestAgencyField;
            }
            set {
                if ((object.ReferenceEquals(this.ArrestAgencyField, value) != true)) {
                    this.ArrestAgencyField = value;
                    this.RaisePropertyChanged("ArrestAgency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=13)]
        public System.DateTime ParoleDate {
            get {
                return this.ParoleDateField;
            }
            set {
                if ((this.ParoleDateField.Equals(value) != true)) {
                    this.ParoleDateField = value;
                    this.RaisePropertyChanged("ParoleDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string CriminalAlertLevel {
            get {
                return this.CriminalAlertLevelField;
            }
            set {
                if ((object.ReferenceEquals(this.CriminalAlertLevelField, value) != true)) {
                    this.CriminalAlertLevelField = value;
                    this.RaisePropertyChanged("CriminalAlertLevel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=15)]
        public string CriminalAlertMsg {
            get {
                return this.CriminalAlertMsgField;
            }
            set {
                if ((object.ReferenceEquals(this.CriminalAlertMsgField, value) != true)) {
                    this.CriminalAlertMsgField = value;
                    this.RaisePropertyChanged("CriminalAlertMsg");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=16)]
        public string RefDocLocation {
            get {
                return this.RefDocLocationField;
            }
            set {
                if ((object.ReferenceEquals(this.RefDocLocationField, value) != true)) {
                    this.RefDocLocationField = value;
                    this.RaisePropertyChanged("RefDocLocation");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataObject", Namespace="http://localhost/AFIS360Webservice")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AFIS360WebApp.PersonCriminalRecordServiceRef.CriminalRecord))]
    public partial class DataObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatedByField;
        
        private System.Nullable<System.DateTime> CreationDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UpdatedByField;
        
        private System.Nullable<System.DateTime> UpdateDateTimeField;
        
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
        public string CreatedBy {
            get {
                return this.CreatedByField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedByField, value) != true)) {
                    this.CreatedByField = value;
                    this.RaisePropertyChanged("CreatedBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Nullable<System.DateTime> CreationDateTime {
            get {
                return this.CreationDateTimeField;
            }
            set {
                if ((this.CreationDateTimeField.Equals(value) != true)) {
                    this.CreationDateTimeField = value;
                    this.RaisePropertyChanged("CreationDateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string UpdatedBy {
            get {
                return this.UpdatedByField;
            }
            set {
                if ((object.ReferenceEquals(this.UpdatedByField, value) != true)) {
                    this.UpdatedByField = value;
                    this.RaisePropertyChanged("UpdatedBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public System.Nullable<System.DateTime> UpdateDateTime {
            get {
                return this.UpdateDateTimeField;
            }
            set {
                if ((this.UpdateDateTimeField.Equals(value) != true)) {
                    this.UpdateDateTimeField = value;
                    this.RaisePropertyChanged("UpdateDateTime");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://localhost/AFIS360Webservice", ConfigurationName="PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap")]
    public interface PersonCriminalRecordServiceSoap {
        
        // CODEGEN: Generating message contract since element name personId from namespace http://localhost/AFIS360Webservice is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost/AFIS360Webservice/GetCriminalRecords", ReplyAction="*")]
        AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponse GetCriminalRecords(AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost/AFIS360Webservice/GetCriminalRecords", ReplyAction="*")]
        System.Threading.Tasks.Task<AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponse> GetCriminalRecordsAsync(AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCriminalRecordsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCriminalRecords", Namespace="http://localhost/AFIS360Webservice", Order=0)]
        public AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequestBody Body;
        
        public GetCriminalRecordsRequest() {
        }
        
        public GetCriminalRecordsRequest(AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class GetCriminalRecordsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string personId;
        
        public GetCriminalRecordsRequestBody() {
        }
        
        public GetCriminalRecordsRequestBody(string personId) {
            this.personId = personId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCriminalRecordsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCriminalRecordsResponse", Namespace="http://localhost/AFIS360Webservice", Order=0)]
        public AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponseBody Body;
        
        public GetCriminalRecordsResponse() {
        }
        
        public GetCriminalRecordsResponse(AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class GetCriminalRecordsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public AFIS360WebApp.PersonCriminalRecordServiceRef.CriminalRecord[] GetCriminalRecordsResult;
        
        public GetCriminalRecordsResponseBody() {
        }
        
        public GetCriminalRecordsResponseBody(AFIS360WebApp.PersonCriminalRecordServiceRef.CriminalRecord[] GetCriminalRecordsResult) {
            this.GetCriminalRecordsResult = GetCriminalRecordsResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PersonCriminalRecordServiceSoapChannel : AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PersonCriminalRecordServiceSoapClient : System.ServiceModel.ClientBase<AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap>, AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap {
        
        public PersonCriminalRecordServiceSoapClient() {
        }
        
        public PersonCriminalRecordServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PersonCriminalRecordServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonCriminalRecordServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonCriminalRecordServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponse AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap.GetCriminalRecords(AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest request) {
            return base.Channel.GetCriminalRecords(request);
        }
        
        public AFIS360WebApp.PersonCriminalRecordServiceRef.CriminalRecord[] GetCriminalRecords(string personId) {
            AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest inValue = new AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest();
            inValue.Body = new AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequestBody();
            inValue.Body.personId = personId;
            AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponse retVal = ((AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap)(this)).GetCriminalRecords(inValue);
            return retVal.Body.GetCriminalRecordsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponse> AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap.GetCriminalRecordsAsync(AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest request) {
            return base.Channel.GetCriminalRecordsAsync(request);
        }
        
        public System.Threading.Tasks.Task<AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsResponse> GetCriminalRecordsAsync(string personId) {
            AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest inValue = new AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequest();
            inValue.Body = new AFIS360WebApp.PersonCriminalRecordServiceRef.GetCriminalRecordsRequestBody();
            inValue.Body.personId = personId;
            return ((AFIS360WebApp.PersonCriminalRecordServiceRef.PersonCriminalRecordServiceSoap)(this)).GetCriminalRecordsAsync(inValue);
        }
    }
}
