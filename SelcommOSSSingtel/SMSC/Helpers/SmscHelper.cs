using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelcommWebServices.SelcommOSS.Singtel.SMSC.Helpers
{
    public class SmscHelper
    {
       /// <summary>
       /// returns first two digit of service number. this is for SMSC, to check if the number starts from 65.
       /// </summary>
       /// <param name="ServiceNumber"></param>
       /// <returns></returns>
       static public int GetFirstTwoDigitOfServiceNumber(string ServiceNumber)
        {
            string FirstTwoDigitOfServiceNumber = ServiceNumber.Substring(0, 2);

            return Convert.ToInt16(FirstTwoDigitOfServiceNumber.Trim());
        }
    }
}