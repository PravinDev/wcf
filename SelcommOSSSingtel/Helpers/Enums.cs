using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.Helpers
{
    [DataContract]
    public class Enums
    {
        [DataContract]
        public enum activationType
        {
            //MD interface
            [Description("MD_PNEW")]
            ActivateService,
            [Description("MD_CDATA")]
            DeactivateData,
            [Description("MD_CEASE")]
            DeactivateService,
            [Description("MD_CGPRS")]
            DeactivateGPRS,
            [Description("MD_CHGFAC")]
            ValueAddedService,
            [Description("MD_CHGINST")]
            SIMSwap,
            [Description("MD_CHGPSTN")]
            MSNChange,
            [Description("MD_PDATA")]
            ActivateData,
            [Description("MD_PGPRS")]
            ActivateGPRS,
            [Description("MD_RECONN1")]
            TemporaryUnsuspension,
            [Description("MD_TD1")]
            TemporarySuspension_CreditControl,
            [Description("MD_TD")]
            TemporarySuspension_CustomerRequest,
            [Description("MD_LNPDB_INSERT")]
            LnpdbInsert,
            [Description("MD_LNPDB_UPDATE")]
            LnpdbUpdate,
            [Description("MD_LNPDB_DELETE")]
            LnpdbDelete,

            //SMSC
            [Description("SMSC_SENDSMS")]
            Send_SMS,
        }
    }
}