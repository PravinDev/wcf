using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class PortInNotificationRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public PortingRequestID requestId { get; set; }
        /// <summary>
        /// The request message
        /// </summary>
        [MessageBodyMember]
        public PortInNotification PortInNotification { get; set; }
    }
    [MessageContract]
    public class PortInNotification
    {
        /// <summary>
        /// Unique ID for the portability request
        /// </summary>
        [MessageBodyMember]
        public PortingReferenceID referenceID { get; set; }
        /// <summary>
        /// transfer request identifier
        /// </summary>
        [MessageBodyMember]
        public PortingRequestID requestID { get; set; }
        /// <summary>
        /// Phone number of the mobile service
        /// </summary>
        [MessageBodyMember]
        public ProductServiceID serviceID { get; set; }
        [MessageBodyMember]
        public PortingResult portingResult { get; set; }
        [MessageBodyMember]
        public PortingDonor donor { get; set; }
        /// <summary>
        /// This is ordered Approval Time by CNPMS, Present for NPO to DNO and Echo
        /// </summary>
        [MessageBodyMember]
        public DateType orderedApprovalTime { get; set; }
        /// <summary>
        /// This field will be populated when CNPMS rejects the NPO for = Insufficient quota
        /// </summary>
        [MessageBodyMember]
        public DateType recommendTransferTime { get; set; }
    }

    [MessageContract]
    public class PortingResult
    {
        /// <summary>
        /// Valid values of different responses
        /// </summary>
        [MessageBodyMember]
        public string responseCode { get; set; }
        /// <summary>
        /// Rejection code send if Result = PNINV/ PNREJ/ PNFAI
        /// </summary>
        [MessageBodyMember]
        public string rejectionCode { get; set; }
        /// <summary>
        /// Message text of porting request confirmation or reject
        /// </summary>
        [MessageBodyMember]
        public string reasonText { get; set; }
    }
    [MessageContract]
    public class PortingDonor
    {
        /// <summary>
        /// Tele operator identifier identifying the donor carrier (DNO)
        /// </summary>
        [MessageBodyMember]
        public string id { get; set; }
    }
}