using SelcommWebServices.SelcommOSS.Singtel.MNPBSS2.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web.Services;
using System.Text;
using SelcommWebServices.SelcommOSS.Singtel.Helpers;

namespace SelcommWebServices.SelcommOSS.Singtel.MNPBSS2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MNPBSS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MNPBSS.svc or MNPBSS.svc.cs at the Solution Explorer and start debugging.
    [WebService]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class MNPBSS : IMNPBSS
    {
      
        public ConnectServiceResponse connectService(ConnectServiceRequest Request)
        {
            string errMsg = "";
            //  Check the Reference Id is not null
            if (string.IsNullOrWhiteSpace(Request.connectService.ServiceDetailElement.referenceId.Id))
            {
                errMsg = "Reference Id is null";
                throw new FaultException<FaultResponse>(MandatoryParameterMissing(errMsg), errMsg);
            }

            //  Invalid Reference Id 
            if (Request.connectService.ServiceDetailElement.referenceId.Id != "VT40000287_160727")
            {
                errMsg = "Reference Id is NOT VT40000287_160727";
                throw new FaultException<FaultResponse>(MandatoryParameterMissing(errMsg), errMsg);
            }

            //  Check the MSISDN
            if (string.IsNullOrWhiteSpace(Request.connectService.ServiceDetailElement.serviceID.ToString()))
            {
                errMsg = "Service Id is null";
                throw new FaultException<FaultResponse>(MandatoryParameterMissing(errMsg), errMsg);
            }

            //  Invalid MSISDN
            if (Request.connectService.ServiceDetailElement.serviceID.serviceID != "98340654")
            {
                errMsg = "Service ID is NOT 98340654";
                throw new FaultException<FaultResponse>(MandatoryParameterMissing(errMsg), errMsg);
            }

            ConnectServiceResponse ConnectServiceResponse = new ConnectServiceResponse();

            ConnectServiceResponse = new ConnectServiceResponse();

            ConnectServiceResponse.RequestId = Request.connectService.requestId.Id;
            ConnectServiceResponse.ReferenceId = Request.connectService.ServiceDetailElement.referenceId.Id;
            ConnectServiceResponse.ResponseDateTime = DateTime.Now;
            ConnectServiceResponse.RequestDateTime = Request.sdpServiceHeaders.consumerReferenceDateTime;
            ConnectServiceResponse.Results = "SUCCESS";

            return ConnectServiceResponse;

        }


        /// <summary>
        /// validate Mandatory fields
        /// </summary>
        private FaultResponse MandatoryParameterMissing(string Description)
        {
            FaultResponse FaultResponse = new FaultResponse();
            FaultResponse.statusCode = "SPD_MandatoryParameterMissing";
            FaultResponse.SDPStatusLine.statusCode = "SPD_MandatoryParameterMissing";
            FaultResponse.SDPStatusLine.statusDescription = Description;
            FaultResponse.SDPStatusLine.Severity = "CRITICAL";
            FaultResponse.statusTime = DateTime.Now;
            FaultResponse.SDPStatusLine.stepStatusTime = DateTime.Now;
            return FaultResponse;
        }


        public PortInNotificationResponse portInNotification(PortInNotificationRequest Request)
        {
            FaultResponse FaultResponse;

            PortInNotificationResponse PortInNotificationResponse = new PortInNotificationResponse();

            //  Check the Reference Id is not null
            if (Request.PortInNotification.referenceID.Id == null)
            {
                FaultResponse = new FaultResponse();
                FaultResponse.statusCode = "SPD_MandatoryParameterMissing";
                FaultResponse.SDPStatusLine.statusCode = "SPD_MandatoryParameterMissing";
                FaultResponse.SDPStatusLine.statusDescription = "Missing Reference Id";
                FaultResponse.SDPStatusLine.Severity = "CRITICAL";
                FaultResponse.statusTime = DateTime.Now;
                FaultResponse.SDPStatusLine.stepStatusTime = DateTime.Now;
                FaultResponse.consumerReferenceId = Request.sdpServiceHeaders.consumerReferenceId;
                throw new FaultException<FaultResponse>(FaultResponse);
            }
            //  Invalid Reference Id 
            if (Request.PortInNotification.referenceID.Id != "VT40000287_160727")
            {
                FaultResponse = new FaultResponse();
                FaultResponse.statusCode = "SPD_InputValidationFault";
                FaultResponse.SDPStatusLine.statusCode = "SPD_InputValidationFault";
                FaultResponse.SDPStatusLine.statusDescription = "Invalid Reference Id";
                FaultResponse.SDPStatusLine.Severity = "CRITICAL";
                FaultResponse.statusTime = DateTime.Now;
                FaultResponse.SDPStatusLine.stepStatusTime = DateTime.Now;
                FaultResponse.consumerReferenceId = Request.sdpServiceHeaders.consumerReferenceId;
                throw new FaultException<FaultResponse>(FaultResponse);
            }
            //  Check the MSISDN
            if (Request.PortInNotification.serviceID.serviceID == null)
            {
                FaultResponse = new FaultResponse();
                FaultResponse.statusCode = "SPD_MandatoryParameterMissing";
                FaultResponse.SDPStatusLine.statusCode = "SPD_MandatoryParameterMissing";
                FaultResponse.SDPStatusLine.statusDescription = "Missing Service Number";
                FaultResponse.SDPStatusLine.Severity = "CRITICAL";
                FaultResponse.statusTime = DateTime.Now;
                FaultResponse.SDPStatusLine.stepStatusTime = DateTime.Now;
                FaultResponse.consumerReferenceId = Request.sdpServiceHeaders.consumerReferenceId;
                throw new FaultException<FaultResponse>(FaultResponse);
            }

            //  Invalid MSISDN
            if (Request.PortInNotification.serviceID.serviceID != "98340654")
            {
                FaultResponse = new FaultResponse();
                FaultResponse.statusCode = "SPD_InputValidationFault";
                FaultResponse.SDPStatusLine.statusCode = "SPD_InputValidationFault";
                FaultResponse.SDPStatusLine.statusDescription = "Invalid Service Number";
                FaultResponse.SDPStatusLine.Severity = "CRITICAL";
                FaultResponse.statusTime = DateTime.Now;
                FaultResponse.SDPStatusLine.stepStatusTime = DateTime.Now;
                FaultResponse.consumerReferenceId = Request.sdpServiceHeaders.consumerReferenceId;
                throw new FaultException<FaultResponse>(FaultResponse);
            }

            PortInNotificationResponse = new PortInNotificationResponse();

            PortInNotificationResponse.RequestId = Request.PortInNotification.requestID.Id;
            PortInNotificationResponse.ReferenceId = Request.PortInNotification.referenceID.Id;
            PortInNotificationResponse.ResponseDateTime = DateTime.Now;
            PortInNotificationResponse.RequestDateTime = Request.sdpServiceHeaders.consumerReferenceDateTime;
            PortInNotificationResponse.Results = "SUCCESS";

            return PortInNotificationResponse;
        }
    }
}
