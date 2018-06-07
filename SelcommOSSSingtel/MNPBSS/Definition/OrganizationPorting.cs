using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class OrganizationPorting
    {
        /// <summary>
        /// The Organization Name
        /// </summary>
        [MessageBodyMember]
        public string name { get; set; }
        /// <summary>
        /// Company registration code Either RegistrationCod or AccountNumber or both must exists.
        /// </summary>
        [MessageBodyMember]
        public string id { get; set; }
        /// <summary>
        /// Account Number at the Donor Either RegistrationCod or AccountNumber or both must exists.
        /// </summary>
        [MessageBodyMember]
        public string AccountNumber { get; set; }
        
    }
}