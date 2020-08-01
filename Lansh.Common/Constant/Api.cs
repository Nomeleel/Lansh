using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Constant
{
    public class Api
    {

        /// <summary>
        /// App Secret for Wp
        /// </summary>
        public readonly static string AppSecretWp = "ba3a4e554e9a6e15dc4d1d70c2b154e3";

        /// <summary>
        /// App key
        /// </summary>
        public readonly static string AppKey = "422fd9d7289a1dd9";

        /// <summary>
        /// Home index data
        /// </summary>
        public readonly static string Index = "http://www.bilibili.com/index/ding.json"; // http://api.bilibili.cn/index

        /// <summary>
        /// Serach box auggest by key
        /// </summary>
        public readonly static string Suggest = "http://s.search.bilibili.com/main/suggest?suggest_type=accurate&sub_type=tag&main_ver=v1&term={0}";

        /// <summary>
        /// Search T by Keyword, Page, SearchType
        /// </summary>
        public readonly static string Search = "http://api.bilibili.com/search?_device=wp&appkey=" + Api.AppKey + "&build=411005&keyword={0}&main_ver=v3&page={1}&pagesize=18&platform=android&search_type={2}&source_type=0";

        /// <summary>
        /// Search page hot search keyword
        /// </summary>
        public readonly static string HotWord = "http://www.bilibili.com/search?action=hotword&main_ver=v1";

        /// <summary>
        /// Video Info by aid
        /// </summary>
        public readonly static string VideoInfo = "http://app.bilibili.com/x/view?_device=wp&_ulv=10000&access_key=&aid={0}&appkey=" + Api.AppKey + "&build=411005&plat=4&platform=android";

        /// <summary>
        /// BiliBili play url
        /// </summary>
        public readonly static string PlayUrl = "http://api.bilibilih5.club/video/{0}?quality={0}";

        /// <summary>
        /// Video reply
        /// </summary>
        public readonly static string Feedback = "http://api.bilibili.cn/feedback?aid={0}&page={1}";

        /// <summary>
        /// MV url
        /// </summary>
        public readonly static string MvUrl = "http://music.163.com/eapi/song/enhance/play/mv/url";

        /// <summary>
        /// MV Comment
        /// </summary>
        public readonly static string Comment = "http://music.163.com/eapi/v1/resource/comments/get";

        /// <summary>
        /// One id array
        /// </summary>
        public readonly static string IdArray = "http://v3.wufazhuce.com:8000/api/onelist/idlist";

        /// <summary>
        /// One IdList
        /// </summary>
        public readonly static string IdList = "http://v3.wufazhuce.com:8000/api/onelist/{0}/0";
    }
}
