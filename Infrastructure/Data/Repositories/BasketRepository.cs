using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basketAsAString = await _database.StringGetAsync(basketId);

            return basketAsAString.IsNullOrEmpty
                ? null
                : JsonConvert.DeserializeObject<CustomerBasket>(basketAsAString);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var createBasket = await _database.StringSetAsync(basket.Id,
                JsonConvert.SerializeObject(basket),
                TimeSpan.FromDays(2));

            if (!createBasket) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
