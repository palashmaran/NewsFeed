using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    public class Follow
    {
        string followedBy;
        string followTo;

        public string FollowedBy { get => followedBy; set => followedBy = value; }
        public string FollowTo { get => followTo; set => followTo = value; }
    }
}
