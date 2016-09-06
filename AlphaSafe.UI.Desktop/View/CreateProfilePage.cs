using AlphaSafe.Core.Cryptography;
using AlphaSafe.UI.BasisFunctions;
using AlphaSafe.UI.DataStructure;
using AlphaStrauss.AlphaCloudApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.View
{
    public class CreateProfilePage : ContentPage
    {
        #region variables declaration

        public StackLayout stackLayout_page;

        public int headHeight;
        public int fontSize;
        public Label label_headline;

        public Entry entry_profileName;
        public Entry entry_password;
        public Picker picker_cloud;

        public Label label_error;
        public Button button_create;
        public Button button_back;

        #endregion

        public CreateProfilePage()
        {
            #region variables definition

            stackLayout_page = new StackLayout();
            
            label_headline = new Label();

            entry_profileName = new Entry();
            entry_password = new Entry();
            picker_cloud = new Picker();

            label_error = new Label();

            button_back = new Button();
            button_create = new Button();

            #endregion

            #region static initializations

            NavigationPage.SetHasNavigationBar(this, false);

            stackLayout_page.BackgroundColor = ColorProvider.LightBlue;

            headHeight = 100;
            fontSize = 30;
            
            label_headline.Text = "Create new profile";
            label_headline.FontSize = fontSize;
            label_headline.FontAttributes = FontAttributes.Bold;
            label_headline.HorizontalOptions = LayoutOptions.Center;
            label_headline.TextColor = ColorProvider.White;
            label_headline.TranslationY = (headHeight - fontSize) / 2;

            entry_profileName.Placeholder = "profile name";

            entry_password.Placeholder = "password";
            entry_password.IsPassword = true;

            picker_cloud.Items.Add("Dropbox");
            picker_cloud.SelectedIndex = 0;

            label_error.Text = "";
            label_error.TextColor = ColorProvider.Red;
            label_error.HorizontalOptions = LayoutOptions.Center;

            button_back.Text = "back to profile overview";
            button_back.Clicked += Button_back_Clicked;
            button_back.TextColor = ColorProvider.White;
            button_back.BorderColor = ColorProvider.White;
            button_back.BorderWidth = 2;

            button_create.Text = "create new profile";
            button_create.Clicked += Button_create_Clicked;
            button_create.TextColor = ColorProvider.White;
            button_create.BorderColor = ColorProvider.White;
            button_create.BorderWidth = 2;

            #endregion

            #region define structure

            Content = stackLayout_page;

            stackLayout_page.Children.Add(label_headline);
            stackLayout_page.Children[0].HeightRequest = headHeight;

            stackLayout_page.Children.Add(entry_profileName);
            stackLayout_page.Children.Add(entry_password);

            stackLayout_page.Children.Add(picker_cloud);

            stackLayout_page.Children.Add(label_error);

            stackLayout_page.Children.Add(button_create);
            stackLayout_page.Children.Add(button_back);

            #endregion
        }

        private async void Button_create_Clicked(object sender, EventArgs e)
        {
            // @todo: check and create profile
            string profileName = entry_profileName.Text;
            string tmpPassword = entry_password.Text;

            CloudType cloud = CloudType.Dropbox;
            switch (picker_cloud.SelectedIndex)
            {
                case 0: // Dropbox
                    cloud = CloudType.Dropbox;
                    break;
            }

            // @todo: define number of clouds
            int numClouds = 1;

            if(profileName == null || profileName == "")
            {
                label_error.Text = "Please choose a profile name!";
                return;
            }

            if (tmpPassword == null || tmpPassword == "")
            {
                label_error.Text = "Please choose a password!";
                return;
            }

            if(picker_cloud.SelectedIndex < 0 || picker_cloud.SelectedIndex >= picker_cloud.Items.Count)
            {
                label_error.Text = "Fatal error! Please create an issue under github.com/AlphaStrauss/AlphaSafe and report this bug!";
                return;
            }

            string password = Convert.ToBase64String(SHA3.GetSHA3Hash(tmpPassword));

            Profile profile = new Profile(profileName, password, cloud);
            AlphaSafeData.profileList.Add(profile);

            Debug.WriteLine("CreateProfilePage > Button_create_Clicked > profile added ('"+profileName+"', '"+password+"', '"+cloud+"')");

            await AlphaSafeData.WriteProfileList();
            
            Navigation.PopAsync();
        }

        private void Button_back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
