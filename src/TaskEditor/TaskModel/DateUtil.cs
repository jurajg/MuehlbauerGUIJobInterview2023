using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    public class DateUtil
    {
        /// <summary>
        /// Takes in local zone DateTime and returns unix time in seconds string. Guarantees that time is
        /// stored corectly regardless of local time zone.
        /// </summary>
        /// <param name="dt">local zone DateTime</param>
        /// <returns>unix time in seconds string</returns>
        public static string DateToString(DateTime dt)
        {
            // source: https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
            long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();
            return unixTime.ToString();
        }

        /// <summary>
        /// Takes in string with a unix time number and returns DateTime in local time zone.
        /// </summary>
        /// <param name="s">unix time number in seconds</param>
        /// <returns>DateTime in local time zone</returns>
        public static DateTime StringToLocalDate(string s)
        {
            // source: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
            long seconds = long.Parse(s);
            DateTime timestamp = DateTime.UnixEpoch.AddSeconds(seconds);
            return timestamp.ToLocalTime();
        }
    }
}
