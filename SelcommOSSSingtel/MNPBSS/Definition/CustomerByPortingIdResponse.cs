using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [DataContract]
    public class CustomerByPortingIdResponse : Response
    {
        [DataMember]
        public List<NPOQCustomerByPortingIdResult> Results { get; set; }
    }
    [DataContract]
    public class NPOQCustomerByPortingIdResult:NPOQResult
    {
        /// <summary>
        /// Porting ID from the request
        /// </summary>
        [DataMember]
        public string PortingID { get; set; }
        [DataMember]
        public NPOQResult NPOQResults { get;  set;}

    }
}