using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition
{
    [MessageContract]
    public class FaultResponse
    {
        private string _statusCode;
        private string _consumerReferenceId;
        private string _transactionId;
        private DateTime _statusTime;
        private SDPStatusLine _SDPStatusLine;


        public FaultResponse()
        {
            this._SDPStatusLine = new SDPStatusLine();
        }

        public FaultResponse(string statusCode)
        {
            this._statusCode = statusCode;
            this._SDPStatusLine = new SDPStatusLine();
        }
        public FaultResponse(string statusCode, string consumerReferenceId)
        {
            this._statusCode = statusCode;
            this._consumerReferenceId = consumerReferenceId;
            this._SDPStatusLine = new SDPStatusLine();
        }

        [MessageBodyMember]
        public string statusCode
        {
            get { return this._statusCode; }
            set { this._statusCode = value; }
        }
        [MessageBodyMember]
        public string consumerReferenceId
        {
            get { return this._consumerReferenceId; }
            set { this._consumerReferenceId = value; }
        }
        [MessageBodyMember]
        public string transactionId
        {
            get { return this._transactionId; }
            set { this._transactionId = value; }
        }
        [MessageBodyMember]
        public DateTime statusTime
        {
            get { return this._statusTime; }
            set { this._statusTime = value; }
        }
        [MessageBodyMember]
        public SDPStatusLine SDPStatusLine
        {
            get { return this._SDPStatusLine; }
            set { this._SDPStatusLine = value; }
        }

    }
    
    [MessageContract]
    public class SDPStatusLine
    {
        private DateTime _stepStatusTime;
        private string _processStep;
        private string _Severity;
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
        [MessageBodyMember]
        public string Severity
        {
            get { return this._Severity; }
            set { this._Severity = value; }
        }
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