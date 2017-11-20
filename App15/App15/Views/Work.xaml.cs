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
        Coworker _user = null;
        DateTime _dateSelected = DateTime.MinValue;
        OrderAchievement _actOrderAchievement = null;
        List<OrderAchievement> list = null;

        public Work()
        {
            InitializeComponent();
            SetUIHandlers();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            _user = await App.Database.GetCoworker();
            if (_user != null)
            {
                DayDate.Date = DateTime.Now;
                _dateSelected = DayDate.Date;
                await LoadList(true);
            }

            //if (_user == null)
            //{
            //    var tabbedPage = this.Parent as MainTabbedPage;
            //    tabbedPage.SwitchTab(3);
            //}
            //else
            //{
            //    OADayDate.Date = DateTime.Now;
            //    _dateSelected = OADayDate.Date;
            //    await LoadList(false);
            //}
        }

        private async void SetUIHandlers()
        {
            DayDate.Date = DateTime.Now;
            _dateSelected = DayDate.Date;

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
                    SetDisplayText();
                    OrderAchievementListView.ItemsSource = list;
                }
            }
            catch (Exception)
            {
                OrderAchievementListView.ItemsSource = null;
            }
        }

        private void SetDisplayText()
        {
            foreach (OrderAchievement item in list)
            {
                string menge = " Menge: " + item.Amount.ToString("0.00") + item.Unit;
                item.TxtLarge = item.OrderNrDesc;
                item.TxtSmall = item.AchieName;
                item.TxtSmall2 = item.AmountInfo + menge;

                if (item.Status == 100)
                    item.Image = "ic_update_black_24dp.png";
                else
                    item.Image = "ic_check_circle_black_24dp.png";
            }
        }

        private async void btnStart_Clicked(object sender, EventArgs e)
        {
            // Basic-http
            App.restManager = new RestManager(new Web.RestService());

            try
            {
                list = await App.restManager.GetNewOrderAchievementAsync(string.Empty,string.Empty,true,true);
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

                //var modalPage = new WorkDetail(_actOrderAchievement);
                //modalPage.Disappearing += (sender2, e2) =>
                //{
                //    OrderAchievementListView.SelectedItem = null;
                //    Refresh();
                //};
                //await Navigation.PushModalAsync(modalPage);

                await Navigation.PushAsync(new WorkDetail(_actOrderAchievement));

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