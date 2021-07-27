using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    class Post
    {
        User createdBy;
        string postmsg;
        List<Comment> comments = new List<Comment>();
        List<Reply> reply = new List<Reply>();
        int upVotes = 0;
        int downVotes = 0;
        DateTime timestamp;
        int postId;
        public bool AddComment(Comment comment)
        {
            comments.Add(comment);
            return true;
        }

        public bool AddReply(Reply reply)
        {
            this.reply.Add(reply);
            return true;
        }
        public int UpVotes { get => upVotes; set => upVotes = value; }
        public int DownVotes { get => downVotes; set => downVotes = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public User CreatedBy { get => createdBy; set => createdBy = value; }
        internal List<Comment> Comments { get => comments; set => comments = value; }
        public int PostId { get => postId; set => postId = value; }
        public string Postmsg { get => postmsg; set => postmsg = value; }
        internal List<Reply> Reply { get => reply; set => reply = value; }
    }
}
