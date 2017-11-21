﻿using App15.Data;
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
    public partial class WorkDetail : ContentPage
    {
        OrderAchievement _actOrderAchievement = null;

        public WorkDetail(OrderAchievement oa)
        {
            InitializeComponent();
            _actOrderAchievement = oa;
            SetData();
        }
        private void SetData()
        {
            DateTime d = DateTime.Now;

            btnOrder.Text = _actOrderAchievement.OrderNrDesc;
            btnAchievement.Text = _actOrderAchievement.AchieName;

            DateAchie.Date = _actOrderAchievement.DateTimeAchie.Date;
            TimeAchie.Time = new TimeSpan(_actOrderAchievement.DateTimeAchie.TimeOfDay.Ticks);

            switch (_actOrderAchievement.Status)
            {
                case 100:
                    btnStopp.IsVisible = true;
                    btnStart.IsVisible = false;
                    break;

                case 200:
                case 300:
                    btnStart.IsVisible = true;
                    btnStopp.IsVisible = false;
                    break;

                default:
                    break;
            }

            if (_actOrderAchievement.Unit == "h")
            {
                DateAchie2.IsVisible = true;
                TimeAchie2.IsVisible = true;
                DateAchie2.Date = d.Date;
                TimeAchie2.Time = new TimeSpan(d.TimeOfDay.Ticks);
            }
            else
            {
                DateAchie2.IsVisible = false;
                TimeAchie2.IsVisible = false;
            }

            DateAchie2.Date = d.Date;
            TimeAchie2.Time = new TimeSpan(d.TimeOfDay.Ticks);

            Menge.Text = _actOrderAchievement.Amount.ToString("0.00");
            Unit.Text = _actOrderAchievement.Unit;


            if (_actOrderAchievement.FlagCharge)
                chkCharge.Checked = true;
            else
                chkCharge.Checked = false;

        }


        private async void btnOrder_Clicked(object sender, EventArgs e)
        {
            try
            {
                var modalPage = new OrderSearch();
                modalPage.Disappearing += (sender2, e2) =>
                {
                    if (modalPage._actOrder != null)
                    {
                        _actOrderAchievement.IdOrder = modalPage._actOrder.Id;
                        _actOrderAchievement.OrderNrDesc = modalPage._actOrder.OrderNumber + " - " + modalPage._actOrder.Description;
                        btnOrder.Text = _actOrderAchievement.OrderNrDesc;
                    }

                };
                await Navigation.PushModalAsync(modalPage);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw;
            }
        }
        private async void btnAchievement_Clicked(object sender, EventArgs e)
        {
            try
            {
                var modalPage = new AchievementSearch();
                modalPage._actOrderId = _actOrderAchievement.IdOrder;
                modalPage.Disappearing += (sender2, e2) =>
                {
                    if (modalPage._actAchievement != null)
                    {
                        _actOrderAchievement.IdAchievement = modalPage._actAchievement.Id;
                        _actOrderAchievement.AchieNumber = modalPage._actAchievement.AchieNumber;
                        _actOrderAchievement.AchieName = modalPage._actAchievement.AchieName;
                        _actOrderAchievement.Unit = modalPage._actAchievement.Unit;
                        //btnAchievement.Text = _actOrderAchievement.AchieName;
                        SetData();
                    }
                };
                await Navigation.PushModalAsync(modalPage);

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw;
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            _actOrderAchievement.DateTimeAchie = new DateTime(DateAchie.Date.Year, DateAchie.Date.Month, DateAchie.Date.Day, TimeAchie.Time.Hours, TimeAchie.Time.Minutes, 0);
            _actOrderAchievement.Amount = Convert.ToDecimal(Menge.Text);

            if (chkCharge.Checked)
                _actOrderAchievement.FlagCharge = true;
            else
                _actOrderAchievement.FlagCharge = false;

            if (Info.Text.Length > 0)
                _actOrderAchievement.Remark = Info.Text;

            _actOrderAchievement.IsNew = false;

            // Basic-http
            App.restManager = new RestManager(new Web.RestService());
            await App.restManager.UpdateOrderAchievement(_actOrderAchievement);

            DependencyService.Get<IMessage>().ShortAlert("Daten gespeichert.");

            await Navigation.PopAsync();

        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            App.restManager = new RestManager(new Web.RestService());
            await App.restManager.DeleteOrderAchievement(_actOrderAchievement);

            DependencyService.Get<IMessage>().ShortAlert("Daten gelöscht.");

            await Navigation.PopAsync();
        }

        private async void btnAbort_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void DateAchie_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void setMenge()
        {
            _actOrderAchievement.DateTimeAchie = new DateTime(DateAchie.Date.Year, DateAchie.Date.Month, DateAchie.Date.Day, TimeAchie.Time.Hours, TimeAchie.Time.Minutes, 0);
            DateTime dateTo = new DateTime(DateAchie2.Date.Year, DateAchie2.Date.Month, DateAchie2.Date.Day, TimeAchie2.Time.Hours, TimeAchie2.Time.Minutes, 0);

            if (_actOrderAchievement.Status == 100)
            {
                dateTo = DateTime.Now;
            }

            if (_actOrderAchievement.Unit == "h")
            {
                TimeSpan timeSpan = dateTo - _actOrderAchievement.DateTimeAchie;
                Double l = timeSpan.TotalHours;
                _actOrderAchievement.Amount = Convert.ToDecimal(l);
                Menge.Text = _actOrderAchievement.Amount.ToString("0.00");
            }

        }

        private void TimeAchie_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                setMenge();
        }

        private void DateAchie2_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void TimeAchie2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                setMenge();
        }

        private async void btnStopp_Clicked(object sender, EventArgs e)
        {
            App.restManager = new RestManager(new Web.RestService());

            try
            {
                var list = await App.restManager.StartStopAsync(_actOrderAchievement.Id.ToString(), false);
                if (list != null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Daten gespeichert.");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }

        private async void btnStart_Clicked(object sender, EventArgs e)
        {
            App.restManager = new RestManager(new Web.RestService());

            try
            {
                var list = await App.restManager.GetNewOrderAchievementAsync(_actOrderAchievement.IdOrder.ToString(), _actOrderAchievement.IdAchievement.ToString(), true, true);
                if (list != null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Daten gespeichert.");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }
    }
}