﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeavyClient.Routing {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Routing.IRoutingService")]
    public interface IRoutingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoutingService/GetStations", ReplyAction="http://tempuri.org/IRoutingService/GetStationsResponse")]
        string GetStations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoutingService/GetStations", ReplyAction="http://tempuri.org/IRoutingService/GetStationsResponse")]
        System.Threading.Tasks.Task<string> GetStationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoutingService/GetPath", ReplyAction="http://tempuri.org/IRoutingService/GetPathResponse")]
        string GetPath(string location1, string location2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoutingService/GetPath", ReplyAction="http://tempuri.org/IRoutingService/GetPathResponse")]
        System.Threading.Tasks.Task<string> GetPathAsync(string location1, string location2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoutingServiceChannel : HeavyClient.Routing.IRoutingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoutingServiceClient : System.ServiceModel.ClientBase<HeavyClient.Routing.IRoutingService>, HeavyClient.Routing.IRoutingService {
        
        public RoutingServiceClient() {
        }
        
        public RoutingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RoutingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoutingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoutingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetStations() {
            return base.Channel.GetStations();
        }
        
        public System.Threading.Tasks.Task<string> GetStationsAsync() {
            return base.Channel.GetStationsAsync();
        }
        
        public string GetPath(string location1, string location2) {
            return base.Channel.GetPath(location1, location2);
        }
        
        public System.Threading.Tasks.Task<string> GetPathAsync(string location1, string location2) {
            return base.Channel.GetPathAsync(location1, location2);
        }
    }
}