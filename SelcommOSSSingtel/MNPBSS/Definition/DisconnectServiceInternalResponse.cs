using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [DataContract]
    public class DisconnectServiceInternalResponse:Response
    {
        /// <summary>
        /// Subscriber result allowing multiple occurance
        /// </summary>
        [DataMember]
        public List<SDResult> SubscriberResult { get; set; }

    }
    [DataContract]
    public class SDResult
    {
        /// <summary>
        /// reference id for the port
        /// </summary>
        [DataMember]
        public string ReferenceId { get; set; }
        /// <summary>
        /// the subscriber number in the "Send SDRsp" business process
        /// </summary>
        [DataMember]
        public string SubscriberNumber { get; set; }
        /// <summary>
        /// 0 – success and 1- Fail to locate 
        /// </summary>
        [DataMember]
        public int ResultCode { get; set; }
        [DataMember]
        public bool ResultCodeSpecified { get; set; }
        /// <summary>
        /// If failed to locate of service is not in the correct state, will add text here
        /// </summary>
        [DataMember]
        public string ResultText { get; set; }
    
    }

}