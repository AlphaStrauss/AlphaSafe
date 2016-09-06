using AlphaSafe.Core.Cryptography;
using AlphaSafe.UI.BasisFunctions;
using AlphaSafe.UI.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.View
{
    public class PasswordPage : ContentPage
    {
        #region variables declaration

        public StackLayout stackLayout_page;

        public int headHeight;
        public int fontSize;
        public Label label_headline;

        public Entry entry_password;
        public Label label_error;
        public Button button_confirm;
        public Button button_back;

        public Profile profile;

        #endregion

        public PasswordPage(Profile profile) : base()
        {
            #region save argument

            this.profile = profile;

            #endregion

            #region variables definition

            stackLayout_page = new StackLayout();

            label_headline = new Label();
            
            entry_password = new Entry();

            label_error = new Label();

            button_confirm = new Button();
            button_back = new Button();

            #endregion

            #region static initializations

            NavigationPage.SetHasNavigationBar(this, false);

            stackLayout_page.BackgroundColor = ColorProvider.LightBlue;

            headHeight = 100;
            fontSize = 30;

            label_headline.Text = "Confirm your password";
            label_headline.FontSize = fontSize;
            label_headline.FontAttributes = FontAttributes.Bold;
            label_headline.HorizontalOptions = LayoutOptions.Center;
            label_headline.TextColor = ColorProvider.White;
            label_headline.TranslationY = (headHeight - fontSize) / 2;

            entry_password.IsPassword = true;
            entry_password.Placeholder = "password";
            entry_password.Focus();

            label_error.Text = "";
            label_error.TextColor = ColorProvider.Red;
            label_error.HorizontalOptions = LayoutOptions.Center;

            button_confirm.Text = "confirm password";
            button_confirm.Clicked += Button_confirm_Clicked;
            button_confirm.TextColor = ColorProvider.White;
            button_confirm.BorderColor = ColorProvider.White;
            button_confirm.BorderWidth = 2;

            button_back.Text = "back";
            button_back.Clicked += Button_back_Clicked;
            button_back.TextColor = ColorProvider.White;
            button_back.BorderColor = ColorProvider.White;
            button_back.BorderWidth = 2;

            #endregion

            #region define structure

            Content = stackLayout_page;

            stackLayout_page.Children.Add(label_headline);
            stackLayout_page.Children[0].HeightRequest = headHeight;

            stackLayout_page.Children.Add(entry_password);

            stackLayout_page.Children.Add(label_error);

            stackLayout_page.Children.Add(button_confirm);
            stackLayout_page.Children.Add(button_back);

            #endregion
        }

        private void Button_back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Button_confirm_Clicked(object sender, EventArgs e)
        {
            string tmpPassword = entry_password.Text;

            if(tmpPassword == null || tmpPassword == "")
            {
                label_error.Text = "enter the password!";

                return;
            }

            string password = Convert.ToBase64String(SHA3.GetSHA3Hash(tmpPassword));

            if (password == profile.password)
            {
                // Password is correct
                
                entry_password.Text = "";
                Navigation.PushAsync(new SafePage(profile, tmpPassword));
            }
            else
            {
                label_error.Text = "password is not correct!";
            }
        }
    }
}
