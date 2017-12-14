using App15.Data;
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
    public OrderAchievement _actOrderAchievement = null;
    int _listIndex = 0;

    public Signature(OrderAchievement oa)
    {
      InitializeComponent();
      _actOrderAchievement = oa;
      PickerList.Items.Add("Heutige Leistungen zu Auftrag.");
      PickerList.Items.Add("Diesen Auftrag.");
      PickerList.SelectedIndex = 0;

      SetData();
    }

    private async void SetData()
    {
      if (_actOrderAchievement.IdSignature != null && _actOrderAchievement.IdSignature != Guid.Empty)
      {
        // Basic-http
        App.restManager = new RestManager(new Web.RestService());
        CSignature sig = await App.restManager.GetSignatureAsync(_actOrderAchievement.IdSignature.ToString());

        if (sig != null)
        {
          ImageSource SigImage = ImageSource.FromStream(() => new MemoryStream(sig.Signature1.ToArray()));
          SignatureView.BackgroundImage = SigImage;
        }
      }
    }


    private async void btnSave_Clicked(object sender, EventArgs e)
    {
      Stream image = await SignatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);

      CSignature unterschrift = new CSignature();
      unterschrift.Name = EntryName.Text;
      unterschrift.Id = Guid.NewGuid();
      unterschrift.Signature1 = ReadFully(image); // serializeData(points);

      bool rc = await App.restManager.SetSignatureAsync(unterschrift);
      if (rc)
      {
        if (_listIndex == 0)
          rc = await App.restManager.SetSignatureAssign(unterschrift.Id.ToString(), _actOrderAchievement.IdOrder.ToString(), DateTime.Now);
        else if (_listIndex == 1)
          rc = await App.restManager.SetSignatureAssign(unterschrift.Id.ToString(), _actOrderAchievement.IdOrder.ToString(), DateTime.MinValue);

        DependencyService.Get<IMessage>().ShortAlert("Unterschrift gespeichert.");
      }
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

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
      //if (_actOrderAchievement.IdSignature != null && _actOrderAchievement.IdSignature != Guid.Empty)
      //{
      //  // Basic-http
      //  App.restManager = new RestManager(new Web.RestService());
      //  bool sig = await App.restManager.DeleteSignatureAssign(_actOrderAchievement.IdSignature.ToString());

      //  if (sig)
      //  {
      //    ImageSource SigImage = ImageSource.FromStream(() => new MemoryStream(sig.Signature1.ToArray()));
      //    SignatureView.BackgroundImage = SigImage;
      //  }
      //}

    }

  }
}