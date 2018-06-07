using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class PortOutNotificationRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public PortingRequestID requestID { get; set; }
        [MessageBodyMember]
        public PortingRequestID referenceRequestId { get; set; }
        /// <summary>
        /// Tele operator identifier identifying the losing carrier (DNO).
        /// </summary>
        [MessageBodyMember]
        public PortingReferenceID referenceId { get; set; }

        /// <summary>
        /// The Corporate Customer information. Either this element or Individual element should exist.For prepaid and migration, dummy data can be provided if not available. /// </summary>
        [MessageBodyMember]
        public PortOutError portOutError { get; set; }

    }
    [MessageContract]
    public class PortOutError
    {
        [MessageBodyMember]
        public string messageType { get; set; }

        [MessageBodyMember]
        public Int32 errorCode { get; set; }

        [MessageBodyMember]
        public string errorText { get; set; }
    }

        /// <summary>
        /// Result values with enumeration fields for CNPMS
        /// </summary>
    //    [DataContract]
    //    public enum PortOutNotifyType
    //{
    //        /// <summary>
    //        /// FAILURE
    //        /// </summary>
    //        [EnumMember]
    //        NPOR,
    //        /// <summary>
    //        /// SUCCESS
    //        /// </summary>
    //        [EnumMember]
    //        NPOC,
    //        /// <summary>
    //        /// FAILURE
    //        /// The process has not been performed in sufficient time. There should be no action by the BSS
    //        /// </summary>
    //        [EnumMember]
    //        WINTO,
    //        /// <summary>
    //        /// FAILURE
    //        /// The service Disconnection has been unsuccessful and the BSS should wait till the transfer window to process the request
    //        /// </summary>
    //        [EnumMember]
    //        SD,
    //        /// <summary>
    //        /// FAILURE
    //        /// If the ReferenceRequestId is populated then this is a rejection of the previous SDRsp request and the BSS should raise an error and cancel the porting.
    //        /// </summary>        
    //        [EnumMember]
    //        SDRSP            
    //    }
    
}