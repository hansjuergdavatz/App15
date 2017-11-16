using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AsNum.XFControls.Droid;

namespace App15.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MyTheme);

            base.OnCreate(bundle);

            // für XFControls; iOS Project Please insert the following code before global::Xamarin.Forms.Forms.Init(); at file AppDelegate.cs
            AsNumAssemblyHelper.HoldAssembly();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}