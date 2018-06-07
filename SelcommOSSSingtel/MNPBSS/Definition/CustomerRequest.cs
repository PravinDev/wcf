using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class CustomerRequest : Request
    {
        /// <summary>
        /// Array of customer information to validate
        /// </summary>
        [DataMember]
        public List<CustomerInfo> CustomerInfo { get; set; }
    }
    [DataContract]
    public class CustomerInfo
    {
        /// <summary>
        /// Valid identification number from the agreed list of ID types [eg: Passport Number, FIN Number, NRI]
        /// </summary>
        [DataMember]
        public string IdentificationNumber { get; set; }
        /// <summary>
        /// MSISDN
        /// </summary>
        [DataMember]
        public string ServiceNumber { get; set; }

    }
}