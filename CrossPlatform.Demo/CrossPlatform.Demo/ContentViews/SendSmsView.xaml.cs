using CrossPlatform.Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatform.Demo.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendSmsView : ContentView
    {
        public Entry MobileNo
        {
            get
            {
                return Receipient;
            }
        }

        public Entry TextMessage
        {
            get
            {
                return Message;
            }
        }

        public SendSmsView()
        {
            InitializeComponent();
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

            this.IsVisible = false;

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
    }
}