using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15
{
  public static class Constants
  {
    // URL of REST service
#if DEBUG
    public static string RestUrl = "http://caprex.ddns.net:5509/api/";
#else
    public static string RestUrl = "https://www.timedoc.ch/RestService/api/";
#endif
  }
}
