using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class DisconnectServiceInternal:Request
    {

        /// <summary>
        /// the SD info tab which can have multiple occurances. if there is no SD for an operator, an empty SD message will also be sent to operator
        /// </summary>
        [DataMember]
        public List<SDInfo> SDInfos { get; set; }


    }

    [DataContract]
    public class SDInfo
    {
        /// <summary>
        /// reference id for the port
        /// </summary>
        [DataMember]
        public string ReferenceId { get; set; }
        /// <summary>
        /// The subscriber number in the "Send SD" business process
        /// </summary>
        [DataMember]
        public string SubscriberNumber { get; set; }
    }
}