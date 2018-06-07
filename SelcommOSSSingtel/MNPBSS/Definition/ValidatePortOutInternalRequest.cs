using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [DataContract]
    public class ValidatePortOutInternalRequest : Request //xVNE probably means “x Virtual Network Enabler” where “x” might be anything
    {
        
        //NPO Elements
        /// <summary>
        /// Reference Id for porting
        /// </summary>
        [DataMember]
        public string ReferenceId { get; set; }
        /// <summary>
        /// RNO Identifier
        /// </summary>
        [DataMember]
        public string RecipientTelco { get; set; }
        /// <summary>
        /// DNO Identifier
        /// </summary>
        [DataMember]
        public string DonorTelco { get; set; }
        /// <summary>
        /// old reference ID for the porting for the retry cases
        /// </summary>
        [DataMember]
        public string OldReferenceId { get; set; }
        /// <summary>
        /// '1'-Yes
        /// '0'-No(default) 
        /// If this indicator is '1', this means the NPO is applied through a Letter Of Authorization. this indication of DNO's information only. It is nor meant for Port-out approval Validation.
        /// </summary>
        [DataMember]
        public string ByLoa { get; set; }
        /// <summary>
        /// '1'-Yes
        /// '0'-No(default) 
        /// If this indicator is '1', this means the subscriber acknowledges that service package, including but not limited to, supplementary service, bundle service, service contract and equipment contract will be terminated or reset after the subscriber port-out, if any.
        /// </summary>
        [DataMember]
        public string UndertakingAck { get; set; }

        /// <summary>
        /// Subscriber Sequence
        /// a list of subscriber number. there can be more than one subscriber number.
        /// </summary>
        [DataMember]
        public List<string> SubscriberNumber { get; set; }
        /// <summary>
        /// the ordered transfer time of the port order. specify the start time of the time window.
        /// </summary>
        [DataMember]
        public DateTime OrderedTransferTime { get; set; }
        /// <summary>
        /// Present for NPO to DNO and Echo NPO to RNO only.
        /// specify end-time of the DNO.
        /// </summary>
        [DataMember]
        public DateTime OrderedApprovalTime { get; set; }
        /// <summary>
        /// routing number of RNO.
        /// </summary>
        [DataMember]
        public string RouteNumber { get; set; }
        /// <summary>
        /// service type of 'Prepaid' or 'Postpaid'
        /// </summary>
        [DataMember]
        public string ServiceType { get; set; }


        [DataMember]
        public CustomerBase Customer { get; set; }
        /// <summary>
        /// the author in "Send NPO business process"
        /// </summary>
        [DataMember]
        public Author Author { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [DataMember]
        public string Remark { get; set; }


    }

    [DataContract]
    [KnownType(typeof(CorporateCustomer))]
    [KnownType(typeof(PersonCustomer))]
    public class CustomerBase
    {

    }
    [DataContract]
    public class CorporateCustomer : CustomerBase
    {
        /// <summary>
        /// name of Company
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }
        /// <summary>
        /// company registration code. either registration code or account number or both must exist.
        /// </summary>
        [DataMember]
        public string RegistrationCode { get; set; }
        /// <summary>
        /// account number at the donor
        /// </summary>
        [DataMember]
        public string AccountNumber { get; set; }
        /// <summary>
        /// Signing date
        /// </summary>
        [DataMember]
        public DateTime SignatureDate { get; set; }
    }
    [DataContract]
    public class PersonCustomer : CustomerBase
    {
        /// <summary>
        /// name of owner
        /// </summary>
        [DataMember]
        public string OwnerName { get; set; }
        /// <summary>
        /// Id of owner
        /// </summary>
        [DataMember]
        public string OwnerId { get; set; }
        /// <summary>
        /// Type of owner ID 
        /// </summary>
        [DataMember]
        public int TypeOfId { get; set; }
        /// <summary>
        /// Signing date
        /// </summary>
        [DataMember]
        public DateTime SignatureDate { get; set; }
    }
    [DataContract]
    public class Author
    {
        /// <summary>
        /// operators name
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// author's phone number
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// author's fax number
        /// </summary>
        [DataMember]
        public string Telefax { get; set; }
        /// <summary>
        /// authors email address
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// handing date
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }

    }

}