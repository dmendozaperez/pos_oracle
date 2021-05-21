using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInspector
{
    public class MessageBehavior : IEndpointBehavior
    {
        string _header;
        string _value;

        public MessageBehavior(string header, string value)
        {
            _header = header;
            _value = value;
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        { }

        void IEndpointBehavior.ApplyClientBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new MessageInspector(_header, _value));
        }
        void IEndpointBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        { }
        void IEndpointBehavior.Validate(System.ServiceModel.Description.ServiceEndpoint endpoint)
        { }

    }
}
