using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.DataStructure
{
    public class SafeTree
    {
        public SafeNode root;

        public SafeTree()
        {
            root = new SafeNode();
            root.ID = 0;
            root.name = "root";
            root.parent = null;
        }
    }
}
