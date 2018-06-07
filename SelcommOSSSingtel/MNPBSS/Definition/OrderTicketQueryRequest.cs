using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class OrderTicketQueryRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public OrderTicketQueryResult orderTicketQueryResult { get; set; }
    }

    [MessageContract]
    public class OrderTicketQueryResult
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
        public TQResultCode resultCode { get; set; }

        [MessageBodyMember]
        public QuotaResult quotaResult { get; set; }
    }


    [MessageContract]
    public class TQResultCode
    {
        /// <summary>
        /// the transfer request identifier
        /// </summary>
        [MessageBodyMember]
        public Int32 Code { get; set; }
    }

    [MessageContract]
    public class QuotaResult
    {
        [MessageBodyMember]
        public DateTime quotaDate { get; set; }

        [MessageBodyMember]
        public PortingDonor donor { get; set; }

        [MessageBodyMember]
        public Int32 quotaAmount1 { get; set; }

        [MessageBodyMember]
        public Int32 quotaAmount2 { get; set; }

        [MessageBodyMember]
        public Int32 quotaAmount3 { get; set; }

        [MessageBodyMember]
        public Int32 activationQuota { get; set; }

        [MessageBodyMember]
        public Int32 maxSNPerPortInReq { get; set; }
    }




}