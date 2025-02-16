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
                    returnValue = HttpContext.Current.Request.Url.AbsoluteUri;
                    //(루트경로)https://localhost:44399/
                    break;

                case "path":
                    returnValue = HttpContext.Current.Request.Url.AbsolutePath;
                    
                    break;
            }

            return returnValue;
        }

        public List<UrlParameter> UrlParameters
        {
            get {
                
                var returnValue = new List<UrlParameter>();

                string url = this.GetUrl("full");
                //https://localhost:44399/?searchKind=Title&keyword=%EC%B6%9C%ED%8C%90
                string[] paramArr = null;

                string[] urlArr = url.Split('?');
                if (urlArr.Count() > 1)
                {
                    paramArr = urlArr[1].Split('&');
                    //paramArr[0] = searchKind=Title
                    //paramArr[1] = keyword=%EC%B6%9C%ED%8C%90

                    foreach (var item in paramArr)
                    {
                        var urlParam = new UrlParameter();
                        {
                            urlParam.Key = item.Split('=')[0];
                            urlParam.Value = item.Split('=')[1];
                        };

                        returnValue.Add(urlParam);
                    }
                }
                return returnValue;
            }
        }


        public string AddUrlParameter(string paramKey, string paramValue)
        {
            string returnValue = string.Empty;

            List<UrlParameter> urlParams = this.UrlParameters;

            UrlParameter urlParameter = urlParams.Where(x => x.Key == paramKey).SingleOrDefault();

            if (urlParameter != null) 
                urlParams.Remove(urlParameter);

            urlParams.Add(new UrlParameter()
            {
                Key = paramKey,
                Value = paramValue
            });

            for(var i=0; i<urlParams.Count(); i++)
            {
                returnValue += (i == 0) ? "?" : "&";
                returnValue += urlParams[i].Key + "=" + urlParams[i].Value;
            }

            return returnValue;
        }
    }
}
