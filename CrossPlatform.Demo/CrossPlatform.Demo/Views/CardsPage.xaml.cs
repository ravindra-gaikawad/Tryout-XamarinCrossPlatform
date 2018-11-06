using CrossPlatform.Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatform.Demo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardsPage : ContentPage
    {
        CardsViewModel viewModel;

        public CardsPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new CardsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}