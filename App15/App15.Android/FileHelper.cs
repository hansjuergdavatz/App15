using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App15.Helpers;
using System.IO;
using Xamarin.Forms;
using App15.Droid;

[assembly: Dependency(typeof(FileHelper))]
namespace App15.Droid
{
  public class FileHelper : IFileHelper
  {
    public string GetLocalFilePath(string filename)
    {
      string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      return Path.Combine(path, filename);
    }
  }
}