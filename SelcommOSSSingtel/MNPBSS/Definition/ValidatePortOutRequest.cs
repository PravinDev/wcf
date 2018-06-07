using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
    {
    [MessageContract]
    public class ValidatePortOutRequest
    {
        [MessageHeader]
        public SDPHeader sdpServiceHeaders { get; set; }
        [MessageBodyMember]
        public ValidatePortOut validatePortOut { get; set; }
       

    }

    [MessageContract]
    public class ValidatePortOut
    {
        [MessageBodyMember]
        public PortingReferenceID referenceId { get; set; }
        [MessageBodyMember]
        public PortOutDetailsSN portOutDetails { get; set; }
    }

}