using SelcommWSsvc.SelcommWSAll;
using System;
using System.Collections.Generic;
using System.Linq;
using SelcommWebServices.SelcommOSS.Singtel.Helpers;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC.Processes
{
    public abstract class SMSCActionHandlerBase
    {
        public abstract string TargetActionCode { get; }
        protected string PrivateKey { get; }
        public string ClientName { get; }
        protected ClientSession ClientSessionWS { get; set; }
        public String SessionKey
            => this.ClientSessionWS?.SessionKey;

        public string ContBusUnitCode = string.Empty;

        public SMSCActionHandlerBase(string privateKey, string clientName, ClientSession session = null)
        {
            using (var SessionClientWS = new SessionClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
            {
                ClientSessionWS = new ClientSession();
                var sessionDetails = SessionClientWS.SessionCreateByPrivateKey(privateKey, "webuser", "resubew");
                this.ClientSessionWS.SessionKey = sessionDetails.SessionKey;
            }

        }
        /// <summary>
        /// This method retrieves events based on the provided provisioning action type.
        /// </summary>
        protected EventDispatcherList GetEventsByActionCode(String actionCode)
        {
            using (var eventClient = new SelcommWSsvc.SelcommWSAll.EventClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
            {
                var eventDispatcherList = new EventDispatcherList();
                if (!string.IsNullOrWhiteSpace(actionCode))
                {
                    //load the event type/code from evnt_dispatcher table based on the service id
                    eventDispatcherList = eventClient.EventDispatcherList(ClientSessionWS.SessionKey, actionCode, true);
                }
                return eventDispatcherList;
            }
        }
        protected virtual String GetSystemConfigValue(String key, String altKey)
            => Singtel.Helpers.Helpers.GetSystemConfigValue(this.SessionKey, key, altKey);
        protected abstract IEnumerable<EventDispatcher> GetTargetEvents();

        protected virtual IEnumerable<EventDispatcher> GetTargetEventDispatchers()
            => GetEventsByActionCode(this.TargetActionCode);
        /// <summary>
        /// This method is responsible for processing a single Event
        /// </summary>
        public abstract void ProcessEvent(EventDisplay targetEvent, TraceLog t);
        /// <summary>
        /// <para>Checks the event dispatcher list and if available, Processes the Event Dispatchers.</para>
        /// </summary>
        public void ProcessAllEventDispatchers(TraceLog t)
        {
            var eventDispatcherList = this.GetTargetEventDispatchers();
            t.CreateLog(null);
            t.CreateLog("---------------------------------------------------------------------------------------------------------");
            t.CreateLog("List of Event Dispatcher:" + eventDispatcherList.Count());
            this.ProcessEventDispatchers(eventDispatcherList, t);
        }
        /// <summary>
        /// <para>Checks the event and if available, Processes the service actions.</para>
        /// </summary>
        protected virtual void ProcessEventDispatchers(IEnumerable<EventDispatcher> eventDispatcherList, TraceLog t)
        {
            using (var eventClient = new SelcommWSsvc.SelcommWSAll.EventClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
            {
                foreach (var eventDispatcher in eventDispatcherList)
                {
                    var events = eventClient.EventDisplayList(this.ClientSessionWS.SessionKey, new EventType { EventCode = eventDispatcher.EventCode, EventTypeMember = eventDispatcher.EventType }, eventDispatcher.ScheduleStatus);

                    t.CreateLog($"Number of events to be processed- {events.Count}");
                    int totalEvents = events?.Count ?? 0;
                    if (events?.Any() == true)
                    {
                        foreach (var evnt in events)
                        {
                            try
                            {
                                this.ProcessEvent(evnt, t);
                                //TODO - below should be in generic helper                            
                                // WcfService2.Helpers.Helpers.updateEventStatus(this.SessionKey, evnt.EventId, "C", t, null);

                            }
                            catch (Exception ex)
                            {
                                //record the error and continue with next event
                                t.CreateLog("----------------------------------------!! ERROR OCCURRED !!----------------------------------------");
                                string note = $"ERROR OCCURED. The provisioning action {TargetActionCode} stopped due to the reason: {ex.Message}";
                                t.CreateLog(note);
                                // SendEmailToWebSupport(note, t);
                                //TODO - below should be in generic helper
                                Singtel.Helpers.Helpers.updateEventStatus(this.SessionKey, evnt.EventId, "F", t, null);

                            }

                        }
                    }
                }
            }
        }


        /// <summary>
        /// Sends email to Web Support if an event failure occurs 
        /// </summary>
        protected void SendEmailToWebSupport(string Note, TraceLog t)
        {
            try
            {
                using (var ConfigurationClientWS = new SelcommWSsvc.SelcommWSAll.ConfigurationClient(Singtel.Helpers.Helpers.GetSelcommWsEndpointName()))
                {
                    string body = $"provider code : {this.PrivateKey} <br />" +
                         $"server environment : {Singtel.Helpers.Utility.ServerEnvironment}<br />" +
                         $"{Note}";
                    string subject = "this is just for test ";//$"selcommoss - attention required for trustpower event processing. server : {utility.serverenvironment}";

                    string emailTo = ConfigurationClientWS.GetConfigValue(this.SessionKey, "TPOWER_WEBSUPPORT_EMAIL", null, null, null, null, null, null);
                    if (string.IsNullOrWhiteSpace(emailTo))
                    {
                        emailTo = "websupport@selectsoftware.com.au";
                        t.CreateLog($"To email is not configured in the system. The email address [{emailTo}] is assigned as To email on the fly to send email.");
                    }
                    EmailHelper.SendEmail("noreply@selectsoftware.com.au", emailTo, subject, body, true);
                    t.CreateLog($"Email has been sent to {emailTo} to notify the action.");
                }
            }
            catch (Exception ex)
            {
                t.CreateLog("Email to websupport is unsuccessful");
                t.CreateLog(Singtel.Helpers.Helpers.getExceptionString(ex).Replace("<br/>", Environment.NewLine).Replace("<br />", Environment.NewLine));
                t.CreateLog("==================The email was supposed to be sent to notify the following issue.===============================");
                t.CreateLog(Note.Replace("<br/>", Environment.NewLine).Replace("<br />", Environment.NewLine));



            }
        }
    }
}