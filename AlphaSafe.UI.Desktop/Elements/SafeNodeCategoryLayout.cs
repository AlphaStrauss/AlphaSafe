using AlphaSafe.Core.DataStructure;
using AlphaSafe.UI.BasisFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.Elements
{
    public class SafeNodeCategoryLayout : StackLayout
    {
        #region variables declaration

        public SafeNodeCategory node;

        public AbsoluteLayout absoluteLayout_head;
        public Button button_name;
        
        public Button button_edit;
        public Button button_editSave;
        public Button button_addCategory;
        public Button button_addPassword;
        public Button button_delete;

        public Entry entry_name;

        #endregion

        public SafeNodeCategoryLayout(SafeNodeCategory node)
        {
            #region arguments

            this.node = node;

            #endregion

            #region variables definition

            absoluteLayout_head = new AbsoluteLayout();

            button_name = new Button();
            button_edit = new Button();
            button_editSave = new Button();
            button_addCategory = new Button();
            button_addPassword = new Button();
            button_delete = new Button();

            entry_name = new Entry();

            #endregion

            #region static initializations

            this.TranslationX = 20;

            UpdateButtonName();
            button_name.TextColor = ColorProvider.DarkGrey;
            button_name.HorizontalOptions = LayoutOptions.Start;
            button_name.Clicked += Button_name_Clicked;

            button_edit.Text = "edit";
            button_edit.TextColor = ColorProvider.White;
            button_edit.BorderColor = ColorProvider.White;
            button_edit.TranslationX = 150;
            button_edit.Clicked += Button_edit_Clicked;

            button_editSave.Text = "save";
            button_editSave.TextColor = ColorProvider.White;
            button_editSave.BorderColor = ColorProvider.White;
            button_editSave.TranslationX = 150;
            button_editSave.Clicked += Button_editSave_Clicked;
            button_editSave.IsVisible = false;

            button_addCategory.Text = "+ category";
            button_addCategory.TextColor = ColorProvider.White;
            button_addCategory.BorderColor = ColorProvider.White;
            button_addCategory.TranslationX = 200;
            button_addCategory.Clicked += Button_addCategory_Clicked;

            button_addPassword.Text = "+ password";
            button_addPassword.TextColor = ColorProvider.White;
            button_addPassword.BorderColor = ColorProvider.White;
            button_addPassword.TranslationX = 295;
            button_addPassword.Clicked += Button_addPassword_Clicked;

            button_delete.Text = "delete";
            button_delete.TextColor = ColorProvider.White;
            button_delete.BorderColor = ColorProvider.White;
            button_delete.TranslationX = 395;
            button_delete.Clicked += Button_delete_Clicked; ;

            entry_name.Text = node.name;
            entry_name.IsVisible = false;

            #endregion

            #region define structure

            absoluteLayout_head.Children.Add(button_name);
            absoluteLayout_head.Children.Add(button_edit);
            absoluteLayout_head.Children.Add(button_editSave);
            absoluteLayout_head.Children.Add(button_addCategory);
            absoluteLayout_head.Children.Add(button_addPassword);
            absoluteLayout_head.Children.Add(button_delete);

            absoluteLayout_head.Children.Add(entry_name);
            
            #endregion

            CreateNodeLayout(node);

            Hide();
        }

        private void UpdateButtonName()
        {
            button_name.Text = (node.children.Count > 0 ? "-" : "  ") + " " + node.name;
        }

        private void Button_editSave_Clicked(object sender, EventArgs e)
        {
            node.name = entry_name.Text;
            UpdateButtonName();

            ChangeEditMode();

            CreateNodeLayout(node);
        }

        public bool editMode = false;
        private void Button_edit_Clicked(object sender, EventArgs e)
        {
            ChangeEditMode();

            CreateNodeLayout(node);
        }

        public void ChangeEditMode()
        {
            editMode = !editMode;

            button_edit.IsVisible = !button_edit.IsVisible;
            button_editSave.IsVisible = !button_editSave.IsVisible;

            button_name.IsVisible = !button_name.IsVisible;
            entry_name.IsVisible = !entry_name.IsVisible;
        }

        private void Button_addCategory_Clicked(object sender, EventArgs e)
        {
            node.AddCategoryNode("New Category");

            UpdateButtonName();

            CreateNodeLayout(node);
        }

        private void Button_addPassword_Clicked(object sender, EventArgs e)
        {
            node.AddPasswordNode("", "New Password", "");

            UpdateButtonName();

            CreateNodeLayout(node);
        }

        private void Button_delete_Clicked(object sender, EventArgs e)
        {
            if(node.children.Count == 0)
            {
                // delete category
            }
        }


        public void CreateNodeLayout(SafeNode root)
        {
            Children.Clear();

            Children.Add(absoluteLayout_head);

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

        private bool visible = false;
        private void Button_name_Clicked(object sender, EventArgs e)
        {
            if (visible)
            {
                Hide();
            }
            else
            {
                Show();
            }

            visible = !visible;
        }

        private void Hide()
        {
            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].IsVisible = false;
            }

            button_name.Text = (node.children.Count > 0 ? "+" : "  ") + " " + node.name;
            button_edit.IsVisible = false;
            button_editSave.IsVisible = false;
            button_addCategory.IsVisible = false;
            button_addPassword.IsVisible = false;
            button_delete.IsVisible = false;
        }

        private void Show()
        {
            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].IsVisible = true;
            }

            button_name.Text = (node.children.Count > 0 ? "-" : "  ") + " " + node.name;
            if (editMode)
            {
                button_edit.IsVisible = true;
                button_editSave.IsVisible = false;
            }
            else
            {
                button_edit.IsVisible = true;
                button_editSave.IsVisible = false;
            }
            button_addCategory.IsVisible = true;
            button_addPassword.IsVisible = true;
            button_delete.IsVisible = true;
        }
    }
}
