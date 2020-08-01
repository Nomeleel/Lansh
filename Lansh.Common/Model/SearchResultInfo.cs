using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Model
{
    public class SearchResultInfo
    {
        #region Video
        public string Play { get; set; }
        public string Description { get; set; }
        public long Senddate { get; set; }
        public bool Badgepay { get; set; }
        public string Pic { get; set; }
        public string Tag { get; set; }
        public string Video_Review { get; set; }
        public string Favorites { get; set; }
        public string Duration { get; set; }
        public long Id { get; set; }
        public long Rank_Score { get; set; }
        public string TypeId { get; set; }
        public string Pubdate { get; set; }
        public string Title { get; set; }
        public int Review { get; set; }
        public string Author { get; set; }
        public string Mid { get; set; }
        public string Arcurl { get; set; }
        public string Typename { get; set; }
        public string Aid { get; set; }
        public string Type { get; set; }
        public string Arcrank { get; set; }
        #endregion

        #region  Bangumi
        public string Styles { get; set; }
        public int Status { get; set; }
        public string Typeurl { get; set; }
        public long Spid { get; set; }
        //public long Pubdate { get; set; }
        public string Newest_Season { get; set; }
        public string Evaluate { get; set; }
        public string Cover { get; set; }
        public bool Is_Finish { get; set; }
        public long Season_Id { get; set; }
        public string[] Bgmlist { get; set; }
        //public string Favorites { get; set; }
        public string Cv { get; set; }
        public long Danmaku_Count { get; set; }
        public long Play_Count { get; set; }
        ////public string Hit_Columns { get; set; }
        public long Newest_Ep_Id { get; set; }
        //public string Title { get; set; }
        public long Total_Count { get; set; }
        public string Newest_Ep_Index { get; set; }
        public string Brief { get; set; }
        ////public List<object> Catlist { get; set; }
        public string Newest_Cat { get; set; }
        public long Bangumi_Id { get; set; }
        //public long Rank_Score { get; set; }
        //public string Type { get; set; }
        public string[] Eplist { get; set; }
        #endregion

        #region Special
        //public string Styles { get; set; }
        public string Postdate { get; set; }
        //public string Pic { get; set; }
        //public string Typeurl { get; set; }
        //public string Description { get; set; }
        //public string Pubdate { get; set; }
        public string Favourite { get; set; }
        public string Attention { get; set; }
        //public string Season_Id { get; set; }
        //public string Tag { get; set; }
        //public string Spid { get; set; }
        //public string Id { get; set; }
        //public string Arcurl { get; set; }
        //public string Rank_Score { get; set; }
        public string Count { get; set; }
        public string Lastupdate { get; set; }
        public string Bgmcount { get; set; }
        ////public string[] Videos { get; set; }
        //public string Title { get; set; }
        //public string Author { get; set; }
        public string[] Hit_Columns { get; set; }
        //public string Mid { get; set; }
        public string Click { get; set; }
        public string Spcount { get; set; }
        //public string Type { get; set; }
        #endregion

        #region Special
        ////public string Styles { get; set; }
        //public string Postdate { get; set; }
        ////public string Pic { get; set; }
        ////public string Typeurl { get; set; }
        ////public string Description { get; set; }
        ////public string Pubdate { get; set; }
        //public string Favourite { get; set; }
        //public string Attention { get; set; }
        ////public string Season_Id { get; set; }
        ////public string Tag { get; set; }
        ////public string Spid { get; set; }
        ////public string Id { get; set; }
        ////public string Arcurl { get; set; }
        ////public string Rank_Score { get; set; }
        //public string Count { get; set; }
        //public string Lastupdate { get; set; }
        //public string Bgmcount { get; set; }
        //public string[] Videos { get; set; }
        ////public string Title { get; set; }
        ////public string Author { get; set; }
        //public string[] Hit_Columns { get; set; }
        ////public string Mid { get; set; }
        //public string Click { get; set; }
        //public string Spcount { get; set; }
        ////public string Type { get; set; }
        #endregion

        #region Upuser
        public string Usign { get; set; }
        //public string Videos { get; set; }
        public string Level { get; set; }
        public string Gender { get; set; }
        public string Upic { get; set; }
        //public string[] Hit_Columns { get; set; }
        //public string Mid { get; set; }
        public string Uname { get; set; }
        public string Fans { get; set; }
        ////public string Official_Verify { get; set; }
        public string[] Res { get; set; }
        public string Verify_Info { get; set; }
        //public string Type { get; set; }
        //public string Rank_Score { get; set; }
        #endregion

    }
}
