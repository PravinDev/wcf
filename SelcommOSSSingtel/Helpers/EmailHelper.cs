using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace SelcommWebServices.SelcommOSS.Singtel.Helpers
{
    public static class EmailHelper
    {

        /// <summary>
        /// This method is can be used to send email if need to include attachments, alternativeViews and linkedResources
        /// </summary>
        /// <param name="mailMessage">This object will hold all mail data including attachments, alternativeViews and linkedResources</param>
        public static void SendEmail(MailMessage mailMessage)
        {
            try
            {
                Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Standard way of sending email along with the name of sender and receiver
        /// </summary>
        /// <param name="from">this will include email address and sender's name</param>
        /// <param name="to">this will include email address and receiver's name</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtmlEnabled"></param>
        public static void SendEmail(MailAddress from, MailAddress to, string subject, string body, bool isHtmlEnabled = false)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtmlEnabled;
                Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Standard method to send email
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtmlEnabled"></param>
        public static void SendEmail(string from, string to, string subject, string body, bool isHtmlEnabled = false)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(from, to, subject, body);
                mailMessage.IsBodyHtml = isHtmlEnabled;
                Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Send email by supplying body template URL from resource
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="bodyTemplateUrl">URL of external file stored in external resource</param>
        public static void SendEmail(MailAddress from, MailAddress to, string subject, string bodyTemplateUrl)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.AlternateViews.Add(new AlternateView(bodyTemplateUrl));
                mailMessage.Subject = subject;
                Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private static void Send(MailMessage mailMessage)
        {
            using (var smtpClient = new SmtpClient("mailhost"))
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
        }
    }
}