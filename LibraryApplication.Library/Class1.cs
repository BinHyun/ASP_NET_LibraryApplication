using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryApplication.Library
{
    public class CommonLibrary
    {
        public string GetUrl(string paramType)
        {
            string returnValue = string.Empty;

            switch (paramType)
            {
                case "full":
                    returnValue = HttpContext.Current.Request.Url.AbsolutePath;
                    break;
            }

            return returnValue;
        }


        public string AddUrlParameter(string paramKey, string paramValue)
        {
            string returnValue = string.Empty;

            return returnValue;
        }
    }
}
