using System;
using System.Runtime.Serialization;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class ExtendedResponse
    {
        /// <summary>
        /// reference id for the port
        /// </summary>
        [MessageBodyMember]
        public string RequestId { get; set; }
        /// <summary>
        /// reference id for the port
        /// </summary>
        [MessageBodyMember]
        public string ReferenceId { get; set; }

        [MessageBodyMember]
        public DateTime RequestDateTime { get; set; }

        [MessageBodyMember]
        public DateTime ResponseDateTime { get; set; }
        [MessageBodyMember]
        public string Results { get; set; }
    }
}