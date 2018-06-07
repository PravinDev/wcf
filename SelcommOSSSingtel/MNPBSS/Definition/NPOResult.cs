using System.Runtime.Serialization;
using System.ServiceModel;
namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition

{
    [MessageContract]
    public class NPOResult{
        /// <summary>
        /// MSISDN
        /// </summary>
        [MessageBodyMember]
        public string SubscriberNumber { get; set; }
        /// <summary>
        /// 0-success 
        /// 1-fail
        /// </summary>
        [MessageBodyMember]
        public int ResultCode { get; set; }
        [MessageBodyMember]
        public bool ResultCodeSpecified { get; set; }
        /// <summary>
        ///If failed to locate of service is not in the correct state, will add text here
        /// </summary>
        [MessageBodyMember]
        public string ResultText { get; set; }
        
    }
}