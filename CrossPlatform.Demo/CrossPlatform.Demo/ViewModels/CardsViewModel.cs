using CrossPlatform.Demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossPlatform.Demo.ViewModels
{
    public class CardsViewModel : BaseViewModel
    {
        public ObservableCollection<Card> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CardsViewModel()
        {
            Title = "Cards";
            Items = new ObservableCollection<Card>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewCardPage, Card>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Card;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await CardDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
