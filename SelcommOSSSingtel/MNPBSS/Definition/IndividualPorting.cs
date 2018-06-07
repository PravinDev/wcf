using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class IndividualPorting
    {
        /// <summary>
        /// The Individual Name
        /// </summary>
        [MessageBodyMember]
        public string name { get; set; }
        /// <summary>
        /// An Identifier Individual ID
        /// </summary>
        [MessageBodyMember]
        public string id { get; set; }
        /// <summary>
        ///The Individual Identifier type with values: 1-NRIC (Pink), NRIC (Blue), Driving License ID, 2-SAF 11B (SAF Identity Card),
        ///3-Reserved,4-FIN Pass,5-BRN (supported by NRIC of AO, letter to identify Authorized Officer(“AO”)), 6-Singapore Passport,
        ///7-Social Visit Pass, 8-Foreign Passport (TBD: for prepaid only), 99-Others
        /// </summary>
        [MessageBodyMember]
        public int idType { get; set; }
    }
}