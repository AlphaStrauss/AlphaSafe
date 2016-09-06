using AlphaSafe.Core.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.Elements
{
    public class SafeTreeLayout : StackLayout
    {
        public SafeTree tree;
        public SafeTreeLayout(SafeTree tree) : base()
        {
            this.tree = tree;

            CreateTreeLayout();
        }

        public void CreateTreeLayout()
        {
            foreach(SafeNode node in tree.root.children)
            {
                switch (node.type)
                {
                    case SafeNodeType.Category:
                        Children.Add(new SafeNodeCategoryLayout(node as SafeNodeCategory));
                        break;
                    case SafeNodeType.Password:
                        Children.Add(new SafeNodePasswordLayout(node as SafeNodePassword));
                        break;
                    case SafeNodeType.Notice:
                        Children.Add(new SafeNodeNoticeLayout(node as SafeNodeNotice));
                        break;
                }
            }
        }
    }
}
