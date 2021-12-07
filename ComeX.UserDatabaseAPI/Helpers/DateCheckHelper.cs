using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Helpers
{
    public class DateCheckHelper
    {
        public DateCheckHelper()
        {
        }

        public bool IsNotExpired(string date)
        {
            return DateTime.Compare(DateTime.Parse(date), DateTime.UtcNow) > 0;
        }
    }
}
