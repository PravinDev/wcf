using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class SDPHeader
    {
        [MessageBodyMember]
        public string applicationIdentity { get; set; }
        [MessageBodyMember]
        public string systemIdentity { get; set; }
        [MessageBodyMember]
        public string consumerReferenceId { get; set; }
        [MessageBodyMember]
        public DateTime consumerReferenceDateTime { get; set; }
        [MessageBodyMember]
        public string csrIdentity { get; set; }
        [MessageBodyMember]
        public string userIdentity { get; set; }
        [MessageBodyMember]
        public string languageCode { get; set; }
        [MessageBodyMember]
        public string countryCode { get; set; }


        [MessageContract]
        public class SDPResponseHeader
        {
            [MessageBodyMember]
            public string consumerReferenceId { get; set; }
            [MessageBodyMember]
            public DateTime transactionResponseDateTime { get; set; }
            [MessageBodyMember]
            public string transactionId { get; set; }
        }

    }
}