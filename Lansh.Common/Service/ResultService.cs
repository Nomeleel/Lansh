using Lansh.Common.Constant;
using Lansh.Common.Helper;
using Lansh.Common.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lansh.Common.Service
{
    public class ResultService
    {

        private WebClientHelper _webClientHelper = new WebClientHelper();
        private ApiService _apiService = new ApiService();

        #region B
        /// <summary>
        /// Get home video data
        /// </summary>
        /// <returns></returns>
        public async Task<Index> GetIndexAsync()
        {
            string results = await _webClientHelper.GetResultAsync(new Uri(Api.Index));
            Index index = JsonConvert.DeserializeObject<Index>(results);
            return index;
        }

        /// <summary>
        /// Get home video data list
        /// </summary>
        /// <returns></returns>
        public async Task<List<List<HomeVideo>>> GetIndexListAsync()
        {
            List<List<HomeVideo>> indexList = new List<List<HomeVideo>>();
            string results = await _webClientHelper.GetResultAsync(new Uri(Api.Index));
            JObject json = JObject.Parse(results);
            int listCount = 0;
            foreach (KeyValuePair<string, JToken> keyValuePair in json)
            {
                if (listCount ++ > 14)
                    return indexList;
                List<HomeVideo> list = new List<HomeVideo>();
                JObject jObject = JObject.Parse(keyValuePair.Value.ToString());
                foreach (KeyValuePair<string, JToken> jToken in jObject)
                {
                    list.Add(JsonConvert.DeserializeObject<HomeVideo>(jToken.Value.ToString()));
                }
                indexList.Add(list);
            }
            return indexList;
        }

        /// <summary>
        /// Get suggest collection by keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<String>> GetSuggestAsync(string keyword)
        {
            try
            {
                Uri uri = new Uri(string.Format(Api.Suggest, keyword));
                string results = await _webClientHelper.GetResultAsync(uri);
                JObject json = JObject.Parse(results);
                List<Suggest> list = JsonConvert.DeserializeObject<List<Suggest>>(json["result"]["tag"].ToString());
                List<String> suggestions = new List<string>();
                if (list != null && list.Count != 0)
                {
                    foreach (Suggest item in list)
                {
                    suggestions.Add(item.Value);
                }
                }
                return suggestions;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Get search result by searchType keyword
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SearchType">such as video, up</param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<T>> GetSearchResultAsync<T>(string SearchType, string keyword, int page)
        {
            keyword = Uri.EscapeDataString(keyword);
            string url = string.Format(Api.Search, keyword, page, SearchType);
            string results = await _webClientHelper.GetResultAsync(_apiService.GetSignUri(url));
            JObject json = JObject.Parse(results);
            return JsonConvert.DeserializeObject<List<T>>(json["result"].ToString());
        }

        /// <summary>
        /// Get hot keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<HotWord>> GetHotWordAsync()
        {
            try
            {
                Uri uri = new Uri(Api.HotWord);
                string results = await _webClientHelper.GetResultAsync(uri);
                JObject json = JObject.Parse(results);
                List<HotWord> list = JsonConvert.DeserializeObject<List<HotWord>>(json["list"].ToString());
                return list;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// By aid get video info
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public async Task<VideoInfo> GetVideoInfo(string aid)
        {
            string result = await _webClientHelper.GetResultAsync(_apiService.GetSignUri(string.Format(Api.VideoInfo, aid)));
            JObject json = JObject.Parse(result);
            VideoInfo videoInfo = JsonConvert.DeserializeObject<VideoInfo>(json["data"].ToString());
            return videoInfo;
        }

        /// <summary>
        /// Get play url by cid and quality
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<string> GetPlayUrl(string cid, int quality)
        {
            string result = await _webClientHelper.GetResultAsync(new Uri(string.Format(Api.PlayUrl, cid, quality)));
            string playUrl = Regex.Match(result, "\"url\\\":\\\"(.*?)\",\\\"backup_url").Groups[1].Value.Replace("\\u0026", "&");
            return playUrl;
        }

        /// <summary>
        /// Get reply
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ReplyResult> GetReply(string aid, int page)
        {
            string result = await _webClientHelper.GetResultAsync(new Uri(string.Format(Api.Feedback, aid, page)));
            ReplyResult replyResult = JsonConvert.DeserializeObject<ReplyResult>(result); 
            return replyResult;
        }

        public async Task<string> GetMVUrl(string mid, int quality)
        {
            string url = "http://music.163.com/eapi/song/enhance/play/mv/url";
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            string param = "FA90B329E9614F79E79598F37DC2EDB404D1A8456D661938E537BF5C19E1746844F91591EBFDA7F9C69AEE13BA3D431D229D61CF885F96E1B1349E1E85B51196954D42975E82C48EBE42CEDCF1EEC6D3041E8BBD216394CD331458FC20B7255715F55AFE74AFBFC76BEEABE40F9829D9D2A55931D4AAC514A9E78D15A3B72822AB4921BB8A6612AD2578366666DE82F4AFC466721C0195197D25C0E3CFFC31776C9A956A0B66E8DD698835B47E571801BA7A8EB02A299EB99FDB4D721B49C441336BD6FCD2C228F237DDB363C0A6DCEE742B6F860AFB3E7DF682102359AFD2711345AE05DF07929FC713BFA9D13C05F7F6FCEF222DF33E4EF6D5A38AB35D3B5621B258DAD0C2BC2A6D8ACC3A17E8830DADCCCE5EB8DBD86F9DF11F25FF23640CCFA50418AEFF6FD4A2ECF47362277126D823432B8455CB1E4A147C4B4DCB1FEF9C170532D717E6DAB244F65D4B5C3908AE28C1207DCDB9965A36D173ED6F404EFB0943A6ABB6A77D52DB518FFEBE83BA3624B112784B563E14B2196505E9B21EE8305C94E292C6D62388AAF7255A3AACE14D8F62E4836FC81F0DCEB625784F03B0DC9054FFB411D86A9A155E7E913A4735E694B1DC9F5DEFA4097FE9DA4E576732E664543BCB5108C9503C0D8F328999";
            int lenght = param.Length;
            KeyValuePair <string, string> parm = new KeyValuePair<string, string>("params", param);
            data.Add(parm);
            string result = await _webClientHelper.PostResultAsync(new Uri(url), data);
            return result;
        }

        #endregion

        #region One
        /// <summary>
        /// Get one id list
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetIdList()
        {
            string results = await _webClientHelper.GetResultAsync(new Uri(Api.IdArray));
            JObject json = JObject.Parse(results);
            List < string > idList = JsonConvert.DeserializeObject<List<string>>(json["data"].ToString());
            return idList;
        }

        /// <summary>
        /// Get one id list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IdList> GetIdList(string id)
        {
            string results = await _webClientHelper.GetResultAsync(new Uri(string.Format(Api.IdList, id)));
            JObject json = JObject.Parse(results);
            IdList idList = JsonConvert.DeserializeObject<IdList>(json["data"].ToString());
            return idList;
        }

        /// <summary>
        /// Get one One by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<One> GetOne(string id)
        {
            IdList IdList = await GetIdList(id);
            One one = IdList.Content_List[0];
            return one;
        }

        /// <summary>
        /// Get one One list by id array
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<One>> GetOneList()
        {
            List<One> oneList = new List<One>();
            List<string> idList = await GetIdList();
            foreach (string id in idList)
                oneList.Add(await GetOne(id));
            return oneList;
        }

        #endregion

        #region WYY

        /// <summary>
        /// Get Comment
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<CommentResult> GetComment(string param)
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> parm = new KeyValuePair<string, string>("params", param);
            data.Add(parm);
            string result = await _webClientHelper.PostResultAsync(new Uri(Api.Comment), data);
            CommentResult commentResult = JsonConvert.DeserializeObject<CommentResult>(result);
            return commentResult;
        }

        #endregion

    }
}
