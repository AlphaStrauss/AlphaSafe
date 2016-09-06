using AlphaSafe.Core.Cryptography;
using AlphaSafe.Core.DataStructure;
using AlphaSafe.UI.BasisFunctions;
using AlphaSafe.UI.DataStructure;
using AlphaSafe.UI.Desktop.Elements;
using AlphaStrauss.AlphaCloudApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.View
{
    public class SafePage : ContentPage
    {
        #region variables declaration

        public Profile profile;
        private string password;
        private SafeTree tree;

        public ScrollView scrollView_page;
        public StackLayout stackLayout_page;

        public int headHeight;
        public int fontSize;
        public Label label_headline;

        public Button button_logout;

        public AlphaCloudApi cloudapi;
        
        #endregion

        public SafePage(Profile profile, string password)
        {
            #region variables definition

            this.profile = profile;
            this.password = password;

            scrollView_page = new ScrollView();
            stackLayout_page = new StackLayout();

            label_headline = new Label();

            button_logout = new Button();

            cloudapi = new AlphaCloudApi(profile.cloud);

            #endregion

            #region static initializations
            
            NavigationPage.SetHasNavigationBar(this, false);

            stackLayout_page.BackgroundColor = ColorProvider.LightBlue;

            headHeight = 100;
            fontSize = 30;

            label_headline.Text = profile.profileName;
            label_headline.FontSize = fontSize;
            label_headline.FontAttributes = FontAttributes.Bold;
            label_headline.HorizontalOptions = LayoutOptions.Center;
            label_headline.TextColor = ColorProvider.White;
            label_headline.TranslationY = (headHeight - fontSize) / 2;

            button_logout.Text = "logout";
            button_logout.Clicked += Button_logout_Clicked; ;
            button_logout.TextColor = ColorProvider.White;
            button_logout.BorderColor = ColorProvider.White;
            button_logout.BorderWidth = 2;

            #endregion

            #region create test data

            tree = new SafeTree();
            tree.root.AddCategoryNode("example category");

            SafeTreeLayout treelayout = new SafeTreeLayout(tree);

            #endregion

            #region define structure

            Content = scrollView_page;
            scrollView_page.Content = stackLayout_page;

            stackLayout_page.Children.Add(label_headline);
            stackLayout_page.Children[0].HeightRequest = headHeight;

            stackLayout_page.Children.Add(treelayout);

            stackLayout_page.Children.Add(button_logout);

            #endregion
        }

        private bool appeared = false;
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            #region get cloud connection

            if (!appeared)
            {
                Debug.WriteLine("SafePage > OnAppearing > Push BrowserPage");
                Navigation.PushAsync(new BrowserPage(cloudapi));
                appeared = true;
            }
            else
            {
                try
                {
                    string profile = await LoadProfileFromCloud();
                    string safe = await LoadSafeFromCloud();

                    if (profile == "")
                    {
                        Debug.WriteLine("SafePage > LoadProfile: no profile");
                    }

                    if (safe == "")
                    {
                        Debug.WriteLine("SafePage > LoadSafe: no safe");
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine("SafePage > OnAppearing > try load profile and safe > Exception:\n"+e.Message);

                    if(e.Message.Contains("empty"))
                    {
                        Debug.WriteLine("SafePage > OnAppearing > try create profile and safe");

                        try
                        {
                            string jsonProfile = JsonConvert.SerializeObject(profile);
                            string jsonSafe = JsonConvert.SerializeObject(tree.root);

                            await cloudapi.Upload("/profiles", PrepareName(profile.profileName) + ".profile", Aes256.EncryptByteArray(ASCIIEncoding.ASCII.GetBytes(jsonProfile), ASCIIEncoding.ASCII.GetBytes(password)));
                            await cloudapi.Upload("/safes", PrepareName(profile.profileName) + ".alphasafe", Aes256.EncryptByteArray(ASCIIEncoding.ASCII.GetBytes(jsonSafe), ASCIIEncoding.ASCII.GetBytes(password)));
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine("SafePage > OnAppearing > try create profile and safe > Exception:\n" + e.Message);
                        }
                    }
                }
            }

            #endregion
        }

        private async Task<string> LoadProfileFromCloud()
        {
            return await cloudapi.Download("/profiles", PrepareName(profile.profileName)+".profile");
        }

        private async Task<string> LoadSafeFromCloud()
        {
            return await cloudapi.Download("/safes", PrepareName(profile.profileName) + ".alphasafe");
        }

        private string nameChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
        private string PrepareName(string name)
        {
            string result = "";

            foreach(char c in name)
                if (nameChars.Contains(c+""))
                    result += c;

            return result;
        }

        private void Button_logout_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
