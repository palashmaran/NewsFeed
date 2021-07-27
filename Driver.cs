﻿using System;

namespace NewsFeed
{
    class Driver
    {
        static void Main(string[] args)
        {
            Console.WriteLine("News Feed started");

            UserService userService = new UserService();

            userService.SignUp("lucious");
            userService.SignUp("albus");
            userService.SignUp("tom");

            userService.LogIn("tom");

            userService.AddPost("I am going to be the darkest dark wizard of all time");
            userService.AddPost("I am lord Voldemort btw");

            userService.LogIn("lucious");

            userService.ShowNewsFeed();

            userService.AddUpvote(1);
            userService.DownUpvote(1);

            userService.ShowNewsFeed();

            userService.FollowUser("tom");
            userService.AddReply(1, "reply post 1");

            userService.ShowNewsFeed();

            Console.WriteLine("News Feed End");
        }
    }
}
