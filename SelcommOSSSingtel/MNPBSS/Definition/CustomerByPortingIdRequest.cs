using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [DataContract]
    public class CustomerByPortingIdRequest:Request
    {
        [DataMember]
        public List<string> PortingRequestID { get; set; }
    }
}