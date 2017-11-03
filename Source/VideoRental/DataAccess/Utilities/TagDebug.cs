using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class TagDebug
    {
        
        public static void D(Type className,string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string TAG = className.Name;
            Debug.WriteLine(timestamp + " " + TAG + ":   " + message);
        }
    }
}
