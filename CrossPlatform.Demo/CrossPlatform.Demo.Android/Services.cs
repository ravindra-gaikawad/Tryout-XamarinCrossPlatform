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

[assembly: Xamarin.Forms.Dependency(typeof(CrossPlatform.Demo.Droid.Services))]
namespace CrossPlatform.Demo.Droid
{    
    public class Services : IServices
    {
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