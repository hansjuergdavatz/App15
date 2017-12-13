using App15.Helpers;
using App15.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App15.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Signature : ContentPage
  {
    public Order _actOrder = null;
    int _listIndex = 0;

    public Signature()
    {
      InitializeComponent();
      PickerList.Items.Add("Heutige Leistungen zu Auftrag.");
      PickerList.Items.Add("Diesen Auftrag.");
      PickerList.SelectedIndex = 0;
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
      Stream image = await SignatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);

      CSignature unterschrift = new CSignature();
      unterschrift.Name = EntryName.Text;
      unterschrift.Id = Guid.NewGuid();
      unterschrift.Signature1 = ReadFully(image); // serializeData(points);

      bool rc = await App.restManager.SetSignatureAsync(unterschrift);
      if (_listIndex == 0)


      DependencyService.Get<IMessage>().ShortAlert("Unterschrift gespeichert.");


      await Navigation.PopModalAsync();

    }

    private async void btnAbort_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync();
    }

    private void PickerList_SelectedIndexChanged(object sender, EventArgs e)
    {
      _listIndex = PickerList.SelectedIndex;
    }
    private static byte[] ReadFully(Stream input)
    {
      using (MemoryStream ms = new MemoryStream())
      {
        input.CopyTo(ms);
        return ms.ToArray();
      }
    }
  }
}