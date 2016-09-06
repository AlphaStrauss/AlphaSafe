using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using AlphaStrauss.AlphaCloudApi;
using System.Diagnostics;

namespace AlphaSafe.UI.Desktop.View
{
    public class BrowserPage : ContentPage
    {
        #region variables declaration

        public AbsoluteLayout absoluteLayout_page;
        public WebView webView_cloud;

        public AlphaCloudApi api;

        #endregion

        public BrowserPage(AlphaCloudApi api)
        {
            #region save arguments

            this.api = api;

            #endregion

            #region variables definition

            absoluteLayout_page = new AbsoluteLayout();
            webView_cloud = new WebView();

            #endregion

            #region static initializations

            NavigationPage.SetHasNavigationBar(this, false);

            webView_cloud.Navigating += WebView_cloud_Navigating;
            webView_cloud.Navigated += WebView_cloud_Navigated;
            webView_cloud.WidthRequest = this.WidthRequest;
            webView_cloud.HeightRequest = this.HeightRequest;

            absoluteLayout_page.WidthRequest = this.WidthRequest;
            absoluteLayout_page.HeightRequest = this.HeightRequest;

            this.SizeChanged += BrowserPage_SizeChanged;

            #endregion

            #region define structure

            this.Content = absoluteLayout_page;
            absoluteLayout_page.Children.Add(webView_cloud);

            #endregion

            webView_cloud.Source = api.Start();
        }

        private void BrowserPage_SizeChanged(object sender, EventArgs e)
        {
            webView_cloud.WidthRequest = this.Width;
            webView_cloud.HeightRequest = this.Height;

            absoluteLayout_page.WidthRequest = this.Width;
            absoluteLayout_page.HeightRequest = this.Height;

        }

        private void WebView_cloud_Navigated(object sender, WebNavigatedEventArgs e)
        {
            switch (e.Result)
            {
                case WebNavigationResult.Cancel:
                    Debug.WriteLine("WebView Navigated: Result: Cancel");
                    break;
                case WebNavigationResult.Failure:
                    Debug.WriteLine("WebView Navigated: Result: Failure");
                    break;
                case WebNavigationResult.Success:
                    Debug.WriteLine("WebView Navigated: Result: Success");
                    break;
                case WebNavigationResult.Timeout:
                    Debug.WriteLine("WebView Navigated: Result: Timeout");
                    break;
            }
        }

        private void WebView_cloud_Navigating(object sender, WebNavigatingEventArgs e)
        {
            bool success = api.BrowserNavigating(new Uri(e.Url));

            if (success)
            {
                Debug.WriteLine("BrowserPage: Api erfolgreich geladen!");
                Navigation.PopAsync();
            }
        }
    }
}
