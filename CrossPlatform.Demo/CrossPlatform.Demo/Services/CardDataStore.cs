using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossPlatform.Demo.Models;

namespace CrossPlatform.Demo.Services
{
    public class CardDataStore : IDataStore<Card>
    {
        List<Card> Cards;

        public CardDataStore()
        {
            Cards = new List<Card>();
            var mockCards = new List<Card>
            {
                new Card { Id = Guid.NewGuid().ToString(), Text = "First Card", Description="This is an Card description." },
                new Card { Id = Guid.NewGuid().ToString(), Text = "Second Card", Description="This is an Card description." },
                new Card { Id = Guid.NewGuid().ToString(), Text = "Third Card", Description="This is an Card description." },
                new Card { Id = Guid.NewGuid().ToString(), Text = "Fourth Card", Description="This is an Card description." },
                new Card { Id = Guid.NewGuid().ToString(), Text = "Fifth Card", Description="This is an Card description." },
                new Card { Id = Guid.NewGuid().ToString(), Text = "Sixth Card", Description="This is an Card description." },
            };

            foreach (var Card in mockCards)
            {
                Cards.Add(Card);
            }
        }

        public async Task<bool> AddItemAsync(Card Card)
        {
            Cards.Add(Card);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Card Card)
        {
            var oldCard = Cards.Where((Card arg) => arg.Id == Card.Id).FirstOrDefault();
            Cards.Remove(oldCard);
            Cards.Add(Card);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldCard = Cards.Where((Card arg) => arg.Id == id).FirstOrDefault();
            Cards.Remove(oldCard);

            return await Task.FromResult(true);
        }

        public async Task<Card> GetItemAsync(string id)
        {
            return await Task.FromResult(Cards.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Card>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Cards);
        }
    }
}