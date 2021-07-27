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

        private bool isValidPostId(int postId)
        {
            if (!this.allpostsById.ContainsKey(postId))
            {
                return false;
            }

            return true;
        }
        public bool AddReply(int postid, string replymsg)
        {
            if(!this.isValidPostId(postid))
            {
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
            if (!this.isValidPostId(postId))
            {
                return false;
            }

            this.allpostsById[postId].UpVotes++;
           // this.loggedInuser.AddUpVote(postId);
            return true;
        }

        public bool DownUpvote(int postId)
        {
            if (!this.isValidPostId(postId))
            {
                return false;
            }
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
            Console.WriteLine("Id :" + reply.Replyid);
            Console.WriteLine("userID :" + reply.CreatedBy);
            Console.WriteLine("message : " + reply.Replymessage);
            Console.WriteLine("timestamp " + reply.Timestamp.ToString());
        }
        void PrintPost(Post post)
        {
            Console.WriteLine("Id: " + post.PostId);
            Console.WriteLine(post.UpVotes + " UpVotes " + post.DownVotes + " DownVotes");
            Console.WriteLine("Username: " + post.CreatedBy);
            Console.WriteLine("Msg : "+ post.Postmsg);
            Console.WriteLine("timestamp " + post.Timestamp.ToString());
            
            foreach(var reply in post.Reply)
            {
                PrintReply(reply);
            }
        }

        public SortedSet<Post> CreateSet(IComparer<Post> sortBy)
        {
            SortedSet<Post> set = new SortedSet<Post>(sortBy);

            foreach (var post in this.allpostsById)
            {
                set.Add(post.Value);
            }

            return set;
        }
        public void printAllPostsFromSet(SortedSet<Post> posts)
        {
            foreach(var post in posts)
            {
                this.PrintPost(post);
            }
        }

        public void ShowNewsFeed(SortType type)
        {
            SortedSet<Post> set;
            switch(type)
            {
                case SortType.FOLLOWERS:
                    Console.WriteLine("SortBy Followers start\n");
                    ShowNewsFeedByFollowers();
                    Console.WriteLine("SortBy Follower End\n");
                    break;

                case SortType.TIMESTAMP:
                    Console.WriteLine("SortBy Timestamp List start\n");
                    var sortBytimestamp = new SortByTimeStamp();
                    set = CreateSet(sortBytimestamp);
                    this.printAllPostsFromSet(set);
                    Console.WriteLine("SortBy Timestamp List End\n");

                    break;

                case SortType.VOTES:
                    Console.WriteLine("SortBy Votes List start\n");
                    var sortByVotes = new SortByVotes();
                    set = CreateSet(sortByVotes);
                    this.printAllPostsFromSet(set);
                    Console.WriteLine("SortBy Votes List end\n");
                    break;
            }

        }

        public void ShowNewsFeedByFollowers()
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
