using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class CancelPortOutRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public CancelPortOut cancelPortOut { get; set; }
    }

    [MessageContract]
    public class CancelPortOut
    {
        /// <summary>
        /// Transfer request creator’s identifier that the creator ensures to be unambiguous.
        /// </summary>
        [MessageBodyMember]
        public PortingRequestID requestId { get; set; }
        [MessageBodyMember]
        public PortingReferenceID referenceID { get; set; }
        /// <summary>
        /// Array of ServiceInfo elements
        /// </summary>
        [MessageBodyMember]
        public CancellationReason cancellationReason { get; set; }
    }


    [MessageContract]
    public class CancellationReason
    {
        /// <summary>
        /// Unique ID for the portability request
        /// </summary>
        [MessageBodyMember]
        public Int32 reasonCode { get; set; }
        /// <summary>
        /// Service number (for example: mobile number)
        /// </summary>
        [MessageBodyMember]
        public string reasonText { get; set; }
    }


}