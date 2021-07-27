using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    class IdGenerator
    {
        private static int id = 0;

        private IdGenerator()
        {

        }

        public static int GetId()
        {
            id++;
            return id;
        }
    }
}
