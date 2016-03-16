﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AFIS360WebApp.GetMatchServiceRef {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://localhost/AFIS360Webservice", ConfigurationName="GetMatchServiceRef.MatchFingerprintSoap")]
    public interface MatchFingerprintSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost/AFIS360Webservice/GetMatch", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Person))]
        AFIS360WebApp.GetMatchServiceRef.Match GetMatch(string fingerName, string fingerprintBase64Str, string visitorId, int threshold);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost/AFIS360Webservice/GetMatch", ReplyAction="*")]
        System.Threading.Tasks.Task<AFIS360WebApp.GetMatchServiceRef.Match> GetMatchAsync(string fingerName, string fingerprintBase64Str, string visitorId, int threshold);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class Match : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MyPerson matchedPersonField;
        
        private MyPerson probeField;
        
        private float scoreField;
        
        private bool statusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MyPerson MatchedPerson {
            get {
                return this.matchedPersonField;
            }
            set {
                this.matchedPersonField = value;
                this.RaisePropertyChanged("MatchedPerson");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public MyPerson Probe {
            get {
                return this.probeField;
            }
            set {
                this.probeField = value;
                this.RaisePropertyChanged("Probe");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public float Score {
            get {
                return this.scoreField;
            }
            set {
                this.scoreField = value;
                this.RaisePropertyChanged("Score");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public bool Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("Status");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class MyPerson : Person {
        
        private string nameField;
        
        private string personIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string PersonId {
            get {
                return this.personIdField;
            }
            set {
                this.personIdField = value;
                this.RaisePropertyChanged("PersonId");
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MyPerson))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class Person : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Fingerprint[] fingerprintsField;
        
        private int idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        public Fingerprint[] Fingerprints {
            get {
                return this.fingerprintsField;
            }
            set {
                this.fingerprintsField = value;
                this.RaisePropertyChanged("Fingerprints");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("Id");
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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MyFingerprint))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class Fingerprint : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] asImageDataField;
        
        private byte[] templateField;
        
        private Finger fingerField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=0)]
        public byte[] AsImageData {
            get {
                return this.asImageDataField;
            }
            set {
                this.asImageDataField = value;
                this.RaisePropertyChanged("AsImageData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=1)]
        public byte[] Template {
            get {
                return this.templateField;
            }
            set {
                this.templateField = value;
                this.RaisePropertyChanged("Template");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public Finger Finger {
            get {
                return this.fingerField;
            }
            set {
                this.fingerField = value;
                this.RaisePropertyChanged("Finger");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public enum Finger {
        
        /// <remarks/>
        Any,
        
        /// <remarks/>
        RightThumb,
        
        /// <remarks/>
        LeftThumb,
        
        /// <remarks/>
        RightIndex,
        
        /// <remarks/>
        LeftIndex,
        
        /// <remarks/>
        RightMiddle,
        
        /// <remarks/>
        LeftMiddle,
        
        /// <remarks/>
        RightRing,
        
        /// <remarks/>
        LeftRing,
        
        /// <remarks/>
        RightLittle,
        
        /// <remarks/>
        LeftLittle,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/AFIS360Webservice")]
    public partial class MyFingerprint : Fingerprint {
        
        private string filenameField;
        
        private string fingernameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Filename {
            get {
                return this.filenameField;
            }
            set {
                this.filenameField = value;
                this.RaisePropertyChanged("Filename");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Fingername {
            get {
                return this.fingernameField;
            }
            set {
                this.fingernameField = value;
                this.RaisePropertyChanged("Fingername");
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MatchFingerprintSoapChannel : AFIS360WebApp.GetMatchServiceRef.MatchFingerprintSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MatchFingerprintSoapClient : System.ServiceModel.ClientBase<AFIS360WebApp.GetMatchServiceRef.MatchFingerprintSoap>, AFIS360WebApp.GetMatchServiceRef.MatchFingerprintSoap {
        
        public MatchFingerprintSoapClient() {
        }
        
        public MatchFingerprintSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MatchFingerprintSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MatchFingerprintSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MatchFingerprintSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AFIS360WebApp.GetMatchServiceRef.Match GetMatch(string fingerName, string fingerprintBase64Str, string visitorId, int threshold) {
            return base.Channel.GetMatch(fingerName, fingerprintBase64Str, visitorId, threshold);
        }
        
        public System.Threading.Tasks.Task<AFIS360WebApp.GetMatchServiceRef.Match> GetMatchAsync(string fingerName, string fingerprintBase64Str, string visitorId, int threshold) {
            return base.Channel.GetMatchAsync(fingerName, fingerprintBase64Str, visitorId, threshold);
        }
    }
}