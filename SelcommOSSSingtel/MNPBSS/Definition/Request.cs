using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class Request
    {
        /// <summary>
        /// Common Elements
        /// the operator that has send message
        /// </summary>
        [DataMember]
        public string MessageSenderTelco { get; set; }
        /// <summary>
        /// the operator that has received message
        /// </summary>
        [DataMember]
        public string MessageReceiverTelco { get; set; }
        /// <summary>
        /// transfer request creator's identifier that the creator ensures to be unambigious
        /// </summary>
        [DataMember]
        public string RequestId { get; set; }
        /// <summary>
        /// the time and date message was created
        /// </summary>
        [DataMember]
        public DateTime Timestamp { get; set; }
    }
}