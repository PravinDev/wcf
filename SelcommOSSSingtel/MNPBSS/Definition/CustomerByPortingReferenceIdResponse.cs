using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [DataContract]
    public class CustomerByPortingReferenceIdResponse:Response
    {
        [DataMember]
        public List <NPOQCustomerByPortingReferenceIdResult> Results { get; set; }
    }
    [DataContract]
    public class NPOQCustomerByPortingReferenceIdResult:NPOQResult
    {
        [DataMember]
        /// <summary>
        /// Porting Reference ID from the request
        /// </summary>
        public string PortingReferenceID { get; set; }
        [DataMember]
        public NPOQResult NPOQResults { get; set; }
    }
}