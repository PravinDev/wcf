using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using static SelcommWebServices.SelcommOSS.Singtel.MNPBSS.Helpers.Enums;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS.Definition
{
    [MessageContract]
    public class SDPStatusLine
    {
        private DateTime _stepStatusTime;
        private string _processStep;
        //enums not handled
        //private Severity _Severity;
        private string _statusCode;
        private string _statusDescription;
        private string _providerFault;

        public SDPStatusLine()
        {

        }

        [MessageBodyMember]
        public DateTime stepStatusTime
        {
            get { return this._stepStatusTime; }
            set { this._stepStatusTime = value; }
        }
        [MessageBodyMember]
        public string processStep
        {
            get { return this._processStep; }
            set { this._processStep = value; }
        }
        //[MessageBodyMember]
        //public Severity Severity
        //{
        //    get { return this._Severity; }
        //    set { this._Severity = value; }
        //}
        [MessageBodyMember]
        public string statusCode
        {
            get { return this._statusCode; }
            set { this._statusCode = value; }
        }
        [MessageBodyMember]
        public string statusDescription
        {
            get { return this._statusDescription; }
            set { this._statusDescription = value; }
        }
        [MessageBodyMember]
        public string providerFault
        {
            get { return this._providerFault; }
            set { this._providerFault = value; }
        }

    }
}