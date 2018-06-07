using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition


{
    [MessageContract]
    public class PortingRequestID
    {
        /// <summary>
        /// the transfer request identifier
        /// </summary>
        [MessageBodyMember]
        public string Id { get; set; }
    }

}