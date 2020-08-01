using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class Bangumi : SearchResult
    {
        #region Attr
        public string Styles { get; set; }
        public int Status { get; set; }
        public string Typeurl { get; set; }
        public long Spid { get; set; }
        public long Pubdate { get; set; }
        public string NewestSeason { get; set; }
        public string Evaluate { get; set; }
        public string Cover { get; set; }
        public bool IsFinish { get; set; }
        public long SeasonId { get; set; }
        public string[] Bgmlist { get; set; }
        public string Favorites { get; set; }
        public string Cv { get; set; }
        public long DanmakuCount { get; set; }
        public long PlayCount { get; set; }
        public string HitColumns { get; set; }
        public long NewestEpId { get; set; }
        public string Title { get; set; }
        public string TotalCount { get; set; }
        public string NewestEpIndex { get; set; }
        public string Brief { get; set; }
        public string Catlist { get; set; }
        public string NewestCat { get; set; }
        public long BangumiId { get; set; }
        public long RankScore { get; set; }
        public string Type { get; set; }
        public string[] Eplist { get; set; }
        #endregion

        public override SearchResult Conversion(SearchResultInfo searchResultInfo)
        {
            Bangumi bangumi = new Bangumi();
            bangumi.Cover = searchResultInfo.Cover;
            bangumi.Title = searchResultInfo.Title;
            bangumi.Evaluate = searchResultInfo.Evaluate;

            return bangumi;
        }

    }
}
