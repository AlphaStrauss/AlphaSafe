using AlphaSafe.Core.DataStructure;
using AlphaSafe.UI.BasisFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.Elements
{
    public class SafeNodeNoticeLayout : AbsoluteLayout
    {
        #region variables declaration

        public SafeNodeNotice node;

        public Label label_name;

        #endregion

        public SafeNodeNoticeLayout(SafeNodeNotice node)
        {
            #region arguments

            this.node = node;

            #endregion

            #region variables definition

            label_name = new Label();

            #endregion

            #region static initializations

            this.TranslationX = 35;

            label_name.Text = node.name;
            label_name.TextColor = ColorProvider.Orange;

            #endregion

            #region define structure

            Children.Add(label_name);

            #endregion

            CreateNodeLayout(node);
        }

        public void CreateNodeLayout(SafeNode root)
        {
            foreach (SafeNode node in root.children)
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
