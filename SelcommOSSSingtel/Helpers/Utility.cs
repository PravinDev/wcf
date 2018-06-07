using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Xsl;

namespace SelcommWebServices.SelcommOSS.Singtel.Helpers
{
    public class Utility
    {
        public static string GenerateErrorHTML(Exception ex)
        {
            // TODO: need to fix the layout of xslt file.
            //using the ExceptionXElement class
            var xmlException = new ExceptionXElement(ex);
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            // loading xslt file
            myXslTrans.Load(XmlReader.Create(HostingEnvironment.MapPath("~/Helpers/ExceptionFormatter.xslt")));


            //initialize a TextWriter, in this case a StringWriter and set it to write to a StringBuilder
            StringBuilder stringBuilder = new StringBuilder();
            XmlTextWriter myWriter = new XmlTextWriter(new StringWriter(stringBuilder));

            //apply the XSL transformations to the xmlException and output them to the XmlWriter
            myXslTrans.Transform(xmlException.CreateReader(), null, myWriter);

            return stringBuilder.ToString();
        }
        /// <summary>
        /// This method converts an object to jason string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToJSON(object obj)
        {
            try
            {
                return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public static string ServerEnvironment
        {
            get
            {
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\Selcomm", false);
                return (regKey == null ? "DEV" : regKey.GetValue("Environment", "DEV").ToString());
            }
        }
    }
}