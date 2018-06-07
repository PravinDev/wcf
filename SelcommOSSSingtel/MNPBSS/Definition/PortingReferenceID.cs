using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class PortingReferenceID
    {
        /// <summary>
        /// Unique ID for the portability request
        /// </summary>
        [MessageBodyMember]
        public string Id { get; set; }
    }


}