using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    public class Comment
    {
        int commentId;
        string commentmsg;
        List<Reply> reply;
        public int CommentId { get => commentId; set => commentId = value; }
        public string Commentmsg { get => commentmsg; set => commentmsg = value; }
        internal List<Reply> Reply { get => reply; set => reply = value; }
    }
}
