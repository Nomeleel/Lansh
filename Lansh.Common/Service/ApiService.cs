using Lansh.Common.Constant;
using Lansh.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Service
{
    class ApiService
    {

        /// <summary>
        /// Sign Api
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Sign(string url)
        {
            string subUrl = url.Substring(url.IndexOf("?", 4) + 1);
            List<string> parameterList = subUrl.Split('&').ToList();
            parameterList.Sort();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str in parameterList)
                stringBuilder.Append(str + "&");
            stringBuilder.Replace("&", Api.AppSecretWp, stringBuilder.Length -1, 1);
            return MD5Helper.GetMd5String(stringBuilder.ToString()).ToLower();
        }

        /// <summary>
        /// Get sgin url, return Uri
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Uri GetSignUri(string url)
        {
            string uri = url + "&sign=" + Sign(url);
            return new Uri(uri);
        }

    }
}
