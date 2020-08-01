using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Model
{
    public class ReplyResult
    {
        public string Results { get; set; }
        public string Page { get; set; }
        public string Pages { get; set; }
        public string IsAdmin { get; set; }
        public string Owner { get; set; }
        public List<Reply> HotList { get; set; }
        public List<Reply> List { get; set; }
    }

    public class Reply
    {
        public string Mid { get; set; }
        public string Lv { get; set; }
        public string Fbid { get; set; }
        public string Good { get; set; }
        public string Msg { get; set; }
        public string Create { get; set; }
        public string Create_At { get; set; }
        public string Reply_Count { get; set; }
        public string Face { get; set; }
        public string Owner { get; set; }
        public string Rank { get; set; }
        public string Nick { get; set; }
        public string Sex { get; set; }
    }
}
