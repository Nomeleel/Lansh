using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class Special: SearchResult
    {
        public string Styles { get; set; }
        public string Postdate { get; set; }
        public string Pic { get; set; }
        public string Typeurl { get; set; }
        public string Description { get; set; }
        public string Pubdate { get; set; }
        public string Favourite { get; set; }
        public string Attention { get; set; }
        public string SeasonId { get; set; }
        public string Tag { get; set; }
        public string Spid { get; set; }
        public string Id { get; set; }
        public string Arcurl { get; set; }
        public string RankScore { get; set; }
        public string Count { get; set; }
        public string Lastupdate { get; set; }
        public string Bgmcount { get; set; }
        public string[] Videos { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string[] HitColumns { get; set; }
        public string Mid { get; set; }
        public string Click { get; set; }
        public string Spcount { get; set; }

        public override SearchResult Conversion(SearchResultInfo searchResultInfo)
        {
            Special special = new Special();
            special.Pic = searchResultInfo.Pic;
            special.Title = searchResultInfo.Title;
            special.Description = searchResultInfo.Description;
            return special;
        }
    }
}
