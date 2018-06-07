using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class CancelPortInRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public CancelPortIn cancelPortIn { get; set; }
    }

    [MessageContract]
    public class CancelPortIn
    {
        /// <summary>
        /// Transfer request creator’s identifier that the creator ensures to be unambiguous.
        /// </summary>
        [MessageBodyMember]
        public PortingRequestID requestId { get; set; }
        [MessageBodyMember]
        public PortingReferenceID referenceID { get; set; }
        [MessageBodyMember]
        public Int32 cancelReasonCode { get; set; }
        /// <summary>
        /// Service number (for example: mobile number)
        /// </summary>
        [MessageBodyMember]
        public string cancelReasonText { get; set; }
    }

}