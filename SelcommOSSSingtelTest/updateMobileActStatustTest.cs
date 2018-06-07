using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelcommWebServices.SelcommOSS.Singtel.MD.Definition;
using SelcommWebServices.SelcommOSS.Singtel.MD;

namespace SelcommWebServices.SelcommOSS.Singtel.MD.Tests
{
    [TestClass()]
    public class updateMobileActStatustTest
    {
        [TestMethod()]
        public void updateMobileActStatusTest()
        {
            var m = new MobileActStatusDO();

            m.mdComDate = DateTime.Now;
            m.sequenceId = "12345";
            m.status = "test";
            m.svcNo = "98989855"; // "82365755";
            m.switchId = "123";
            m.workOrdNo = "SING100045";

            var u = new UserProfileInfo();

            u.accessType = "testAccessType";
            u.accessValue = "testAccessValue";
            u.firstNm = "testFirstName";
            u.function = "testFunction";
            u.lastNm = "testLastName";
            u.omUserGroup = "testUserGroup";
            u.password = "password";
            u.sAcct = "qqq";
            u.userId = "testUserId";
            u.userRole = "testUserRole";


            var MdCallBack = new MDBCCNotification();
            var response = new StatusDo();

            response = MdCallBack.updateMobileActStatus(u, m);
            Assert.AreEqual(response.status, true, "Response status is excepted to be true");
        }
    }
}

