﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GearAlert.Web.Search {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Search.ISearchService")]
    public interface ISearchService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/Query", ReplyAction="http://tempuri.org/ISearchService/QueryResponse")]
        GearAlert.Infrastructure.Search.IndexItem[] Query([System.ServiceModel.MessageParameterAttribute(Name="query")] string query1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISearchServiceChannel : GearAlert.Web.Search.ISearchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SearchServiceClient : System.ServiceModel.ClientBase<GearAlert.Web.Search.ISearchService>, GearAlert.Web.Search.ISearchService {
        
        public SearchServiceClient() {
        }
        
        public SearchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SearchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GearAlert.Infrastructure.Search.IndexItem[] Query(string query1) {
            return base.Channel.Query(query1);
        }
    }
}
