using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerTicket.API.Helper
{
    public static class Utilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="someval"></param>
        /// <returns></returns>
        public static bool IsValidDatetime(string dateTime)        {            bool valid = false;            DateTime dateTimeOut;                       if (DateTime.TryParse(dateTime, out dateTimeOut))            {                                   valid = true;                           }            return valid;        }
    }
}