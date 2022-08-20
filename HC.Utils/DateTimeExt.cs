using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Utils
{
    public static class DateTimeExt
    {
        public static string ToSQLDateFormat(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
