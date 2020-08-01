using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class Upuser: SearchResult
    {
        public string Usign { get; set; }
        public string Videos { get; set; }
        public string Level { get; set; }
        public string Gender { get; set; }
        public string Upic { get; set; }
        public string[] HitColumns { get; set; }
        public string Mid { get; set; }
        public string Uname { get; set; }
        public string Fans { get; set; }
        ////public string Official_Verify { get; set; }
        public string[] Res { get; set; }
        public string VerifyInfo { get; set; }
        public string RankScore { get; set; }

        public override SearchResult Conversion(SearchResultInfo searchResultInfo)
        {
            Upuser upuser = new Upuser();
            upuser.Upic = searchResultInfo.Upic;
            upuser.Uname = searchResultInfo.Uname;
            upuser.Usign = searchResultInfo.Usign;
            return upuser;
        }
    }
}
