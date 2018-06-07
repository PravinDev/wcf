using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class NPOQResult
    {
        /// <summary>
        /// 0-success 
        /// 1-fail
        /// </summary>
        [DataMember]
        public int ResultCode { get; set; }
        /// <summary>
        /// true- result code is provided
        /// false- result code is not provided
        /// </summary>
        [DataMember]
        public bool ResultCodeSpecified { get; set; }
        /// <summary>
        /// Result Message
        /// </summary>
        [DataMember]
        public string ResultText { get; set; }
        /// <summary>
        /// MSISDN
        /// </summary>
        [DataMember]
        public string ServiceNumber { get; set; }
        /// <summary>
        /// Name of the MVNO that the subscriber of the service belongs to which has the porting reference id
        /// </summary>
        [DataMember]
        public string MVNOName { get; set; }
        /// <summary>
        /// Postpaid of Prepaid from MVNE Platform
        /// </summary>
        [DataMember]
        public string ServiceType { get; set; }
        /// <summary>
        /// MSISDN with the porting reference id
        /// </summary>
        [DataMember]
        public string SubscriberNumber { get; set; }
    }
}