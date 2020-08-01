using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Lansh.Common.Helper
{
    public class WebClientHelper
    {
        /// <summary>
        /// By uri get request 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetResultAsync(Uri uri)
        {
            HttpBaseProtocolFilter fiter = new HttpBaseProtocolFilter();
            fiter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Expired);
            using (HttpClient hc = new HttpClient(fiter))
            {
                HttpResponseMessage hrm = await hc.GetAsync(uri);
                hrm.EnsureSuccessStatusCode();
                string results = await hrm.Content.ReadAsStringAsync();
                return results;
            }
        }

        /// <summary>
        /// By uri post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameterData">from data </param>
        /// <returns></returns>
        public async Task<string> PostResultAsync(Uri uri, IEnumerable<KeyValuePair<string, string>> parameterData)
        {
            using (HttpClient hc = new HttpClient())
            {
                IHttpContent httpContent = new HttpFormUrlEncodedContent(parameterData);
                HttpResponseMessage hrm = await hc.PostAsync(uri, httpContent);
                hrm.EnsureSuccessStatusCode();
                string results = await hrm.Content.ReadAsStringAsync();
                return results;
            }

        }
    }
}
