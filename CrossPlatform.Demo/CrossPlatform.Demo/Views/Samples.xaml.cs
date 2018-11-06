using CrossPlatform.Demo.Services;
using Plugin.Geofencing;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
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
            ((Button)sender).IsEnabled = false;
            await ShowLocation();
            ((Button)sender).IsEnabled = true;
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
            ((Button)sender).IsEnabled = false;

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

            ((Button)sender).IsEnabled = true;
        }

        private void BtnNotify_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            CrossLocalNotifications.Current.Show("title", "body");
            ((Button)sender).IsEnabled = true;
        }

        private void BtnGeofence_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            DependencyService.Get<IServices>().EnableGeofencing();
            DependencyService.Get<IServices>().Toast("Geofencing enabled...");
            ((Button)sender).IsEnabled = true;
        }
    }
}