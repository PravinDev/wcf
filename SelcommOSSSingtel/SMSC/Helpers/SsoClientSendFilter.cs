using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3;
using System.Xml;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC.Helpers
{
    public class SsoClientSendFilter : SendSecurityFilter
    {
        protected const string SSO_HEADER_ELEMENT = "sessionId";

        public SsoClientSendFilter(Microsoft.Web.Services3.Design.SecurityPolicyAssertion parentAssertion) :
            base(parentAssertion.ServiceActor, true)
        {
        }

        public override void SecureMessage(SoapEnvelope envelope, Security security)
        {
            string ssoTokenString = "sessionless";

            if(String.IsNullOrEmpty(ssoTokenString))
            {
                throw new ApplicationException(
             "Could not generate an SSO token. Please ensure your user name exists within SSO and the password matches the one expected by SSO.");
            }

            Microsoft.Web.Services3.Security.Tokens.UsernameToken defaultToken2;
            var username = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            var password = System.Configuration.ConfigurationManager.AppSettings["PassWord"]; defaultToken2 = new Microsoft.Web.Services3.Security.Tokens.UsernameToken("mvne", "mvne1234", Microsoft.Web.Services3.Security.Tokens.PasswordOption.SendPlainText);
            security.Tokens.Add(defaultToken2);

            XmlElement ssoTokenElement = envelope.CreateElement(SSO_HEADER_ELEMENT);
            ssoTokenElement.InnerText = ssoTokenString;
            envelope.Header.AppendChild(ssoTokenElement);

        }

    }
}