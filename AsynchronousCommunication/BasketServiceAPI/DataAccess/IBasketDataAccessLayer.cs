using BasketServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketServiceAPI.DataAccess
{
    public interface IBasketDataAccessLayer
    {
        Task<IEnumerable<Item>> GetBasketItemsAsync(string basketId);
        IEnumerable<Item> GetBasketItems(string basketId);
    }
}
