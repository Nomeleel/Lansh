using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class ReplyComment
    {
        public string Good { get; set; }
        public string Desc { get; set; }
        public string CreateDate { get; set; }
        public string ReplyCount { get; set; }
        public string Face { get; set; }
        public string NickName { get; set; }
        public string Rank { get; set; }
        public string Sex { get; set; }

        public static ReplyComment Conversion(Reply reply)
        {
            ReplyComment replyComment = new ReplyComment();
            replyComment.Good = reply.Good;
            replyComment.Desc = reply.Msg;
            replyComment.CreateDate = reply.Create_At;
            replyComment.Face = reply.Face;
            replyComment.NickName = reply.Nick;
            return replyComment;
        }

        public static ReplyComment Conversion(Comment comment)
        {
            ReplyComment replyComment = new ReplyComment();
            replyComment.Good = comment.LikedCount;
            replyComment.Desc = comment.Content;
            replyComment.CreateDate = DateTimeConversion(comment.Time);
            replyComment.Face = comment.User.AvatarUrl;
            replyComment.NickName = comment.User.NickName;
            return replyComment;
        }

        public static string DateTimeConversion(string dateString)
        {
            DateTime dtStart = new DateTime(1970, 1, 1);
            long lTime = long.Parse(dateString + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            string result = dtResult.ToString();
            return result.Substring(0, result.Length - 3);
        }
    }
}
