using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Model
{
    public class CommentResult
    {
        public string UserId { get; set; }
        public string Total { get; set; }
        public List<Comment> HotComments { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public string Content { get; set; }
        public string LikedCount { get; set; }
        public string Time { get; set; }
        public User User { get; set; }
    }
    
    public class User
    {
        public string AuthStatus { get; set; }
        public string AvatarUrl { get; set; }
        public string NickName { get; set; }
        public string UserId { get; set; }

    }

}
