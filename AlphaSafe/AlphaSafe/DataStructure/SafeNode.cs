using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.DataStructure
{
    public class SafeNode
    {
        public int ID;
        public SafeNodeType type;
        public string name;

        public SafeNode parent;
        public List<SafeNode> children;

        public SafeNode()
        {
            children = new List<SafeNode>();
        }

        public void AddCategoryNode(string name)
        {
            SafeNodeCategory categoryNode = new SafeNodeCategory();
            categoryNode.name = name;
            categoryNode.parent = this;

            children.Add(categoryNode);
        }

        public void AddPasswordNode(string masterpassword, string name, string password)
        {
            SafeNodePassword passwordNode = new SafeNodePassword();
            passwordNode.name = name;
            passwordNode.parent = this;
            passwordNode.SetPassword(masterpassword, password);

            children.Add(passwordNode);
        }

        public void AddNoticeNode(string masterpassword, string name, string notice)
        {
            SafeNodeNotice noticeNode = new SafeNodeNotice();
            noticeNode.name = name;
            noticeNode.parent = this;
            noticeNode.SetNotice(masterpassword, notice);

            children.Add(noticeNode);
        }
    }
}
