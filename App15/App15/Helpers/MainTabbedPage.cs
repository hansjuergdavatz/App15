﻿using App15.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App15.Helpers
{
    public class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            //var playPage = new Time() { Title = "TIMEDOC Zeiten", Icon = "tab_feed.png" };
            //var settingsPage = new AboutPage() { Title = "About", Icon = "tab_about.png" };
            //var aboutPage = new Login() { Title = "Login", Icon = "p_generic.png" };

            var playPage = new Time() { Title = "TIMEDOC Zeiten", Icon = null };
            var workPage = new NavigationPage(new Work() { Title = "Work", Icon = null });
            var settingsPage = new AboutPage() { Title = "About", Icon = null };
            var aboutPage = new Login() { Title = "Login", Icon = null };

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    playPage.Icon = "tab_feed.png";
                    settingsPage.Icon = "tab_about.png";
                    aboutPage.Icon = "p_generic.png";
                    break;
                case Device.Android:
                    playPage.Title = string.Empty;
                    playPage.Icon = "ic_access_time_black_24dp.png";
                    workPage.Title = string.Empty;
                    workPage.Icon = "ic_list_black_24dp.png";
                    settingsPage.Title = string.Empty;
                    settingsPage.Icon = "ic_error_outline_black_24dp.png";
                    aboutPage.Title = string.Empty;
                    aboutPage.Icon = "ic_accessibility_black_24dp.png";
                    break;
                default:
                    break;
            }

            Children.Add(playPage);
            Children.Add(workPage);
            Children.Add(settingsPage);
            Children.Add(aboutPage);

            // Do not embedded your ContentPages within a "single" level NavigationPage.

            //      Children.Add(new NavigationPage(new Time())
            //      {
            //        Title = "TIMEDOC Zeiten",
            //        Icon = Device.OnPlatform("tab_feed.png", null, null)
            //      }
            //);
            //      Children.Add(new NavigationPage(new AboutPage())
            //      {
            //        Title = "About",
            //        Icon = Device.OnPlatform("tab_about.png", null, null)
            //      }
            //);
            //      Children.Add(new NavigationPage(new Login())
            //      {
            //        Title = "Account",
            //        Icon = Device.OnPlatform("tab_about.png", null, null)
            //      }
            //      );

        }

        public void SwitchTab(int index)
        {
            //  ((TabbedPage)Current.MainPage).CurrentPage = ((TabbedPage)Current.MainPage).Children[2];
            CurrentPage = this.Children[index];
        }
    }
}

