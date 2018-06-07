using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IntwkSMSC" in both code and config file together.
    [ServiceContract]
    public interface IntwkSMSC
    {
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "ServiceActivatePrivateKey")]
        //string ServiceActivatePrivateKey(string PrivateKey);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "ServiceActivatePrivateKey/?PrivateKey={PrivateKey}")]
        string ServiceActivatePrivateKey(string PrivateKey);
    }
}
