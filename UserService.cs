using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    class UserService
    {
        User loggedInuser = null;
        Dictionary<string, User> usersByName = new Dictionary<string, User>();
        Dictionary<int, Post> allpostsById = new Dictionary<int, Post>();
        //Dictionary<int, string> postIdByUser = new Dictionary<int, string>();

        //  Dictionary<string, List<User>> followers = new Dictionary<string, List<User>>;
        public bool SignUp(string username)
        {
            User user = new User();
            //user.Userid = ;
            user.Username = username;
            usersByName.Add(username, user);
            return true;
        }

        public bool LogIn(string username)
        {
            if(!usersByName.ContainsKey(username))
            {
                Console.WriteLine("user does not exist");
                return false;
            }

            this.loggedInuser = usersByName[username];
            return true;
        }

        private Comment AddComment(int postId, string commentmsg)
        {
            Comment comment = new Comment();
            comment.CommentId = IdGenerator.GetId();
            comment.Commentmsg = commentmsg;
            
            return comment;
        }
        public bool AddPost(string postmsg)
        {
            Post post = new Post();
            post.PostId = IdGenerator.GetId();
            post.Postmsg = postmsg;
            this.allpostsById.Add(post.PostId, post);
            this.loggedInuser.AddPost(post);
            return true;
        }

        public bool AddReply(int postid, string replymsg)
        {
            if(!this.allpostsById.ContainsKey(postid))
            {
                Console.WriteLine("post does not exist");
                return false;
            }

            var post = this.allpostsById[postid];

            Reply reply = new Reply();
            reply.Replyid = IdGenerator.GetId();
            reply.Replymessage = replymsg;
            reply.PostId = postid;
            reply.CreatedBy = this.loggedInuser.Username;
            post.AddReply(reply);

            this.allpostsById[postid] = post;
            return true;
        }

        public bool AddUpvote(int postId)
        {
            this.allpostsById[postId].UpVotes++;
           // this.loggedInuser.AddUpVote(postId);
            return true;
        }

        public bool DownUpvote(int postId)
        {
            this.allpostsById[postId].DownVotes++;
            //this.loggedInuser.AddDownVote(postId);
            return true;
        }

        public bool FollowUser(string username)
        {
            Follow follow = new Follow();
            follow.FollowedBy = this.loggedInuser.Username;
            follow.FollowTo = username;
            this.loggedInuser.AddFollower(follow);
            return true;
        }

        void PrintReply(Reply reply)
        {
            Console.WriteLine(reply.Replyid);
            Console.WriteLine(reply.CreatedBy);
            Console.WriteLine(reply.Replymessage);
        }
        void PrintPost(Post post)
        {
            Console.WriteLine(post.PostId);
            Console.WriteLine(post.UpVotes + " UpVotes " + post.DownVotes + " DownVotes");
            Console.WriteLine(post.CreatedBy);
            Console.WriteLine(post.Postmsg);
            foreach(var reply in post.Reply)
            {
                PrintReply(reply);
            }


        }
        public void ShowNewsFeed()
        {
            User user = this.loggedInuser;
            List<Follow> followers = user.Followers;
            Dictionary<int, int> SeenPost = new Dictionary<int, int>(); 
            foreach(var follow in followers)
            {
                string username = follow.FollowTo;
                User followToUser = usersByName[username];
                var postDic = followToUser.PostsById;

                foreach(var post in postDic)
                {
                    SeenPost.Add(post.Key, 1);
                    Post userpost = this.allpostsById[post.Key];
                    PrintPost(userpost);
                }
            }

            foreach (var post in this.allpostsById)
            {
                if (!SeenPost.ContainsKey(post.Key))
                {
                    Post userpost = this.allpostsById[post.Key];
                    PrintPost(userpost);
                }
            }

        }
    }
}
