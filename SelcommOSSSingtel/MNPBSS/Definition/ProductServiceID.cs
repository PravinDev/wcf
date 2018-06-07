using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class ProductServiceID
    {
        /// <summary>
        /// The users means to identify a Product
        /// </summary>
        [MessageBodyMember]
        public string serviceID { get; set; }
    }
}