using App15.Data;
using App15.Helpers;
using App15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App15.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Work : ContentPage
  {
    bool _hasCostUnit = false;
    Coworker _user = null;
    DateTime _dateSelected = DateTime.MinValue;
    OrderAchievement _actOrderAchievement = null;
    List<OrderAchievement> list = null;

    public Work()
    {
      InitializeComponent();
      SetUIHandlers();
    }

    private async void ReadSetting()
    {
      // Basic-http
      App.restManager = new RestManager(new Web.RestService());
      Setting setting = await App.restManager.GetSettingAsync("CostUnit");
      if (setting != null)
      {
        if (setting.Value == "1")
        {
          _hasCostUnit = true;  // Kostenträger in Verwendung
        }
      }
    }

    protected async override void OnAppearing()
    {
      base.OnAppearing();

      _user = await App.Database.GetCoworker();

      if (_user == null)
      {
        var tabbedPage = this.Parent;

        var tabbedPage2 = tabbedPage.Parent as MainTabbedPage;
        tabbedPage2.SwitchTab(3);

        //if (_hasCostUnit)
        //  OrderAchievementListView.ItemsSource = null;
        //else
        //  OrderAchievementListViewSmall.ItemsSource = null;


      }

      //_user = await App.Database.GetCoworker();
      if (_user != null)
      {
        if (_dateSelected == DateTime.MinValue)
          _dateSelected = DayDate.Date;
        await LoadList(true);
      }
    }

    private async void SetUIHandlers()
    {
      DayDate.Date = DateTime.Now;
      _dateSelected = DayDate.Date;


      ReadSetting();



      _user = await App.Database.GetCoworker();
      if (_user != null)
        await LoadList(true);
    }

    async Task LoadList(bool detail)
    {
      // Basic-http
      App.restManager = new RestManager(new Web.RestService());

      try
      {
        list = await App.restManager.GetListOrderAchievementAsync(_dateSelected, detail);
        if (list != null)
        {
          if (_hasCostUnit)
          {
            OrderAchievementListView.IsVisible = true;
            OrderAchievementListView.RowHeight = 100;
            OrderAchievementListViewSmall.IsVisible = false;
            OrderAchievementListViewSmall.RowHeight = 0;
          }
          else
          {
            OrderAchievementListViewSmall.IsVisible = true;
            OrderAchievementListViewSmall.RowHeight = 80;
            OrderAchievementListView.IsVisible = false;
            OrderAchievementListView.RowHeight = 0;
          }

          SetDisplayText();
          if (_hasCostUnit)
            OrderAchievementListView.ItemsSource = list;
          else
            OrderAchievementListViewSmall.ItemsSource = list;
        }
      }
      catch (Exception)
      {
        if (_hasCostUnit)
          OrderAchievementListView.ItemsSource = null;
        else
          OrderAchievementListViewSmall.ItemsSource = null;
      }
    }

    private void SetDisplayText()
    {
      foreach (OrderAchievement item in list)
      {
        string menge = " Menge: " + item.Amount.ToString("0.00") + item.Unit;
        item.TxtLarge = item.OrderNrDesc;
        item.TxtSmall = item.AchieName;
        item.TxtSmall3 = item.CostNrDesc;
        item.TxtSmall2 = item.AmountInfo + menge;

        if (item.Status == 100)
        {
          if (Device.RuntimePlatform == Device.iOS)
            item.Image = "ic_update.png";
          else
            item.Image = "ic_update_black_24dp.png";
        }
        else
        {
          if (Device.RuntimePlatform == Device.iOS)
            item.Image = "ic_check_circle.png";
          else
            item.Image = "ic_check_circle_black_24dp.png";
        }
      }


    }

    private async void btnStart_Clicked(object sender, EventArgs e)
    {
      waitCursor.IsVisible = true;
      waitCursor.IsRunning = true;

      // Basic-http
      App.restManager = new RestManager(new Web.RestService());

      try
      {
        list = await App.restManager.GetNewOrderAchievementAsync(string.Empty, string.Empty, true, true);
        if (list != null)
        {
          SetDisplayText();
          OrderAchievementListView.ItemsSource = list;
        }
      }
      catch (Exception)
      {
        OrderAchievementListView.ItemsSource = null;
      }

      waitCursor.IsVisible = false;
      waitCursor.IsRunning = false;

    }

    private async void DayDate_DateSelected(object sender, DateChangedEventArgs e)
    {
      _dateSelected = e.NewDate;
      if (e.NewDate != e.OldDate)
        await LoadList(true);
    }

    private async void OrderAchievement_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      try
      {
        _actOrderAchievement = e.SelectedItem as OrderAchievement;
        if (_actOrderAchievement == null)
          return;
        await Navigation.PushAsync(new WorkDetail(_actOrderAchievement, _hasCostUnit));

      }
      catch (Exception ex)
      {
        string s = ex.Message;
        throw;
      }
    }
    private async void Refresh()
    {
      await LoadList(true);
    }

    private async void btnCreate_Clicked(object sender, EventArgs e)
    {
      waitCursor.IsVisible = true;
      waitCursor.IsRunning = true;

      // Basic-http
      App.restManager = new RestManager(new Web.RestService());

      try
      {
        list = await App.restManager.GetNewOrderAchievementAsync(string.Empty, string.Empty, false, true);
        if (list != null)
        {
          SetDisplayText();
          OrderAchievementListView.ItemsSource = list;
        }
      }
      catch (Exception)
      {
        OrderAchievementListView.ItemsSource = null;
      }

      waitCursor.IsVisible = false;
      waitCursor.IsRunning = false;

    }


    //private async void btnList_Clicked(object sender, EventArgs e)
    //{
    //    // TODO immer Detail-Liste
    //    if (btnList.Text == "Detail")
    //    {
    //        await LoadList(true);
    //        btnList.Text = "Komp.";
    //    }
    //    else
    //    {
    //        await LoadList(false);
    //        btnList.Text = "Detail";
    //    }
    //}
  }
}