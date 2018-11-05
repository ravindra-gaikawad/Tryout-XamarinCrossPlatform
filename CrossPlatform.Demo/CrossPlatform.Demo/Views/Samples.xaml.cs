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

        private void BtnGeofence_Clicked(object sender, EventArgs e)
        {
            GeofenceRegion geofenceRegion4Mobiliya = new GeofenceRegion("Office", new Position(18.556696, 73.793168), Distance.FromMeters(50));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Mobiliya);

            GeofenceRegion geofenceRegion4University = new GeofenceRegion("University", new Position(18.542156, 73.828755), Distance.FromMeters(100));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4University);

            GeofenceRegion geofenceRegion4Deccan = new GeofenceRegion("Deccan", new Position(18.519159, 73.830352), Distance.FromMeters(100));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Deccan);

            GeofenceRegion geofenceRegion4Swargate = new GeofenceRegion("Swargate", new Position(18.496684, 73.857776), Distance.FromMeters(250));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Swargate);

            GeofenceRegion geofenceRegion4Balajinagar = new GeofenceRegion("Home", new Position(18.465645, 73.858136), Distance.FromMeters(150));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Balajinagar);

            CrossGeofences.Current.RegionStatusChanged += OnRegionStatusChanged;

            DependencyService.Get<IServices>().Toast("Geofencing enabled...");
        }

        private void OnRegionStatusChanged(object sender, GeofenceStatusChangedEventArgs e)
        {
            try
            {       
                var terminals = new List<string> { "Office", "Home" };

                if (terminals.Contains(e.Region.ToString()))
                {
                    CrossLocalNotifications.Current.Show("You are at", $"{e.Region.ToString()}", (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
                }
                else if (terminals.Contains(e.Region.ToString()) == false && e.Status == GeofenceStatus.Entered)
                {
                    CrossLocalNotifications.Current.Show("You are at", $"{e.Region.ToString()}", (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
                }
            }
            catch
            {

            }
        }

    }
}