using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class ServiceIdResponse
    {
        /// <summary>
        /// transfer request creator's identifier that the creator ensures to be unambigious
        /// </summary>
        [DataMember]
        public string RequestId { get; set; }
        /// <summary>
        /// transfer request creator's identifier that the creator ensures to be unambigious
        /// </summary>
        [DataMember]
        public DateTime RequestDateTime { get; set; }
        /// <summary>
        /// the time and date message was sent out from BSS
        /// </summary>
        [DataMember]
        public DateTime ResponseDateTime { get; set; }
        /// <summary>
        /// Service Number
        /// </summary>
        [DataMember]
        public string ServiceNumber { get; set; }
        /// <summary>
        /// 00 - Success, 01 - Number not found
        /// </summary>
        [DataMember]
        public string StatusCode { get; set; }

        /// <summary>
        /// If statusCode is not 00 then the reason text
        /// </summary>
        [DataMember]
        public string StatusDescription { get; set; }


    }


   
}