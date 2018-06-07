using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class PortOutDetailsSN
    {
        [MessageBodyMember]
        public PortingRequestID requestID { get; set; }
        [MessageBodyMember]
        public ProductServiceID serviceId { get; set; }
        /// <summary>
        /// Tele operator identifier identifying the losing carrier (DNO).
        /// </summary>
        [MessageBodyMember]
        public PortingDonor Donor { get; set; }
        /// <summary>
        /// Tele operator identifier identifying the gaining carrier (RNO) as it’s defined in SingTel (CNPMS)
        /// </summary>
        [MessageBodyMember]
        public string Recipient { get; set; }
        /// <summary>
        /// Old Reference ID for the porting for retry case
        /// </summary>
        [MessageBodyMember]
        public string prevPortingReferenceID { get; set; }
        /// <summary>
        /// The Ordered Transfer Time of the Port Order. Specify the start time of the time window.
        /// </summary>
        [MessageBodyMember]
        public DateTime portingTransferDateTime { get; set; }
        /// <summary>
        /// Present on ‘NPO’ to DNO and ‘Echo NPO’ to RNO. Specify the end-time of the DNO approval time window (Customer request date)
        /// </summary>
        [MessageBodyMember]
        public DateTime portingApprovalDateTime { get; set; }
        /// <summary>
        /// Subscription Type of ‘Prepaid’ or ‘Postpaid’ (at the RNO side)
        /// </summary>
        [MessageBodyMember]
        public string paymentCategory { get; set; }
        /// <summary>
        /// Customer information. Either this element or Organization element must exist. For prepaid and migration, dummy data can be provided if not available   
        /// </summary>
        [MessageBodyMember]
        public IndividualPorting individual { get; set; }
        /// <summary>
        /// The Corporate Customer information. Either this element or Individual element should exist.For prepaid and migration, dummy data can be provided if not available. /// </summary>
        [MessageBodyMember]
        public OrganizationPorting organization { get; set; }

    }
}