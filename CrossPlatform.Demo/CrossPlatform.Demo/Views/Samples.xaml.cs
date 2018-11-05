using CrossPlatform.Demo.ContentViews;
using CrossPlatform.Demo.Services;
using Plugin.LocalNotifications;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatform.Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Samples : ContentPage
    {
        private string currentListItem = null;
        public Samples()
        {
            InitializeComponent();
        }

        private async void BtnLocation_Clicked(object sender, EventArgs e)
        {
            await ShowLocation();
        }

        private void BtnToast_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IServices>().Toast("Toast Message");
        }

        private async System.Threading.Tasks.Task ShowLocation()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);
                Location location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    await DisplayAlert("Detected Location", $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}", "OK");
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                else
                {
                    DependencyService.Get<IServices>().Toast("Null location");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Error", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Error", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void BtnSms_Clicked(object sender, EventArgs e)
        {
            popupSendSms.IsVisible = true;
        }

        private void BtnSend_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Message.Text))
            {
                DependencyService.Get<IServices>().Toast("Write message");
            }

            if (string.IsNullOrEmpty(this.Receipient.Text))
            {
                DependencyService.Get<IServices>().Toast("Add receipient");
            }

            popupSendSms.IsVisible = false;

            bool success = DependencyService.Get<IServices>().SendSms(this.Message.Text, this.Receipient.Text);

            if (success)
            {
                DependencyService.Get<IServices>().Toast("Message sent");
            }
            else
            {
                DependencyService.Get<IServices>().Toast("Message not sent");
            }
        }

        private void BtnNotify_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("title", "body");
        }
    }
}