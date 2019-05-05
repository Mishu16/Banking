using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingWebsite.Utilities
{
    public class PinGen
    {
        public static int RandomPinGen()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}