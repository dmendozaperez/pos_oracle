using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInspector
{
    public class MessageInspector : IClientMessageInspector
    {
        string _header;
        string _value;

        public MessageInspector(string header, string value)
        {
            _header = header;
            _value = value;
        }


        void IClientMessageInspector.AfterReceiveReply(ref System.ServiceModel.Channels.Message reply,
            Object correlationState)
        {
        }

        object IClientMessageInspector.BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;

                if (string.IsNullOrEmpty(httpRequestMessage.Headers[_header]))
                {
                    httpRequestMessage.Headers[_header] = _value;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                httpRequestMessage.Headers.Add(_header, _value);
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }
            return null;
           
        }
    }
}
