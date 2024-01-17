﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientWpf.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RSA_Connect", ReplyAction="http://tempuri.org/IService1/RSA_ConnectResponse")]
        string RSA_Connect(string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RSA_Connect", ReplyAction="http://tempuri.org/IService1/RSA_ConnectResponse")]
        System.Threading.Tasks.Task<string> RSA_ConnectAsync(string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Verification", ReplyAction="http://tempuri.org/IService1/VerificationResponse")]
        string Verification(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Verification", ReplyAction="http://tempuri.org/IService1/VerificationResponse")]
        System.Threading.Tasks.Task<string> VerificationAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Register", ReplyAction="http://tempuri.org/IService1/RegisterResponse")]
        bool Register(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Register", ReplyAction="http://tempuri.org/IService1/RegisterResponse")]
        System.Threading.Tasks.Task<bool> RegisterAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Authorization", ReplyAction="http://tempuri.org/IService1/AuthorizationResponse")]
        int Authorization(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Authorization", ReplyAction="http://tempuri.org/IService1/AuthorizationResponse")]
        System.Threading.Tasks.Task<int> AuthorizationAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Count_Simplex", ReplyAction="http://tempuri.org/IService1/Count_SimplexResponse")]
        ClientWpf.ServiceReference1.Count_SimplexResponse Count_Simplex(ClientWpf.ServiceReference1.Count_SimplexRequest request);
        
        // CODEGEN: Идет формирование контракта на сообщение, так как операция может иметь много возвращаемых значений.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Count_Simplex", ReplyAction="http://tempuri.org/IService1/Count_SimplexResponse")]
        System.Threading.Tasks.Task<ClientWpf.ServiceReference1.Count_SimplexResponse> Count_SimplexAsync(ClientWpf.ServiceReference1.Count_SimplexRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Count_Generic", ReplyAction="http://tempuri.org/IService1/Count_GenericResponse")]
        ClientWpf.ServiceReference1.Count_GenericResponse Count_Generic(ClientWpf.ServiceReference1.Count_GenericRequest request);
        
        // CODEGEN: Идет формирование контракта на сообщение, так как операция может иметь много возвращаемых значений.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Count_Generic", ReplyAction="http://tempuri.org/IService1/Count_GenericResponse")]
        System.Threading.Tasks.Task<ClientWpf.ServiceReference1.Count_GenericResponse> Count_GenericAsync(ClientWpf.ServiceReference1.Count_GenericRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Count_Simplex", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Count_SimplexRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string[] str;
        
        public Count_SimplexRequest() {
        }
        
        public Count_SimplexRequest(string[] str) {
            this.str = str;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Count_SimplexResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Count_SimplexResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Count_SimplexResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string[] Proc;
        
        public Count_SimplexResponse() {
        }
        
        public Count_SimplexResponse(string Count_SimplexResult, string[] Proc) {
            this.Count_SimplexResult = Count_SimplexResult;
            this.Proc = Proc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Count_Generic", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Count_GenericRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string[] parameters;
        
        public Count_GenericRequest() {
        }
        
        public Count_GenericRequest(string[] parameters) {
            this.parameters = parameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Count_GenericResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Count_GenericResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Count_GenericResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string[] Proc;
        
        public Count_GenericResponse() {
        }
        
        public Count_GenericResponse(string Count_GenericResult, string[] Proc) {
            this.Count_GenericResult = Count_GenericResult;
            this.Proc = Proc;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : ClientWpf.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<ClientWpf.ServiceReference1.IService1>, ClientWpf.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string RSA_Connect(string param) {
            return base.Channel.RSA_Connect(param);
        }
        
        public System.Threading.Tasks.Task<string> RSA_ConnectAsync(string param) {
            return base.Channel.RSA_ConnectAsync(param);
        }
        
        public string Verification(string email) {
            return base.Channel.Verification(email);
        }
        
        public System.Threading.Tasks.Task<string> VerificationAsync(string email) {
            return base.Channel.VerificationAsync(email);
        }
        
        public bool Register(string login, string password) {
            return base.Channel.Register(login, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterAsync(string login, string password) {
            return base.Channel.RegisterAsync(login, password);
        }
        
        public int Authorization(string login, string password) {
            return base.Channel.Authorization(login, password);
        }
        
        public System.Threading.Tasks.Task<int> AuthorizationAsync(string login, string password) {
            return base.Channel.AuthorizationAsync(login, password);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ClientWpf.ServiceReference1.Count_SimplexResponse ClientWpf.ServiceReference1.IService1.Count_Simplex(ClientWpf.ServiceReference1.Count_SimplexRequest request) {
            return base.Channel.Count_Simplex(request);
        }
        
        public string Count_Simplex(string[] str, out string[] Proc) {
            ClientWpf.ServiceReference1.Count_SimplexRequest inValue = new ClientWpf.ServiceReference1.Count_SimplexRequest();
            inValue.str = str;
            ClientWpf.ServiceReference1.Count_SimplexResponse retVal = ((ClientWpf.ServiceReference1.IService1)(this)).Count_Simplex(inValue);
            Proc = retVal.Proc;
            return retVal.Count_SimplexResult;
        }
        
        public System.Threading.Tasks.Task<ClientWpf.ServiceReference1.Count_SimplexResponse> Count_SimplexAsync(ClientWpf.ServiceReference1.Count_SimplexRequest request) {
            return base.Channel.Count_SimplexAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ClientWpf.ServiceReference1.Count_GenericResponse ClientWpf.ServiceReference1.IService1.Count_Generic(ClientWpf.ServiceReference1.Count_GenericRequest request) {
            return base.Channel.Count_Generic(request);
        }
        
        public string Count_Generic(string[] parameters, out string[] Proc) {
            ClientWpf.ServiceReference1.Count_GenericRequest inValue = new ClientWpf.ServiceReference1.Count_GenericRequest();
            inValue.parameters = parameters;
            ClientWpf.ServiceReference1.Count_GenericResponse retVal = ((ClientWpf.ServiceReference1.IService1)(this)).Count_Generic(inValue);
            Proc = retVal.Proc;
            return retVal.Count_GenericResult;
        }
        
        public System.Threading.Tasks.Task<ClientWpf.ServiceReference1.Count_GenericResponse> Count_GenericAsync(ClientWpf.ServiceReference1.Count_GenericRequest request) {
            return base.Channel.Count_GenericAsync(request);
        }
    }
}
