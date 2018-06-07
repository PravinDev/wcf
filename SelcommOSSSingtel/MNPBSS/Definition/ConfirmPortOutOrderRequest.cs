using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class ConfirmPortOutOrderRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public ConfirmPortOutOrder confirmPortOutOrder { get; set; }
    }

    [MessageContract]
    public class ConfirmPortOutOrder
    {
        /// <summary>
        /// Transfer request creator’s identifier that the creator ensures to be unambiguous.
        /// </summary>
        [MessageBodyMember]
        public PortingRequestID requestId { get; set; }
        [MessageBodyMember]
        public PortingReferenceID referenceID { get; set; }        
    }

}