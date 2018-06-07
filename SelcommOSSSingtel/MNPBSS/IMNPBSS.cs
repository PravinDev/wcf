using SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMNPBSS" in both code and config file together.
    [ServiceContract]
    public interface IMNPBSS
    {
        //[OperationContract]
        ////[FaultContract(typeof(Singtel.MNPBSS2.Definition.FaultResponse))]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "connectService")]
        //ConnectServiceResponse connectService(ConnectServiceRequest Request);

        [OperationContract]
        ConnectServiceResponse connectService(ConnectServiceRequest testRequest);
        ///// <summary>Not Implemented</summary>
        ///// Customer requeting to cancel a previously requested port out
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "cancelPortOut")]
        //CancelPortOutResponse cancelPortOut(CancelPortOutRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// Customer requeting to cancel a previously requested port out
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "cancelPortIn")]
        //CancelPortInResponse cancelPortIn(CancelPortInRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// Contains the status of portOut request sent by MVNE to Singtel.
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "confirmPortOrder")]
        //ConfirmPortOutOrderResponse confirmPortOutOrder(ConfirmPortOutOrderRequest Request);

        ///// <summary>Not Implemented</summary>
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "disconnectServiceInternal")]
        //DisconnectServiceInternalResponse disconnectServiceInternal(DisconnectServiceInternal Request);

        ///// <summary>Not Implemented</summary>
        ///// Contains the status of portIn request sent by MVNE to Singtel.
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "portInNotification")]
        //PortInNotificationResponse portInNotification(PortInNotificationRequest Request);

        /// <summary>Not Implemented</summary>
        /// Contains the status of portIn request sent by MVNE to Singtel.
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "portInNotification")]
        PortInNotificationResponse portInNotification(PortInNotificationRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// Contains the status of portOut request sent by MVNE to Singtel.
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "portOutNotification")]
        //PortOutNotificationResponse portOutNotification(PortOutNotificationRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// This operation is exposed for CNPMS to validate a port out order request in BCC system before porting out the number from BCC system.
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "validatePortOut")]
        //ValidatePortOutResponse validatePortOut(ValidatePortOutRequest Request);

        ///// <summary>Not Implemented</summary>
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "validatePortOutInternal")]
        //ValidatePortOutInternalResponse validatePortOutInternal(ValidatePortOutInternalRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// Return customer and service information based on the given Porting Reference  ID
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getxVNECustomerByPortingReferenceID")]
        //CustomerByPortingReferenceIdResponse getxVNECustomerByPortingReferenceID(CustomerByPortingReferenceIdRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// Return customer and service information based on the given ID information 
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getxVNECustomer")]
        //CustomerResponse getxVNECustomer(CustomerRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// Return customer and service information based on the given Porting Request ID
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getxVNECustomerByPortingRequestID")]
        //CustomerByPortingIdResponse getxVNECustomerByPortingRequestID(CustomerByPortingIdRequest Request);

        ///// <summary>Not Implemented</summary>
        ///// This operation is exposed for CNPMS to validate a service number withing MVNE platform.  SDP-CE will call this method after the quarantine period
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "queryServiceId")]
        //ServiceIdResponse queryServiceId(string RequestId, DateTime RequestDateTime, string ServiceNumber);

        ///// <summary>Not Implemented</summary>
        ///// This operation is exposed for CNPMS to send the number back to MVNE.  SDP-CE will call this method after the quarantine period
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "storeServiceId")]
        //ServiceIdResponse storeServiceId(string RequestId, DateTime RequestDateTime, string ServiceNumber);

        ///// <summary>Not Implemented</summary>
        ///// Customer requeting to cancel a previously requested port out
        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "orderTicketQueryResult")]
        //OrderTicketQueryResponse orderTicketQueryResult(OrderTicketQueryRequest Request);

    }
}
