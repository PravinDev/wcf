using SelcommWebServices.SelcommOSS.Singtel.Helpers;
using SelcommWSsvc.SelcommWSAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SelcommWebServices.SelcommOSS.Singtel.Helpers.Enums;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC.Processes.ActionHandler
{
    public class SendSMSActionHandler: SMSCActionHandlerBase
    {
        public SendSMSActionHandler(string privateKey, string clientName, ClientSession session = null) : base(privateKey, clientName, session)
        {
        }


        public override string TargetActionCode
         => activationType.Send_SMS.GetEnumDescription();
        protected override IEnumerable<EventDispatcher> GetTargetEvents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// process an event for sendSms service.
        /// </summary>
        /// <param name="targetEvent"></param>
        /// <param name="t"></param>
        public override void ProcessEvent(EventDisplay targetEvent, TraceLog t)
        {

            t.CreateLog(null);
            t.CreateLog($"********************STARTED { TargetActionCode}********************************");
            t.CreateLog("Start Of The Process For Event " + targetEvent.EventId + " And Service Number " + targetEvent.ServiceNumber + " - !!!! ServiceRequest() !!!!");

            using (var EventClientWS = new SelcommWSsvc.SelcommWSAll.EventClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
            using (var ContactClientWS = new SelcommWSsvc.SelcommWSAll.ContactClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
            using (var ServiceClientWS = new SelcommWSsvc.SelcommWSAll.ServiceClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
            {


                // Updating EventSchedule Status to R - START
                t.CreateLog("Updating Event Status to R- START - EventClientWS.EventScheduleUpdate()");

                Singtel.Helpers.Helpers.updateEventStatus(SessionKey, targetEvent.EventId, "R", t, null);
                t.CreateLog("Updating Completed");
                // Updating EventSchedule Status - END

                // Loading ServiceDisplay details of a current processing event
                t.CreateLog("Loading ServiceDisplay Details - ServiceClientWS.ServiceDisplay() For service id: " + targetEvent.ServiceId);
                var aService = ServiceClientWS.ServiceDisplay(this.ClientSessionWS.SessionKey, new SelcommWSsvc.SelcommWSAll.ServiceDisplay { Id = targetEvent.ServiceId });
                if (aService != null)
                    t.CreateLog("ServiceDisplay Details loaded Successfully");
                else
                    t.CreateLog("ServiceDisplay Details not loaded");

                var ContactObj = ContactClientWS.Contact(ClientSessionWS.SessionKey, targetEvent.ContactCode, true, false, false, false);
                var businessUnitCode = ContactObj.CurrentBusinessUnit.Code;

                var eventNote = EventClientWS.EventNoteList(ClientSessionWS.SessionKey, targetEvent.EventId, false).FirstOrDefault();

                try
                {
                    SendSmsService newSMS = new SendSmsService();
                    sendSms newMSG = new sendSms();

                    List<string> address = new List<string>();
                    string aAddress = aService.ServiceNumber; //"tel:6596165414";
                    var FirstTwoDigitOfServiceNumber= Helpers.SmscHelper.GetFirstTwoDigitOfServiceNumber(aAddress);
                    if (FirstTwoDigitOfServiceNumber != 65)
                        aAddress = "tel:65".Trim() + aAddress;
                    address.Add(aAddress);
                    newMSG.addresses = address.ToArray();
                    newMSG.message = eventNote?.Text.ToString().Trim();
                    if (newMSG.message == null)
                        throw new Exception("Message should not be null. Event notes is null.");
                    newMSG.senderName = GetSystemConfigValue("SG_SMSC_FROM", "SG_SMSC_FROM_" + ContBusUnitCode);
                    t.CreateLog($"Request sent from IOTB:{Utility.SerializeToJSON(newMSG)}");
                    sendSmsResponse newResponse = newSMS.sendSms(newMSG);
                    t.CreateLog($"Response received{Utility.SerializeToJSON(newResponse)}");

                }

                catch (Exception ex)
                {

                    var sx = ex as System.Web.Services.Protocols.SoapException;
                    if (sx != null)
                    {
                        t.CreateLog($"got SoapException. InnerText=\"{sx.Detail.InnerText}\"");
                        t.CreateLog($"OuterXML=\"{sx.Detail.OuterXml}\"");
                    }
                    t.CreateLog("ex.ToString=" + ex.ToString());

                    //record the error and continue with next event
                    t.CreateLog("----------------------------------------!! ERROR OCCURRED !!----------------------------------------");
                    string note = $"ERROR OCCURED. The provisioning action {TargetActionCode} stopped due to the reason: {ex.Message}";
                    t.CreateLog(note);
                    SendEmailToWebSupport(note, t);
                    Singtel.Helpers.Helpers.updateEventStatus(SessionKey, targetEvent.EventId, "F", t, note);

                }
            }
        }

    }
}