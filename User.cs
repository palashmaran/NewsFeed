using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    class User
    {
        int userid;
        string username;
        Dictionary<int , Post> postsById = new Dictionary<int,Post>();
        List<Follow> followers = new List<Follow>();
        public bool AddPost(Post post)
        {
            postsById[post.PostId] = post;
            return true;
        }
        public int Userid { get => Userid; set => Userid = value; }
        public string Username { get => username; set => username = value; }
        public List<Follow> Followers { get => followers; set => followers = value; }
        public Dictionary<int, Post> PostsById { get => postsById; set => postsById = value; }

        public bool AddUpVote(int postId)
        {
            if(!postsById.ContainsKey(postId))
            {
                Console.WriteLine("post does not exit or deleted");
                return false;
            }

            var post = postsById[postId];
            post.UpVotes++;

            return true;
        }

        public bool AddDownVote(int postId)
        {
            if (!postsById.ContainsKey(postId))
            {
                Console.WriteLine("post does not exit or deleted");
                return false;
            }

            var post = postsById[postId];
            post.DownVotes++;
            return true;
        }

        public bool AddFollower(Follow follower)
        {
            this.followers.Add(follower);
            return true;
        }
    }
}
