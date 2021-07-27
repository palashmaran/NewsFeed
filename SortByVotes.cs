using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NewsFeed
{
    public class SortByVotes : IComparer<Post>
    {
        public int Compare([AllowNull] Post x, [AllowNull] Post y)
        {
            int xvotes = x.UpVotes - x.DownVotes;
            int yvotes = y.UpVotes - y.DownVotes;

            if (xvotes == yvotes)
                return 1;

            return yvotes.CompareTo(xvotes);
        }
    }
}