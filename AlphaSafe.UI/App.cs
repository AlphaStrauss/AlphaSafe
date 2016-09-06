using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using AlphaSafe.UI.Desktop.View;
using AlphaSafe.Core.Cryptography;
using Org.BouncyCastle.Security;

namespace AlphaSafe.UI
{
	public class App : Application
	{
		public App ()
		{
            KeyGenerator.random = new SecureRandom();

            // The root page of your application
            MainPage = new NavigationPage(new ProfileOverviewPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
