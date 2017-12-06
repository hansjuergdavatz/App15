using App15.Views;
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
      //var playPage = new Time() { Title = "TIMEDOC Zeiten", Icon = null };
      //var workPage = new NavigationPage(new Work() { Title = "Leistungserfassung", Icon = null });
      //var settingsPage = new AboutPage() { Title = "About", Icon = null };
      //var aboutPage = new Login() { Title = "Login", Icon = null };

      var playPage = new Time() { Title = "", Icon = null };
      var workPage = new NavigationPage(new Work() { Title = "Leistungserfassung", Icon = null });
      var settingsPage = new AboutPage() { Title = "", Icon = null };
      var aboutPage = new Login() { Title = "", Icon = null };

      switch (Device.RuntimePlatform)
      {
        case Device.iOS:
          playPage.Icon = "ic_access_time.png";
          workPage.Icon = "ic_list.png";
          settingsPage.Icon = "ic_error_outline.png";
          aboutPage.Icon = "ic_accessibility.png";
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
    }

    public void SwitchTab(int index)
    {
      CurrentPage = this.Children[index];
    }
  }
}


