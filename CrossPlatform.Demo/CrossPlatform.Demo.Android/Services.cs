using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using CrossPlatform.Demo.Services;
using Plugin.Geofencing;
using Plugin.LocalNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(CrossPlatform.Demo.Droid.Services))]
namespace CrossPlatform.Demo.Droid
{
    public class Services : IServices
    {
        bool IServices.EnableGeofencing()
        {
            GeofenceRegion geofenceRegion4Mobiliya = new GeofenceRegion("Office", new Position(18.556696, 73.793168), Distance.FromMeters(500));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Mobiliya);

            GeofenceRegion geofenceRegion4University = new GeofenceRegion("University", new Position(18.542156, 73.828755), Distance.FromKilometers(1));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4University);

            GeofenceRegion geofenceRegion4Deccan = new GeofenceRegion("Deccan", new Position(18.519159, 73.830352), Distance.FromKilometers(1));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Deccan);

            GeofenceRegion geofenceRegion4Swargate = new GeofenceRegion("Swargate", new Position(18.496684, 73.857776), Distance.FromKilometers(1));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Swargate);

            GeofenceRegion geofenceRegion4Balajinagar = new GeofenceRegion("Home", new Position(18.461347, 73.861634), Distance.FromKilometers(1));
            CrossGeofences.Current.StartMonitoring(geofenceRegion4Balajinagar);

            CrossGeofences.Current.RegionStatusChanged += OnRegionStatusChanged;

            return true;
        }

        private void OnRegionStatusChanged(object sender, GeofenceStatusChangedEventArgs e)
        {
            try
            {
                CrossLocalNotifications.Current.Show("You are at", $"{e.Region.ToString()}", (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);

                var terminals = new List<string> { "Office", "Home" };

                if (terminals.Contains(e.Region.ToString()))
                {
                    SmsManager.Default.SendTextMessage("<Mobile No>", null, $"Hi Gochu!!!\nRavindra is at {e.Region}", null, null);
                }
                else if (terminals.Contains(e.Region.ToString()) == false && e.Status == GeofenceStatus.Entered)
                {
                    SmsManager.Default.SendTextMessage("<Mobile No>", null, $"Hi Gochu!!!\nRavindra is at {e.Region}", null, null);
                }
            }
            catch
            {

            }
        }

        bool IServices.SendSms(string message, string receipient)
        {
            try
            {
                SmsManager.Default.SendTextMessage(receipient, null, message, null, null);
                return true;
            }
            catch (Exception ex)
            {
                Android.Widget.Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
                return false;
            }
        }

        void IServices.Toast(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}