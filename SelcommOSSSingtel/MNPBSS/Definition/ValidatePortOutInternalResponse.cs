using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class ValidatePortOutInternalResponse : Response
    {
        [DataMember]
        public NPOResult Result { get; set; }
        /// <summary>
        /// Prepaid or postpaid
        /// </summary>
        [DataMember]
        public string ServiceType { get; set; }
        /// <summary>
        /// Reference Id for porting
        /// </summary>
        [DataMember]
        public string ReferenceId { get; set; }
        /// <summary>
        /// MVNO Name
        /// </summary>
        [DataMember]
        public string MvnoName { get; set; }
        /// <summary>
        /// DNO Identifier
        /// </summary>
        [DataMember]
        public string DonorTelco { get; set; }
        /// <summary>
        /// RNO Identifier
        /// </summary>
        [DataMember]
        public string RecipientTelco { get; set; }        
    }

  
    
}