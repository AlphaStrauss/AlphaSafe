using AlphaSafe.UI.BasisFunctions;
using AlphaSafe.UI.DataStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Xamarin.Forms;

namespace AlphaSafe.UI.Desktop.View
{
    public class ProfileOverviewPage : ContentPage
    {
        #region variables declaration

        public int pageWidth = 0;
        public int pageHeight = 0;

        public StackLayout stackLayout_page;

        public int headHeight;
        public int fontSize;
        public Label label_headline;

        public List<ProfileButton> profileButtonList;

        public Button button_createNewProfile;

        #endregion

        public ProfileOverviewPage()
        {
            #region variables defintion

            stackLayout_page = new StackLayout();
            
            label_headline = new Label();
            profileButtonList = new List<ProfileButton>();
            button_createNewProfile = new Button();

            #endregion

            #region static initializations

            NavigationPage.SetHasNavigationBar(this, false);

            stackLayout_page.BackgroundColor = ColorProvider.LightBlue;
            //stackLayout_page.VerticalOptions = LayoutOptions.Center;
            //stackLayout_page.HorizontalOptions = LayoutOptions.CenterAndExpand;

            headHeight = 100;
            fontSize = 30;

            label_headline.Text = "Your Profiles";
            label_headline.FontSize = fontSize;
            label_headline.FontAttributes = FontAttributes.Bold;
            label_headline.TextColor = ColorProvider.White;
            label_headline.HorizontalOptions = LayoutOptions.Center;
            label_headline.TranslationY = (headHeight - fontSize) / 2;

            button_createNewProfile.Text = "Create new Profile";
            button_createNewProfile.Clicked += Button_createNewProfile_Clicked;
            button_createNewProfile.TextColor = ColorProvider.White;
            button_createNewProfile.BorderColor = ColorProvider.White;
            button_createNewProfile.BorderWidth = 2;

            #endregion

            AddElementsToPage();

            SizeChanged += ProfileOverviewPage_SizeChanged;
        }

        private async void AddElementsToPage()
        {
            Debug.WriteLine("ProfileOverviewPage > AddElementsToPage");

            await AlphaSafeData.ReadProfileList();

            stackLayout_page.Children.Clear();

            #region define structure

            Content = stackLayout_page;

            stackLayout_page.Children.Add(label_headline);
            stackLayout_page.Children[0].HeightRequest = headHeight;

            profileButtonList = new List<ProfileButton>();
            foreach (Profile profile in AlphaSafeData.profileList)
            {
                ProfileButton profileButton = new ProfileButton(profile);
                profileButtonList.Add(profileButton);
            }

            foreach (ProfileButton button in profileButtonList)
                stackLayout_page.Children.Add(button);

            stackLayout_page.Children.Add(button_createNewProfile);

            #endregion

        }

        private void ProfileOverviewPage_SizeChanged(object sender, EventArgs e)
        {
            pageWidth = (int)Width;
            pageHeight = (int)Height;
        }

        private void Button_createNewProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateProfilePage());
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("ProfileOverviewPage > OnAppearing");

            base.OnAppearing();

            AddElementsToPage();
        }

        protected override void InvalidateMeasure()
        {
            Debug.WriteLine("ProfileOverviewPage > InvalidateMeasure");

            base.InvalidateMeasure();

            AddElementsToPage();
        }
    }
}
