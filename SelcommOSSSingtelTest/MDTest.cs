using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelcommWSsvc.SelcommWSAll;
using SelcommWebServices.SelcommOSS.Singtel.Helpers;

namespace SelcommOSSSingtelTest
{
    [TestClass]
    public class MDTest
    {

        public long GlobalServiceID { get; set; }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void pNewTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");
                GlobalServiceID = newId; //160505
                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);
                
                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.pNewActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.pNewActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);


               // pDataTest();
               // pGprsTest();
               // ceaseTest();
               // chgFacTest();
               // TD1Test();
               // reconn1Test();
               //// chgInstTest();
               // chgPstnTest();
               // cDataTest();
               // cGprsTest();
               // lnPdbTest();
               // TDTest();
                Assert.AreEqual(response.Length,195,"length of Command string for pnew are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void pDataTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");
              
                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId});
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.pDataActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.pDataActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

                Assert.AreEqual(response.Length, 82, "length of Command string for pdata are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void pGprsTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.pGprsActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.pGprsActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

                  Assert.AreEqual(response.Length,168, "length of Command string for pGprs are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void ceaseTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.ceaseActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.ceaseActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

                Assert.AreEqual(response.Length,107, "length of Command string for cease are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void chgFacTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.chgFacActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.chgFacActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

                Assert.AreEqual(response.Length,143, "length of Command string for chgfac are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void TD1Test()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");


                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.TD1ActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.TD1ActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

                Assert.AreEqual(response.Length,76, "length of Command string for TD1 are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void reconn1Test()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.reconn1ActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.reconn1ActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

               Assert.AreEqual(response.Length,103, "length of Command string for reconn1 are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void chgInstTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.chgInstActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.chgInstActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);
                Assert.AreEqual(response.Length, 203, "length of command string for ChgInst is correct");

            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void chgPstnTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.chgPstnActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.chgPstnActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

               Assert.AreEqual(response.Length,183, "length of command string for ChgPstn is correct");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void cDataTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.cDataActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.cDataActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);
                Assert.AreEqual(response.Length, 80, "length of Command string for cData are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void cGprsTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");
              
                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.cGprsActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.cGprsActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);
                Assert.AreEqual(response.Length, 90, "length of Command string for cGprs are equal");

            //    Assert.AreEqual(response.Length,163, "length of Command string for cGprs are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void lnPdbTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.LnPdbInsertActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.LnPdbInsertActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

              Assert.AreEqual(response.Length,98, "length of Command string for lnPdb are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.Database)]
        public void TDTest()
        {
            var newtraceLog = new TraceLog("SingtelTest", "Zero1", null);
            using (var eventClient = new EventClient(Helpers.GetSelcommWsEndpointName()))
            {
                ServiceClient serviceclient = new ServiceClient(Helpers.GetSelcommWsEndpointName());
                ContactClient contactClient = new ContactClient(Helpers.GetSelcommWsEndpointName());

                //Create the session.
                var authService = new SelcommWSsvc.SelcommWSAll.AuthenticationClient(Helpers.GetSelcommWsEndpointName());
                var sessionKey = authService.AuthenticateSimpleCreateSessionAndAuthenticateContact("2041591443", "webuser", "resubew", "40000287", "1234");

                //Adding a new service to an existing account
                var pack = new SelcommWSsvc.SelcommWSAll.PackagesClient(Helpers.GetSelcommWsEndpointName()) { };
                var packageList = pack.PackageDisplayListCurrent(sessionKey, false);
                var newId = serviceclient.ServiceAddNewSimple(sessionKey, "40000287", DateTime.Now, packageList[0].Code, 1, 1, "MRBR", "0298" + (new System.Random()).Next(0, 1000000).ToString("000000"), "1234");

                //creating new event associated with service Id
                Event NewEvent = new Event
                {
                    EventType = new EventType
                    {
                        EventTypeMember = "CM",
                        EventCode = "IC",
                    },
                    Schedule = new EventSchedule
                    {
                        ToLogin = "system",
                        ToDepartment = new Department { Code = "SYS" },
                        EventScheduleType = new EventScheduleType { Code = "ACT" },
                        EventScheduleStatus = new EventScheduleStatus { Code = "O" }
                    },
                    Note = string.Format("Event opened for sp_cn_ref {0}", newId)
                };


                var testevent = eventClient.EventAddForService(sessionKey, NewEvent, new Service { ServiceId = newId });
                var eventDisplay = eventClient.EventDisplay(sessionKey, testevent, true);
                newtraceLog.CreateLog($"Global ServiceId: " + newId);
                newtraceLog.CreateLog($"Event Display: " + eventDisplay);

                SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.TDActionHandler a = new SelcommWebServices.SelcommOSS.Singtel.MD.Processes.ActionHandler.TDActionHandler("2041591443");
                var response = a.ProcessEvent(eventDisplay, newtraceLog);
                newtraceLog.CreateLog($"Command string: " + response);

               Assert.AreEqual(response.Length,76 , "length of Command string for TD are equal");
            }
        }

        [TestMethod()]
        [TestCategory(TestCategories.HasSideEffects), TestCategory(TestCategories.NoDatabase)]
        public void SwitchIDTest()
        {
           string resp= SelcommWebServices.SelcommOSS.Singtel.MD.MD.GetSwitchId("98989859");
           
        }
    }
}
