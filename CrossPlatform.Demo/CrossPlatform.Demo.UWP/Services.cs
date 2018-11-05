using CrossPlatform.Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(CrossPlatform.Demo.UWP.Services))]
namespace CrossPlatform.Demo.UWP
{
    class Services : IServices
    {
        bool IServices.SendSms(string message, string receipient)
        {
            throw new NotImplementedException();
        }

        void IServices.Toast(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Message",
                Content = message,
                CloseButtonText = "Ok"
            };

            dialog.ShowAsync();
        }

        void PushNotification(string message)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(message));

            XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/Logo.scale-240.png");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "logo");

            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "short");

            var toastNavigationUriString = "#/MainPage.xaml?param1=12345";
            var toastElement = ((XmlElement)toastXml.SelectSingleNode("/toast"));
            toastElement.SetAttribute("launch", toastNavigationUriString);

            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
