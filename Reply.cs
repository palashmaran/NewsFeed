using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    public class Reply
    {
        int replyid;
        string replymessage;
        int postId;
        int commentId;
        string createdBy;
        DateTime timestamp = DateTime.Now;
        public int Replyid { get => replyid; set => replyid = value; }
        public string Replymessage { get => replymessage; set => replymessage = value; }
        public int PostId { get => postId; set => postId = value; }
        public int CommentId { get => commentId; set => commentId = value; }
        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
    }
}
