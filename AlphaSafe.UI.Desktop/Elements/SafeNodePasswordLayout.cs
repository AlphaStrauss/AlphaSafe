using AlphaSafe.Core.Cryptography;
using AlphaSafe.Core.DataStructure;
using AlphaSafe.SystemSpecific.Utils;
using AlphaSafe.UI.BasisFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.Elements
{
    public class SafeNodePasswordLayout : StackLayout
    {
        #region variables declaration

        public SafeNodePassword node;

        public AbsoluteLayout absoluteLayout_head;
        public AbsoluteLayout absoluteLayout_edit;
        public AbsoluteLayout absoluteLayout_generate;

        public Button button_name;
        public Entry entry_name;

        public Button button_edit;
        public Button button_editSave;
        public Button button_delete;

        public Entry entry_password;
        public Button button_show;
        public Button button_copy;

        public Entry entry_numSymbols;
        public Button button_generate;

        #endregion

        public SafeNodePasswordLayout(SafeNodePassword node)
        {
            #region arguments

            this.node = node;

            #endregion

            #region variables definition

            absoluteLayout_head = new AbsoluteLayout();
            button_name = new Button();
            entry_name = new Entry();

            button_edit = new Button();
            button_editSave = new Button();
            button_delete = new Button();

            absoluteLayout_edit = new AbsoluteLayout();
            absoluteLayout_generate = new AbsoluteLayout();

            entry_password = new Entry();
            button_show = new Button();
            button_copy = new Button();

            entry_numSymbols = new Entry();
            button_generate = new Button();

            #endregion

            #region static initializations

            this.TranslationX = 35;

            absoluteLayout_edit.TranslationX = 20;
            absoluteLayout_generate.TranslationX = 20;

            UpdateButtonName();
            button_name.TextColor = ColorProvider.Red;
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
            button_editSave.Clicked += Button_editSave_Clicked; ;
            button_editSave.IsVisible = false;

            entry_name.Text = node.name;
            entry_name.IsVisible = false;

            entry_password.Placeholder = "no password";
            entry_password.Text = node.encryptedPassword;
            entry_password.IsPassword = true;
            entry_password.WidthRequest = 200;

            button_show.Text = "show";
            button_show.TextColor = ColorProvider.White;
            button_show.BorderColor = ColorProvider.White;
            button_show.TranslationX = 210;
            button_show.Clicked += Button_show_Clicked;

            button_copy.Text = "copy";
            button_copy.TextColor = ColorProvider.White;
            button_copy.BorderColor = ColorProvider.White;
            button_copy.TranslationX = 270;
            button_copy.Clicked += Button_copy_Clicked;

            button_delete.Text = "delete";
            button_delete.TextColor = ColorProvider.White;
            button_delete.BorderColor = ColorProvider.White;
            button_delete.TranslationX = 200;
            button_delete.Clicked += Button_delete_Clicked;

            entry_numSymbols.Placeholder = "number symbols";
            entry_numSymbols.WidthRequest = 100;
            entry_numSymbols.Keyboard = Keyboard.Numeric;

            button_generate.Text = "generate";
            button_generate.TextColor = ColorProvider.White;
            button_generate.BorderColor = ColorProvider.White;
            button_generate.Clicked += Button_generate_Clicked;
            button_generate.TranslationX = 110;

            #endregion

            #region define structure

            absoluteLayout_head.Children.Add(button_name);
            absoluteLayout_head.Children.Add(button_edit);
            absoluteLayout_head.Children.Add(button_editSave);
            absoluteLayout_head.Children.Add(button_delete);

            absoluteLayout_head.Children.Add(entry_name);

            absoluteLayout_edit.Children.Add(entry_password);
            absoluteLayout_edit.Children.Add(button_show);
            absoluteLayout_edit.Children.Add(button_copy);

            absoluteLayout_generate.Children.Add(entry_numSymbols);
            absoluteLayout_generate.Children.Add(button_generate);


            Children.Add(absoluteLayout_head);
            Children.Add(absoluteLayout_edit);
            Children.Add(absoluteLayout_generate);

            Hide();

            #endregion
        }

        private void UpdateButtonName()
        {
            button_name.Text = node.name;
        }

        public bool editMode = false;
        private void Button_edit_Clicked(object sender, EventArgs e)
        {
            ChangeEditMode();
        }

        private void Button_editSave_Clicked(object sender, EventArgs e)
        {
            node.name = entry_name.Text;

            UpdateButtonName();
            
            ChangeEditMode();
        }

        public void ChangeEditMode()
        {
            editMode = !editMode;

            button_edit.IsVisible = !button_edit.IsVisible;
            button_editSave.IsVisible = !button_editSave.IsVisible;

            button_name.IsVisible = !button_name.IsVisible;
            entry_name.IsVisible = !entry_name.IsVisible;
        }


        private void Button_delete_Clicked(object sender, EventArgs e)
        {
            // @todo: check before delete it

            // @todo: delete it

            // @todo: rewrite tree in cloud
        }

        private void Button_show_Clicked(object sender, EventArgs e)
        {
            entry_password.IsPassword = !entry_password.IsPassword;
            if (entry_password.IsPassword)
            {
                entry_password.Text = node.encryptedPassword;
                button_show.Text = "show";
            }
            else
            {
                // @todo: get decrypted password
                entry_password.Text = node.encryptedPassword;
                button_show.Text = "hide";
            }
        }

        private void Button_copy_Clicked(object sender, EventArgs e)
        {
            // @todo: decrypt password

            ClipboardService.CopyToClipboard(node.encryptedPassword);
        }

        private void Button_generate_Clicked(object sender, EventArgs e)
        {
            string newPassword = KeyGenerator.GenerateReadableKey(int.Parse(entry_numSymbols.Text));

            node.SetPassword("mpw", newPassword);
            entry_password.Text = node.encryptedPassword;
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
            
            button_edit.IsVisible = false;
            button_editSave.IsVisible = false;
            button_delete.IsVisible = false;
        }

        private void Show()
        {
            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].IsVisible = true;
            }

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
            button_delete.IsVisible = true;
        }
    }
}
