using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace App15.ViewModels
{
  public class AboutViewModel : BaseViewModel
  {
    public AboutViewModel()
    {
      Title = "About";

      OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://www.timedoc.ch")));
    }

    /// <summary>
    /// Command to open browser to xamarin.com
    /// </summary>
    public ICommand OpenWebCommand { get; }
  }
}
