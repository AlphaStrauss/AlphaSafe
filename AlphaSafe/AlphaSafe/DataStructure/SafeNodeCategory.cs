using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.DataStructure
{
    public class SafeNodeCategory : SafeNode
    {
        public SafeNodeCategory() : base()
        {
            type = SafeNodeType.Category;
        }
    }
}
