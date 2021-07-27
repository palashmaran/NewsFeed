using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NewsFeed
{
    public class SortByTimeStamp : IComparer<Post>
    {
        public int Compare([AllowNull] Post x, [AllowNull] Post y)
        {
            long cticks = DateTime.Now.Ticks;
            long xelaspedtime = cticks - x.Timestamp.Ticks;
            long yelaspedtime = cticks - y.Timestamp.Ticks;
            return xelaspedtime.CompareTo(yelaspedtime);
        }
    }
}