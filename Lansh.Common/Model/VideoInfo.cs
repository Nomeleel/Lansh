using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Model
{
    public class VideoInfo
    {
        public string Aid { get; set; }
        public string Attribute { get; set; }
        public string Copyright { get; set; }
        public string Ctime { get; set; }
        public string Desc { get; set; }
        //public string[] Dislike_Reasons { get; set; }
        public string Duration { get; set; }
        public string Online { get; set; }
        public Owner Owner { get; set; }
        //public OwnerExt Owner_Ext { get; set; }
        public List<VideoInfoPage> Pages { get; set; }
        public string Pic { get; set; }
        public string Pubdate { get; set; }
        //public List<object> Relates { get; set; }
        //public Right Rights { get; set; }
        public Stat Stat { get; set; }
        public string state { get; set; }
        //public Tag tag { get; set; }
        public string[] Tags { get; set; }
        public string Tid { get; set; }
        public string Title { get; set; }
        public string Tname { get; set; }
    }

    public class Owner
    {
        public string Face { get; set; }
        public string Mid { get; set; }
        public string Name { get; set; }
    }

    public class VideoInfoPage
    {
        public string Cid { get; set; }
        public string From { get; set; }
        public string Has_Alias { get; set; }
        public string Link { get; set; }
        public string Page { get; set; }
        public string Part { get; set; }
        public string Rich_Vid { get; set; }
        public string Vid { get; set; }
        public string WebLink { get; set; }
    }

    public class Stat
    {
        public string Coin { get; set; }
        public string Danmaku { get; set; }
        public string Favorite { get; set; }
        public string His_Rank { get; set; }
        public string Now_Rank { get; set; }
        public string Reply { get; set; }
        public string Share { get; set; }
        public string View { get; set; }

    }

}
