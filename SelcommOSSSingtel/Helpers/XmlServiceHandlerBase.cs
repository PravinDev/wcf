using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Xml;


namespace SelcommWebServices.Helpers
{
    public abstract class XmlServiceHandlerBase : IHttpHandler
    {

        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        public virtual bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }


        public void ProcessRequest(HttpContext context)
        {
            var reqStream = context.Request.InputStream;
            var reqContentEncoding = context.Request.ContentEncoding;


            var respContentEncoding = context.Response.ContentEncoding;
            //instead of directly using the req/response, we're going via an intermediate stream so we can log everything. it needs to be a byte-backed; not stringbuilder-backed, because a stringbuilder makes all the xml go into utf-16 (because .net strings are utf-16), even when we specify utf-8
            //(logging the req is internal; since it does the stream->reader->string->xmldoc conversion anyway; so it's logged in there.
            var respMemStream = new System.IO.MemoryStream();
            var respTextWriter = new System.IO.StreamWriter(respMemStream, respContentEncoding);
            respTextWriter.AutoFlush = true;//we use the underlying stream; so it needs to be written (it's only a memorystream, and it's written all at once; so not a big deal)


            context.Response.ContentType = $"text/xml; charset={respContentEncoding.WebName}";
            context.Response.ClearContent();

            var reqSoapAction = context.Request.Headers["SOAPAction"]; //NameValueCollection returns null for non-existing keys


            System.Net.HttpStatusCode httpStatusCode;
            string httpStatusDescription;
            try
            {
                ProcessOperation(reqStream, reqContentEncoding, reqSoapAction, respTextWriter, respContentEncoding, out httpStatusCode, out httpStatusDescription);
            }
            catch (Exception ex)
            {
                LogReq(ex.ToString());
                httpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpStatusDescription = "Internal Server error";
            }


            var respString = respContentEncoding.GetString(respMemStream.ToArray());
            LogReq(respString);

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.StatusDescription = httpStatusDescription;

            context.Response.Output.Write(respString);

            context.Response.Flush();
            context.Response.End();//don't force close the socket yet; just flush, stop, and endreq (socket still connected, for gzip; and for later messages in same connection, if persistent)



        }

        /// <summary>
        /// Processes an incoming soap message for the specified action, from the given <paramref name="reqStream"/> in the specified <paramref name="reqContentEncoding"/>;
        /// returns the appropriate http status (e.g. 200 for ok; or errors with 400(bad req)/500(internal) for error, etc)
        /// performs the processing for that operation, builds the xml response, and writes it to the specified <paramref name="respTextWriter"/> in the specified <paramref name="respContentEncoding"/>.
        /// The <paramref name="respContentEncoding"/> is also used for the xml document itself, including the xml declaration.
        /// The streams are assumed to be declared as their appropriate encodings, outside the scope of this method (i.e. caller should match it from http headers, etc).
        /// This may throw exceptions; caller should handle those by http status 500. Later versions may handle them and send soap faults in the response. If anything is written to the response before throwing, the response should still be sent to the client (i.e. it will write the fault xml, and then throw; and caller should put http status from the return value)
        /// </summary>
        public abstract void ProcessOperation(Stream reqStream, System.Text.Encoding reqContentEncoding, string reqSoapAction, TextWriter respTextWriter, System.Text.Encoding respContentEncoding, out System.Net.HttpStatusCode httpStatusCode, out string httpStatusDescription);




        /// <summary>
        /// populates the properties of each parameter's value. only works if the parameters are objects which have properties which are just a flat list of strings.
        /// puts the results into the list (i.e. mutates the input)
        /// (i.e. puts the <see cref="XPathItem.Value"/> of all the child Elements of the <see cref="ParamElement.FoundNode"/> into the <see cref="ParamElement.ParsedParameterProperties"/>)
        /// This should be used if each parameter is an object, and that object has primitive/string properties. If the parameters themselves are primitive/strings, instead use <see cref="ExtractParamNodesToDictionary(List{ParamElement})"/> 
        /// </summary>
        protected virtual void ExtractParamNodeElementsToDictionary(List<ParamElement> ParamElems)
        {
            foreach (var par in ParamElems)
            {
                if (par.FoundNode == null) continue;
                var vals = new Dictionary<string, string>();
                var nav = par.FoundNode.Clone();

                if (!nav.MoveToFirstChild()) throw new ArgumentException($"Unable to inspect properties for message parameter {par.ElementName}: {par.ToString()}");
                do
                {
                    vals.Add(nav.Name, nav.Value);
                }
                while (nav.MoveToNext());
                par.ParsedParameterProperties = vals;
            }
        }


        /// <summary>
        /// populates a dictionary from the parameters - i.e. this should be used if each parameter is itself just a string.
        /// returns the results; without mutating the input.
        /// if each parameter is an object, with multiple properties, instead use <see cref="ExtractParamNodeElementsToDictionary(List{ParamElement})"/> 
        /// -i.e. this returns one dictionary total, for all params. the other one returns many dictionaries, one for each param.
        /// this method extracts the values directly, as strings. alternatively, to get the nodes, consider ParamElems.ToDictionary(x => x.ElementName);
        /// </summary>
        protected virtual Dictionary<string, string> ExtractParamNodesToDictionary(List<ParamElement> ParamElems)
        {
            var vals = new Dictionary<string, string>();
            foreach (var par in ParamElems)
            {
                if (par.FoundNode != null) vals.Add(par.ElementName, par.FoundNode.Value);
            }
            return vals;
        }


        /// <summary>
        /// find the xml nodes for each of the parameters in the child of the body of the envelope of the request; and dereference them if they are multiRefs
        /// </summary>
        protected virtual void ParseXmlForParameters(System.Xml.XmlDocument reqXml, string MsgElemName, string MsgElemNS, IEnumerable<ParamElement> ParamElems)
        {
            XPathNavigator body = GetSoapBody(reqXml);
            var msg = body.Clone();
            if (!msg.MoveToChild(MsgElemName, MsgElemNS)) throw new ArgumentException($@"Couldn't find message element ""{MsgElemName}"" of namespace ""{MsgElemNS}"" in request");

            foreach (var paramElem in ParamElems)
            {
                var elem = msg.Clone();
                var found = elem.MoveToChild(paramElem.ElementName, paramElem.ElementNS);
                if (!found && paramElem.Mandatory) throw new ArgumentException($@"Couldn't find element ""{paramElem.ElementName}"" of namespace ""{paramElem.ElementNS}"" in request");
                if (found) paramElem.FoundNode = elem;
            }

            //so we've found all the nodes; but they still just point to the ones in the message.
            //now check those params to see if any have a href, and if the do, then find the id in the body. (in SOAP 1.1, all multirefs are direct children of the body (in an unspecified order); as siblings of the message)

            SwapMultiRefs(ParamElems, body);
        }
        
        /// <summary>
        /// gets the body in the envelope (i.e. the 1st child of this node is probably the message)
        /// </summary>
        public static XPathNavigator GetSoapBody(XmlDocument reqXml)
        {
            var nav = reqXml.CreateNavigator();
            if (!nav.MoveToChild("Envelope", @"http://schemas.xmlsoap.org/soap/envelope/")) throw new ArgumentException("Couldn't find envelope of xmlsoap ns in request");
            if (!nav.MoveToChild("Body", @"http://schemas.xmlsoap.org/soap/envelope/")) throw new ArgumentException("Couldn't find body of xmlsoap ns in request");
            var body = nav.Clone();
            return body;
        }


        /// <summary>
        /// for any params which have a reference, change them to instead point to an XPathNavigator on the dereferenced node (i.e. the one with the actual contents), instead of the original node
        /// (e.g. for <foo href="#id1"> swap it to the <multiref id="id1">; or if it's not href to something, just leave it as-is)
        /// </summary>
        protected virtual void SwapMultiRefs(IEnumerable<ParamElement> ParamElems, XPathNavigator body)
        {
            var refs = new Dictionary<string, XPathNavigator>();
            foreach (var itm in body.SelectChildren("multiRef", ""))
            {
                var elem = itm as XPathNavigator;
                if (elem == null) throw new InvalidCastException($"Unable to navigate for multiref. Type:{itm?.GetType()}; ToString:{itm?.ToString()}");
                var id = elem.GetAttribute("id", "");
                if (id == null) throw new ArgumentException($"Found multiref with no id attribute; Cross-referencing failed.");
                refs.Add(id, elem);
            }

            foreach (var param in ParamElems)
            {
                if (param.FoundNode == null) continue;
                var href = param.FoundNode.GetAttribute("href", "");
                if (href?.StartsWith("#") == true)
                {
                    //it's a reference to a fragment identifier - swap it with another one from a multiRef.
                    XPathNavigator xref;
                    if (!refs.TryGetValue(href.Substring(1), out xref)) throw new ArgumentException($@"Found href=""{href}"" with no matching <multiRef> element in body");
                    param.FoundNode = xref;
                }
                else
                {
                    //(if not starting with # or if no href attribute at all (GetAttribute returns empty string), then this element itself must be the one we're looking for), and no lookup needed.
                    //just keep it as-is
                }
            }
        }



        /// <summary>reads the stream to a string in the specified encoding, logs it, and loads it into an XmlDocument</summary>
        public virtual System.Xml.XmlDocument ParseRequestToXml(Stream reqStream, System.Text.Encoding reqContentEncoding)
        {
            var reqXml = new System.Xml.XmlDocument();
            using (var httpBodyReader = new System.IO.StreamReader(reqStream, reqContentEncoding, true))
            {
                var reqBody = httpBodyReader.ReadToEnd();

                LogReq(reqBody);
                reqXml.LoadXml(reqBody);
            }

            return reqXml;
        }



        public class ParamElement
        {
            public string ElementName { get; set; }
            public string ElementNS { get; set; }
            /// <summary>once the node is found in the document, this is mutated to hold a navigator to the node which has this parameter; or null if not found (and not <see cref="Mandatory"/>)</summary>
            public XPathNavigator FoundNode { get; set; } = null;
            /// <summary>from the node, the direct child elements are collected into this dictionary for easy access.
            /// This is only used if each parameter is an object, with multiple properties: this dictionary is the properties of this one parameter.
            /// If the parameters themselves are strings, this is not used</summary>
            public Dictionary<string, string> ParsedParameterProperties { get; set; } = null;

            /// <summary>if the parser cannot find this parameter, this controls whether or not that should be considered an error.</summary>
            public bool Mandatory { get; set; } = true;

            public int Usages { get; set; } = 0;
            public ParamElement(string elementName, string elementNS, bool mandatory = true)
            {
                this.ElementName = elementName;
                this.ElementNS = elementNS;
                this.Mandatory = mandatory;
            }
        }



        protected virtual string LogFolderBaseName { get; } = "XmlServiceHandlerBase";

        /// <summary>to guarentee uniqueness in logging filename (use interlocked.increment)</summary>
        static volatile int LogCtr = 0;
        public virtual void LogReq(string message)
        {
            //TODO: move this to the rest of the logging stuff
            var filename = $@"C:\Logs\{LogFolderBaseName}\Log{DateTime.Now:yyyy-MM-dd HHmmss.ffffff}-{System.Threading.Interlocked.Increment(ref LogCtr):00000000}.txt";
            var file = new System.IO.FileInfo(filename);
            if (!file.Directory.Exists) file.Directory.Create();
            System.IO.File.AppendAllText(filename, message);
        }













    }
}