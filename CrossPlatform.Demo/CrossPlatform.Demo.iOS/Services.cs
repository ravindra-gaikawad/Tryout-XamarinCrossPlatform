using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossPlatform.Demo.Services;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CrossPlatform.Demo.iOS.Services))]
namespace CrossPlatform.Demo.iOS
{
    public class Services : IServices
    {
        const double LONG_DELAY = 3.5;


        NSTimer alertDelay;
        UIAlertController alert;


        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }

        void IServices.Toast(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        bool IServices.SendSms(string message, string receipient)
        {
            throw new NotImplementedException();
        }
    }
}