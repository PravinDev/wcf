using SelcommWebServices.SelcommOSS.Helpers;
using SelcommWebServices.SelcommOSS.Singtel.SMSC.Processes;
using SelcommWebServices.SelcommOSS.Singtel.SMSC.Processes.ActionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ntwkSMSC" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ntwkSMSC.svc or ntwkSMSC.svc.cs at the Solution Explorer and start debugging.
    public class ntwkSMSC : IntwkSMSC
    {
        #region "Daemon process"
        /// <summary>
        /// Process all pending events relates to Openet actions
        /// </summary>
        /// <param name="PrivateKey">Key assigned to the client by Select Software Solutions</param>
        /// <returns></returns>
        public string ServiceActivatePrivateKey(string PrivateKey)
        {
            string uniqueFileName = Guid.NewGuid().ToString();
            using (var traceLog = new Singtel.Helpers.TraceLog(this.GetType().Name, PrivateKey, uniqueFileName) { IsEnabled = true })
            {
                try
                {
                    ServerCertificateValidation();
                    var handlersToExecute = new List<SMSCActionHandlerBase>
                    {

                        new SendSMSActionHandler(PrivateKey, Singtel.Helpers.Helpers.ClientName),
                        // new getSMSDeliveryStatusActionHandler(PrivateKey, Helpers.Helpers.ClientName)
                };
                    foreach (var handler in handlersToExecute)
                    {
                        handler.ProcessAllEventDispatchers(traceLog);
                    }
                    return $"{Singtel.Helpers.Helpers.ReturnSuccess}";
                }
                catch (Exception ex)
                {
                    return $"{Singtel.Helpers.Helpers.ReturnFailure} - {ex.Message}";
                }
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// This method is to validate a server certificate.
        /// Make sure this method is called once during the life of the application if you can. As it might lead to a memory leak if you do it many times.
        /// </summary>
        public void ServerCertificateValidation()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
        }
        private bool SenderIsLoopbackRequest(object sender)
        {
            WebRequest Request = sender as WebRequest;
            if (Request != null && Request.RequestUri != null)
            {
                IPHostEntry ServiceIp = Dns.GetHostEntry(Request.RequestUri.Host);
                // If we have a name mismatch for a certificate but we're a request to a loopback
                // address we can ignore it for the web services since we control the whole server anyway
                return ServiceIp.AddressList.All(IPAddress.IsLoopback);
            }
            else
                return false;
        }

        private bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            switch (sslPolicyErrors)
            {
                // If the certificate is fine let it through.
                case SslPolicyErrors.None:
                    return true;
                // If not, just check if the request is a loopback request and then allow it if it is.
                default:
                    return SenderIsLoopbackRequest(sender);
            }
        }
        #endregion
    }
}
