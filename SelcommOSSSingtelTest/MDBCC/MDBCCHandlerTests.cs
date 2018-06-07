using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelcommOSSSingtel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelcommOSSSingtel.Tests
{
    [TestClass()]
    public class MDBCCHandlerTests
    {
        [TestMethod()]
        public void ProcessRequest_Normal_Test()
        {
            //var testreq = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <soapenv:Body>\r\n    <ns1:updateMobileActStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://sabesm.pegasus\">\r\n      <userInfo href=\"#id0\"/>\r\n      <mobileActStatusDO href=\"#id1\"/>\r\n    </ns1:updateMobileActStatus>\r\n    <multiRef id=\"id0\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns3:UserProfileInfo\" xmlns:ns3=\"http://sabesm.pegasus\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <userId xsi:type=\"xsd:string\">SOFTACT_UPD</userId>\r\n      <password xsi:type=\"xsd:string\"></password>\r\n      <userRole xsi:type=\"xsd:string\"></userRole>\r\n      <omUserGroup xsi:type=\"xsd:string\"></omUserGroup>\r\n      <function xsi:type=\"xsd:string\"></function>\r\n      <sAcct xsi:type=\"xsd:string\" />\r\n      <accessType xsi:type=\"xsd:string\"></accessType>\r\n      <accessValue xsi:type=\"xsd:string\"></accessValue>\r\n      <firstNm xsi:type=\"xsd:string\"></firstNm>\r\n      <lastNm xsi:type=\"xsd:string\"></lastNm>\r\n    </multiRef>\r\n    <multiRef id=\"id1\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns2:MobileActStatusDO\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns2=\"http://sabesm.pegasus\">\r\n      <workOrdNo xsi:type=\"xsd:string\">232385MVNE</workOrdNo>\r\n      <svcNo xsi:type=\"xsd:string\">99999003</svcNo>\r\n      <sequenceId xsi:type=\"xsd:string\">514709</sequenceId>\r\n      <switchId xsi:type=\"xsd:string\">22</switchId>\r\n      <status xsi:type=\"xsd:string\">FAIL-7-</status>\r\n      <mdComDate xsi:type=\"xsd:dateTime\">2018-04-13T05:11:47.780Z</mdComDate>\r\n    </multiRef>\r\n  </soapenv:Body>\r\n</soapenv:Envelope>";
            var sampleReq = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><soapenv:Body><ns1:updateMobileActStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://sabesm.pegasus\"><userInfo href=\"#id0\"/><mobileActStatusDO href=\"#id1\"/></ns1:updateMobileActStatus><multiRef id=\"id1\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns2:MobileActStatusDO\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns2=\"http://sabesm.pegasus\"><workOrdNo xsi:type=\"xsd:string\">232385MVNE</workOrdNo><svcNo xsi:type=\"xsd:string\">99999003</svcNo><sequenceId xsi:type=\"xsd:string\">514709</sequenceId><switchId xsi:type=\"xsd:string\">22</switchId><status xsi:type=\"xsd:string\">FAIL-7-</status><mdComDate xsi:type=\"xsd:dateTime\">2018-04-17T05:31:07.536Z</mdComDate></multiRef><multiRef id=\"id0\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns3:UserProfileInfo\" xmlns:ns3=\"http://sabesm.pegasus\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\"><userId xsi:type=\"xsd:string\">SOFTACT_UPD</userId><password xsi:type=\"xsd:string\"></password><userRole xsi:type=\"xsd:string\"></userRole><omUserGroup xsi:type=\"xsd:string\"></omUserGroup><function xsi:type=\"xsd:string\"></function><sAcct xsi:type=\"xsd:string\"></sAcct><accessType xsi:type=\"xsd:string\"></accessType><accessValue xsi:type=\"xsd:string\"></accessValue><firstNm xsi:type=\"xsd:string\"></firstNm><lastNm xsi:type=\"xsd:string\"></lastNm></multiRef></soapenv:Body></soapenv:Envelope>";
            var reqSoapAction = "updateMobileActStatus";

            System.Net.HttpStatusCode httpStatusCode;
            var respText = TestProcOpSimple(sampleReq, reqSoapAction, out httpStatusCode);

            CheckSimpleGood(httpStatusCode, respText);

        }

        [TestMethod()]
        public void ProcessRequest_NonMultiRef_Test()
        {
            //xml without multirefs (i.e. inline; like a normal document/literal, rather than rpc/encoded)
            //it's also got whitespace

            var sampleReq = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <soapenv:Body>\r\n    <ns1:updateMobileActStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://sabesm.pegasus\">\r\n      <userInfo xsi:type=\"ns3:UserProfileInfo\" xmlns:ns3=\"http://sabesm.pegasus\">\r\n        <userId xsi:type=\"xsd:string\">SOFTACT_UPD</userId>\r\n        <password xsi:type=\"xsd:string\"></password>\r\n        <userRole xsi:type=\"xsd:string\"></userRole>\r\n        <omUserGroup xsi:type=\"xsd:string\"></omUserGroup>\r\n        <function xsi:type=\"xsd:string\"></function>\r\n        <sAcct xsi:type=\"xsd:string\"></sAcct>\r\n        <accessType xsi:type=\"xsd:string\"></accessType>\r\n        <accessValue xsi:type=\"xsd:string\"></accessValue>\r\n        <firstNm xsi:type=\"xsd:string\"></firstNm>\r\n        <lastNm xsi:type=\"xsd:string\"></lastNm>\r\n      </userInfo>\r\n      <mobileActStatusDO xsi:type=\"ns2:MobileActStatusDO\" xmlns:ns2=\"http://sabesm.pegasus\">\r\n        <workOrdNo xsi:type=\"xsd:string\">232385MVNE</workOrdNo>\r\n        <svcNo xsi:type=\"xsd:string\">99999003</svcNo>\r\n        <sequenceId xsi:type=\"xsd:string\">514709</sequenceId>\r\n        <switchId xsi:type=\"xsd:string\">22</switchId>\r\n        <status xsi:type=\"xsd:string\">FAIL-7-</status>\r\n        <mdComDate xsi:type=\"xsd:dateTime\">2018-04-17T05:31:07.536Z</mdComDate>\r\n      </mobileActStatusDO>\r\n    </ns1:updateMobileActStatus>\r\n  </soapenv:Body>\r\n</soapenv:Envelope>";
            var reqSoapAction = "updateMobileActStatus";

            System.Net.HttpStatusCode httpStatusCode;
            var respText = TestProcOpSimple(sampleReq, reqSoapAction, out httpStatusCode);

            CheckSimpleGood(httpStatusCode, respText);

        }

        private static void CheckSimpleGood(System.Net.HttpStatusCode httpStatusCode, string respText)
        {
            var sampleResp = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><soapenv:Body><ns1:updateMobileActStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://sabesm.pegasus\"><userInfo href=\"#id0\"/><mobileActStatusDO href=\"#id1\"/></ns1:updateMobileActStatus><multiRef id=\"id1\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns2:MobileActStatusDO\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns2=\"http://sabesm.pegasus\"><workOrdNo xsi:type=\"xsd:string\">232385MVNE</workOrdNo><svcNo xsi:type=\"xsd:string\">99999003</svcNo><sequenceId xsi:type=\"xsd:string\">514709</sequenceId><switchId xsi:type=\"xsd:string\">22</switchId><status xsi:type=\"xsd:string\">FAIL-7-</status><mdComDate xsi:type=\"xsd:dateTime\">2018-04-17T05:31:07.536Z</mdComDate></multiRef><multiRef id=\"id0\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns3:UserProfileInfo\" xmlns:ns3=\"http://sabesm.pegasus\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\"><userId xsi:type=\"xsd:string\">SOFTACT_UPD</userId><password xsi:type=\"xsd:string\"></password><userRole xsi:type=\"xsd:string\"></userRole><omUserGroup xsi:type=\"xsd:string\"></omUserGroup><function xsi:type=\"xsd:string\"></function><sAcct xsi:type=\"xsd:string\"></sAcct><accessType xsi:type=\"xsd:string\"></accessType><accessValue xsi:type=\"xsd:string\"></accessValue><firstNm xsi:type=\"xsd:string\"></firstNm><lastNm xsi:type=\"xsd:string\"></lastNm></multiRef></soapenv:Body></soapenv:Envelope>";
            var requiredParts = new[] { "<?xml version=\"1.0\" encoding=\"utf-8\"?>", "<soapenv:Envelope", "<soapenv:Body", "updateMobileActStatusResponse", "xmlns:ns1=\"http://sabesm.pegasus\"", "<updateMobileActStatusResult", "<status xsi:type=\"xsd:boolean\"", "true</status>", "</soapenv:Envelope>" };

            int pos = -1;
            foreach (var itm in requiredParts)
            {
                pos = respText.IndexOf(itm, pos + 1);
                Assert.IsTrue(pos > 0, $"missing required/expected message component or out of order: not found after pos={pos}, searched for:{itm}");
            }

            Assert.AreEqual(System.Net.HttpStatusCode.OK, httpStatusCode, "bad status code");
        }

        [TestMethod()]
        public void ProcessRequest_BadAction_Test()
        {
            var sampleReq = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><soapenv:Body><ns1:updateMobileActStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://sabesm.pegasus\"><userInfo href=\"#id0\"/><mobileActStatusDO href=\"#id1\"/></ns1:updateMobileActStatus><multiRef id=\"id1\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns2:MobileActStatusDO\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns2=\"http://sabesm.pegasus\"><workOrdNo xsi:type=\"xsd:string\">232385MVNE</workOrdNo><svcNo xsi:type=\"xsd:string\">99999003</svcNo><sequenceId xsi:type=\"xsd:string\">514709</sequenceId><switchId xsi:type=\"xsd:string\">22</switchId><status xsi:type=\"xsd:string\">FAIL-7-</status><mdComDate xsi:type=\"xsd:dateTime\">2018-04-17T05:31:07.536Z</mdComDate></multiRef><multiRef id=\"id0\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns3:UserProfileInfo\" xmlns:ns3=\"http://sabesm.pegasus\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\"><userId xsi:type=\"xsd:string\">SOFTACT_UPD</userId><password xsi:type=\"xsd:string\"></password><userRole xsi:type=\"xsd:string\"></userRole><omUserGroup xsi:type=\"xsd:string\"></omUserGroup><function xsi:type=\"xsd:string\"></function><sAcct xsi:type=\"xsd:string\"></sAcct><accessType xsi:type=\"xsd:string\"></accessType><accessValue xsi:type=\"xsd:string\"></accessValue><firstNm xsi:type=\"xsd:string\"></firstNm><lastNm xsi:type=\"xsd:string\"></lastNm></multiRef></soapenv:Body></soapenv:Envelope>";
            var reqSoapAction = "updateMobileActStatuszzzzzzz";


            System.Net.HttpStatusCode httpStatusCode;
            try
            {
                TestProcOpSimple(sampleReq, reqSoapAction, out httpStatusCode);
                Assert.Fail("didn't throw for bad soap action");
            } catch (Exception ex) when (!(ex is AssertFailedException))
            {
                //good
            }
        }

        [TestMethod()]
        public void ProcessRequest_BadXml_Test()
        {
            var badReq = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><soapenv:Body><ns1:updateMobileActStatus soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://sabesmzzzzzzz.pegasus\"><userInfo href=\"#id0\"/><mobileActStatusDO href=\"#id1\"/></ns1:updateMobileActStatus><multiRef id=\"id1\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns2:MobileActStatusDO\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns2=\"http://sabesm.pegasus\"><workOrdNo xsi:type=\"xsd:string\">232385MVNE</workOrdNo><svcNo xsi:type=\"xsd:string\">99999003</svcNo><sequenceId xsi:type=\"xsd:string\">514709</sequenceId><switchId xsi:type=\"xsd:string\">22</switchId><status xsi:type=\"xsd:string\">FAIL-7-</status><mdComDate xsi:type=\"xsd:dateTime\">2018-04-17T05:31:07.536Z</mdComDate></multiRef><multiRef id=\"id0\" soapenc:root=\"0\" soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xsi:type=\"ns3:UserProfileInfo\" xmlns:ns3=\"http://sabesm.pegasus\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\"><userId xsi:type=\"xsd:string\">SOFTACT_UPD</userId><password xsi:type=\"xsd:string\"></password><userRole xsi:type=\"xsd:string\"></userRole><omUserGroup xsi:type=\"xsd:string\"></omUserGroup><function xsi:type=\"xsd:string\"></function><sAcct xsi:type=\"xsd:string\"></sAcct><accessType xsi:type=\"xsd:string\"></accessType><accessValue xsi:type=\"xsd:string\"></accessValue><firstNm xsi:type=\"xsd:string\"></firstNm><lastNm xsi:type=\"xsd:string\"></lastNm></multiRef></soapenv:Body></soapenv:Envelope>";
            var reqSoapAction = "updateMobileActStatus";


            System.Net.HttpStatusCode httpStatusCode;
            try
            {
                TestProcOpSimple(badReq, reqSoapAction, out httpStatusCode);
                Assert.Fail("didn't throw for bad soap action");
            }
            catch (Exception ex) when (!(ex is AssertFailedException))
            {
                //good
            }
        }


        private static string TestProcOpSimple(string sampleReq, string reqSoapAction, out System.Net.HttpStatusCode httpStatusCode)
        {
            var reqContentEncoding = System.Text.Encoding.UTF8;
            var reqStream = new System.IO.MemoryStream();

            var respContentEncoding = System.Text.Encoding.UTF8;
            var respMemStream = new System.IO.MemoryStream();
            var respTextWriter = new System.IO.StreamWriter(respMemStream, respContentEncoding);
            respTextWriter.AutoFlush = true;

            var reqWriter = new System.IO.StreamWriter(reqStream, reqContentEncoding);
            reqWriter.AutoFlush = true;

            //put in a request; then go back to the start so it can be read
            reqWriter.Write(sampleReq);
            reqStream.Seek(0, System.IO.SeekOrigin.Begin);
            string httpStatusDescription;

            var handler = new MDBCCHandler();
            handler.ProcessOperation(reqStream, reqContentEncoding, reqSoapAction, respTextWriter, respContentEncoding, out httpStatusCode, out httpStatusDescription);

            respMemStream.Seek(0, System.IO.SeekOrigin.Begin);
            return respContentEncoding.GetString(respMemStream.ToArray());
        }
    }
}