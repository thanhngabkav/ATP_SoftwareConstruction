using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class TagDebug
    {
        
        private static string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                             CultureInfo.InvariantCulture);
        public static void D(Type className,string message)
        {
            string TAG = className.GetType().Name;
            Console.WriteLine(TAG + "  " + timestamp + ":   " + message);
        }
    }
}
