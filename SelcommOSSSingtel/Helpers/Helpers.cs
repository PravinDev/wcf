using SelcommWSsvc.SelcommWSAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.Helpers
{
    public static class Helpers
    {
        #region PrivateProperties

        public static string AutoNumCode { get; set; }

        #endregion
        #region PublicProperties
        public static readonly string ClientName;
        public const string ReturnSuccess = "SUCCESS";
        public const string ReturnFailure = "FAILURE";

        #endregion

        /// <summary>Returns the <see cref="Utility.ServerEnvironment"/> in lowercase if it is one of the well known values, otherwise returns null.</summary>
        /// <remarks>
        /// This was used previously only to determine the name of the endpoint to use for calls to <see cref="System.ServiceModel.ClientBase{TChannel}.ClientBase(String)"/>. 
        /// However, that has been deprecated as it is not a viable solution of the off-site deployment nature of Singtel. If you are just using an endpoint name just call <see cref="GetSelcommWsEndpointName"/>. If you are trying to do something else try to use <see cref="Utility.ServerEnvironment"/> directly if you must, however, do note it may break in Singapore vs Sydney.
        /// </remarks>
        [Obsolete("Use `" + nameof(GetSelcommWsEndpointName) + "` to determine the SelcommWS endpoint name instead as it will work in Singtel. Otherwise, you may need to refer to `" + nameof(Utility.ServerEnvironment) + "` depending on what exactly you're trying to achieve.", error: true)]
        public static string GetSystemEnvironment()
        {
            switch (Utility.ServerEnvironment)
            {
                case "DEV":
                    return "dev";
                case "QA":
                    return "qa";
                case "UA":
                    return "ua";
                case "PROD":
                    return "prod";
                default:
                    return null;
            }

        }

        /// <summary>Returns the endpoint name (refer to the ctor: <see cref="System.ServiceModel.ClientBase{TChannel}.ClientBase(String)"/>) to use based on the <see cref="Utility.ServerEnvironment"/> for SelcommWS service clients.</summary>
        private static String GetEndpointNameFromLegacy()
        {
            switch (Utility.ServerEnvironment)
            {
                case "DEV":
                    return "localhost";
                case "QA":
                    return "qa.selcomm.com";
                case "UA":
                    return "ua.selcomm.com";
                case "PROD":
                    return "selcomm.com";
                default:
                    throw new InvalidOperationException("Unknown legacy selcomm environment value configured.");
            }
        }

        /// <summary>Returns the endpoint name (refer to the ctor: <see cref="System.ServiceModel.ClientBase{TChannel}.ClientBase(String)"/>) to use for SelcommWS service clients.</summary>
        /// <remarks>This will first try to use the <![CDATA[<add key="SelcommWS-Endpoint-Name" />]]> value for the endppint name, however, if it is not defined (null or whitespace) then it will fallback to <see cref="GetEndpointNameFromLegacy()"/> (which is based on the <see cref="Utility.ServerEnvironment"/>) which should work in Sydney-based environments.</remarks>
        public static String GetSelcommWsEndpointName()
        {
            var webConfiguredEndpointName = System.Configuration.ConfigurationManager.AppSettings["SelcommWS-Endpoint-Name"];
            var targetEndpointName = !String.IsNullOrWhiteSpace(webConfiguredEndpointName) ? webConfiguredEndpointName : GetEndpointNameFromLegacy();
            if (targetEndpointName == null)
                throw new InvalidOperationException("No endpoint name specified.");

            return targetEndpointName;
        }
        /// <summary>
        /// This is for generating autonumber unique to AutoNumberCode defined.
        /// </summary>
        /// <returns>returns numeric string</returns>
        public static string GenerateAutoNumber(String sessionKey, String autoNumberCode)
        {
            string autoNumber = string.Empty;
            try
            {
                using (var ConfigurationClientWS = new ConfigurationClient(GetSelcommWsEndpointName()))
                {
                    autoNumber = ConfigurationClientWS.Autonumber(sessionKey, autoNumberCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while generating autonumber", ex);
            }

            return autoNumber;
        }

        public static string GetSystemConfigValue(String sessionKey, string Key, string AltKey)
        {
            string autoNumber = string.Empty;
            try
            {
                using (var ConfigurationClientWS = new ConfigurationClient(GetSelcommWsEndpointName()))
                {
                    autoNumber = ConfigurationClientWS.GetConfigValue(sessionKey, Key, AltKey, "", false, 100, false, false);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while generating autonumber", ex);
            }

            return autoNumber;
        }
        #region Private Methods
        public static string getExceptionString(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            Exception newException = ex;
            sb.Append($"Exception Type: {ex.GetType()}.");
            do
            {
                sb.Append(getExceptionDetails(newException));
                if (ex?.InnerException != null)
                {
                    sb.Append("==============Inner Exception=====================");
                    newException = ex.InnerException;
                }
                else
                {
                    newException = null;
                }
            } while (newException != null);
            return sb.ToString();
        }

        private static string getExceptionDetails(Exception ex)
        {
            if (ex == null)
                return null;

            return $"Error Message : {ex.Message} <br />" + "\t\t" +
                $"Error Source : {ex.Source} <br />" + "\t\t" +
               $"StackTrace : {ex.StackTrace} <br />" + "\t\t" +
               $"Targe site : {ex.TargetSite} <br />" + "\t\t";
        }

        #endregion

        //TODO: uncomment below methods

        public static void updateEventStatus(string sessionKey, long eventRef, string schedStatCode, TraceLog t, string eventNote = null)
        {
            Event NewEvent = new Event();
            NewEvent = new Event { Id = eventRef };
            EventSchedule Schedule = new EventSchedule
            {
                EventScheduleStatus = new EventScheduleStatus { Code = schedStatCode },
                Event = NewEvent,
                DueDate = DateTime.Now
            };
            using (var EventClientWS = new SelcommWSsvc.SelcommWSAll.EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                EventClientWS.EventScheduleUpdate(sessionKey, Schedule, eventNote);
                string reason = string.IsNullOrWhiteSpace(eventNote) ? null : $"Reason : {eventNote}";
                t.CreateLog($"Event {eventRef} is updated to the status {schedStatCode}. {reason}");
            }
        }

        public static void UpdateEventNote(string sessionKey, long eventID, string note, TraceLog t)
        {
            using (var EventClientWS = new SelcommWSsvc.SelcommWSAll.EventClient(GetSelcommWsEndpointName()))
            {
                EventClientWS.EventNoteUpdate(sessionKey, eventID, note);
                t.CreateLog(note);
            }
        }

        public static void ActionResponse(string SessionKey, EventDisplay targetEvent, string AttribId, string AttribValue, string targetActionCode, TraceLog t)
        {
            t.CreateLog("---START OF ACTION_RESPONSE---");
            try
            {
                using (var ServiceClientWS = new SelcommWSsvc.SelcommWSAll.ServiceClient(GetSelcommWsEndpointName()))
                {
                    var serviceAttribute = new ServiceAttribute
                    {
                        AttributeTemplate = new AttributeTemplate { Id = Convert.ToInt64(AttribId) },
                        Key = targetEvent.ServiceId,
                        Value = AttribValue,
                        Event = new Event() { Id = targetEvent.EventId }
                    };
                    t.CreateLog($"Creating service attribute for service id:{targetEvent.ServiceId}");
                    ServiceClientWS.ServiceAttributeUpdate(SessionKey, serviceAttribute);
                    t.CreateLog($"Service attribute for {targetActionCode} with Attribute ID: {serviceAttribute.AttributeTemplate.Id} and Attribute value: {AttribValue} created against service id {targetEvent.ServiceId}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sends email to Web Support if an event failure occurs 
        /// </summary>
        public static void SendEmailToWebSupport(string sessionKey, string privateKey, string Note, TraceLog t)
        {
            try
            {
                using (var ConfigurationClientWS = new SelcommWSsvc.SelcommWSAll.ConfigurationClient(GetSelcommWsEndpointName()))
                {
                    string body = $"Provider Code : {privateKey} <br />" +
                         $"Server Environment : {Utility.ServerEnvironment}<br />" +
                         $"{Note}";
                    string subject = "THIS IS JUST FOR TEST ";//$"SelcommOSS - Attention required for TrustPower event processing. Server : {Utility.ServerEnvironment}";
                    string emailTo = ConfigurationClientWS.GetConfigValue(sessionKey, "TPOWER_WEBSUPPORT_EMAIL", null, null, null, null, null, null);
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
                t.CreateLog(getExceptionString(ex).Replace("<br/>", Environment.NewLine).Replace("<br />", Environment.NewLine));
                t.CreateLog("==================The email was supposed to be sent to notify the following issue.===============================");
                t.CreateLog(Note.Replace("<br/>", Environment.NewLine).Replace("<br />", Environment.NewLine));
            }
        }


        /// <summary>
        /// convert standard datetime format to specific openet datetime format i.e. "yyyy-MM-dd'T'HH:mm:ss'Z'".
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateTime(this DateTime date)  //  NOT SURE ABOUT THE FORMAT GIVEN: YYYY-MM-DDTHH:MM:SS.SSSTZ
        {
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        /// <summary>
        /// this method loads the type of customer i.e B = Corporate and P=Consumer
        /// </summary>
        /// <returns></returns>
        public static string LoadCustomerType(string sessionKey, EventDisplay targetEvent)
        {
            using (var ContactClientWS = new SelcommWSsvc.SelcommWSAll.ContactClient(GetSelcommWsEndpointName()))
            {
                var ContactObj = ContactClientWS.Contact(sessionKey, targetEvent.ContactCode, true, false, false, false);
                var customerType = ContactObj?.SubType?.Type;
                switch (customerType)
                {
                    case "P":
                        customerType = "01";
                        break;
                    case "B":
                        customerType = "02";
                        break;
                    default:
                        throw new Exception("Customer Type does not contain value");

                }
                return customerType;

            }
        }

    }
}