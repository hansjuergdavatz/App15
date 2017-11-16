
using Xamarin.Forms;

namespace App15.Views
{
  public partial class AboutPage : ContentPage
  {
    public AboutPage()
    {
      InitializeComponent();
      SetUIHandlers();

    }

    private async void SetUIHandlers()
    {
      var user = await App.Database.GetCoworker();
      if (user == null)
      { 

      }
    }

  }
}
