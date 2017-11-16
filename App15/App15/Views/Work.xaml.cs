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
            if (_user == null)
            {
                var tabbedPage = this.Parent as MainTabbedPage;
                tabbedPage.SwitchTab(3);
            }
            else
            {
                OADayDate.Date = DateTime.Now;
                _dateSelected = OADayDate.Date;
                await LoadList(false);
            }
        }

        private async void SetUIHandlers()
        {
            OADayDate.Date = DateTime.Now;
            _dateSelected = OADayDate.Date;

            _user = await App.Database.GetCoworker();

            if (_user != null)
                await LoadList(false);
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
                string menge = item.Amount.ToString("0.00") + item.Unit;
                item.TxtLarge = item.OrderNrDesc;
                item.TxtSmall = item.AchieName + " - Menge: " + menge;

                if (item.Status == 100)
                    item.Image = "ic_update_black_24dp.png";
                else
                    item.Image = "ic_check_circle_black_24dp.png";
            }
        }



        private void btnCreate_Clicked(object sender, EventArgs e)
        {

        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {

        }

        private void OADayDate_DateSelected(object sender, DateChangedEventArgs e)
        {

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
            await LoadList(false);
        }


        private async void btnList_Clicked(object sender, EventArgs e)
        {
            if (btnList.Text == "Detail")
            {
                await LoadList(true);
                btnList.Text = "Komp.";
            }
            else
            {
                await LoadList(false);
                btnList.Text = "Detail";
            }
        }
    }
}