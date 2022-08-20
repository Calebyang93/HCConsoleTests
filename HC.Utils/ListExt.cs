using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Utils
{
   public static class ListExt
    {
        public static string ToCsv(this List<string> sourceIList, string delimiterStr, char? quoteChar) 
        {
            int limit = 200000;
            Boolean truncated = false;

            if (sourceIList == null)
                return "";
            else if (sourceIList.Count == 1 && String.IsNullOrEmpty(sourceIList[0].ToString()))
                return "";

            if (string.IsNullOrEmpty(delimiterStr))
                delimiterStr = ",";

            string s = string.Empty;
            string itemStr = "";
            foreach (var item in sourceIList)
            {
                if (item != null)
                {
                    itemStr = item.ToString();
                    // Quote any strings
                    if (quoteChar.HasValue && (item.GetType() == typeof(String)))
                        itemStr = quoteChar + itemStr + quoteChar;
                }
                else
                {
                    // If quoteChar is present, add an empty, quoted, string
                    //  - this might not be always suitable for, say, a nullable non-string that has a value of null but which should not be quoted
                    if (quoteChar.HasValue)
                        itemStr = quoteChar.ToString() + quoteChar.ToString();
                    else
                        itemStr = string.Empty;
                }

                // Problem if string is exceptionally long - discard rest!
                if (!truncated)
                    if (s.Length > limit)
                    {
                        s = s.Substring(0, limit);
                        s += "[NB: truncated at " + limit.ToString() + " chars]";
                        truncated = true;
                    }
                    else
                        s += delimiterStr + itemStr;
            }

            return s.Length > delimiterStr.Length ? s.Substring(delimiterStr.Length) : s;
        }
    }
}
