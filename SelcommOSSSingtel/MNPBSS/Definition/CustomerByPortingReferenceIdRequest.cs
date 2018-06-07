using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [DataContract]
    public class CustomerByPortingReferenceIdRequest:Request
    {
        /// <summary>
        /// Array of Porting Reference IDs 
        /// </summary>
        [DataMember]
        public List <string> PortingReferenceID { get; set; }
    }
}