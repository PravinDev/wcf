using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Win32;
using System.IO;
using System.Xml.Linq;
using System.Collections;
using System.Text.RegularExpressions;

namespace SelcommWebServices.SelcommOSS.Singtel.Helpers
{
    public abstract class Log:IDisposable
    {
        private const string BASE_URL = @"C:\Logs\";

        #region "Properties"

        public Guid LogID { get; set; }
        public string BaseURL
        {
            get
            {
                return BASE_URL;
            }
        }
        public string Environment
        {
            get
            {
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\Selcomm", false);
                return (regKey == null ? "DEV" : regKey.GetValue("Environment", "DEV").ToString()) + "\\";
            }
        }
        public DateTime DateTimeStamp { get; set; }
        public bool IsEnabled { get; set; }
        public abstract string ProjectName { get; set; }
        public abstract string ClientName { get; set; }
        public abstract string UniqueFileName { get; set; }
        public abstract string FullDirectoryPath { get; protected set; }

        protected Log(string ProjectName, string ClientName, string UniqueFileName = null)
        {
            LogID = Guid.NewGuid();
            DateTimeStamp = DateTime.Now;
            this.ProjectName = ProjectName + "\\";
            this.ClientName = ClientName + "\\";
            this.UniqueFileName = this.GetType().Name + "-" + (UniqueFileName ?? "History") + ".txt";

            string year = this.DateTimeStamp.ToString("yyyy"), month = this.DateTimeStamp.ToString("MM"), day = this.DateTimeStamp.ToString("dd");

            FullDirectoryPath = BaseURL + Environment + this.ProjectName + this.ClientName + this.GetType().Name + "\\" + year + month + "\\" + day;
        }

        public void CreateLog(string logMessage)
        {
            if (IsEnabled)
            {
                if (!Directory.Exists(FullDirectoryPath))
                    Directory.CreateDirectory(FullDirectoryPath);

                var filePath = FullDirectoryPath + "\\" + UniqueFileName;

                if (File.Exists(filePath))
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(string.IsNullOrWhiteSpace(logMessage) ? string.Empty : string.Format("{0} : {1}", DateTime.Now, logMessage));
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine(string.IsNullOrWhiteSpace(logMessage) ? string.Empty : string.Format("{0} : {1}", DateTime.Now, logMessage));
                    }
                }
            }
        }

        public void Dispose()
        {
            
        }

        #endregion
    }

    public class TraceLog : Log
    {

        public override string ProjectName { get; set; }
        public override string ClientName { get; set; }
        public override string UniqueFileName { get; set; }
        public override string FullDirectoryPath { get; protected set; }
        public string TraceMessage { get; set; }

        public TraceLog(string ProjectName, string ClientName, string UniqueFileName = null)
            : base(ProjectName, ClientName, UniqueFileName)
        {

        }
    }

    public class ErrorLog : Log
    {
        public override string ProjectName { get; set; }
        public override string ClientName { get; set; }
        public override string UniqueFileName { get; set; }
        public override string FullDirectoryPath { get; protected set; }

        public bool IsHtmlEnabled { get; set; }
        public Exception Exception { get; set; }

        public ErrorLog(string ProjectName, string ClientName, string UniqueFileName = null)
            : base(ProjectName, ClientName, UniqueFileName)
        {

        }

        public void CreateHtml(string content)
        {
            if (!Directory.Exists(FullDirectoryPath))
                Directory.CreateDirectory(FullDirectoryPath);

            var filePath = FullDirectoryPath + "\\" + this.LogID + ".htm";

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(content);
            }
        }
    }

    /// <summary>Represent an Exception as XML data.</summary>
    public class ExceptionXElement : XElement
    {
        /// <summary>Create an instance of ExceptionXElement.</summary>
        /// <param name="exception">The Exception to serialize.</param>
        public ExceptionXElement(Exception exception)
            : this(exception, false)
        { }
 
        /// <summary>Create an instance of ExceptionXElement.</summary>
        /// <param name="exception">The Exception to serialize.</param>
        /// <param name="omitStackTrace">
        /// Whether or not to serialize the Exception.StackTrace member
        /// if it's not null.
        /// </param>
        public ExceptionXElement(Exception exception, bool omitStackTrace)
            : base(new Func<XElement>(() =>
            {
                // Validate arguments
 
                if (exception == null)
                {
                    throw new ArgumentNullException("exception");
                }
 
                // The root element is the Exception's type
 
                XElement root = new XElement
                    (exception.GetType().ToString());
 
                if (exception.Message != null)
                {
                    root.Add(new XElement("Message", exception.Message));
                }
 
                // StackTrace can be null, e.g.:
                // new ExceptionAsXml(new Exception())
 
                if (!omitStackTrace && exception.StackTrace != null)
                {
                    root.Add
                    (
                        new XElement("StackTrace",
                            from frame in Regex.Replace(exception.StackTrace, "[\r\t]", string.Empty).Split('\n')
                            where !string.IsNullOrWhiteSpace(frame)
                            let prettierFrame = frame.Trim()
                            select new XElement("Frame", prettierFrame))
                    );
                }
 
                // Data is never null; it's empty if there is no data
 
                if (exception.Data.Count > 0)
                {
                    root.Add
                    (
                        new XElement("Data",
                            from entry in
                                exception.Data.Cast<DictionaryEntry>()
                            let key = entry.Key.ToString()
                            let value = (entry.Value == null) ?
                                "null" : entry.Value.ToString()
                            select new XElement(key, value))
                    );
                }
 
                // Add the InnerException if it exists
                if (exception.InnerException != null)
                {
                    root.Add
                    (
                        new ExceptionXElement
                            (exception.InnerException, omitStackTrace)
                    );
                }
 
                return root;
            })())
        { }
    }

}