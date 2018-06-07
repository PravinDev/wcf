using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class ConnectServiceRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public ConnectService connectService { get; set; }
    }

    [MessageContract]
    public class ConnectService
    {
        /// <summary>
        /// Transfer request creator’s identifier that the creator ensures to be unambiguous.
        /// </summary>
        [MessageBodyMember]
        public PortingRequestID requestId { get; set; }
        /// <summary>
        /// Array of ServiceInfo elements
        /// </summary>
        [MessageBodyMember]
        public ServiceInfo ServiceDetailElement { get; set; }
    }


    [MessageContract]
    public class ServiceInfo
    {
        /// <summary>
        /// Unique ID for the portability request
        /// </summary>
        [MessageBodyMember]
        public PortingReferenceID referenceId { get; set; }
        /// <summary>
        /// Service number (for example: mobile number)
        /// </summary>
        [MessageBodyMember]
        public ProductServiceID serviceID { get; set; }
    }


}