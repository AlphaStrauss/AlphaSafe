using AlphaSafe.UI.DataStructure;
using AlphaSafe.UI.Desktop.View;
using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace AlphaSafe.UI.BasisFunctions
{
    public class ProfileButton : Button
    {
        private Profile profile;

        public ProfileButton(Profile profile) : base()
        {
            this.profile = profile;

            this.BackgroundColor = ColorProvider.Transparent;
            this.BorderColor = ColorProvider.Transparent;
            this.Text = profile.profileName;
            this.TextColor = ColorProvider.White;
            this.FontAttributes = FontAttributes.Bold;

            this.Clicked += Button_Clicked;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PasswordPage(profile));
        }
    }
}
