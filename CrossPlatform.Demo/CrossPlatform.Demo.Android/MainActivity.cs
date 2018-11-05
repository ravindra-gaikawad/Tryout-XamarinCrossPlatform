using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.LocalNotifications;
using Android.Telephony;
using Plugin.Geofencing;
using System.Collections.Generic;

namespace CrossPlatform.Demo.Droid
{
    [Activity(Label = "CrossPlatform.Demo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            SetAlarmForBackgroundServices(this);

            CrossGeofences.Current.RegionStatusChanged += OnRegionStatusChanged;
        }

        private void OnRegionStatusChanged(object sender, GeofenceStatusChangedEventArgs e)
        {
            try
            {
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

        public static void SetAlarmForBackgroundServices(Context context)
        {
            var alarmIntent = new Intent(context.ApplicationContext, typeof(AlarmReceiver));
            var broadcast = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, PendingIntentFlags.NoCreate);
            if (broadcast == null)
            {
                var pendingIntent = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, 0);
                var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
                alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime(), 1200000, pendingIntent);
            }
        }
    }
}