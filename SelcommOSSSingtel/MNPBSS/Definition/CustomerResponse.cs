using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class CustomerResponse:Response
    {
        [DataMember]
        public List<NPOQResult> Results { get; set; }
    }
}