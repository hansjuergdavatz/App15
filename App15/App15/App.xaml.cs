using App15.Data;
using App15.Helpers;
using App15.Models;
using App15.Views;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App15
{
  public partial class App : Application
  {
    static TimedocDatabase database;

    public static RestManager restManager { get; set; }


    public App()
    {
      InitializeComponent();

      //System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
      //var assembly = typeof(App).GetTypeInfo().Assembly;
      //foreach (var res in assembly.GetManifestResourceNames())
      //  System.Diagnostics.Debug.WriteLine("found resource: " + res);
      //System.Diagnostics.Debug.WriteLine("====================================");

      //// This lookup NOT required for Windows platforms - the Culture will be automatically set
      //if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
      //{
      //  // determine the correct, supported .NET culture
      //  var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
      //  Resx.AppResources.Culture = ci; // set the RESX for resource localization
      //  DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
      //}
      SetMainPage();
    }

    public static TimedocDatabase Database
    {
      get
      {
        if (database == null)
        {
          string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");
          database = new TimedocDatabase(s);
        }
        return database;
      }
    }


    public static void SetMainPage()
    {
      Current.MainPage = new MainTabbedPage();

      //Current.MainPage = new TabbedPage
      //{
      //  Children =
      //          {
      //              new NavigationPage(new ItemsPage())
      //              {
      //                  Title = "Browse",
      //      Icon = Device.OnPlatform("tab_feed.png",null,null)
      //              },
      //              new NavigationPage(new AboutPage())
      //              {
      //                  Title = "About",
      //      Icon = Device.OnPlatform("tab_about.png",null,null)
      //              },
      //              new NavigationPage(new Login())
      //              {
      //                  Title = "Account",
      //      Icon = Device.OnPlatform("tab_about.png",null,null)
      //              },
      //          }
      //};

      //if (user == null)
      //  ((TabbedPage)Current.MainPage).CurrentPage = ((TabbedPage)Current.MainPage).Children[2];
      //else
      //  ((TabbedPage)Current.MainPage).CurrentPage = ((TabbedPage)Current.MainPage).Children[1];

    }

    public static void SetMainPage2()
    {

      Current.MainPage = new Login();
    }
  }
}
