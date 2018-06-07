using Microsoft.Web.Services3.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Services3;
using System.Xml;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC.Helpers
{
    public class SsoTokenAssertion : SecurityPolicyAssertion
    {
        private const string SSO_TOKEN_ASSERTION = "ssoTokenAssertion";

        public override SoapFilter CreateClientInputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override SoapFilter CreateClientOutputFilter(FilterCreationContext context)
        {
            return new SsoClientSendFilter(this);
        }

        public override SoapFilter CreateServiceInputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override SoapFilter CreateServiceOutputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override void ReadXml(XmlReader reader, IDictionary<string, Type> extensions)
        {
            bool isEmpty = reader.IsEmptyElement;
            reader.ReadStartElement(SSO_TOKEN_ASSERTION);
            if(!isEmpty)
            {
                reader.ReadEndElement();
            }
        }

        public override IEnumerable<KeyValuePair<string, Type>> GetExtensions()
        {
            return new[] { new KeyValuePair<string, Type>(SSO_TOKEN_ASSERTION, GetType()) };
        }

    }
}